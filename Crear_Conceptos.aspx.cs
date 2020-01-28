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
    public partial class Conceptos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BTN_GUARDAR_Click(object sender, EventArgs e)
        {
            try
            {
                BTN_GUARDAR.Enabled = false;
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_CREATE_DETALLE_CONCEPTO", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DETALLE", TXT_CONCEPTO.Text));
                    cmd.Parameters.Add(new SqlParameter("@TIPO_CONCEPTO", DDL_TIPO_CONCEPTO.SelectedValue.ToString()));
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
                        MENSAJE_CORRECTO.Text = "COMERCIAL :   " + TXT_CONCEPTO.Text + ",FUE CREADO CORRECTAMENTE ";

                        //LIMPIAR
                        TXT_CONCEPTO.Text = "";
                        BTN_GUARDAR.Enabled = true;
                    }
                    else
                    {
                        BTN_GUARDAR.Enabled = true;
                        CUADRO_CORRECTO.Visible = false;
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = "ERROR AL REGISTRAR DATOS , REVISAR SP_CREATE_COMERCIAL DEVOLVIO RETURN 0";
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
    }
}