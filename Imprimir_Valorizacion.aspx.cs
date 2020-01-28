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
    public partial class Imprimir_Valorizacion : System.Web.UI.Page
    {
        public int VALOR_SOLICITUD;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                VALOR_SOLICITUD = int.Parse(Request.QueryString["NRO_OP"]);
                TXT_OPERACION.Text = VALOR_SOLICITUD.ToString();
                CARGAR_VALORES_TOTALES();
                CARGAR_VALORIZACION();
                CARGAR_CLIENTE();
            }

      

        }

        public void CARGAR_VALORIZACION()
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("SP_IMPRIMIR_VALORACION_X_OP", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", TXT_OPERACION.Text));

                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                bool VALOR = false;
                if (dr.Read())
                {
                    TXT_FECHA_CREACION.Text = dr["FECHA_CREACION"].ToString();
                    TXT_FECHA_PAGO.Text = dr["FECHA_PAGO"].ToString();
                    TXT_VALOR_USD.Text = dr["VALOR_USD"].ToString();
                    TXT_VALOR_EUR.Text = dr["VALOR_EUR"].ToString();
                    TXT_VALOR_GBP.Text = dr["VALOR_GBP"].ToString();
                    VALOR = Convert.ToBoolean(dr["LIQUIDAR"].ToString());

               
                  
                }
             
                conexion.Close();
            }
        }


        /*-----------------------------------------------------------------*/
        /*                     CARGAR VALORES TOTALES                      */
        /*-----------------------------------------------------------------*/
        public void CARGAR_VALORES_TOTALES()
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("SP_CONSOLIDAR_VALORACION", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", TXT_OPERACION.Text));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    LBL_COSTOS_EXENTOS.Text = dr["COSTO_EXENTO"].ToString();
                    LBL_COSTOS_AFECTAS.Text = dr["COSTO_AFECTO"].ToString();
                    LBL_IVA_COSTOS.Text = dr["IVA_COSTOS"].ToString();
                    LBL_TOTAL_COSTOS.Text = dr["TOTAL_COSTOS"].ToString();
                    LBL_VENTAS_EXENTAS.Text = dr["VENTA_EXENTA"].ToString();
                    LBL_VENTAS_AFECTAS.Text = dr["VENTA_AFECTA"].ToString();
                    LBL_IVA_VENTAS.Text = dr["IVA_VENTAS"].ToString();
                    LBL_TOTAL_VENTAS.Text = dr["TOTAL_VENTAS"].ToString();
                    LBL_PROFIT.Text = dr["PROFIT"].ToString();
                }
                conexion.Close();
            }
        }



        /*-----------------------------------------------------------------*/
        /*                     CARGA DATOS DEL CLIENTE                     */
        /*-----------------------------------------------------------------*/
        public void CARGAR_CLIENTE()
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("[SP_READ_CLIENTE_X_OP]", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID_OPERACIONES", TXT_OPERACION.Text));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    TXT_NOMBRE.Text = dr["NOMBRE"].ToString();
                    TXT_RUT.Text = dr["RUT"].ToString();
                    TXT_DIRECCION.Text = dr["DIRECCION"].ToString();
                    TXT_CIUDAD.Text = dr["CIUDAD"].ToString();
                }
                else
                {
                    TXT_NOMBRE.Text = "";
                    TXT_RUT.Text = "";
                    TXT_DIRECCION.Text = "";
                    TXT_CIUDAD.Text = "";
                }
                conexion.Close();
            }
        }
    }
}