using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;

    public class Helpers
    {

    public String API_KEY      = "6bedaf7ca6d85e14679b860f5c66bca90ffa91c0";
    public String FORMATO_API = "json"; 
    /*-----------------------------------------------------------------*/
    /*                METODO VALIDAR PERMISO USUARIO                   */
    /*-----------------------------------------------------------------*/

    /// <summary>ESTE METODO VALIDA SI TIENE PERMISO DE 1:MODIFICAR ; 2:ELIMINAR : 3:ADMINISTRADOR 
    /// </summary>
    public bool VALIDAR_PERMISO(int ID_USUARIO,int TIPO_PERMISO)
        {
            bool VALOR_DEVUELTO = false;

            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_VALIDA_PERFIL_USUARIO_X_SOLICITUD", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", ID_USUARIO));
                    cmd.Parameters.Add(new SqlParameter("@TIPO_VALIDACION", TIPO_PERMISO));
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
                        VALOR_DEVUELTO = true;
                    }
                    else
                    {
                        VALOR_DEVUELTO = false;
                    }
                }
            }
            catch (Exception)
            {
                VALOR_DEVUELTO = false;
            }
            return VALOR_DEVUELTO;
        }


    /*-----------------------------------------------------------------*/
    /*                CONSULTAR EXISTENCIA TASA DE CAMBIO              */
    /*-----------------------------------------------------------------*/

    /// <summary>ESTE METODO CONSULTA SI HAY TASA DE CAMBIO
    /// </summary>
        public bool VERIFICAR_TASA_CAMBIO() {

                bool VALOR_DEVUELTO = false;

                try
                {
                    using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("SP_READ_TASA_CAMBIO_FECHA", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
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

                        if (VALOR == 0)
                        {
                            VALOR_DEVUELTO = true;
                        }
                        else
                        {
                            VALOR_DEVUELTO = false;
                        }
                    }
                }
                catch (Exception)
                {
                    VALOR_DEVUELTO = true;
                }
                return VALOR_DEVUELTO;
            }

    /*-----------------------------------------------------------------*/
    /*                INSERTAR TASA DE CAMBIO                          */
    /*-----------------------------------------------------------------*/

    /// <summary>ESTE METODO INSERTA LA TASA DE CAMBIO DIARIA
    /// </summary>
        public bool INSERTAR_TASA_CAMBIO(String USD, String EUR) {

                bool VALOR_DEVUELTO = true;

                try
                {
                    using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("SP_CREATE_TASA_CAMBIO", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@USD", USD));
                        cmd.Parameters.Add(new SqlParameter("@EUR", EUR));
                        SqlParameter VALOR_RETORNO = new SqlParameter("@RETURN_VALUE", DbType.Int32);
                        VALOR_RETORNO.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(VALOR_RETORNO);
                        conexion.Open();
                        cmd.ExecuteNonQuery();
                        int VALOR = Int32.Parse(cmd.Parameters["@RETURN_VALUE"].Value.ToString());
                        conexion.Close();
                        VALOR_DEVUELTO = true;
                    }
                }
                catch (Exception)
                {
                    VALOR_DEVUELTO = true;
                }
                return VALOR_DEVUELTO;
            }




    /*-----------------------------------------------------------------*/
    /*                CARGA NOMBRE DE USUARIO                          */
    /*-----------------------------------------------------------------*/
    /// <summary>ESTE METODO CONSULTA EL NOMBRE DEL USUARIO LOGUEADO
    /// </summary>
    public string  CONSULTAR_NOMBRE_USUARIO(int ID_USUARIO)
        {
            string NOMBRE = "";

            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_READ_USUARIO_X_SOLICITUD", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", ID_USUARIO));
                    conexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        NOMBRE = dr["NOMBRE"].ToString();

                    }
                    conexion.Close();
                }
            }
            catch (Exception)
            {
                NOMBRE = "SIN INFO";
            }
            return NOMBRE;
        }

   

    /// <summary>ESTE METODO DEVUELVE LA TASA DE CAMBIO DEL EUR DEL API REST SBIF
    /// </summary>
    public String OBTENER_EUR()
    {
        String VALOR_RETORNADO = "";
        String Valor_buscado = "euro"; 
        Entidades.VALOR_EURO valor_euro = new Entidades.VALOR_EURO();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://api.sbif.cl/api-sbifv3/recursos_api/"+ Valor_buscado + "?apikey="+ API_KEY + "&formato="+ FORMATO_API + "");
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            var json = reader.ReadToEnd();
            valor_euro = JsonConvert.DeserializeObject<Entidades.VALOR_EURO>(json);
        }

        foreach (var e in valor_euro.Euros)
        {
            VALOR_RETORNADO = e.Valor;
        }
  
        return VALOR_RETORNADO;
    }

    /// <summary>ESTE METODO DEVUELVE LA TASA DE CAMBIO DEL EUR DEL API REST SBIF
    /// </summary>
    public String OBTENER_USD()
    {
        String VALOR_RETORNADO = "";
        String Valor_buscado = "dolar";
        Entidades.VALOR_DOLAR valor_dolar = new Entidades.VALOR_DOLAR();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://api.sbif.cl/api-sbifv3/recursos_api/"+ Valor_buscado + "?apikey=" + API_KEY + "&formato=" + FORMATO_API + "");
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            var json = reader.ReadToEnd();
            valor_dolar = JsonConvert.DeserializeObject<Entidades.VALOR_DOLAR>(json);
        }

        foreach (var e in valor_dolar.Dolares)
        {
            VALOR_RETORNADO = e.Valor;
        }

        return VALOR_RETORNADO;
    }

    public class TASA_CAMBIO
    {
        public Entidades.DOLAR dolar { get; set; }
        public Entidades.EURO euro { get; set; }

    }


    /// <summary>ESTE METODO TUPLA DEVUELVE LA TASA DE CAMBIO DEL EUR DEL API REST mindicador.cl
    /// </summary>
    public (string, string) TASA_CAMBIO_MINDI ()
    {
        TASA_CAMBIO tasa_cambio = new TASA_CAMBIO();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://www.mindicador.cl/api");
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            var json = reader.ReadToEnd();
            tasa_cambio = JsonConvert.DeserializeObject<Helpers.TASA_CAMBIO>(json);
        }
        var USD = tasa_cambio.dolar.valor.ToString();
        var EUR = tasa_cambio.euro.valor.ToString();
        return (USD, EUR);
    }


    /// <summary>ESTE METODO DEVUELVE UN NUMERO CONVERTIDO A LETRAS
    /// </summary>
    public string CONVER_NUM_LETRAS(int num)
    {
        string res, dec = "";
        Int64 entero;
        int decimales;
        double nro;

        try{
            nro = Convert.ToDouble(num);
        }catch{
            return "";
        }

        entero = Convert.ToInt64(Math.Truncate(nro));
        decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
        if (decimales > 0)
        {
            dec = " CON " + decimales.ToString() + "/100";
        }

        res = toText(Convert.ToDouble(entero)) + dec;
        return res;
    }


    private string toText(double value)
    {
        string Num2Text = "";
        value = Math.Truncate(value);
        if (value == 0) Num2Text = "CERO";
        else if (value == 1) Num2Text = "UNO";
        else if (value == 2) Num2Text = "DOS";
        else if (value == 3) Num2Text = "TRES";
        else if (value == 4) Num2Text = "CUATRO";
        else if (value == 5) Num2Text = "CINCO";
        else if (value == 6) Num2Text = "SEIS";
        else if (value == 7) Num2Text = "SIETE";
        else if (value == 8) Num2Text = "OCHO";
        else if (value == 9) Num2Text = "NUEVE";
        else if (value == 10) Num2Text = "DIEZ";
        else if (value == 11) Num2Text = "ONCE";
        else if (value == 12) Num2Text = "DOCE";
        else if (value == 13) Num2Text = "TRECE";
        else if (value == 14) Num2Text = "CATORCE";
        else if (value == 15) Num2Text = "QUINCE";
        else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
        else if (value == 20) Num2Text = "VEINTE";
        else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
        else if (value == 30) Num2Text = "TREINTA";
        else if (value == 40) Num2Text = "CUARENTA";
        else if (value == 50) Num2Text = "CINCUENTA";
        else if (value == 60) Num2Text = "SESENTA";
        else if (value == 70) Num2Text = "SETENTA";
        else if (value == 80) Num2Text = "OCHENTA";
        else if (value == 90) Num2Text = "NOVENTA";
        else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
        else if (value == 100) Num2Text = "CIEN";
        else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
        else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
        else if (value == 500) Num2Text = "QUINIENTOS";
        else if (value == 700) Num2Text = "SETECIENTOS";
        else if (value == 900) Num2Text = "NOVECIENTOS";
        else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
        else if (value == 1000) Num2Text = "MIL";
        else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
        else if (value < 1000000)
        {
            Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
            if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
        }

        else if (value == 1000000) Num2Text = "UN MILLON";
        else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
        else if (value < 1000000000000)
        {
            Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
            if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
        }

        else if (value == 1000000000000) Num2Text = "UN BILLON";
        else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

        else
        {
            Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
            if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
        }
        return Num2Text;

    }






}
