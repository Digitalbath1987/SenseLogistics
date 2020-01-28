//------------------------------------------------------------------------------
// <generado automáticamente>
//     Este código fue generado por una herramienta.
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código. 
// </generado automáticamente>
//------------------------------------------------------------------------------

namespace Operaciones {
    
    
    public partial class Crear_Factura {
        
        /// <summary>
        /// Control UpdatePanel1.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.UpdatePanel UpdatePanel1;
        
        /// <summary>
        /// Control DDL_OPERACION.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList DDL_OPERACION;
        
        /// <summary>
        /// Control OPERACIONES_FACTURA_PENDIENTES.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.SqlDataSource OPERACIONES_FACTURA_PENDIENTES;
        
        /// <summary>
        /// Control BTN_CARGAR_FACTURAS.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlButton BTN_CARGAR_FACTURAS;
        
        /// <summary>
        /// Control RPT_FACTURAS.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Repeater RPT_FACTURAS;
        
        /// <summary>
        /// Control SP_READ_FACTURAS_X_OP.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.SqlDataSource SP_READ_FACTURAS_X_OP;
        
        /// <summary>
        /// Control DIV_FORMULARIO_FACTURA.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl DIV_FORMULARIO_FACTURA;
        
        /// <summary>
        /// Control LBL_GRUPO_FACTURA.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Label LBL_GRUPO_FACTURA;
        
        /// <summary>
        /// Control LBL_ID_FACTURA.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Label LBL_ID_FACTURA;
        
        /// <summary>
        /// Control TXT_NOMBRE_CLIENTE.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TXT_NOMBRE_CLIENTE;
        
        /// <summary>
        /// Control TXT_RUT_CLIENTE.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TXT_RUT_CLIENTE;
        
        /// <summary>
        /// Control TXT_TELEFONO_CLIENTE.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TXT_TELEFONO_CLIENTE;
        
        /// <summary>
        /// Control TXT_GIRO_CLIENTE.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TXT_GIRO_CLIENTE;
        
        /// <summary>
        /// Control TXT_REF_CLIENTE.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TXT_REF_CLIENTE;
        
        /// <summary>
        /// Control TXT_DOC_TRANSPORTE.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TXT_DOC_TRANSPORTE;
        
        /// <summary>
        /// Control TXT_DIRECCION_CLIENTE.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TXT_DIRECCION_CLIENTE;
        
        /// <summary>
        /// Control TXT_COMUNA_CLIENTE.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TXT_COMUNA_CLIENTE;
        
        /// <summary>
        /// Control TXT_CIUDAD_CLIENTE.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TXT_CIUDAD_CLIENTE;
        
        /// <summary>
        /// Control TXT_FECHA_EMISION.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TXT_FECHA_EMISION;
        
        /// <summary>
        /// Control TXT_FECHA_VECIMIENTO.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TXT_FECHA_VECIMIENTO;
        
        /// <summary>
        /// Control TXT_VENDEDOR.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TXT_VENDEDOR;
        
        /// <summary>
        /// Control DDL_CONDICION_PAGO.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList DDL_CONDICION_PAGO;
        
        /// <summary>
        /// Control SP_READ_CONDICION_PAGO.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.SqlDataSource SP_READ_CONDICION_PAGO;
        
        /// <summary>
        /// Control RPT_CONCEPTOS.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Repeater RPT_CONCEPTOS;
        
        /// <summary>
        /// Control SP_READ_CONCEPTOS_FACTURAR_X_ID_FACTURA.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.SqlDataSource SP_READ_CONCEPTOS_FACTURAR_X_ID_FACTURA;
        
        /// <summary>
        /// Control TXT_OBSERVACION_FACTURA.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TXT_OBSERVACION_FACTURA;
        
        /// <summary>
        /// Control LBL_NETO.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Label LBL_NETO;
        
        /// <summary>
        /// Control LBL_IVA.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Label LBL_IVA;
        
        /// <summary>
        /// Control LBL_EXENTO.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Label LBL_EXENTO;
        
        /// <summary>
        /// Control LBL_TOTAL.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Label LBL_TOTAL;
        
        /// <summary>
        /// Control CUADRO_ERROR.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl CUADRO_ERROR;
        
        /// <summary>
        /// Control MENSAJE_ERROR.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Label MENSAJE_ERROR;
        
        /// <summary>
        /// Control CUADRO_CORRECTO.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl CUADRO_CORRECTO;
        
        /// <summary>
        /// Control MENSAJE_CORRECTO.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Label MENSAJE_CORRECTO;
        
        /// <summary>
        /// Control ACCIONES.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl ACCIONES;
        
        /// <summary>
        /// Control BTN_VISUALIZAR.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Button BTN_VISUALIZAR;
        
        /// <summary>
        /// Control BTN_FACTURAR.
        /// </summary>
        /// <remarks>
        /// Campo generado automáticamente.
        /// Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        /// </remarks>
        protected global::System.Web.UI.WebControls.Button BTN_FACTURAR;
    }
}
