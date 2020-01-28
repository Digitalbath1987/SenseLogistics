using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Entidades
{

    [Serializable]
    public class DETALLE_BL
    {

        public int ID { get; set; }
        public string MARKS_NUMBERS { get; set; }
        public string N_PKGS { get; set; }
        public string DESCRIPTION_PKGS { get; set; }
        public string GROSS_WIGHT { get; set; }
        public string MEASUREMENT { get; set; }
    }




    public class EUR
    {
        public string Valor { get; set; }
        public string Fecha { get; set; }
    }

    public class VALOR_EURO
    {
        public List<EUR> Euros { get; set; }
    }
          
    public class USD
    {
        public string Valor { get; set; }
        public string Fecha { get; set; }
    }


    public class VALOR_DOLAR
    {
        public List<USD> Dolares { get; set; }
    }

    public class DOLAR
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string unidad_medida { get; set; }
        public DateTime fecha { get; set; }
        public double valor { get; set; }
    }


    public class EURO
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string unidad_medida { get; set; }
        public DateTime fecha { get; set; }
        public double valor { get; set; }
    }

    public class CAMPOS_CHART
    {
        public string label { get; set; }
        public int y { get; set; }
    }


    public class CAMPOS_DDL
    {
        public int ID { get; set; }
        public String NOMBRE { get; set; }
    }


}
