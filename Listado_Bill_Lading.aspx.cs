using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Packaging;
using Newtonsoft.Json;
using static Entidades;

namespace Operaciones
{
    public partial class Listado_bill_lading : System.Web.UI.Page
    {

        public string TIPO_AGENTE = "";
        public int TIPO_BL = 0;
        string RUTA_WORD = @"C:\Users\Digital\Desktop\Sense Logistict V4.0\Operaciones\Formatos\Bill_Landing.docx";
        string RUTA_DESTINO = @"C:\Users\Digital\Desktop\Sense Logistict V4.0\Operaciones\Archivos\";

        //string RUTA_WORD = @"C:\inetpub\wwwroot\Operaciones\Formatos\Bill_Landing.docx";
        //string RUTA_DESTINO = @"C:\inetpub\wwwroot\Operaciones\Archivos\";


        protected void Page_Load(object sender, EventArgs e){
            if (!this.IsPostBack){
                Establecer_Globales();
            }
        }


        protected void BTN_CERRAR_ServerClick(object sender, EventArgs e){
            Response.Redirect("~/Listado_Bill_Lading.aspx");
        }


        protected void RPT_BL_ItemCommand(object source, RepeaterCommandEventArgs e){

            string num = Convert.ToString(e.CommandArgument);
            switch (e.CommandName){
                case "DECARGAR_BL":

                    LBL_ID_BL.Text = num;

                    string script = @"window.location.href = '#modalHtml';";
                    ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "invocarfuncion", script, true);

                    break;

                case "ELIMINAR":

                    Helpers helpers = new Helpers();

                    bool VAR = helpers.VALIDAR_PERMISO(int.Parse(Request.Cookies["ID_USUARIO"].Value.ToString()), 2);

                    if (VAR)
                    {

                        try{
                            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                                SqlCommand cmd = new SqlCommand("SP_DELETE_BL", conexion);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(new SqlParameter("@ID_BILL_LADING", num));
                                SqlParameter VALOR_RETORNO = new SqlParameter("@RETURN_VALUE", DbType.Int32);
                                VALOR_RETORNO.Direction = ParameterDirection.ReturnValue;
                                cmd.Parameters.Add(VALOR_RETORNO);
                                conexion.Open();
                                cmd.ExecuteNonQuery();
                                int VALOR = Int32.Parse(cmd.Parameters["@RETURN_VALUE"].Value.ToString());
                                conexion.Close();
                                /*-----------------------------------------------------------------*/
                                /*  VALIDA RETURN_VALUE DEVUELTO DESDE LA BASE DE DATOS            */
                                /*-----------------------------------------------------------------*/
                                if (VALOR == 1){
                                    LIST_BL.DataBind();
                                    RPT_BL.DataBind();
                                    CUADRO_CORRECTO.Visible = true;
                                    CUADRO_ERROR.Visible = false;
                                    MENSAJE_CORRECTO.Text = "EL B/L FUE ELIMINADO CORRECTAMENTE";
                                }else{
                                    CUADRO_ERROR.Visible = true;
                                    CUADRO_CORRECTO.Visible = false;
                                    MENSAJE_ERROR.Text = "EL PROCEDIMIENTO EXEC SP_DELETE_BL " + num + " FUE ERRONEO";
                                }
                            }
                        }catch (Exception EX){
                            CUADRO_CORRECTO.Visible = false;
                            CUADRO_ERROR.Visible = true;
                            MENSAJE_ERROR.Text = EX.ToString();
                        }
                    }else{
                        CUADRO_ERROR.Visible = true;
                        CUADRO_CORRECTO.Visible = false;
                        MENSAJE_ERROR.Text = "USUARIO NO POSEE PERMISOS PARA ELIMINAR.";
                    }

                    break;
                case "EDITAR":
                    Response.Redirect("~/Crear_Cliente.aspx?NRO_ID=" + num + "");
                    break;
            }
        }


        public void DESCARGAR_BL(string num,string TIP_DOC)
        {

            string ID_SHIPPER = "", ID_CONSIGNEE = "", ID_NOTIFY = "", ID_DELIVERY = "";
            int CANTIDAD_HOJAS = 0, CANTIDAD_REGISTROS = 0, POSICION_EN_ARREGLO = 1;


            /********************************************************************/
            /**                 CUENTA LA CANTIDA DE HOJAS                     **/
            /********************************************************************/

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                SqlCommand cmd = new SqlCommand("SP_READ_CANTIDAD_HOJAS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@NRO_BL", num));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()){
                    CANTIDAD_HOJAS = int.Parse(dr["CANTIDAD"].ToString());
                    CANTIDAD_REGISTROS = int.Parse(dr["CANTIDAD_REGISTROS"].ToString());
                }
            }
            

            /********************************************************************/
            /**       ASIGNAR DETALLE BL A VIEWSTATE                           **/
            /********************************************************************/

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                int numero = 1;
                SqlDataAdapter da = new SqlDataAdapter("SP_READ_DETALLE_BL", conexion);
                DataSet dt = new DataSet();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@N_BL", num);
                da.Fill(dt);

                dt.Tables[0].TableName = "DETALLE";
                List<DETALLE_BL> DETALLE_BL = new List<DETALLE_BL>();

                foreach (DataRow data in dt.Tables["DETALLE"].Rows){
                    DETALLE_BL detalle_bl = new DETALLE_BL();
                    detalle_bl.ID = numero;
                    detalle_bl.MARKS_NUMBERS = data["MARKS_NUMBER"].ToString();
                    detalle_bl.N_PKGS = data["N_PKGS_CONTAINERS"].ToString();
                    detalle_bl.DESCRIPTION_PKGS = data["DESCRIPTION_PKGS"].ToString();
                    detalle_bl.GROSS_WIGHT = data["GROSS_WIGHT"].ToString();
                    detalle_bl.MEASUREMENT = data["MEASUREMENT"].ToString();
                    DETALLE_BL.Add(detalle_bl);
                    numero++;
                }

                //===========================================================
                // ASIGNAR VIEWSTATE                                  
                //===========================================================
                V_Global().DETALLE_BL = DETALLE_BL;
            }

            /********************************************************************/
            /**             ELIMINAR LOS ARCHIVOS DE LA CARPETA                **/
            /********************************************************************/
            List<string> strFiles = Directory.GetFiles(RUTA_DESTINO, "*", SearchOption.AllDirectories).ToList();
            foreach (string fichero in strFiles)
            {
                File.Delete(fichero);
            }



            for (int hoja = 1; hoja <= CANTIDAD_HOJAS; hoja++){
                try{

                    /**********************************************************************************************/
                    /**                 PRINCIPIO DE HOJA                                                        **/
                    /**********************************************************************************************/


                    /********************************************************************/
                    /**       COPIAR EL ARCHIVO ORIGINAL Y LO PEGA EN EL DESTINO       **/
                    /********************************************************************/
                    string NOMBRE_ARCHIVO = num;
                    File.Copy(RUTA_WORD, Path.Combine(RUTA_DESTINO, NOMBRE_ARCHIVO + "" + hoja + ".docx"), true);

                    /********************************************************************/
                    /**               ABRE EL WORD COPIADO EN AL RUTA DESTINO          **/
                    /********************************************************************/

                    String RUTA_NUEVO_ARCHIVO = RUTA_DESTINO + NOMBRE_ARCHIVO + "" + hoja + ".docx";

                    using (WordprocessingDocument wordDoc =  WordprocessingDocument.Open(RUTA_NUEVO_ARCHIVO, true))
                    {


                        /********************************************************************/
                        /**                     DTIPO DE DOCUMENTO                        **/
                        /********************************************************************/

                        BUSCAR_REMPLAZAR(wordDoc, "TIPDOC", TIP_DOC);



                        /********************************************************************/
                        /**               MODIFICAR PARAMETROS DEL WORD BL                 **/
                        /********************************************************************/
                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                            SqlCommand cmd = new SqlCommand("SP_READ_DATOS_BL_PDF", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@N_BL", num));
                            conexion.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read()){
                                BUSCAR_REMPLAZAR(wordDoc, "NBL", dr["N_BL"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "NBOOKING", dr["N_BOOKING"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "EXPORTREFERENCES", "Hoja :" + hoja + "/" + CANTIDAD_HOJAS + "");
                                BUSCAR_REMPLAZAR(wordDoc, "POINTCOUNTRY", dr["POINT_COUNTRY"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "VESSEL", dr["VESSEL"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "PORTLOADING", dr["PORT_LOADING"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "PORTDISCHARGE", dr["PORT_DISCHARGE"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "PLACEDELIVERY", dr["PLACE_DELIVERY"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "BOARDATE", dr["ON_BOAR_DATE"].ToString());
                                ID_SHIPPER   = dr["ID_SHIPPER"].ToString();
                                ID_CONSIGNEE = dr["ID_CONSIGNEE"].ToString();
                                ID_NOTIFY    = dr["ID_NOTIFY"].ToString();
                                ID_DELIVERY  = dr["ID_DELIVERY"].ToString();
                                conexion.Close();
                            }
                        }

                        /********************************************************************/
                        /**                           SHIPPER                              **/
                        /********************************************************************/

                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                            SqlCommand cmd = new SqlCommand("SP_READ_AGENTE_X_ID", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@ID_AGENTE", ID_SHIPPER));
                            conexion.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read()){
                                BUSCAR_REMPLAZAR(wordDoc, "SHIPPERNOMBRE", dr["NOMBRE"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "SHIPPERRUT", dr["RUT"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "SHIPPERDIRECCION", dr["DIRECCION"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "SHIPPERCIUDAD", dr["CIUDAD"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "SHIPPERPAIS", dr["PAIS"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "SHIPPERCP", dr["COD_POSTAL"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "SHIPERTELEFONO", dr["TELEFONO_1"].ToString() + " / " + dr["TELEFONO_2"].ToString());
                                conexion.Close();
                            }
                        }

                        /********************************************************************/
                        /**                           CONSIGNEE                            **/
                        /********************************************************************/

                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                            SqlCommand cmd = new SqlCommand("SP_READ_AGENTE_X_ID", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@ID_AGENTE", ID_CONSIGNEE));
                            conexion.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read()){
                                BUSCAR_REMPLAZAR(wordDoc, "CONSIGNENOMBRE", dr["NOMBRE"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "CONSIGNERUT", dr["RUT"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "CONSIGNEDIRECCION", dr["DIRECCION"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "CONCIUDAD", dr["CIUDAD"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "CONPAIS", dr["PAIS"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "CONSIGNECODP", dr["COD_POSTAL"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "CONTELEFONO", dr["TELEFONO_1"].ToString() + " / " + dr["TELEFONO_2"].ToString());
                                conexion.Close();
                            }
                        }


                        /********************************************************************/
                        /**                           NOTIFY                               **/
                        /********************************************************************/

                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                        {
                            SqlCommand cmd = new SqlCommand("SP_READ_AGENTE_X_ID", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@ID_AGENTE", ID_NOTIFY));
                            conexion.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                BUSCAR_REMPLAZAR(wordDoc, "NOTIFYNOMBRE", dr["NOMBRE"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "NOTIFYRUT", dr["RUT"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "NOTIFYDIRECCION", dr["DIRECCION"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "NOTIFYCIUDAD", dr["CIUDAD"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "NOTIFYPAIS", dr["PAIS"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "NOTIFYCP", dr["COD_POSTAL"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "NOTIFYTELEFONO", dr["TELEFONO_1"].ToString() + " / " + dr["TELEFONO_2"].ToString());

                                conexion.Close();
                            }
                        }

                        /********************************************************************/
                        /**                           DELIVERY                             **/
                        /********************************************************************/

                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                            SqlCommand cmd = new SqlCommand("SP_READ_AGENTE_X_ID", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@ID_AGENTE", ID_DELIVERY));
                            conexion.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read()){
                                BUSCAR_REMPLAZAR(wordDoc, "DNOM", dr["NOMBRE"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "RUTDELIVER", dr["RUT"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "DDIR", dr["DIRECCION"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "SDELRCIUDAD", dr["CIUDAD"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "DELIVPAIS", dr["PAIS"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "DELIVCODP", dr["COD_POSTAL"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "DELIVTELEFONO", dr["TELEFONO_1"].ToString() + " / " + dr["TELEFONO_2"].ToString());
                                conexion.Close();
                            }
                        }

                        /********************************************************************/
                        /**                     DETALLE DEL BL                              **/
                        /********************************************************************/

                        for (int i = 1; i <= 6; i++)
                        {
                            int h = V_Global().DETALLE_BL.Where(p => p.ID == POSICION_EN_ARREGLO).Count();
                            if (h == 1)
                            {
                                BUSCAR_REMPLAZAR(wordDoc, "MARKSNUMBERS" + i, V_Global().DETALLE_BL.Where(p => p.ID == POSICION_EN_ARREGLO).First().MARKS_NUMBERS);
                                BUSCAR_REMPLAZAR(wordDoc, "NPKGS" + i, V_Global().DETALLE_BL.Where(p => p.ID == POSICION_EN_ARREGLO).First().N_PKGS);
                                BUSCAR_REMPLAZAR(wordDoc, "DESPACKSREPLACE_" + i, V_Global().DETALLE_BL.Where(p => p.ID == POSICION_EN_ARREGLO).First().DESCRIPTION_PKGS);
                                BUSCAR_REMPLAZAR(wordDoc, "GROSSWIGHT" + i, V_Global().DETALLE_BL.Where(p => p.ID == POSICION_EN_ARREGLO).First().GROSS_WIGHT);
                                BUSCAR_REMPLAZAR(wordDoc, "MEASUREMENT" + i, V_Global().DETALLE_BL.Where(p => p.ID == POSICION_EN_ARREGLO).First().MEASUREMENT);
                            }
                            else
                            {
                                BUSCAR_REMPLAZAR(wordDoc, "MARKSNUMBERS" + i, " ");
                                BUSCAR_REMPLAZAR(wordDoc, "NPKGS" + i, " ");
                                BUSCAR_REMPLAZAR(wordDoc, "DESPACKSREPLACE_" + i, " ");
                                BUSCAR_REMPLAZAR(wordDoc, "GROSSWIGHT" + i, " ");
                                BUSCAR_REMPLAZAR(wordDoc, "MEASUREMENT" + i, " ");
                            }
                            POSICION_EN_ARREGLO++;
                        }


                        /********************************************************************/
                        /**                           CONCEPTOS                            **/
                        /********************************************************************/
                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                            SqlDataAdapter da = new SqlDataAdapter("SP_READ_VENTAS_EXENTAS_X_BL_PDF", conexion);
                            DataSet dt = new DataSet();
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand.Parameters.AddWithValue("@N_BL", num);
                            da.Fill(dt);
                            dt.Tables[0].TableName = "CONCEPTOS";
                            int C = 1;
                            foreach (DataRow data in dt.Tables["CONCEPTOS"].Rows){
                                BUSCAR_REMPLAZAR(wordDoc, "CONCEPTO" + C, data["DETALLE"].ToString());
                                if (Boolean.Parse(data["PRE_COL"].ToString()) == true){
                                    BUSCAR_REMPLAZAR(wordDoc, "PREPAID" + C, data["MONTO_USD"].ToString());
                                    BUSCAR_REMPLAZAR(wordDoc, "COLLECT" + C , " ");
                                }else if (Boolean.Parse(data["PRE_COL"].ToString()) == false){
                                    BUSCAR_REMPLAZAR(wordDoc, "PREPAID" + C , " ");
                                    BUSCAR_REMPLAZAR(wordDoc, "COLLECT" + C, data["MONTO_USD"].ToString());
                                }
                                C++;
                            }for (int i = C; i < 10; i++){
                                BUSCAR_REMPLAZAR(wordDoc, "CONCEPTO" + i, " ");
                                BUSCAR_REMPLAZAR(wordDoc, "PREPAID" + i, " ");
                                BUSCAR_REMPLAZAR(wordDoc, "COLLECT" + i , " ");
                            }
                            conexion.Close();
                        }

                        /********************************************************************/
                        /**                           MONTOS TOTALES                       **/
                        /********************************************************************/

                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                            SqlCommand cmd = new SqlCommand("SP_READ_VENTAS_EXENTA_MONTOS_TOTALES_PDF", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@N_BL", num));
                            conexion.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read()){
                                BUSCAR_REMPLAZAR(wordDoc, "PREPAIDTOTAL", dr["MONTO_TOTAL_PREPAID"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "COLLECTTOTAL", dr["MONTO_TOTAL_COLLECT"].ToString());
                            }
                            conexion.Close();
                        }
                    }

                    /********************************************************************/
                    /**                         CIERRA EL WORD                        **/
                    /********************************************************************/

                    Response.Write("<script>");
                    Response.Write("window.open('http://localhost:58997/Archivos/" + NOMBRE_ARCHIVO + "" + hoja + ".docx','_blank')");
                    //Response.Write("window.open('http://190.107.176.150//Archivos/" + NOMBRE_ARCHIVO + "" + hoja + ".docx','_blank')");

                    Response.Write("</script>");

                }
                catch (Exception EX){
                    CUADRO_CORRECTO.Visible = false;
                    CUADRO_ERROR.Visible = true;
                    MENSAJE_ERROR.Text = EX.ToString();
                }
                /********************************************************************/
                /**                 FINAL DE HOJA                                  **/
                /********************************************************************/
            }
        }

        /// <summary>
        /// REEMPLAZA LOS CAMPOS EN EL DOCUMENTO WORD
        /// </summary>
        public void BUSCAR_REMPLAZAR(WordprocessingDocument wordDoc, string BUSCAR , string REEMPLAZAR){
            try { 
                    string docText = null;
                    using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream())){
                        docText = sr.ReadToEnd();
                    }
                    Regex regexText = new Regex(BUSCAR);
                    docText = regexText.Replace(docText, REEMPLAZAR);
                    using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create))){
                        sw.Write(docText);
                    }
            }catch (Exception EX){
                    CUADRO_CORRECTO.Visible = false;
                    CUADRO_ERROR.Visible = true;
                    MENSAJE_ERROR.Text = EX.ToString();
            }
        }

        /// <summary>
        /// VIEWSTATE PARA VARIABLES GLOBALES
        /// </summary>
        private void Establecer_Globales(){
            try{
                ViewState["GlobalesFormulario"] = new GlobalesFormulario();
            }catch{
                throw;
            }
        }

        /// <summary>
        /// VIEWSTATE PARA VARIABLES GLOBALES
        /// </summary>
        /// <returns></returns>
        private GlobalesFormulario V_Global(){
            GlobalesFormulario item = new GlobalesFormulario();
            try{
                item = (GlobalesFormulario)ViewState["GlobalesFormulario"] ?? null;
                return item;
            }catch{
                return item;
            }
        }


        [Serializable]
        public class GlobalesFormulario{
            public List<DETALLE_BL> DETALLE_BL { get; set; }
        }

        protected void BTN_IMPRIMIR_BL_ServerClick(object sender, EventArgs e){
            DESCARGAR_BL(LBL_ID_BL.Text, RBL_TIPO_DOC.SelectedValue.ToString());
        }
    }
}