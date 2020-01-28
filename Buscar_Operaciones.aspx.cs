using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Operaciones
{
    public partial class Buscar_Operaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RPT_OPERACIONES_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string num = Convert.ToString(e.CommandArgument);
            switch (e.CommandName)
            {
                case "ELIMINAR":
                    Helpers helpers = new Helpers();
                
                  //  bool VAR = helpers.VALIDAR_PERMISO((int)Session["ID_USUARIO"], 2);
                    bool VAR = helpers.VALIDAR_PERMISO(int.Parse(Request.Cookies["ID_USUARIO"].Value.ToString()), 2);

                    if (VAR)
                   {

                        try
                        {
                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                        {
                            SqlCommand cmd = new SqlCommand("SP_DELETE_OPERACIONES", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", num));
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
                                RPT_OPERACIONES.DataBind();
                                CUADRO_CORRECTO.Visible = true;
                                CUADRO_ERROR.Visible = false;
                                MENSAJE_CORRECTO.Text = "EL REGISTRO N° : "+ num + " FUE ELIMINADO CORRECTAMENTE";
                            }
                            else
                            {
                                CUADRO_ERROR.Visible = true;
                                CUADRO_CORRECTO.Visible = false;
                                MENSAJE_ERROR.Text = "EL PROCEDIMIENTO EXEC SP_DELETE_OPERACIONES "+ num + " FUE ERRONEO";
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
                    Response.Redirect("~/Registrar_Operacion.aspx?NRO_OP=" + num + "");
                    break;
            }





        }

        protected void BTN_FILTRAR_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SP_READ_OPERACIONES_X_OP", conexion);
                    DataTable dt = new DataTable();
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ID_OPERACIONES", TXT_BUSCAR_OPERACION.Text);
                    da.Fill(dt);
                    this.RPT_OPERACIONES.DataSource = dt;
                    this.RPT_OPERACIONES.DataBind();
                    CUADRO_CORRECTO.Visible = false;
                    CUADRO_ERROR.Visible = false;
                    BTN_DESCARGAR_CSV.Visible = false;
                }
            }
            catch (Exception EX)
            {
                this.RPT_OPERACIONES.DataBind();
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }
        }

        protected void BTN_FILTRAR_FECHAS_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SP_READ_OPERACIONES_X_FECHA", conexion);
                    DataTable dt = new DataTable();
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FECHA_INICIO", TXT_FECHA_DESDE.Text);
                    da.SelectCommand.Parameters.AddWithValue("@FECHA_FIN", TXTFECHA_HASTA.Text);
                    da.Fill(dt);
                    this.RPT_OPERACIONES.DataSource = dt;
                    this.RPT_OPERACIONES.DataBind();
                    CUADRO_CORRECTO.Visible = false;
                    CUADRO_ERROR.Visible = false;
                    BTN_DESCARGAR_CSV.Visible = true;

                }
            }
            catch (Exception EX)
            {
                this.RPT_OPERACIONES.DataBind();
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }
        }

        protected void BTN_DESCARGAR_CSV_ServerClick(object sender, EventArgs e)
        {



            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SP_READ_OPERACIONES_X_FECHA", conexion);
                    DataSet dt = new DataSet();
                    string delimitador = ";";
                    StringBuilder sb = new StringBuilder();
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FECHA_INICIO", TXT_FECHA_DESDE.Text);
                    da.SelectCommand.Parameters.AddWithValue("@FECHA_FIN", TXTFECHA_HASTA.Text);
                    da.Fill(dt);

                    dt.Tables[0].TableName = "Operaciones";


                    sb.Append("NUM_OPE"+ delimitador);
                    sb.Append("FECHA_REGISTRO" + delimitador);
                    sb.Append("MARITIMO" + delimitador);
                    sb.Append("AEREO" + delimitador);
                    sb.Append("TN" + delimitador);
                    sb.Append("TI" + delimitador);
                    sb.Append("SEGURO" + delimitador);
                    sb.Append("AGA" + delimitador);
                    sb.Append("OTROS" + delimitador);
                    sb.Append("CLIENTE" + delimitador);
                    sb.Append("ORIGEN" + delimitador);
                    sb.Append("DESTINO" + delimitador);
                    sb.Append("INCOTERM" + delimitador);
                    sb.Append("UNIDAD" + delimitador);
                    sb.Append("REFERENCIA" + delimitador);
                    sb.Append("COMERCIAL" + delimitador);
                    sb.Append("CUSTOMER" + delimitador);
                    sb.Append("USUARIO_REGISTRO" + delimitador);
                    sb.Append("\r\n");

                    foreach (DataRow data in dt.Tables["Operaciones"].Rows ) {

                        sb.Append(data["NUM_OPE"].ToString() + delimitador);
                        sb.Append(data["FECHA_REGISTRO"].ToString() + delimitador);
                        sb.Append(data["MARITIMO"].ToString() + delimitador);
                        sb.Append(data["AEREO"].ToString() + delimitador);
                        sb.Append(data["TN"].ToString() + delimitador);
                        sb.Append(data["TI"].ToString() + delimitador);
                        sb.Append(data["SEGURO"].ToString() + delimitador);
                        sb.Append(data["AGA"].ToString() + delimitador);
                        sb.Append(data["OTROS"].ToString() + delimitador);
                        sb.Append(data["CLIENTE"].ToString() + delimitador);
                        sb.Append(data["ORIGEN"].ToString() + delimitador);
                        sb.Append(data["DESTINO"].ToString() + delimitador);
                        sb.Append(data["INCOTERM"].ToString() + delimitador);
                        sb.Append(data["UNIDAD"].ToString() + delimitador);
                        sb.Append(data["REFERENCIA"].ToString() + delimitador);
                        sb.Append(data["COMERCIAL"].ToString() + delimitador);
                        sb.Append(data["CUSTOMER"].ToString() + delimitador);
                        sb.Append(data["USUARIO_REGISTRO"].ToString() + delimitador);
                        sb.Append("\r\n");

                    }

                    string VALOR = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string RUTA = @"C:\inetpub\wwwroot\Operaciones\Archivos\Operaciones_" + VALOR + ".csv";
                    StreamWriter sw = new StreamWriter(RUTA, true,Encoding.UTF8);
                    sw.WriteLine(sb.ToString());
                    sw.Close();

                    Response.Clear();
                    Response.ContentType = "text/csv";
                    Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", "Operaciones_" + VALOR + ".csv"));
                    Response.WriteFile(RUTA);
                    Response.End();

                    MENSAJE_CORRECTO.Text = "ARCHIVO OPERACIONES.CSV DESCARGADO CORRECTAMENTE.";
                    CUADRO_CORRECTO.Visible = false;
                    CUADRO_ERROR.Visible = false;
                    File.Delete(RUTA);

                }
            }
            catch (Exception EX)
            {
                this.RPT_OPERACIONES.DataBind();
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }

        }
    }
}