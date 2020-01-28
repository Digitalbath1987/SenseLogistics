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
    public partial class Reporte_Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BTN_FILTRAR_FECHAS_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SP_REPORTE_VENTAS_X_FECHA", conexion);
                    DataTable dt = new DataTable();
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FECHA_DESDE", TXT_FECHA_DESDE.Text);
                    da.SelectCommand.Parameters.AddWithValue("@FECHA_HASTA", TXTFECHA_HASTA.Text);
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

        protected void BTN_DESCARGAR_CSV_ServerClick(object sender, EventArgs e)
        {



            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SP_REPORTE_COSTOS_X_FECHA", conexion);
                    DataSet dt = new DataSet();
                    string delimitador = ";";
                    StringBuilder sb = new StringBuilder();
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FECHA_DESDE", TXT_FECHA_DESDE.Text);
                    da.SelectCommand.Parameters.AddWithValue("@FECHA_HASTA", TXTFECHA_HASTA.Text);
                    da.Fill(dt);

                    dt.Tables[0].TableName = "REPORTES";

                    sb.Append("NRO OPERACION" + delimitador);
                    sb.Append("AEREO" + delimitador);
                    sb.Append("MARITIMO" + delimitador);
                    sb.Append("T. INTERNACIONAL" + delimitador);
                    sb.Append("T. NACIONAL" + delimitador);
                    sb.Append("A.G.A." + delimitador);
                    sb.Append("SEGURO" + delimitador);
                    sb.Append("FECHA OPERACION" + delimitador);
                    sb.Append("CLIENTE" + delimitador);
                    sb.Append("RUT" + delimitador);
                    sb.Append("CONCEPTO" + delimitador);
                    sb.Append("TIPO CONCEPTO" + delimitador);
                    sb.Append("MONTO(CLP)" + delimitador);
                    sb.Append("\r\n");

                    foreach (DataRow data in dt.Tables["REPORTES"].Rows)
                    {
                        sb.Append(data["ID_OPERACIONES"].ToString() + delimitador);
                        sb.Append(data["AEREO"].ToString() + delimitador);
                        sb.Append(data["MARITIMO"].ToString() + delimitador);
                        sb.Append(data["T_INTERNACIONAL"].ToString() + delimitador);
                        sb.Append(data["T_NACIONAL"].ToString() + delimitador);
                        sb.Append(data["AGA"].ToString() + delimitador);
                        sb.Append(data["SEGURO"].ToString() + delimitador);
                        sb.Append(data["FECHA_OPERACION"].ToString() + delimitador);
                        sb.Append(data["CLIENTE"].ToString() + delimitador);
                        sb.Append(data["RUT"].ToString() + delimitador);
                        sb.Append(data["CONCEPTO"].ToString() + delimitador);
                        sb.Append(data["TIP_CONCEPTO"].ToString() + delimitador);
                        sb.Append(data["MONTO_CLP"].ToString() + delimitador);
                        sb.Append("\r\n");
                    }

                    string VALOR = DateTime.Now.ToString("yyyyMMddHHmmss");
                   string RUTA = @"C:\inetpub\wwwroot\Operaciones\Archivos\reportes_" + VALOR + ".csv";
                //    string RUTA = @"C:\Users\digital\Desktop\SenseLogistics-master\Operaciones\Archivos\Reportes_" + VALOR + ".csv";
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