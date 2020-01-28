using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using static Entidades;

namespace Operaciones
{
    public partial class Home : System.Web.UI.Page
    {


        protected string datos_chart { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        


       //[WebMethod]
        public static string CARGAR_CHART()
        {
            string json;
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SP_READ_VALORACION_PENDIENTES_CHART", conexion)) { 
                DataSet dt = new DataSet();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                    dt.Tables[0].TableName = "CHART";
                    List<CAMPOS_CHART> datos = new List<CAMPOS_CHART>();
                    foreach (DataRow data in dt.Tables["CHART"].Rows)
                    {
                        datos.Add(new CAMPOS_CHART() {label = data["ESTADO"].ToString() 
                                                     ,y     = int.Parse(data["CANTIDAD"].ToString())
                        });
                    }
                    json = JsonConvert.SerializeObject(datos);
                }
            }
         return json;
        }


        //[WebMethod]
        public static string CARGAR_CHART_OPERACIONES_X_MES()
        {
            string json;
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SP_READ_OPERACIONES_X_MES_CHART", conexion))
                {
                    DataSet dt = new DataSet();
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                    dt.Tables[0].TableName = "CHART";
                    List<CAMPOS_CHART> datos = new List<CAMPOS_CHART>();
                    foreach (DataRow data in dt.Tables["CHART"].Rows)
                    {
                        datos.Add(new CAMPOS_CHART()
                        {
                            label = data["FECHA_OPERACION"].ToString()
                                                     ,
                            y = int.Parse(data["OPERACIONES"].ToString())
                        });
                    }
                    json = JsonConvert.SerializeObject(datos);
                }
            }
            return json;
        }


    }
}