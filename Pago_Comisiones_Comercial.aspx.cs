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
    public partial class Pago_Comisiones_Comercial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Helpers helpers = new Helpers();

            bool VAR = helpers.VALIDAR_PERMISO(int.Parse(Request.Cookies["ID_USUARIO"].Value.ToString()), 3);

            if (VAR)
            {
            }
            else
            {
                Response.Redirect("~/Home.aspx");
            }
        }

        protected void BTN_FILTRAR_CC_ServerClick(object sender, EventArgs e)
        {
            RPT_PAGO_COMISIONES_X_COMERCIAL.DataBind();
            DDL_OPERACION_A_PAGAR.DataBind();
            CARGAR_CLIENTE();
            RPT_COMISIONES_PAGADAS_X_COMERCIAL.DataBind();
            CALCULAR_MONTOS_TOTALES();
            SELECCIONAR_OPERACION.Visible = true;
        }


        protected void BTN_VER_VALORACION_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Valorar.aspx?NRO_OP=" + DDL_OPERACION_A_PAGAR.SelectedValue.ToString() + "");
        }

        protected void BTN_VER_OPERACION_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Registrar_Operacion.aspx?NRO_OP=" + DDL_OPERACION_A_PAGAR.SelectedValue.ToString() + "");
        }


        protected void RPT_COMISIONES_PENDIENTES_X_COMERCIAL_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string num = Convert.ToString(e.CommandArgument);
            switch (e.CommandName)
            {
                case "OPERACION":
                    Response.Redirect("~/Registrar_Operacion.aspx?NRO_OP=" + num + "");
                    break;
                case "VALORACION":
                    Response.Redirect("~/Valorar.aspx?NRO_OP=" + num + "");
                    break;
            }
        }

        protected void RPT_COMISIONES_PAGADAS_X_COMERCIAL_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string num = Convert.ToString(e.CommandArgument);
            switch (e.CommandName)
            {
                case "OPERACION":
                    Response.Redirect("~/Registrar_Operacion.aspx?NRO_OP=" + num + "");
                    break;
                case "VALORACION":
                    Response.Redirect("~/Valorar.aspx?NRO_OP=" + num + "");
                    break;
                case "ELIMINAR":
                    Helpers helpers = new Helpers();
                    bool VAR = helpers.VALIDAR_PERMISO(int.Parse(Request.Cookies["ID_USUARIO"].Value.ToString()), 2);
                    if (VAR)
                    {
                        try
                        {
                            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                            {
                                SqlCommand cmd = new SqlCommand("SP_DELETE_PAGO_COMISIONES_X_COMERCIAL", conexion);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(new SqlParameter("@ID_PAGO", num));
                                cmd.Parameters.Add(new SqlParameter("@ID_COMERCIAL", DDL_COMERCIAL.SelectedValue.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@ID_OPERACION", DDL_OPERACION_A_PAGAR.SelectedValue.ToString()));
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
                                if (VALOR == 1)
                                {
                                    RPT_PAGO_COMISIONES_X_COMERCIAL.DataBind();
                                    //RPT_COMISIONES_PENDIENTES_X_COMERCIAL.DataBind();
                                    RPT_COMISIONES_PAGADAS_X_COMERCIAL.DataBind();
                                    CALCULAR_MONTOS_TOTALES();
                                    CARGAR_CLIENTE();
                                    CUADRO_CORRECTO.Visible = true;
                                    CUADRO_ERROR.Visible = false;
                                    MENSAJE_CORRECTO.Text = "EL REGISTRO N° : " + num + " FUE ELIMINADO CORRECTAMENTE";
                                }
                                else
                                {
                                    CUADRO_ERROR.Visible = true;
                                    CUADRO_CORRECTO.Visible = false;
                                    MENSAJE_ERROR.Text = "EL PROCEDIMIENTO EXEC SP_DELETE_PAGO_COMISIONES_X_COMERCIAL " + num + " FUE ERRONEO";
                                }
                            }
                        }
                        catch (Exception EX)
                        {
                            CUADRO_CORRECTO.Visible = false;
                            CUADRO_ERROR.Visible = true;
                            MENSAJE_ERROR.Text = EX.ToString();
                        }
                    }
                    else
                    {
                        CUADRO_ERROR.Visible = true;
                        CUADRO_CORRECTO.Visible = false;
                        MENSAJE_ERROR.Text = "USUARIO NO POSEE PERMISOS PARA ELIMINAR.";
                    }
                    break;


            }
        }

        protected void BTN_REGISTRAR_PAGO_ServerClick(object sender, EventArgs e)
        {

                try
                {
                    using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("SP_CREATE_PAGO_COMISION_COMERCIAL", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", int.Parse(Request.Cookies["ID_USUARIO"].Value.ToString())));
                        cmd.Parameters.Add(new SqlParameter("@FECHA_PAGO", DateTime.Parse(TXT_FECHA_PAGO.Text)));
                        cmd.Parameters.Add(new SqlParameter("@ID_COMERCIAL", DDL_COMERCIAL.SelectedValue.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@MONTO", TXT_MONTO.Text));
                        cmd.Parameters.Add(new SqlParameter("@DETALLE", TXT_DETALLE_PAGO.Text));
                        cmd.Parameters.Add(new SqlParameter("@ID_OPERACION", DDL_OPERACION_A_PAGAR.SelectedValue.ToString()));
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
                            MENSAJE_CORRECTO.Text = "PAGO FUE REGISTRADA CORRECTAMENTE ";
                            //LIMPIAR
                            TXT_DETALLE_PAGO.Text = "";
                            TXT_FECHA_PAGO.Text = "";
                            TXT_MONTO.Text = "";
                            RPT_PAGO_COMISIONES_X_COMERCIAL.DataBind();
                            RPT_COMISIONES_PAGADAS_X_COMERCIAL.DataBind();
                            CALCULAR_MONTOS_TOTALES();
                            CARGAR_CLIENTE();
                        }
                        else
                        {
                            CUADRO_CORRECTO.Visible = false;
                            CUADRO_ERROR.Visible = true;
                            MENSAJE_ERROR.Text = "ERROR AL REGISTRAR DATOS , REVISAR SP_CREATE_PAGO_COMISION_COMERCIAL DEVOLVIO RETURN 0";
                        }
                    }
                }
                catch (Exception EX)
                {
                    CUADRO_CORRECTO.Visible = false;
                    CUADRO_ERROR.Visible = true;
                    MENSAJE_ERROR.Text = EX.ToString();
                }
        }

        protected void RPT_PAGO_COMISIONES_X_COMERCIAL_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string num = Convert.ToString(e.CommandArgument);
            switch (e.CommandName)
            {
                case "ELIMINAR":
                    Helpers helpers = new Helpers();
                    bool VAR = helpers.VALIDAR_PERMISO(int.Parse(Request.Cookies["ID_USUARIO"].Value.ToString()), 2);
                    if (VAR)
                    {
                        try
                        {
                            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                            {
                                SqlCommand cmd = new SqlCommand("SP_DELETE_PAGO_COMISIONES_X_COMERCIAL", conexion);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(new SqlParameter("@ID_PAGO", num));
                                cmd.Parameters.Add(new SqlParameter("@ID_COMERCIAL", DDL_COMERCIAL.SelectedValue.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@ID_OPERACION", DDL_OPERACION_A_PAGAR.SelectedValue.ToString()));
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
                                if (VALOR == 1)
                                {
                                    RPT_PAGO_COMISIONES_X_COMERCIAL.DataBind();
                                    //RPT_COMISIONES_PENDIENTES_X_COMERCIAL.DataBind();
                                    RPT_COMISIONES_PAGADAS_X_COMERCIAL.DataBind();
                                    CALCULAR_MONTOS_TOTALES();
                                    CARGAR_CLIENTE();
                                    CUADRO_CORRECTO.Visible = true;
                                    CUADRO_ERROR.Visible = false;
                                    MENSAJE_CORRECTO.Text = "EL REGISTRO N° : " + num + " FUE ELIMINADO CORRECTAMENTE";
                                }
                                else
                                {
                                    CUADRO_ERROR.Visible = true;
                                    CUADRO_CORRECTO.Visible = false;
                                    MENSAJE_ERROR.Text = "EL PROCEDIMIENTO EXEC SP_DELETE_PAGO_COMISIONES_X_COMERCIAL " + num + " FUE ERRONEO";
                                }
                            }
                        }
                        catch (Exception EX)
                        {
                            CUADRO_CORRECTO.Visible = false;
                            CUADRO_ERROR.Visible = true;
                            MENSAJE_ERROR.Text = EX.ToString();
                        }
                    }
                    else
                    {
                        CUADRO_ERROR.Visible = true;
                        CUADRO_CORRECTO.Visible = false;
                        MENSAJE_ERROR.Text = "USUARIO NO POSEE PERMISOS PARA ELIMINAR.";
                    }
                    break;
            }
        }

        public void CALCULAR_MONTOS_TOTALES()
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("SP_CONSOLIDAR_PAGOS_X_COMERCIAL", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID_COMERCIAL", DDL_COMERCIAL.SelectedValue.ToString()));
                cmd.Parameters.Add(new SqlParameter("@ID_OPERACION", DDL_OPERACION_A_PAGAR.SelectedValue.ToString()));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    TXT_MONTO_PAGOS.Text = dr["TOTAL_PAGO_X_COMERCIAL"].ToString();
                    TXT_MONTO_COMISIONES.Text = dr["TOTAL_COMISIONES_X_COMERCIAL"].ToString();
                    TXT_SALDOS.Text = dr["SALDO_TOTAL_PENDIENTE"].ToString();
                    TXT_MONTO.Text = dr["MONTO_PENDIENTE"].ToString();
                    TXT_FECHA_PAGO.Text = dr["FECHA_ACTUAL"].ToString();
                }
                conexion.Close();
            }
        }

        protected void DDL_OPERACION_A_PAGAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            CARGAR_CLIENTE();
            CALCULAR_MONTOS_TOTALES();


        }

        /*-----------------------------------------------------------------*/
        /*                     CARGA DATOS DEL CLIENTE                     */
        /*-----------------------------------------------------------------*/
        public void CARGAR_CLIENTE()
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("SP_READ_CLIENTE_X_OP", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", DDL_OPERACION_A_PAGAR.SelectedValue.ToString()));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    TXT_NOMBRE.Text = dr["NOMBRE"].ToString();
                    TXT_RUT.Text = dr["RUT"].ToString();
                    TXT_DIRECCION.Text = dr["DIRECCION"].ToString();
                    TXT_CIUDAD.Text = dr["CIUDAD"].ToString();
                }
                else
                {
                    TXT_NOMBRE.Text = "";
                    TXT_RUT.Text = "";
                    TXT_DIRECCION.Text = "";
                    TXT_CIUDAD.Text = "";
                }
                conexion.Close();
            }
        }


    }

}


  