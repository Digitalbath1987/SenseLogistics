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
    public partial class Listado_Cliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RPT_CLIENTES_ItemCommand(object source, RepeaterCommandEventArgs e)
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

                            SqlCommand cmd = new SqlCommand("SP_DELETE_CLIENTE", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@ID_CLIENTE", num));
                         
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
                                LIST_CLIENTE.DataBind();
                                RPT_CLIENTES.DataBind();
                                CUADRO_CORRECTO.Visible = true;
                                CUADRO_ERROR.Visible = false;
                                MENSAJE_CORRECTO.Text = "EL REGISTRO N° : " + num + " FUE ELIMINADO CORRECTAMENTE";
                            }
                            else
                            {
                                CUADRO_ERROR.Visible = true;
                                CUADRO_CORRECTO.Visible = false;
                                MENSAJE_ERROR.Text = "EL PROCEDIMIENTO EXEC SP_DELETE_OPERACIONES " + num + " FUE ERRONEO";
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
                case "EDITAR":
                    Response.Redirect("~/Crear_Cliente.aspx?NRO_ID=" + num + "");
                    break;
            }


        }
    }
}