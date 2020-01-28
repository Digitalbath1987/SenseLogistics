//using Microsoft.Office.Interop.Word;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Operaciones
{
    public partial class Crear_Factura : System.Web.UI.Page
    {
        public int TIPO_FACTURA = 0;
        string RUTA_WORD = @"C:\inetpub\wwwroot\Operaciones\Formatos\Factura_Electronica.docx";
        string RUTA_DESTINO = @"C:\inetpub\wwwroot\Operaciones\Archivos\";
        //string RUTA_WORD = @"C:\Users\Digital\Desktop\Sense_Logisticst_V4.0\SenseLogistics-master\Operaciones\Formatos\Factura_Electronica.docx";
        //string RUTA_DESTINO = @"C:\Users\Digital\Desktop\Sense_Logisticst_V4.0\SenseLogistics-master\Operaciones\Archivos\";



        ////public Document DOCUMENTO_WORD { get; set; }




        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BTN_CARGAR_FACTURAS_ServerClick(object sender, EventArgs e)
        {
            SP_READ_FACTURAS_X_OP.DataBind();
            RPT_FACTURAS.DataBind();

        }

        protected void RPT_FACTURAS_ItemCommand(object source, RepeaterCommandEventArgs e){

            string num = Convert.ToString(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SELECCIONAR":
                    DIV_FORMULARIO_FACTURA.Visible = true;
                    LBL_ID_FACTURA.Text    = num;
                    LBL_GRUPO_FACTURA.Text = num;
                    ACCIONES.Visible = true;
                    CARGAR_DATOS_FACTURA();
                break;
            }

        }

        /*-----------------------------------------------------------------*/
        /*                CARGAR DATOS DE LA FACTURA                       */
        /*-----------------------------------------------------------------*/
        public void CARGAR_DATOS_FACTURA()
        {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_READ_DATOS_FACTURACION", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_FACTURA", LBL_ID_FACTURA.Text));
                    conexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        TXT_NOMBRE_CLIENTE.Text     = dr["NOMBRE"].ToString();
                        TXT_RUT_CLIENTE.Text        = dr["RUT"].ToString();
                        TXT_TELEFONO_CLIENTE.Text   = dr["TELEFONO"].ToString();
                        TXT_CIUDAD_CLIENTE.Text     = dr["CIUDAD"].ToString();
                        TXT_COMUNA_CLIENTE.Text     = dr["CIUDAD"].ToString();
                        TXT_DIRECCION_CLIENTE.Text  = dr["DIRECCION"].ToString();
                        TXT_VENDEDOR.Text           = dr["NOMBRE_COMERCIAL"].ToString();
                        TXT_FECHA_EMISION.Text      = DateTime.Today.ToString("yyyy-MM-dd");
                        TXT_FECHA_VECIMIENTO.Text   = DateTime.Today.ToString("yyyy-MM-dd");
                        TIPO_FACTURA                = int.Parse(dr["ID_TIPO_FACTURA"].ToString()); 
                        TXT_OBSERVACION_FACTURA.Text = dr["NRO_OPERACION"].ToString();
                        if (dr["NRO_FACTURA"].ToString() == "0")
                        {
                        BTN_FACTURAR.Visible = true;
                        BTN_VISUALIZAR.Visible = false;
                        }else{
                        BTN_FACTURAR.Visible = false;
                        BTN_VISUALIZAR.Visible = true;
                        DIV_FORMULARIO_FACTURA.Visible = false;
                        }
                    }
                    conexion.Close();
                    SP_READ_CONCEPTOS_FACTURAR_X_ID_FACTURA.DataBind();
                    RPT_CONCEPTOS.DataBind();
                }
                CARGAR_MONTOS_TOTALES();
        }

        /*-----------------------------------------------------------------*/
        /*                CARGAR MONTOS DE LA FACTURA                      */
        /*-----------------------------------------------------------------*/
        public void CARGAR_MONTOS_TOTALES(){
        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                SqlCommand cmd = new SqlCommand("SP_READ_DATOS_FACTURACION_MONTOS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID_FACTURA", LBL_ID_FACTURA.Text));
                cmd.Parameters.Add(new SqlParameter("@TIPO_FACTURA", TIPO_FACTURA));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()){
                    LBL_NETO.Text = dr["NETO"].ToString();
                    LBL_IVA.Text = dr["IVA"].ToString();
                    LBL_EXENTO.Text = dr["EXENTO"].ToString();
                    LBL_TOTAL.Text = dr["TOTAL"].ToString();
                }
                conexion.Close();
                SP_READ_CONCEPTOS_FACTURAR_X_ID_FACTURA.DataBind();
                RPT_CONCEPTOS.DataBind();
            }
        }

        /*-----------------------------------------------------------------*/
        /*                GENERA EL PDF Y DESCARGA                         */
        /*-----------------------------------------------------------------*/
        protected void BTN_VISUALIZAR_Click(object sender, EventArgs e)
        {

            /********************************************************************/
            /**             ELIMINAR LOS ARCHIVOS DE LA CARPETA                **/
            /********************************************************************/
            List<string> strFiles = Directory.GetFiles(RUTA_DESTINO, "*", SearchOption.AllDirectories).ToList();
            foreach (string fichero in strFiles)
            {
                File.Delete(fichero);
            }


            /********************************************************************/
            /**       COPIAR EL ARCHIVO ORIGINAL Y LO PEGA EN EL DESTINO       **/
            /********************************************************************/
            string NOMBRE_ARCHIVO = LBL_ID_FACTURA.Text;
            File.Copy(RUTA_WORD, Path.Combine(RUTA_DESTINO, NOMBRE_ARCHIVO + ".docx"), true);


            /********************************************************************/
            /**               ABRE EL WORD COPIADO EN AL RUTA DESTINO          **/
            /********************************************************************/

            String RUTA_NUEVO_ARCHIVO = RUTA_DESTINO + NOMBRE_ARCHIVO + ".docx";

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(RUTA_NUEVO_ARCHIVO, true))
            {

             /********************************************************************/
             /**   MODIFICAR PARAMETROS DEL WORD FOLIO Y DATOS DEL CLIENTE      **/
             /********************************************************************/

             using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
            SqlCommand cmd = new SqlCommand("SP_READ_DATOS_FACTURACION_PDF", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ID_FACTURA", LBL_ID_FACTURA.Text));
            conexion.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read()){
                    BUSCAR_REMPLAZAR(wordDoc, "F_NUMER", "PENDIENTE");       // dr["FOLIO"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "NUOPCLI", dr["NRO_OPERACION"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "SRCLINOM", dr["NOMBRE"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "RUCLIDET", dr["RUT"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "TECLIDET", dr["TELEFONO"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "GICLIDET", dr["GIRO"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "CICLIDET", dr["CIUDAD"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "COCLIDET", dr["COMUNA"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "DETALLE_DIRECCION", dr["DIRECCION"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "EMISION_FEC", dr["FEC_EMI"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "VEN_FECH", dr["FEC_VENC"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "DICLIDET", dr["DIRECCION"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "VEN_NOMBRE", dr["VENDEDOR"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "OBDECLIDA", dr["OBSERVACION"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "TRADOCCLI", dr["DOC_TRANSPORTE"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "CLIREF", dr["REF_CLIENTE"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "CONPAGCLI", dr["CON_PAGO"].ToString());
                }
                 conexion.Close();
            }

            /********************************************************************/
            /**          MODIFICAR PARAMETROS DEL WORD VALORES DE FACTURA      **/
            /********************************************************************/
   
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                SqlCommand cmd = new SqlCommand("SP_READ_DATOS_FACTURACION_MONTOS_PDF", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID_FACTURA", LBL_ID_FACTURA.Text));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()){
                    BUSCAR_REMPLAZAR(wordDoc, "MONEDECLI", dr["NETO"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "MOIVDECLI", dr["IVA"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "MOEXDECLI", dr["EXENTO"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "MOTODECLI", dr["TOTAL"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "MOSUTOCLI", dr["NETO"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "SOMOTOLET", dr["NETO_LETRAS"].ToString());
 
                }
                conexion.Close();
            }

            /********************************************************************/
            /**          MODIFICAR PARAMETROS DEL WORD CONCEPTOS               **/
            /********************************************************************/

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                int CONTADOR = 1;
                SqlDataAdapter da = new SqlDataAdapter("SP_READ_CONCEPTOS_FACTURAR_X_ID_FACTURA", conexion);
                DataSet dt = new DataSet();

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ID_FACTURA", LBL_ID_FACTURA.Text);
                da.Fill(dt);

                dt.Tables[0].TableName = "REPORTES";

                foreach (DataRow data in dt.Tables["REPORTES"].Rows){

                    BUSCAR_REMPLAZAR(wordDoc, "DENUCLIDE" + CONTADOR, data["DESCRIPCION"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "PREUNDE" + CONTADOR, data["PRECIO_UNITARIO"].ToString());
                    BUSCAR_REMPLAZAR(wordDoc, "TODENU" + CONTADOR, data["SALDO_TOTAL_PENDIENTE"].ToString());
                    CONTADOR++;
                }

                for (int i = CONTADOR; i < 10; i++){
                    BUSCAR_REMPLAZAR(wordDoc, "DENUCLIDE" + i, " ");
                    BUSCAR_REMPLAZAR(wordDoc, "PREUNDE" + i, " ");
                    BUSCAR_REMPLAZAR(wordDoc, "TODENU" + i, " ");
                }
            }
            }

            /********************************************************************/
            /**         DESCARGAR EL ARCHIVO EN EL NAVEGADOR                   **/
            /********************************************************************/
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", NOMBRE_ARCHIVO + ".docx"));
            Response.WriteFile(Path.Combine(RUTA_DESTINO, NOMBRE_ARCHIVO + ".docx"));
            Response.End();

        }


        /// <summary>
        /// REEMPLAZA LOS CAMPOS EN EL DOCUMENTO WORD
        /// </summary>
        public void BUSCAR_REMPLAZAR(WordprocessingDocument wordDoc, string BUSCAR, string REEMPLAZAR)
        {
            try
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }
                Regex regexText = new Regex(BUSCAR);
                docText = regexText.Replace(docText, REEMPLAZAR);
                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                docText = null;
                regexText = null;
            }
            catch (Exception EX)
            {
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }
        }


        protected void BTN_FACTURAR_Click(object sender, EventArgs e)
        {
            if (LBL_TOTAL.Text == "0")
            {
                BTN_FACTURAR.Visible = true;
                BTN_VISUALIZAR.Visible = false;
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = "ERROR AL REGISTRAR FACTURA , MONTO DE FACTURA NO PUEDE SER 0";
            }
            else
            {
                /*-----------------------------------------------------------------*/
                /*                AQUI DEBERIA ENVIAR FACTURA A SII                */
                /*-----------------------------------------------------------------*/
                int FOLIO_SII = 1;
                /*-----------------------------------------------------------------*/
                /*                SI LA RESPUESTA DE SSI ES OK GRABAMOS EN BD      */
                /*-----------------------------------------------------------------*/
                try
                {
                    using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("SP_CREATE_FACTURA", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID_FACTURA", LBL_ID_FACTURA.Text));
                        cmd.Parameters.Add(new SqlParameter("@NOMBRE", TXT_NOMBRE_CLIENTE.Text));
                        cmd.Parameters.Add(new SqlParameter("@RUT", TXT_RUT_CLIENTE.Text));
                        cmd.Parameters.Add(new SqlParameter("@TELEFONO", TXT_TELEFONO_CLIENTE.Text));
                        cmd.Parameters.Add(new SqlParameter("@GIRO", TXT_GIRO_CLIENTE.Text));
                        cmd.Parameters.Add(new SqlParameter("@DIRECCION", TXT_DIRECCION_CLIENTE.Text));
                        cmd.Parameters.Add(new SqlParameter("@COMUNA", TXT_COMUNA_CLIENTE.Text));
                        cmd.Parameters.Add(new SqlParameter("@CIUDAD", TXT_CIUDAD_CLIENTE.Text));
                        cmd.Parameters.Add(new SqlParameter("@FECHA_EMISION", TXT_FECHA_EMISION.Text));
                        cmd.Parameters.Add(new SqlParameter("@FECHA_VENCIMIENTO", TXT_FECHA_VECIMIENTO.Text));
                        cmd.Parameters.Add(new SqlParameter("@VENDEDOR", TXT_VENDEDOR.Text));
                        cmd.Parameters.Add(new SqlParameter("@CON_PAGO", DDL_CONDICION_PAGO.SelectedValue.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@OBSERVACION", TXT_OBSERVACION_FACTURA.Text));
                        cmd.Parameters.Add(new SqlParameter("@REF_CLI", TXT_REF_CLIENTE.Text));
                        cmd.Parameters.Add(new SqlParameter("@DOC_TRANS", TXT_DOC_TRANSPORTE.Text));
                        cmd.Parameters.Add(new SqlParameter("@FOLIO_FACTURA_SII", FOLIO_SII));
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
                        if (VALOR > 0)
                        {
                            CUADRO_ERROR.Visible = false;
                            CUADRO_CORRECTO.Visible = true;
                            MENSAJE_CORRECTO.Text = "FACTURA :   " + FOLIO_SII.ToString() + ",FUE CREADO CORRECTAMENTE Y ENVIADA A SII ";
                            BTN_FACTURAR.Visible = false;
                            BTN_VISUALIZAR.Visible = true;
                        }
                        else
                        {
                            BTN_FACTURAR.Visible = true;
                            BTN_VISUALIZAR.Visible = false;
                            CUADRO_CORRECTO.Visible = false;
                            CUADRO_ERROR.Visible = true;
                            MENSAJE_ERROR.Text = "ERROR AL REGISTRAR DATOS , REVISAR SP_CREATE_FACTURA DEVOLVIO RETURN 0";
                        }
                    }
                }
                catch (Exception EX)
                {
                    BTN_FACTURAR.Visible = true;
                    BTN_VISUALIZAR.Visible = false;
                    CUADRO_CORRECTO.Visible = false;
                    CUADRO_ERROR.Visible = true;
                    MENSAJE_ERROR.Text = EX.ToString();
                }

            }

        }

    }
}
