using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Operaciones
{
    public partial class Listado_Facturas : System.Web.UI.Page
    {

        public int TIPO_FACTURA = 0;


        string RUTA_WORD = @"C:\inetpub\wwwroot\Operaciones\Formatos\Factura_Electronica.docx";
        string RUTA_DESTINO = @"C:\inetpub\wwwroot\Operaciones\Archivos\";
        //string RUTA_WORD = @"C:\Users\Digital\Desktop\Sense_Logisticst_V4.0\SenseLogistics-master\Operaciones\Formatos\Factura_Electronica.docx";
        //string RUTA_DESTINO = @"C:\Users\Digital\Desktop\Sense_Logisticst_V4.0\SenseLogistics-master\Operaciones\Archivos\";


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RPT_FACTURAS_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                case "DESCARGAR":




                    /********************************************************************/
                    /**             ELIMINAR LOS ARCHIVOS DE LA CARPETA                **/
                    /********************************************************************/
                    List<string> strFiles = Directory.GetFiles(RUTA_DESTINO, "*", SearchOption.AllDirectories).ToList();
                    foreach (string fichero in strFiles)
                    {
                        File.Delete(fichero);
                    }


                    /********************************************************************/
                    /**       COPIAR EL ARCHIVO ORIGINAL Y LO PEGA EN EL DESTINO       **/
                    /********************************************************************/
                    string NOMBRE_ARCHIVO = num;
                    File.Copy(RUTA_WORD, Path.Combine(RUTA_DESTINO, NOMBRE_ARCHIVO + ".docx"), true);


                    /********************************************************************/
                    /**               ABRE EL WORD COPIADO EN AL RUTA DESTINO          **/
                    /********************************************************************/

                    String RUTA_NUEVO_ARCHIVO = RUTA_DESTINO + NOMBRE_ARCHIVO + ".docx";

                    using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(RUTA_NUEVO_ARCHIVO, true))
                    {

                        /********************************************************************/
                        /**   MODIFICAR PARAMETROS DEL WORD FOLIO Y DATOS DEL CLIENTE      **/
                        /********************************************************************/

                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                        {
                            SqlCommand cmd = new SqlCommand("SP_READ_DATOS_FACTURACION_PDF", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@ID_FACTURA", num));
                            conexion.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                BUSCAR_REMPLAZAR(wordDoc, "F_NUMER", "PENDIENTE");       // dr["FOLIO"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "NUOPCLI", dr["NRO_OPERACION"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "SRCLINOM", dr["NOMBRE"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "RUCLIDET", dr["RUT"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "TECLIDET", dr["TELEFONO"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "GICLIDET", dr["GIRO"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "CICLIDET", dr["CIUDAD"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "COCLIDET", dr["COMUNA"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "DETALLE_DIRECCION", dr["DIRECCION"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "EMISION_FEC", dr["FEC_EMI"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "VEN_FECH", dr["FEC_VENC"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "DICLIDET", dr["DIRECCION"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "VEN_NOMBRE", dr["VENDEDOR"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "OBDECLIDA", dr["OBSERVACION"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "TRADOCCLI", dr["DOC_TRANSPORTE"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "CLIREF", dr["REF_CLIENTE"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "CONPAGCLI", dr["CON_PAGO"].ToString());
                            }
                            conexion.Close();
                        }

                        /********************************************************************/
                        /**          MODIFICAR PARAMETROS DEL WORD VALORES DE FACTURA      **/
                        /********************************************************************/

                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                        {
                            SqlCommand cmd = new SqlCommand("SP_READ_DATOS_FACTURACION_MONTOS_PDF", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@ID_FACTURA", num));
                            conexion.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                BUSCAR_REMPLAZAR(wordDoc, "MONEDECLI", dr["NETO"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "MOIVDECLI", dr["IVA"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "MOEXDECLI", dr["EXENTO"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "MOTODECLI", dr["TOTAL"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "MOSUTOCLI", dr["NETO"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "SOMOTOLET", dr["NETO_LETRAS"].ToString());

                            }
                            conexion.Close();
                        }

                        /********************************************************************/
                        /**          MODIFICAR PARAMETROS DEL WORD CONCEPTOS               **/
                        /********************************************************************/

                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                        {
                            int CONTADOR = 1;
                            SqlDataAdapter da = new SqlDataAdapter("SP_READ_CONCEPTOS_FACTURAR_X_ID_FACTURA", conexion);
                            DataSet dt = new DataSet();

                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand.Parameters.AddWithValue("@ID_FACTURA", num);
                            da.Fill(dt);

                            dt.Tables[0].TableName = "REPORTES";

                            foreach (DataRow data in dt.Tables["REPORTES"].Rows)
                            {

                                BUSCAR_REMPLAZAR(wordDoc, "DENUCLIDE" + CONTADOR, data["DESCRIPCION"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "PREUNDE" + CONTADOR, data["PRECIO_UNITARIO"].ToString());
                                BUSCAR_REMPLAZAR(wordDoc, "TODENU" + CONTADOR, data["SALDO_TOTAL_PENDIENTE"].ToString());
                                CONTADOR++;
                            }

                            for (int i = CONTADOR; i < 10; i++)
                            {
                                BUSCAR_REMPLAZAR(wordDoc, "DENUCLIDE" + i, " ");
                                BUSCAR_REMPLAZAR(wordDoc, "PREUNDE" + i, " ");
                                BUSCAR_REMPLAZAR(wordDoc, "TODENU" + i, " ");
                            }
                        }
                    }

                    /********************************************************************/
                    /**         DESCARGAR EL ARCHIVO EN EL NAVEGADOR                   **/
                    /********************************************************************/
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", NOMBRE_ARCHIVO + ".docx"));
                    Response.WriteFile(Path.Combine(RUTA_DESTINO, NOMBRE_ARCHIVO + ".docx"));
                    Response.End();

                    break;
            }
        }

        /// <summary>
        /// REEMPLAZA LOS CAMPOS EN EL DOCUMENTO WORD
        /// </summary>
        public void BUSCAR_REMPLAZAR(WordprocessingDocument wordDoc, string BUSCAR, string REEMPLAZAR)
        {
            try
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }
                Regex regexText = new Regex(BUSCAR);
                docText = regexText.Replace(docText, REEMPLAZAR);
                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                docText = null;
                regexText = null;
            }
            catch (Exception EX)
            {
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }
        }
    }
}