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
    public partial class Home_Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

                    if (!IsPostBack)
                    {
                        Decimal paridad;
                        Helpers helpers = new Helpers();

                try
                {
                    NOMBRE_USUARIO.Text = helpers.CONSULTAR_NOMBRE_USUARIO(int.Parse(Request.Cookies["ID_USUARIO"].Value.ToString()));

                }
                catch
                {

                    Response.Redirect("~/Index.aspx");

                }


                try
                {
                                    using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                                    {
                                        SqlCommand cmd = new SqlCommand("SP_READ_TASA_CAMBIO_FECHA", conexion);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        conexion.Open();
                                        SqlDataReader dr = cmd.ExecuteReader();
                                        if (dr.Read())
                                        {
                                            LBL_USD.Text = dr["USD"].ToString();
                                            LBL_EUR.Text = dr["EUR"].ToString();
                                            paridad = Decimal.Parse(LBL_USD.Text) / Decimal.Parse(LBL_EUR.Text);
                                            LBL_PARIDAD.Text = decimal.Round(paridad, 4).ToString();
                                        }
                                        else
                                        {
                                            LBL_USD.Text = "0";
                                            LBL_EUR.Text = "0";
                                            LBL_PARIDAD.Text = "0";
                                        }
                                        conexion.Close();
                                    }
                                }
                                catch (Exception)
                                {
                                    LBL_USD.Text = "0";
                                    LBL_EUR.Text = "0";
                                    LBL_PARIDAD.Text = "0";
                                }
        
                    }

        }

        protected void BTN_SALIR_ServerClick(object sender, EventArgs e)
        {
           
            var cookie = new HttpCookie(Request.Cookies["ID_USUARIO"].Name);
            cookie.Expires = DateTime.Now.AddDays(-1);
            cookie.Value = string.Empty;
            Response.Cookies.Add(cookie);
          
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Index.aspx");
        }
    }
}
