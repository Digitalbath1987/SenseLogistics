using HiQPdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Operaciones
{
    public partial class Valorar : System.Web.UI.Page
    {
        public int VALOR_SOLICITUD = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack){
                VALOR_SOLICITUD = int.Parse(Request.QueryString["NRO_OP"]);
                TXT_OPERACION.Text = VALOR_SOLICITUD.ToString();
                CARGAR_VALORES_TOTALES();
                CARGAR_CANTIDAD_FACTURAS();
                CARGAR_VALORIZACION();
                CARGAR_CLIENTE();
                CUADRO_ERROR.Visible = false;
            }
        }

        /*-----------------------------------------------------------------*/
        /*                     CARGAR VALORES TOTALES                      */
        /*-----------------------------------------------------------------*/
        public void CARGAR_VALORES_TOTALES(){
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                    SqlCommand cmd = new SqlCommand("SP_CONSOLIDAR_VALORACION", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", TXT_OPERACION.Text));
                    conexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read()){
                        LBL_COSTOS_EXENTOS.Text = dr["COSTO_EXENTO"].ToString();
                        LBL_COSTOS_AFECTAS.Text = dr["COSTO_AFECTO"].ToString();
                        LBL_IVA_COSTOS.Text     = dr["IVA_COSTOS"].ToString();
                        LBL_TOTAL_COSTOS.Text   = dr["TOTAL_COSTOS"].ToString();
                        LBL_VENTAS_EXENTAS.Text = dr["VENTA_EXENTA"].ToString();
                        LBL_VENTAS_AFECTAS.Text = dr["VENTA_AFECTA"].ToString();
                        LBL_IVA_VENTAS.Text     = dr["IVA_VENTAS"].ToString();
                        LBL_TOTAL_VENTAS.Text   = dr["TOTAL_VENTAS"].ToString();
                        LBL_PROFIT.Text         = dr["PROFIT"].ToString();
                    }
                    conexion.Close();
                }
        }

        /*-----------------------------------------------------------------*/
        /*                     BUSCAR OPERACIONES                          */
        /*-----------------------------------------------------------------*/
        protected void BTN_BUSCAR_OPERACION_ServerClick(object sender, EventArgs e){
            try{
            CARGAR_VALORES_TOTALES();
            CARGAR_CLIENTE();
            CARGAR_VALORIZACION();
            CARGAR_CANTIDAD_FACTURAS();
            GV_COMISIONES.DataBind();
            GV_COSTOS_AFECTOS.DataBind();
            GV_COSTO_EXENTO.DataBind();
            GV_VENTA_AFECTA.DataBind();
            GV_VENTA_EXENTA.DataBind();
            CUADRO_ERROR.Visible = false;
            }catch(Exception EX){
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = "NO POSEE PERMISOS . " + EX.ToString();
            }
        }


        /*-----------------------------------------------------------------*/
        /*                   CARGAR VALORACION                             */
        /*-----------------------------------------------------------------*/
        public void CARGAR_VALORIZACION(){
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                SqlCommand cmd = new SqlCommand("SP_READ_VALORACION_X_OP", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", TXT_OPERACION.Text));
                cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", int.Parse(Request.Cookies["ID_USUARIO"].Value.ToString())));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                bool VALOR = false;
                if (dr.Read()){
                    TXT_FECHA_CREACION.Text =  dr["FECHA_CREACION"].ToString() ;
                    TXT_FECHA_PAGO.Text     =  dr["FECHA_PAGO"].ToString();
                    TXT_VALOR_USD.Text      =  dr["VALOR_USD"].ToString();
                    TXT_VALOR_EUR.Text      =  dr["VALOR_EUR"].ToString();
                    TXT_VALOR_GBP.Text      =  dr["VALOR_GBP"].ToString();
                    VALOR                   =  Convert.ToBoolean(dr["LIQUIDAR"].ToString());

                    PANEL_COSTOS.Visible  = true;
                    BTN_LIQUIDAR.Visible  = true;
                    BTN_HABILITAR.Visible = false;
                    BTN_FACTURAR.Visible  = false;
                    BTN_MODIFICAR.Visible = true;
                    BTN_IMPRIMIR.Visible  = true;
                    BTN_VALORAR.Visible   = false;

                    if (VALOR.Equals(true)){
                        BTN_LIQUIDAR.Visible  = false;
                        BTN_HABILITAR.Visible = true;
                        BTN_FACTURAR.Visible  = true;
                    }else{
                        BTN_LIQUIDAR.Visible  = true;
                        BTN_HABILITAR.Visible = false;
                        BTN_FACTURAR.Visible  = false;
                    }
                }else{
                    PANEL_COSTOS.Visible  = false;
                    BTN_LIQUIDAR.Visible  = false;
                    BTN_IMPRIMIR.Visible  = false;
                    BTN_LIQUIDAR.Visible  = false;
                    BTN_HABILITAR.Visible = false;
                    BTN_FACTURAR.Visible  = false;
                    BTN_MODIFICAR.Visible = false;
                    BTN_VALORAR.Visible   = true;
                    TXT_FECHA_CREACION.Text = "";
                    TXT_FECHA_PAGO.Text   = "";
                    TXT_VALOR_USD.Text    = "";
                    TXT_VALOR_EUR.Text    = "";
                    TXT_VALOR_GBP.Text    = "";
                }
                conexion.Close();
                    CUADRO_ERROR.Visible = false;
            }
        }

        /*-----------------------------------------------------------------*/
        /*                     CARGA DATOS DEL CLIENTE                     */
        /*-----------------------------------------------------------------*/
        public void CARGAR_CLIENTE(){
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                SqlCommand cmd = new SqlCommand("SP_READ_CLIENTE_X_OP", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", TXT_OPERACION.Text));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()){
                    TXT_NOMBRE.Text    = dr["NOMBRE"].ToString();
                    TXT_RUT.Text       = dr["RUT"].ToString();
                    TXT_DIRECCION.Text = dr["DIRECCION"].ToString();
                    TXT_CIUDAD.Text    = dr["CIUDAD"].ToString();
                }else{
                    TXT_NOMBRE.Text    = "";
                    TXT_RUT.Text       = "";
                    TXT_DIRECCION.Text = "";
                    TXT_CIUDAD.Text    = "";
                }
                conexion.Close();
            }
        }

        /*-----------------------------------------------------------------*/
        /*               IMPRIMIR DOCUMENTO                                */
        /*-----------------------------------------------------------------*/
        protected void BTN_IMPRIMIR_ServerClick(object sender, EventArgs e){
            string url = "http://190.107.176.150/Imprimir_Valorizacion.aspx?NRO_OP=" + TXT_OPERACION.Text + ""; // aqui hay poner el link del certificado
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
            htmlToPdfConverter.BrowserWidth = 1200;
            htmlToPdfConverter.BrowserHeight = 0;
            htmlToPdfConverter.HtmlLoadedTimeout = 120;
            htmlToPdfConverter.Document.Margins = new PdfMargins(5);
            byte[] pdfBuffer = null;
            pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(url);
            HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");
            HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename=Certificado_Antiguedad.pdf; size={1}",
            false ? "inline" : "attachment", pdfBuffer.Length.ToString()));
            HttpContext.Current.Response.BinaryWrite(pdfBuffer);
            HttpContext.Current.Response.End();
        }

        /*-----------------------------------------------------------------*/
        /*                CREAR CLIENTE                                    */
        /*-----------------------------------------------------------------*/

        protected void BTN_VALORAR_ServerClick(object sender, EventArgs e){
            try{
                BTN_VALORAR.Visible = false;
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                    SqlCommand cmd = new SqlCommand("SP_CREATE_VALORACION", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", TXT_OPERACION.Text));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_CREACION", DateTime.Parse(TXT_FECHA_CREACION.Text)));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_PAGO", DateTime.Parse(TXT_FECHA_PAGO.Text)));
                    cmd.Parameters.Add(new SqlParameter("@TASA_CAMBIO_USD", TXT_VALOR_USD.Text));
                    cmd.Parameters.Add(new SqlParameter("@TASA_CAMBIO_EUR", TXT_VALOR_EUR.Text));
                    cmd.Parameters.Add(new SqlParameter("@TASA_CAMBIO_GBP", TXT_VALOR_GBP.Text));
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

                    if (VALOR > 0){
                        CUADRO_ERROR.Visible = false;
                        TXT_OPERACION.Text = VALOR.ToString();
                        CARGAR_VALORES_TOTALES();
                        CARGAR_VALORIZACION();
                        CARGAR_CLIENTE();
                    }else{
                        BTN_VALORAR.Visible  = true;
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text   = "ERROR AL REGISTRAR DATOS , REVISAR SP_CREATE_VALORACION DEVOLVIO RETURN 0";
                    }
                }
            }catch (Exception EX){
                BTN_VALORAR.Visible  = true;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text   = "ERROR AL REGISTRAR DATOS , FAVOR RELLENAR TODA LA INFORMACION REQUERIDA ," + EX.ToString();
           //     CARGAR_VALORES_TOTALES();
           //     CARGAR_VALORIZACION();
               CARGAR_CLIENTE();
            }
        }

        public void REGISTRAR_CONCEPTO(int TIPO_CONCEPTO){
            try{
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                    SqlCommand cmd = new SqlCommand("SP_CREATE_CONCEPTO_X_OP", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TIPO_CONCEPTO", TIPO_CONCEPTO));
                    cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", TXT_OPERACION.Text));
                    cmd.Parameters.Add(new SqlParameter("@ID_CONCEPTO", DDL_CONCEPTO.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@ID_MONEDA", DDL_MONEDA.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@MONTO", TXT_MONTO.Text));
                    cmd.Parameters.Add(new SqlParameter("@NRO_REGISTRO", LBL_NRO_REGISTRO.Text));
                    cmd.Parameters.Add(new SqlParameter("@CHK_BL", CHK_BL.Checked.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@PRE_COL", CHK_PREPAID_COLLECT.Checked.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@ID_FACTURA", DDL_FACTURA.SelectedValue.ToString()));
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
                    if (VALOR > 0){
                        CUADRO_ERROR.Visible = false;
                        CARGAR_VALORES_TOTALES();
                        CARGAR_CLIENTE();
                        CARGAR_VALORIZACION();
                        GV_COMISIONES.DataBind();
                        GV_COSTOS_AFECTOS.DataBind();
                        GV_COSTO_EXENTO.DataBind();
                        GV_VENTA_AFECTA.DataBind();
                        GV_VENTA_EXENTA.DataBind();
                        CUADRO_ERROR.Visible = false;
                        LBL_NRO_REGISTRO.Text = "0";
                    }else{
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text   = "ERROR AL REGISTRAR CONCEPTO , REVISAR SP_CREATE_CONCEPTO_X_OP DEVOLVIO RETURN 0";
                    }
                }
            }catch (Exception EX){
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text   = EX.ToString();
            }
        }

        protected void BTN_REGISTRAR_COSTO_EXENTO_ServerClick(object sender, EventArgs e)
        {
            LBL_TITULO_MODAL.Text  = "REGISTRAR COSTO EXENTO";
            LBL_TIPO_CONCEPTO.Text = "2";
            LIST_CONCEPTOS.DataBind();
            
            string script = @"window.location.href = '#modalHtml2';";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, true);
        }

        protected void BTN_REGISTRAR_VENTA_AFECTA_ServerClick(object sender, EventArgs e)
        {
            LBL_TIPO_CONCEPTO.Text = "3";
            LBL_TITULO_MODAL.Text  = "REGISTRAR VENTA AFECTA";
            LIST_CONCEPTOS.DataBind();
            DIV_ASOCIAR_FACTURA.Visible = true;
            CARGAR_DDL_FACTURAS();

            string script = @"window.location.href = '#modalHtml2';";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, true);
        }

        protected void BTN_REGISTRAR_VENTA_EXENTA_ServerClick(object sender, EventArgs e)
        {
            LBL_TIPO_CONCEPTO.Text = "4";
            LIST_CONCEPTOS.DataBind();
            LBL_TITULO_MODAL.Text = "REGISTRAR VENTA EXENTA";
            DIV_ASOCIAR_FACTURA.Visible = true;
            DIV_CHK_BL.Visible = true;
            CARGAR_DDL_FACTURAS();
            string script = @"window.location.href = '#modalHtml2';";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, true);
        }


        /*-----------------------------------------------------------------*/
        /*             CARGAR DATOS DEL DDL FACTURAS                       */
        /*-----------------------------------------------------------------*/
        public void CARGAR_DDL_FACTURAS()
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                SqlDataAdapter da = new SqlDataAdapter("SP_READ_FACTURAS", conexion);
                DataTable dt = new DataTable();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@NRO_OPERACION", TXT_OPERACION.Text);
                da.SelectCommand.Parameters.AddWithValue("@CONCEPTO", LBL_TIPO_CONCEPTO.Text);
                da.Fill(dt);
                this.DDL_FACTURA.DataSource = dt;
                this.DDL_FACTURA.DataBind();
            }
        }
                         

        protected void BTN_REGISTRAR_COMISION_ServerClick(object sender, EventArgs e){
            try{
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                    SqlCommand cmd = new SqlCommand("SP_CREATE_COMISION_X_OP", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", TXT_OPERACION.Text));
                    cmd.Parameters.Add(new SqlParameter("@ID_COMISIONADO", DDL_COMERCIAL.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@PORCENTAJE", TXT_PORCENTAJE.Text));
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

                    if (VALOR > 0){
                        CUADRO_ERROR.Visible = false;
                        CARGAR_VALORES_TOTALES();
                        CARGAR_CLIENTE();
                        CARGAR_VALORIZACION();
                        GV_COMISIONES.DataBind();
                        GV_COSTOS_AFECTOS.DataBind();
                        GV_COSTO_EXENTO.DataBind();
                        GV_VENTA_AFECTA.DataBind();
                        GV_VENTA_EXENTA.DataBind();
                        CUADRO_ERROR.Visible = false;
                    }else{
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = "ERROR AL REGISTRAR COMISION , REVISAR SP_CREATE_COMISION_X_OP DEVOLVIO RETURN 0";
                    }
                }
            }catch (Exception EX){
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }

        }

        protected void BTN_REGISTRAR_COSTO_AFECTO_ServerClick(object sender, EventArgs e){
            LBL_TIPO_CONCEPTO.Text = "1";
            LIST_CONCEPTOS.DataBind();
            LBL_TITULO_MODAL.Text = "REGISTRAR COSTO AFECTO";
            string script = @"window.location.href = '#modalHtml2';";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, true);
        }

        protected void BTN_CERRAR_ServerClick(object sender, EventArgs e){
            Response.Redirect("~/Valorar.aspx?NRO_OP="+ TXT_OPERACION.Text +"");
        
        }

        protected void BTN_REGISTRAR_CONCEPTO_ServerClick(object sender, EventArgs e)
        {
            REGISTRAR_CONCEPTO(int.Parse(LBL_TIPO_CONCEPTO.Text));
            Response.Redirect("~/Valorar.aspx?NRO_OP=" + TXT_OPERACION.Text + "");
        }

        protected void BTN_COMISION_ServerClick(object sender, EventArgs e)
        {
            string script = @"window.location.href = '#modalHtml';";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, true);
        }

        protected void BTN_LIQUIDAR_ServerClick(object sender, EventArgs e)
        {
            try{
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                    SqlCommand cmd = new SqlCommand("SP_UPDATE_VALORIZACION", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", TXT_OPERACION.Text));
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
                    if (VALOR > 0){
                        CUADRO_ERROR.Visible = false;
                        CARGAR_VALORES_TOTALES();
                        CARGAR_CLIENTE();
                        CARGAR_VALORIZACION();
                        GV_COMISIONES.DataBind();
                        GV_COSTOS_AFECTOS.DataBind();
                        GV_COSTO_EXENTO.DataBind();
                        GV_VENTA_AFECTA.DataBind();
                        GV_VENTA_EXENTA.DataBind();
                        CUADRO_ERROR.Visible = false;
                        BTN_LIQUIDAR.Visible = false;
                        BTN_MODIFICAR.Visible = false;
                    }else{
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = "ERROR AL LIQUITAR OPERACION , REVISAR SP_UPDATE_VALORIZACION DEVOLVIO RETURN 0";
                    }
                }
            }
            catch (Exception EX)
            {
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }
        }

        protected void BTN_MODIFICAR_ServerClick(object sender, EventArgs e)
        {
            Helpers helpers = new Helpers();
            bool VAR = helpers.VALIDAR_PERMISO(int.Parse(Request.Cookies["ID_USUARIO"].Value.ToString()), 1);
            if (VAR){
                try{
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                    SqlCommand cmd = new SqlCommand("SP_UPDATE_MODIFICAR_VALORIZACION", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", TXT_OPERACION.Text));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_CREACION", DateTime.Parse(TXT_FECHA_CREACION.Text)));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_PAGO", DateTime.Parse(TXT_FECHA_PAGO.Text)));
                    cmd.Parameters.Add(new SqlParameter("@TASA_CAMBIO_USD", TXT_VALOR_USD.Text));
                    cmd.Parameters.Add(new SqlParameter("@TASA_CAMBIO_EUR", TXT_VALOR_EUR.Text));
                    cmd.Parameters.Add(new SqlParameter("@TASA_CAMBIO_GBP", TXT_VALOR_GBP.Text));
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", int.Parse(Request.Cookies["ID_USUARIO"].Value.ToString())));
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
                    if (VALOR > 0){
                        CUADRO_ERROR.Visible = false;
                        CARGAR_VALORES_TOTALES();
                        CARGAR_VALORIZACION();
                        CARGAR_CLIENTE();
                    }else{
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = "ERROR AL REGISTRAR DATOS , REVISAR SP_UPDATE_MODIFICAR_VALORIZACION DEVOLVIO RETURN 0";
                    }
                }
            }catch (Exception EX){
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = "ERROR AL REGISTRAR DATOS , FAVOR RELLENAR TODA LA INFORMACION REQUERIDA ," + EX.ToString();
                //     CARGAR_VALORES_TOTALES();
                //     CARGAR_VALORIZACION();
                CARGAR_CLIENTE();
            }
            }else{
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = "USUARIO NO POSEE PERMISOS PARA EDITAR.";
            }
        }

        protected void BTN_HABILITAR_ServerClick(object sender, EventArgs e)
        {
            Helpers helpers = new Helpers();
            bool VAR = helpers.VALIDAR_PERMISO(int.Parse(Request.Cookies["ID_USUARIO"].Value.ToString()), 3);
            if (VAR){
                try{
                    using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("SP_UPDATE_HABILITAR_VALORIZACION", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", TXT_OPERACION.Text));
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
                        if (VALOR > 0){
                            CUADRO_ERROR.Visible = false;
                            CARGAR_VALORES_TOTALES();
                            CARGAR_VALORIZACION();
                            CARGAR_CLIENTE();
                        }else{
                            CUADRO_ERROR.Visible = true;
                            MENSAJE_ERROR.Text = "ERROR AL HABILITAR VALORACION , REVISAR SP_UPDATE_HABILITAR_VALORIZACION DEVOLVIO RETURN 0";
                        }
                    }
                }catch (Exception EX){
                    CUADRO_ERROR.Visible = true;
                    MENSAJE_ERROR.Text = "ERROR AL HABILITAR VALORACION ," + EX.ToString();
                    CARGAR_CLIENTE();
                }
            }else{
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = "USUARIO NO POSEE PERMISOS PARA HABILITAR.";
            }

        }

        protected void GV_COSTOS_AFECTOS_RowCommand(object sender, GridViewCommandEventArgs e){
            string num = Convert.ToString(e.CommandArgument);
            switch (e.CommandName){
            case "ELIMINAR":
                            try{
                            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                            SqlCommand cmd = new SqlCommand("SP_DELETE_COSTO_AFECTOS", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@REGISTRO", num));
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
                            if (VALOR > 0){
                                CUADRO_ERROR.Visible = false;
                                CARGAR_VALORES_TOTALES();
                                CARGAR_CLIENTE();
                                CARGAR_VALORIZACION();
                                GV_COMISIONES.DataBind();
                                GV_COSTOS_AFECTOS.DataBind();
                                GV_COSTO_EXENTO.DataBind();
                                GV_VENTA_AFECTA.DataBind();
                                GV_VENTA_EXENTA.DataBind();
                                CUADRO_ERROR.Visible = false;
                            }else{
                                CUADRO_ERROR.Visible = true;
                                MENSAJE_ERROR.Text = "ERROR AL ELIMINAR REGISTRO , REVISAR SP_DELETE_COSTO_AFECTOS DEVOLVIO RETURN 0";
                            }
                            }
                            }catch (Exception EX){
                            CUADRO_ERROR.Visible = true;
                            MENSAJE_ERROR.Text = EX.ToString();
                            }
            break;
            case "MODIFICAR":
                    LBL_TIPO_CONCEPTO.Text = "1";
                    LIST_CONCEPTOS.DataBind();
                    LBL_TITULO_MODAL.Text = "REGISTRAR COSTO AFECTO";
                    string script = @"window.location.href = '#modalHtml2';";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, true);
                    using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                        SqlCommand cmd = new SqlCommand("SP_READ_CONCEPTO_X_REGISTRO", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@REGISTRO", num));
                        cmd.Parameters.Add(new SqlParameter("@TIPO_CONCEPTO", LBL_TIPO_CONCEPTO.Text));
                        conexion.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read()){
                            DDL_CONCEPTO.SelectedValue = dr["ID_DETALLE_AFECTO"].ToString();
                            DDL_MONEDA.SelectedValue = dr["ID_MONEDA"].ToString();
                            TXT_MONTO.Text = dr["MONTO"].ToString();
                            LBL_NRO_REGISTRO.Text = num;
                        }
                        conexion.Close();
                    }
                    break;
            }
        }

        protected void GV_COSTO_EXENTO_RowCommand(object sender, GridViewCommandEventArgs e){
            string num = Convert.ToString(e.CommandArgument);
            switch (e.CommandName){
            case "ELIMINAR":
                    try{
                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                            SqlCommand cmd = new SqlCommand("SP_DELETE_COSTOS_EXENTOS", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@REGISTRO", num));
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
                                CARGAR_VALORES_TOTALES();
                                CARGAR_CLIENTE();
                                CARGAR_VALORIZACION();
                                GV_COMISIONES.DataBind();
                                GV_COSTOS_AFECTOS.DataBind();
                                GV_COSTO_EXENTO.DataBind();
                                GV_VENTA_AFECTA.DataBind();
                                GV_VENTA_EXENTA.DataBind();
                                CUADRO_ERROR.Visible = false;
                            }else{
                                CUADRO_ERROR.Visible = true;
                                MENSAJE_ERROR.Text = "ERROR AL ELIMINAR REGISTRO , REVISAR SP_DELETE_COSTOS_EXENTOS DEVOLVIO RETURN 0";
                            }
                        }
                    }
                    catch (Exception EX){
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = EX.ToString();
                    }
                    break;
            case "MODIFICAR":
                    LBL_TITULO_MODAL.Text = "REGISTRAR COSTO EXENTO";
                    LBL_TIPO_CONCEPTO.Text = "2";
                    LIST_CONCEPTOS.DataBind();
                    string script = @"window.location.href = '#modalHtml2';";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, true);
                    using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("SP_READ_CONCEPTO_X_REGISTRO", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@REGISTRO", num));
                        cmd.Parameters.Add(new SqlParameter("@TIPO_CONCEPTO", LBL_TIPO_CONCEPTO.Text));
                        conexion.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                          
                            DDL_CONCEPTO.SelectedValue = dr["ID_DETALLE_EXENTO"].ToString();
                            DDL_MONEDA.SelectedValue = dr["ID_MONEDA"].ToString();
                            TXT_MONTO.Text = dr["MONTO"].ToString();
                            LBL_NRO_REGISTRO.Text = num;

                        }
                        conexion.Close();
                    }
                    break;
            }
        }

        protected void GV_VENTA_AFECTA_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string num = Convert.ToString(e.CommandArgument);
            switch (e.CommandName)
            {
                case "ELIMINAR":
                    try
                    {
                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                        {
                            SqlCommand cmd = new SqlCommand("SP_DELETE_VENTAS_AFECTAS", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@REGISTRO", num));
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
                                CARGAR_VALORES_TOTALES();
                                CARGAR_CLIENTE();
                                CARGAR_VALORIZACION();
                                GV_COMISIONES.DataBind();
                                GV_COSTOS_AFECTOS.DataBind();
                                GV_COSTO_EXENTO.DataBind();
                                GV_VENTA_AFECTA.DataBind();
                                GV_VENTA_EXENTA.DataBind();
                                CUADRO_ERROR.Visible = false;
                            }
                            else
                            {
                                CUADRO_ERROR.Visible = true;
                                MENSAJE_ERROR.Text = "ERROR AL ELIMINAR REGISTRO , REVISAR SP_DELETE_VENTAS_AFECTAS DEVOLVIO RETURN 0";
                            }
                        }
                    }
                    catch (Exception EX)
                    {
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = EX.ToString();
                    }
                    break;
                case "MODIFICAR":
                    LBL_TIPO_CONCEPTO.Text = "3";
                    LBL_TITULO_MODAL.Text = "REGISTRAR VENTA AFECTA";
                    LIST_CONCEPTOS.DataBind();
                    DIV_ASOCIAR_FACTURA.Visible = true;
                    CARGAR_DDL_FACTURAS();

                    string script = @"window.location.href = '#modalHtml2';";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, true);
                    using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("SP_READ_CONCEPTO_X_REGISTRO", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@REGISTRO", num));
                        cmd.Parameters.Add(new SqlParameter("@TIPO_CONCEPTO", LBL_TIPO_CONCEPTO.Text));
                        conexion.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            DDL_CONCEPTO.SelectedValue = dr["ID_DETALLE_AFECTA"].ToString();
                            DDL_MONEDA.SelectedValue = dr["ID_MONEDA"].ToString();
                            TXT_MONTO.Text = dr["MONTO"].ToString();
                            DDL_FACTURA.SelectedValue = dr["ID_FACTURA"].ToString();
                            LBL_NRO_REGISTRO.Text = num;
                        }
                        conexion.Close();
                    }
                    break;
            }
        }

        protected void GV_VENTA_EXENTA_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string num = Convert.ToString(e.CommandArgument);
            switch (e.CommandName)
            {
                case "ELIMINAR":
                    try
                    {
                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                        {
                            SqlCommand cmd = new SqlCommand("SP_DELETE_VENTAS_EXENTAS", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@REGISTRO", num));
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
                                CARGAR_VALORES_TOTALES();
                                CARGAR_CLIENTE();
                                CARGAR_VALORIZACION();
                                GV_COMISIONES.DataBind();
                                GV_COSTOS_AFECTOS.DataBind();
                                GV_COSTO_EXENTO.DataBind();
                                GV_VENTA_AFECTA.DataBind();
                                GV_VENTA_EXENTA.DataBind();
                                CUADRO_ERROR.Visible = false;
                            }
                            else
                            {
                                CUADRO_ERROR.Visible = true;
                                MENSAJE_ERROR.Text = "ERROR AL ELIMINAR REGISTRO , REVISAR SP_DELETE_VENTAS_EXENTAS DEVOLVIO RETURN 0";
                            }
                        }
                    }
                    catch (Exception EX)
                    {
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = EX.ToString();
                    }

                    break;
                case "MODIFICAR":
                    LBL_TIPO_CONCEPTO.Text = "4";
                    LIST_CONCEPTOS.DataBind();
                    LBL_TITULO_MODAL.Text = "REGISTRAR VENTA EXENTA";
                    DIV_ASOCIAR_FACTURA.Visible = true;
                    string script = @"window.location.href = '#modalHtml2';";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, true);
                    CARGAR_DDL_FACTURAS();

                    using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("SP_READ_CONCEPTO_X_REGISTRO", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@REGISTRO", num));
                        cmd.Parameters.Add(new SqlParameter("@TIPO_CONCEPTO", LBL_TIPO_CONCEPTO.Text));
                        conexion.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read()){
                        DIV_CHK_BL.Visible = true;
                        DDL_CONCEPTO.SelectedValue = dr["ID_DETALLE_EXENTA"].ToString();
                        DDL_MONEDA.SelectedValue   = dr["ID_MONEDA"].ToString();
                        TXT_MONTO.Text             = dr["MONTO"].ToString();
                        DDL_FACTURA.SelectedValue = dr["ID_FACTURA"].ToString();
                        //CHK_BL.Checked = bool.Parse(dr["BL"].ToString());
                        LBL_NRO_REGISTRO.Text = num;
                        }
                        conexion.Close();
                    }
                    break;
            }
        }

        protected void GV_COMISIONES_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string num = Convert.ToString(e.CommandArgument);
            switch (e.CommandName)
            {
                case "ELIMINAR":
                    try
                    {
                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                        {
                            SqlCommand cmd = new SqlCommand("SP_DELETE_COMISIONES", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@REGISTRO", num));
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
                                CARGAR_VALORES_TOTALES();
                                CARGAR_CLIENTE();
                                CARGAR_VALORIZACION();
                                GV_COMISIONES.DataBind();
                                GV_COSTOS_AFECTOS.DataBind();
                                GV_COSTO_EXENTO.DataBind();
                                GV_VENTA_AFECTA.DataBind();
                                GV_VENTA_EXENTA.DataBind();
                                CUADRO_ERROR.Visible = false;
                            }
                            else
                            {
                                CUADRO_ERROR.Visible = true;
                                MENSAJE_ERROR.Text = "ERROR AL ELIMINAR REGISTRO , REVISAR SP_DELETE_COMISIONES DEVOLVIO RETURN 0";
                            }
                        }
                    }
                    catch (Exception EX)
                    {
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = EX.ToString();
                    }
                break;
                case "MODIFICAR":
                    string script = @"window.location.href = '#modalHtml';";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, true);
                break;
            }

        }



        /*-----------------------------------------------------------------*/
        /*                REDIRIGIR AL SISTEMA DE FACTURACION              */
        /*-----------------------------------------------------------------*/
        protected void BTN_FACTURAR_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Valorar.aspx?NRO_OP=" + TXT_OPERACION.Text + "");
        }


        public void CARGAR_CANTIDAD_FACTURAS()
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                SqlCommand cmd = new SqlCommand("SP_CANTIDAD_FACTURAS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@NRO_OPERACION", TXT_OPERACION.Text));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()){
                    LBL_CANTIDAD_FACTURAS_EXENTA.Text = dr["FACTURAS_EXENTAS"].ToString();
                    LBL_CANTIDAD_FACTURAS_AFECTA.Text = dr["FACTURAS_AFECTAS"].ToString();
                }else{
                    LBL_CANTIDAD_FACTURAS_EXENTA.Text = "0";
                    LBL_CANTIDAD_FACTURAS_AFECTA.Text = "0";
                }
                conexion.Close();
            }
        }

        public void CREAR_NUEVO_GRUPO_FACTURA(int NUM){
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_CREATE_GRUPO_FACTURA", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TIPO_CONCEPTO", NUM));
                    cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", TXT_OPERACION.Text));
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
                        CARGAR_CANTIDAD_FACTURAS();
                    }
                    else
                    {
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = "ERROR AL ELIMINAR REGISTRO , REVISAR SP_CREATE_GRUPO_FACTURA DEVOLVIO RETURN 0";
                    }
                }
            }
            catch (Exception EX)
            {
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }


        }


        protected void BTN_CREAR_NUEVA_FACTURA_AFECTA_ServerClick(object sender, EventArgs e)
        {
            CREAR_NUEVO_GRUPO_FACTURA(1);
        }

        protected void BTN_CREAR_NUEVA_FACTURA_EXENTA_ServerClick(object sender, EventArgs e)
        {
            CREAR_NUEVO_GRUPO_FACTURA(2);
        }
    }
}