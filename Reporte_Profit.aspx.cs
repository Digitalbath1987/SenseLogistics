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
    public partial class Reporte_Profit : System.Web.UI.Page
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
                    SqlDataAdapter da = new SqlDataAdapter("SP_REPORTE_PROFIT_X_FECHA", conexion);
                    DataTable dt = new DataTable();
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FECHA_DESDE", DateTime.Parse(TXT_FECHA_DESDE.Text));
                    da.SelectCommand.Parameters.AddWithValue("@FECHA_HASTA", DateTime.Parse(TXT_FECHA_HASTA.Text));
                    da.SelectCommand.Parameters.AddWithValue("@ID_CLIENTE", DDL_CLIENTE.SelectedValue.ToString());
                    da.Fill(dt);
                    this.RPT_REPORTE.DataSource = dt;
                    this.RPT_REPORTE.DataBind();
                    CUADRO_CORRECTO.Visible = false;
                    CUADRO_ERROR.Visible = false;
                    BTN_DESCARGAR_CSV.Visible = true;
                    CALCULAR_MONTOS();
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
                    SqlDataAdapter da = new SqlDataAdapter("SP_REPORTE_PROFIT_X_FECHA", conexion);
                    DataSet dt = new DataSet();
                    string delimitador = ";";
                    StringBuilder sb = new StringBuilder();
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FECHA_DESDE", DateTime.Parse(TXT_FECHA_DESDE.Text));
                    da.SelectCommand.Parameters.AddWithValue("@FECHA_HASTA", DateTime.Parse(TXT_FECHA_HASTA.Text));
                    da.SelectCommand.Parameters.AddWithValue("@ID_CLIENTE", DDL_CLIENTE.SelectedValue.ToString());
                    da.Fill(dt);

                    dt.Tables[0].TableName = "REPORTES";


                    sb.Append("NRO_OPERACION" + delimitador);
                    sb.Append("AEREO" + delimitador);
                    sb.Append("MARITIMO" + delimitador);
                    sb.Append("T_INTERNACIONAL" + delimitador);
                    sb.Append("T_NACIONAL" + delimitador);
                    sb.Append("AGA" + delimitador);
                    sb.Append("SEGURO" + delimitador);
                    sb.Append("FECHA_OPERACION" + delimitador);
                    sb.Append("FECHA_CREACION" + delimitador);
                    sb.Append("FECHA_PAGO" + delimitador);
                    sb.Append("VALOR_USD" + delimitador);
                    sb.Append("VALOR_EUR" + delimitador);
                    sb.Append("VALOR_GBP" + delimitador);
                    sb.Append("CLIENTE" + delimitador);
                    sb.Append("RUT" + delimitador);
                    sb.Append("VENTAS_EXENTAS" + delimitador);
                    sb.Append("VENTAS_AFECTAS" + delimitador);
                    sb.Append("COSTOS_EXENTOS" + delimitador);
                    sb.Append("COSTOS_AFECTOS" + delimitador);
                    sb.Append("PROFIT" + delimitador);
                    sb.Append("\r\n");

                    foreach (DataRow data in dt.Tables["REPORTES"].Rows)
                    {
                        sb.Append(data["NRO_OPERACION"].ToString() + delimitador);
                        sb.Append(data["AEREO"].ToString() + delimitador);
                        sb.Append(data["MARITIMO"].ToString() + delimitador);
                        sb.Append(data["T_INTERNACIONAL"].ToString() + delimitador);
                        sb.Append(data["T_NACIONAL"].ToString() + delimitador);
                        sb.Append(data["AGA"].ToString() + delimitador);
                        sb.Append(data["SEGURO"].ToString() + delimitador);
                        sb.Append(data["FECHA_OPERACION"].ToString() + delimitador);
                        sb.Append(data["FECHA_CREACION"].ToString() + delimitador);
                        sb.Append(data["FECHA_PAGO"].ToString() + delimitador);
                        sb.Append(data["VALOR_USD"].ToString() + delimitador);
                        sb.Append(data["VALOR_EUR"].ToString() + delimitador);
                        sb.Append(data["VALOR_GBP"].ToString() + delimitador);
                        sb.Append(data["CLIENTE"].ToString() + delimitador);
                        sb.Append(data["RUT"].ToString() + delimitador);
                        sb.Append(data["VENTAS_EXENTAS"].ToString() + delimitador);
                        sb.Append(data["VENTAS_AFECTAS"].ToString() + delimitador);
                        sb.Append(data["COSTOS_EXENTOS"].ToString() + delimitador);
                        sb.Append(data["COSTOS_AFECTOS"].ToString() + delimitador);
                        sb.Append(data["PROFIT"].ToString() + delimitador);
                        sb.Append("\r\n");
                    }

                    CALCULAR_MONTOS();
                    RPT_REPORTE.DataBind();

                    string VALOR = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string RUTA = @"C:\inetpub\wwwroot\Operaciones\Archivos\reportes_" + VALOR + ".csv";
                   // string RUTA = @"C:\Users\digital\Desktop\version 3.0 (actualizada\SenseLogistics-master\Operaciones\Archivos\Reportes_" + VALOR + ".csv";
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
               
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
                CALCULAR_MONTOS();
                RPT_REPORTE.DataBind();
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

        protected void CALCULAR_MONTOS()
        {

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("SP_REPORTE_PROFIT_X_FECHA_MONTOS_TOTALES", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FECHA_DESDE", DateTime.Parse(TXT_FECHA_DESDE.Text)));
                cmd.Parameters.Add(new SqlParameter("@FECHA_HASTA", DateTime.Parse(TXT_FECHA_HASTA.Text)));
                cmd.Parameters.Add(new SqlParameter("@ID_CLIENTE", DDL_CLIENTE.SelectedValue.ToString()));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    TXT_MONTO_VENTAS_AFECTAS.Text = dr["VENTAS_AFECTA"].ToString();
                    TXT_MONTO_VENTAS_EXENTAS.Text = dr["VENTAS_EXENTAS"].ToString();
                    TXT_MONTO_COSTOS_EXENTOS.Text = dr["COSTOS_EXENTOS"].ToString();
                    TXT_MONTO_COSTOS_AFECTOS.Text = dr["COSTOS_AFECTOS"].ToString();
                    TXT_MONTO_PROFIT_TOTAL.Text   = dr["PROFIT"].ToString();

                }
                conexion.Close();
            }


        }




    }
}