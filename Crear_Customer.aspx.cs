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
    public partial class Crear_Customer : System.Web.UI.Page
    {

        public int VALOR_SOLICITUD;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                /*-----------------------------------------------------------------*/
                /*                VALIDAR SI ES MODIFICAR O REGISTRAR              */
                /*-----------------------------------------------------------------*/
                VALOR_SOLICITUD = int.Parse(Request.QueryString["NRO_ID"]);
                ID_CUSTOMER.Text = VALOR_SOLICITUD.ToString();
                if (VALOR_SOLICITUD == 0)
                {
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
                            SqlCommand cmd = new SqlCommand("SP_READ_CUSTOMER_X_SOLICITUD", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@ID_CUSTOMER", VALOR_SOLICITUD));
                            conexion.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                TXT_NOMBRE.Text   = dr["NOMBRE_CUSTOMER"].ToString();
                                TXT_EMAIL.Text    = dr["EMAIL_CUSTOMER"].ToString();
                                TXT_TELEFONO.Text = dr["TELEFONO_CUSTOMER"].ToString();
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
        /*                CREAR CUSTOMER                                   */
        /*-----------------------------------------------------------------*/
        protected void BTN_GUARDAR_Click(object sender, EventArgs e)
        {

            try
            {
                BTN_GUARDAR.Enabled = false;
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_CREATE_CUSTOMER", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NOMBRE", TXT_NOMBRE.Text));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", TXT_EMAIL.Text));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONO", TXT_TELEFONO.Text));
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
                        MENSAJE_CORRECTO.Text = "CUSTOMER :   " + TXT_NOMBRE.Text + ",FUE CREADO CORRECTAMENTE ";

                        //LIMPIAR
                        TXT_EMAIL.Text      = "";
                        TXT_NOMBRE.Text     = "";
                        TXT_TELEFONO.Text   = "";
                        BTN_GUARDAR.Enabled = true;
                    }
                    else
                    {
                        BTN_GUARDAR.Enabled = true;
                        CUADRO_CORRECTO.Visible = false;
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = "ERROR AL REGISTRAR DATOS , REVISAR SP_CREATE_CUSTOMER DEVOLVIO RETURN 0";
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
        /*                ACTUALIZAR CUSTOMER                               */
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
                    SqlCommand cmd = new SqlCommand("SP_UPDATE_CUSTOMER", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_CUSTOMER", ID_CUSTOMER.Text));
                    cmd.Parameters.Add(new SqlParameter("@NOMBRE", TXT_NOMBRE.Text));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", TXT_EMAIL.Text));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONO", TXT_TELEFONO.Text));

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
                        MENSAJE_CORRECTO.Text = "CUSTOMER :   " + TXT_NOMBRE.Text + " SE HA ACTUALIZADO CORRECTAMENTE ";

                        //LIMPIAR
                        TXT_EMAIL.Text    = "";
                        TXT_NOMBRE.Text   = "";
                        TXT_TELEFONO.Text = "";
                        BTN_GUARDAR.Visible = true;
                        BTN_ACTUALIZAR.Visible = false;
                    }
                    else
                    {
                        BTN_ACTUALIZAR.Enabled  = true;
                        BTN_GUARDAR.Visible     = false;
                        CUADRO_CORRECTO.Visible = false;
                        CUADRO_ERROR.Visible    = true;
                        MENSAJE_ERROR.Text      = "EL PROCEDIMIENTO SP_UPDATE_CUSTOMER DEVOLVIO ERROR AL ACTUALIZAR";
                    }
                }
            }
            catch (Exception EX)
            {
                BTN_ACTUALIZAR.Enabled = true;
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