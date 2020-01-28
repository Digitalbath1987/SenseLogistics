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
    public partial class Reporte_Cartola_Comisiones_Comercial : System.Web.UI.Page
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

        protected void BTN_DESCARGAR_CSV_ServerClick(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SP_READ_CARTOLA_COMISIONES_X_COMERCIAL", conexion);
                    DataSet dt = new DataSet();
                    string delimitador = ";";
                    StringBuilder sb = new StringBuilder();
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ID_COMERCIAL", DDL_COMERCIAL.SelectedValue.ToString());
                    da.Fill(dt);

                    dt.Tables[0].TableName = "REPORTES";
                    sb.Append("OPERACION" + delimitador);
                    sb.Append("FECHA" + delimitador);
                    sb.Append("DETALLE" + delimitador);
                    sb.Append("MONTO" + delimitador);
                    sb.Append("TIPO MOVIMIENTO" + delimitador);
                    sb.Append("\r\n");

                    foreach (DataRow data in dt.Tables["REPORTES"].Rows)
                    {
                        sb.Append(data["OPERACION"].ToString() + delimitador);
                        sb.Append(data["FECHA"].ToString() + delimitador);
                        sb.Append(data["DETALLE"].ToString() + delimitador);
                        sb.Append(data["MONTO"].ToString() + delimitador);
                        sb.Append(data["TIPO"].ToString() + delimitador);
                        sb.Append("\r\n");
                    }

                    string VALOR = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string RUTA = @"C:\inetpub\wwwroot\Operaciones\Archivos\reportes_" + VALOR + ".csv";
                   // string RUTA = @"C:\Users\Digital\Desktop\version 3.0 (actualizada\SenseLogistics-master\Operaciones\Archivos\Reportes_" + VALOR + ".csv";
                    StreamWriter sw = new StreamWriter(RUTA, true, Encoding.UTF8);
                    sw.WriteLine(sb.ToString());
                    sw.Close();

                    Response.Clear();
                    Response.ContentType = "text/csv";
                    Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", "Reportes_" + VALOR + ".csv"));
                    Response.WriteFile(RUTA);
                    Response.End();

                    MENSAJE_CORRECTO.Text = "ARCHIVO REPORTES.CSV DESCARGADO CORRECTAMENTE.";
                    CUADRO_CORRECTO.Visible = false;
                    CUADRO_ERROR.Visible = false;
                    File.Delete(RUTA);
                }
            }
            catch (Exception EX)
            {
                this.RPT_REPORTE.DataBind();
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }
        }

        protected void BTN_FILTRAR_CC_ServerClick(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SP_READ_CARTOLA_COMISIONES_X_COMERCIAL", conexion);
                    DataTable dt = new DataTable();
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ID_COMERCIAL", DDL_COMERCIAL.SelectedValue.ToString());
                    da.Fill(dt);
                    this.RPT_REPORTE.DataSource = dt;
                    this.RPT_REPORTE.DataBind();
                    CUADRO_CORRECTO.Visible = false;
                    CUADRO_ERROR.Visible = false;
                    BTN_DESCARGAR_CSV.Visible = true;
                }
            }
            catch (Exception EX)
            {
                this.RPT_REPORTE.DataBind();
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }
        }

        protected void RPT_REPORTE_ItemCommand(object source, RepeaterCommandEventArgs e)
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
    }
}