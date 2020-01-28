using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Operaciones.Operaciones
{
    public partial class Registrar_Operacion : System.Web.UI.Page
    {
        public int VALOR_SOLICITUD ;

        protected void Page_Load(object sender, EventArgs e)
        {

       

            if (!IsPostBack)
            {
       
                
                /*-----------------------------------------------------------------*/
                /*                VALIDAR SI ES MODIFICAR O REGISTRAR              */
                /*-----------------------------------------------------------------*/
                VALOR_SOLICITUD = int.Parse(Request.QueryString["NRO_OP"]);
                ID_OPERACION.Text = VALOR_SOLICITUD.ToString();
                        if (VALOR_SOLICITUD == 0) {
                                    BTN_GUARDAR.Visible = true;
                        }
                        else
                        {
                                    BTN_GUARDAR.Visible = false;
                                    /*-----------------------------------------------------------------*/
                                    /*                CARGAR DATOS PARA MODIFICAR                      */
                                    /*-----------------------------------------------------------------*/

                                    try
                                    {
                                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                                        {
                                            SqlCommand cmd = new SqlCommand("SP_READ_OPERACIONES_X_ID", conexion);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", VALOR_SOLICITUD));
                                            conexion.Open();
                                            SqlDataReader dr = cmd.ExecuteReader();
                                            if (dr.Read())
                                            {
                                             TXT_ORIGEN.Text     = dr["ORIGEN"].ToString();
                                             TXT_DESTINO.Text    = dr["DESTINO"].ToString();
                                             TXT_REFERENCIA.Text = dr["REFERENCIA"].ToString();
                                             TXT_UNIDAD.Text     = dr["UNIDAD"].ToString();
                                             CHK_MARITIMO.Checked = Convert.ToBoolean(dr["MARITIMO"].ToString());
                                             CHK_AEREO.Checked = Convert.ToBoolean(dr["AEREO"].ToString());
                                             CHK_TERRESTRE_INTERNACIONAL.Checked = Convert.ToBoolean(dr["TI"].ToString());
                                             CHK_TERRESTRE_NACIONAL.Checked = Convert.ToBoolean(dr["TN"].ToString());
                                             CHK_AGA.Checked = Convert.ToBoolean(dr["AGA"].ToString());
                                             CHK_OTROS.Checked = Convert.ToBoolean(dr["OTROS"].ToString());
                                             CHK_SEGUROS.Checked = Convert.ToBoolean(dr["SEGURO"].ToString());
                                             DDL_CLIENTE.SelectedValue = dr["ID_CLIENTE"].ToString();
                                             DDL_COMERCIAL.SelectedValue = dr["ID_COMERCIAL"].ToString();
                                             DDL_CUSTOMERS.SelectedValue = dr["ID_CUSTOMER"].ToString();
                                             DDL_INCOTERM.SelectedValue  = dr["ID_INCOTERM"].ToString();
                                            
                                             BTN_ACTUALIZAR.Visible = true;
                                          
                                            }
                                            conexion.Close();
                                        }
                                    }
                                    catch (Exception EX)
                                    {
                                        BTN_GUARDAR.Enabled = true;
                                        CUADRO_CORRECTO.Visible = false;
                                        CUADRO_ERROR.Visible = true;
                                        MENSAJE_ERROR.Text = EX.ToString();
                                    }
                        }
            }
        }


        /*-----------------------------------------------------------------*/
        /*                GRABAR OPERACIONES                               */
        /*-----------------------------------------------------------------*/
        protected void BTN_GUARDAR_Click(object sender, EventArgs e)
        {

            try
            {

                BTN_GUARDAR.Enabled = false;

                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_CREATE_OPERACION", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@MARITIMO", CHK_MARITIMO.Checked));
                    cmd.Parameters.Add(new SqlParameter("@AEREO", CHK_AEREO.Checked));
                    cmd.Parameters.Add(new SqlParameter("@TI", CHK_TERRESTRE_INTERNACIONAL.Checked));
                    cmd.Parameters.Add(new SqlParameter("@TN", CHK_TERRESTRE_NACIONAL.Checked));
                    cmd.Parameters.Add(new SqlParameter("@SEGURO", CHK_SEGUROS.Checked));
                    cmd.Parameters.Add(new SqlParameter("@AGA", CHK_AGA.Checked));
                    cmd.Parameters.Add(new SqlParameter("@OTROS", CHK_OTROS.Checked));
                    cmd.Parameters.Add(new SqlParameter("@ID_CLIENTE",DDL_CLIENTE.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@ORIGEN",TXT_ORIGEN.Text));
                    cmd.Parameters.Add(new SqlParameter("@DESTINO",TXT_DESTINO.Text));
                    cmd.Parameters.Add(new SqlParameter("@ID_INCOTERM",DDL_INCOTERM.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@UNIDAD",TXT_UNIDAD.Text));
                    cmd.Parameters.Add(new SqlParameter("@REFERENCIA",TXT_REFERENCIA.Text));
                    cmd.Parameters.Add(new SqlParameter("@ID_COMERCIAL",DDL_COMERCIAL.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@ID_CUSTOMER",DDL_CUSTOMERS.SelectedValue.ToString()));
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

                    if (VALOR > 0)
                    {

                        CUADRO_ERROR.Visible = false;
                        CUADRO_CORRECTO.Visible = true;
                        MENSAJE_CORRECTO.Text = "N° OPERACION :   "+ VALOR + " ";

                        //LIMPIAR
                        TXT_DESTINO.Text = "";
                        TXT_ORIGEN.Text = "";
                        TXT_REFERENCIA.Text = "";
                        TXT_UNIDAD.Text = "";
                        BTN_GUARDAR.Enabled = true;

                    }
                    else
                    {
                        BTN_GUARDAR.Enabled = true;
                        CUADRO_CORRECTO.Visible = false;
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = "ERROR AL REGISTRAR DATOS , REVISAR SP_CREATE_OPERACION DEVOLVIO IDENTITY NULL";
                    }
                }
            }
            catch (Exception EX)
            {
                BTN_GUARDAR.Enabled = true;
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }




        }


        /*-----------------------------------------------------------------*/
        /*                ACTUALIZAR OPERACIONES                           */
        /*-----------------------------------------------------------------*/
        protected void BTN_ACTUALIZAR_Click(object sender, EventArgs e)
        {

            Helpers helpers = new Helpers();

            bool VAR = helpers.VALIDAR_PERMISO(int.Parse(Request.Cookies["ID_USUARIO"].Value.ToString()), 1);

            if (VAR)
            {


                try
                {
                BTN_ACTUALIZAR.Enabled = false;
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_UPDATE_OPERACION", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", ID_OPERACION.Text));
                    cmd.Parameters.Add(new SqlParameter("@MARITIMO", CHK_MARITIMO.Checked));
                    cmd.Parameters.Add(new SqlParameter("@AEREO", CHK_AEREO.Checked));
                    cmd.Parameters.Add(new SqlParameter("@TI", CHK_TERRESTRE_INTERNACIONAL.Checked));
                    cmd.Parameters.Add(new SqlParameter("@TN", CHK_TERRESTRE_NACIONAL.Checked));
                    cmd.Parameters.Add(new SqlParameter("@SEGURO", CHK_SEGUROS.Checked));
                    cmd.Parameters.Add(new SqlParameter("@AGA", CHK_AGA.Checked));
                    cmd.Parameters.Add(new SqlParameter("@OTROS", CHK_OTROS.Checked));
                    cmd.Parameters.Add(new SqlParameter("@ID_CLIENTE", DDL_CLIENTE.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@ORIGEN", TXT_ORIGEN.Text));
                    cmd.Parameters.Add(new SqlParameter("@DESTINO", TXT_DESTINO.Text));
                    cmd.Parameters.Add(new SqlParameter("@ID_INCOTERM", DDL_INCOTERM.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@UNIDAD", TXT_UNIDAD.Text));
                    cmd.Parameters.Add(new SqlParameter("@REFERENCIA", TXT_REFERENCIA.Text));
                    cmd.Parameters.Add(new SqlParameter("@ID_COMERCIAL", DDL_COMERCIAL.SelectedValue.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@ID_CUSTOMER", DDL_CUSTOMERS.SelectedValue.ToString()));
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

                    if (VALOR == 1)
                    {

                        CUADRO_ERROR.Visible = false;
                        CUADRO_CORRECTO.Visible = true;
                        MENSAJE_CORRECTO.Text = "N° OPERACION :   " + ID_OPERACION.Text + " SE HA ACTUALIZADO CORRECTAMENTE ";

                        //LIMPIAR
                        TXT_DESTINO.Text = "";
                        TXT_ORIGEN.Text = "";
                        TXT_REFERENCIA.Text = "";
                        TXT_UNIDAD.Text = "";
                        BTN_GUARDAR.Visible = true;
                        BTN_ACTUALIZAR.Visible = false;

                    }
                    else
                    {
                        BTN_ACTUALIZAR.Enabled = true;
                        BTN_GUARDAR.Visible= false;
                        CUADRO_CORRECTO.Visible = false;
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = "EL PROCEDIMIENTO SP_UPDATE_OPERACION DEVOLVIO ERROR AL ACTUALIZAR";
                    }
                }
            }
            catch (Exception EX)
            {
                BTN_GUARDAR.Enabled = true;
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }

            }
            else
            {
                CUADRO_ERROR.Visible = true;
                CUADRO_CORRECTO.Visible = false;
                MENSAJE_ERROR.Text = "USUARIO NO POSEE PERMISOS PARA EDITAR.";
            }


        }

      
    }
}