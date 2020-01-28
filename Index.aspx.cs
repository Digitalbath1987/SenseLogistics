using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Operaciones
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string USD;
            string EUR; 
            if (!IsPostBack)
            {
                /*-----------------------------------------------------------------*/
                /*                CONSULTA TASA DE CAMBIO                          */
                /*-----------------------------------------------------------------*/
                Helpers helpers = new Helpers();  
                bool VAR = helpers.VERIFICAR_TASA_CAMBIO();

                if (VAR)
            {
                    try {
                        /*-----------------------------------------------------------------*/
                        /*                BUSCAR TASA DE CAMBIO SBIF                       */
                        /*-----------------------------------------------------------------*/
                        USD = helpers.OBTENER_USD();
                        EUR = helpers.OBTENER_EUR();
                    }
                    catch {
                        try {
                            /*-----------------------------------------------------------------*/
                            /*               SI FALLA SBIF BUSCAR TASA DE CAMBIO MINDICADOR.CL */
                            /*-----------------------------------------------------------------*/
                            (USD, EUR) = helpers.TASA_CAMBIO_MINDI();
                        }
                        catch {
                            USD = "0";
                            EUR = "0";
                        }
                    }
                    /*-----------------------------------------------------------------*/
                    /*                INSERTA TASA DE CAMBIO                           */
                    /*-----------------------------------------------------------------*/
                    helpers.INSERTAR_TASA_CAMBIO(USD, EUR);
                }

                helpers = null;

            }

         

        }
        /*-----------------------------------------------------------------*/
        /*                VALIDAR USUARIO Y CONTRASEÑA                     */
        /*-----------------------------------------------------------------*/
        protected void BTN_VALIDAR_USUARIO_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    CUADRO_ERROR.Visible = false;
                    SqlCommand cmd = new SqlCommand("SP_READ_USUARIO", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NOMBRE_USUARIO", USER.Text));
                    cmd.Parameters.Add(new SqlParameter("@CONTRASENA", CONTRASENA.Text));
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

                    if (VALOR > 0)
                    {
                       // Session["ID_USUARIO"] = VALOR;

                        Response.Cookies["ID_USUARIO"].Value = VALOR.ToString();

                        Response.Redirect("~/Home.aspx");
                    }
                    else
                    {
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = "EL USUARIO O LA CONTRASEÑA ES ERRONEA";
                    }
                }
            }
            catch (Exception EX)
            {
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }
        }

    }
}