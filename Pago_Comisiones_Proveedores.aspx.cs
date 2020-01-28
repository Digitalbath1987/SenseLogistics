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
    public partial class Pago_Comisiones_Proveedores : System.Web.UI.Page
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

        protected void BTN_REGISTRAR_PAGO_ServerClick(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_CREATE_PAGO_COMISION_PROVEEDOR", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", int.Parse(Request.Cookies["ID_USUARIO"].Value.ToString())));
                    cmd.Parameters.Add(new SqlParameter("@FECHA_PAGO", DateTime.Parse(TXT_FECHA_PAGO.Text)));
                    cmd.Parameters.Add(new SqlParameter("@MONTO", TXT_MONTO.Text));
                    cmd.Parameters.Add(new SqlParameter("@DETALLE", TXT_DETALLE_PAGO.Text));
                    cmd.Parameters.Add(new SqlParameter("@NRO_OPERACION", DDL_NRO_OPERACION.SelectedValue.ToString()));
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
                        RPT_LISTADO_ABONOS.DataBind();
                        RPT_ULTIMOS_PAGOS_PROVEEDOR.DataBind();
                        CARGAR_MONTOS();
                        CUADRO_ERROR.Visible = false;
                        CUADRO_CORRECTO.Visible = true;
                        MENSAJE_CORRECTO.Text = "PAGO FUE REGISTRADO CORRECTAMENTE ";
                    }
                    else
                    {
                        CUADRO_CORRECTO.Visible = false;
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = "ERROR AL REGISTRAR DATOS , REVISAR SP_CREATE_PAGO_COMISION_PROVEEDOR DEVOLVIO RETURN 0";
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

        protected void BTN_FILTRAR_CC_ServerClick(object sender, EventArgs e)
        {
            DDL_NRO_OPERACION.DataBind();
            SELECCIONAR_OPERACION.Visible = true;
            CARGAR_MONTOS();
            RPT_LISTADO_ABONOS.DataBind();
            RPT_ULTIMOS_PAGOS_PROVEEDOR.DataBind();
            CUADRO_ERROR.Visible = false;
            CUADRO_CORRECTO.Visible = false;



        }

        protected void CARGAR_MONTOS()
        {

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("SP_READ_PAGO_COMISIONES_X_PROVEEDOR_MONTOS_TOTALES", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@NRO_OPERACION", DDL_NRO_OPERACION.SelectedValue.ToString()));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    TXT_MONTO_PAGOS.Text = dr["MONTO_TOTAL_PAGOS"].ToString();
                    TXT_MONTO_COMISIONES.Text = dr["MONTO_TOTAL_COMISION"].ToString();
                    TXT_SALDOS.Text = dr["SALDO_PENDIENTE"].ToString();
                    TXT_MONTO.Text = dr["SALDO_PAGAR"].ToString();
                    TXT_FECHA_PAGO.Text = dr["FECHA_ACTUAL"].ToString();
                   
                }
                conexion.Close();
            }

        }

        protected void RPT_LISTADO_ABONOS_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                                SqlCommand cmd = new SqlCommand("SP_DELETE_PAGOS_COMISIONES_PROVEEDORES", conexion);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(new SqlParameter("@ID_PAGO", num));
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

                                    CARGAR_MONTOS();
                                    RPT_LISTADO_ABONOS.DataBind();
                                    RPT_ULTIMOS_PAGOS_PROVEEDOR.DataBind();
                                    TXT_DETALLE_PAGO.Text = "";

                                }
                                else
                                {
                                    CUADRO_ERROR.Visible = true;
                                    CUADRO_CORRECTO.Visible = false;
                                    MENSAJE_ERROR.Text = "EL PROCEDIMIENTO EXEC SP_DELETE_PAGOS_COMISIONES_PROVEEDORES " + num + " FUE ERRONEO";
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

        protected void RPT_ULTIMOS_PAGOS_PROVEEDOR_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                                SqlCommand cmd = new SqlCommand("SP_DELETE_PAGOS_COMISIONES_PROVEEDORES", conexion);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(new SqlParameter("@ID_PAGO", num));
                                SqlParameter VALOR_RETORNO = new SqlParameter("@RETURN_VALUE", DbType.Int32);
                                VALOR_RETORNO.Direction = ParameterDirection.ReturnValue;
                                cmd.Parameters.Add(VALOR_RETORNO);
                                conexion.Open();
                                cmd.ExecuteNonQuery();
                                int VALOR = int.Parse(cmd.Parameters["@RETURN_VALUE"].Value.ToString());
                                conexion.Close();
                                /*-----------------------------------------------------------------*/
                                /*  VALIDA RETURN_VALUE DEVUELTO DESDE LA BASE DE DATOS            */
                                /*-----------------------------------------------------------------*/
                                if (VALOR == 1)
                                {

                                    CARGAR_MONTOS();
                                    RPT_LISTADO_ABONOS.DataBind();
                                    RPT_ULTIMOS_PAGOS_PROVEEDOR.DataBind();
                                    TXT_DETALLE_PAGO.Text = "";

                                }
                                else
                                {
                                    CUADRO_ERROR.Visible = true;
                                    CUADRO_CORRECTO.Visible = false;
                                    MENSAJE_ERROR.Text = "EL PROCEDIMIENTO EXEC SP_DELETE_PAGOS_COMISIONES_PROVEEDORES " + num + " FUE ERRONEO";
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

        protected void BTN_VER_VALORACION_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Valorar.aspx?NRO_OP=" + DDL_NRO_OPERACION.SelectedValue.ToString() + "");
        }

        protected void BTN_VER_OPERACION_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Registrar_Operacion.aspx?NRO_OP=" + DDL_NRO_OPERACION.SelectedValue.ToString() + "");
        }

        protected void DDL_NRO_OPERACION_SelectedIndexChanged(object sender, EventArgs e)
        {
            //AQUI DEBERIA LEER LOS MONTOS PENDIENTES Y RELLENAR LOS TEXBOX
            CARGAR_MONTOS();
            RPT_LISTADO_ABONOS.DataBind();
            RPT_ULTIMOS_PAGOS_PROVEEDOR.DataBind();
        }
    }
}