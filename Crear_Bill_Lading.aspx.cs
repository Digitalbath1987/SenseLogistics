using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;



namespace Operaciones
{
    public partial class Crear_Bill_Lading : System.Web.UI.Page{

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CARGAR_DROPDOWNLIST();
                //CARGAR_CONCEPTOS();
            }
        }
               
        protected void BT_REGISTRAR_BL_Click(object sender, EventArgs e) {
            try {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())) {
                    SqlCommand cmd = new SqlCommand("SP_CREATE_BILL_LADING", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@N_OPERACION", TXT_OPERACION.SelectedValue.ToString()));           //TXT_OPERACION.Text.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@N_BOOKING", TXT_BOOKING.Text.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@N_BL", TXT_BL.Text.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@VESSEL", TXT_VESSEL.Text.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@PORT_LOADING", TXT_PORT_LOADING.Text.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@PORT_DISCHARGE", TXT_PORT_DISCHARGE.Text.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@PLACE_DELIVERY", TXT_PLACE_DELIVERY.Text.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@POINT_COUNTRY", TXT_POINT_COUNTRY.Text.ToUpper()));                
                    cmd.Parameters.Add(new SqlParameter("@EXPORT_REFERENCES", ""));
                    cmd.Parameters.Add(new SqlParameter("@ID_AGENTE_SHIPPER", LBL_SHIPPER.Text));
                    cmd.Parameters.Add(new SqlParameter("@ID_AGENTE_CONSIGNEE", LBL_CONSIGNEE.Text));
                    cmd.Parameters.Add(new SqlParameter("@ID_AGENTE_NOTIFY", LBL_NOTIFY.Text));
                    cmd.Parameters.Add(new SqlParameter("@ID_AGENTE_DELIVERY", LBL_DELIVERY.Text));
                    cmd.Parameters.Add(new SqlParameter("@BOAR_DATE", TXT_BOAR_DATE.Text));
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
                    if (VALOR > 0) {
                        CUADRO_ERROR.Visible = false;
                        CUADRO_CORRECTO.Visible = true;
                        MENSAJE_CORRECTO.Text = "B/L :   " + TXT_BL.Text.ToUpper() + ",FUE CREADO CORRECTAMENTE";
                        BT_REGISTRAR_BL.Visible = false;
                        LNK_VER_BL.Visible = true;
                    } else {
                        BT_REGISTRAR_BL.Visible = true;
                        CUADRO_CORRECTO.Visible = false;
                        LNK_VER_BL.Visible = false;
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = "ERROR AL REGISTRAR DATOS , REVISAR SP_CREATE_BILL_LADING DEVOLVIO RETURN 0";
                    }
                }
            } catch (Exception EX) {
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }
        }

        protected void BTN_CARGAR_AGENTE_Click(object sender, EventArgs e)
        {
            try{
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                    SqlCommand cmd = new SqlCommand("SP_READ_AGENTE_X_ID", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_AGENTE", DDL_AGENTES.SelectedValue.ToString()));
                    conexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read()){
                        if (LBL_TIPO_AGENTE.Text == "SHIPPER"){
                            /*--------------------------------------------------*/
                            /*---                SHIPPER                     ---*/
                            /*--------------------------------------------------*/
                            LBL_NOMBRE_SHIPPER.Text = dr["NOMBRE"].ToString();
                            LBL_DIRECCION_SHIPPER.Text = dr["DIRECCION"].ToString();
                            LBL_SHIPPER_CIUDAD.Text = dr["CIUDAD"].ToString();
                            LBL_SHIPPER_PAIS.Text = dr["PAIS"].ToString();
                            LBL_COD_POSTAL_SHIPPER.Text = dr["COD_POSTAL"].ToString();
                            LBL_SHIPPER.Text = dr["ID_AGENTE"].ToString();
                        }else if (LBL_TIPO_AGENTE.Text == "CONSIGNEE"){
                            /*--------------------------------------------------*/
                            /*---                CONSIGNEE                  ---*/
                            /*--------------------------------------------------*/
                            LBL_NOMBRE_CONSIGNEE.Text = dr["NOMBRE"].ToString();
                            LBL_DIRECCION_CONSIGNEE.Text = dr["DIRECCION"].ToString();
                            LBL_CIUDAD_CONSIGNEE.Text = dr["CIUDAD"].ToString();
                            LBL_PAIS_CONSIGNEE.Text = dr["PAIS"].ToString();
                            LBL_COD_POSTAL_CONSIGNEE.Text = dr["COD_POSTAL"].ToString();
                            LBL_CONSIGNEE.Text = dr["ID_AGENTE"].ToString();
                        }else if (LBL_TIPO_AGENTE.Text == "NOTIFY PARTY"){
                            /*--------------------------------------------------*/
                            /*---                NOTIFY PARTY                ---*/
                            /*--------------------------------------------------*/
                            LBL_NOMBRE_NOTIFY.Text = dr["NOMBRE"].ToString();
                            LBL_DIRECCION_NOTIFY.Text = dr["DIRECCION"].ToString();
                            LBL_CIUDAD_NOTIFY.Text = dr["CIUDAD"].ToString();
                            LBL_PAIS_NOTIFY.Text = dr["PAIS"].ToString();
                            LBL_COD_POSTAL_NOTIFY.Text = dr["COD_POSTAL"].ToString();
                            LBL_NOTIFY.Text = dr["ID_AGENTE"].ToString();
                        }else if (LBL_TIPO_AGENTE.Text == "FOR DELIVERY APPLY TO"){

                            /*--------------------------------------------------*/
                            /*---                FOR DELIVERY APPLY TO       ---*/
                            /*--------------------------------------------------*/
                            LBL_NOMBRE_DELIVERY.Text = dr["NOMBRE"].ToString();
                            LBL_DIRECCION_DELIVERY.Text = dr["DIRECCION"].ToString();
                            LBL_CIUDAD_DELIVERY.Text = dr["CIUDAD"].ToString();
                            LBL_PAIS_DELIVERY.Text = dr["PAIS"].ToString();
                            LBL_COD_POSTAL_DELIVERY.Text = dr["COD_POSTAL"].ToString();
                            LBL_DELIVERY.Text = dr["ID_AGENTE"].ToString();
                        }
                        DIV_MODAL_MENSAJE_OK.Visible = false;
                        DIV_MODAL_MENSAJE_ERROR.Visible = false;

                    }
                    conexion.Close();
                }
   
            }catch (Exception EX){
                DIV_MODAL_MENSAJE_OK.Visible = false;
                DIV_MODAL_MENSAJE_ERROR.Visible = true;
                DIV_LABEL_ERROR.Text = EX.ToString();
            }
        }

        protected void CARGAR_DROPDOWNLIST(){
            try{
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                    SqlDataAdapter da = new SqlDataAdapter("SP_READ_AGENTE_CLIENTE", conexion);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                    ListItem i;
                    foreach (DataRow r in dt.Rows){
                        i = new ListItem(r["NOMBRE"].ToString(), r["ID"].ToString());
                        DDL_AGENTES.Items.Add(i);
                    }
                    this.DDL_AGENTES.DataBind();
                    CUADRO_CORRECTO.Visible = false;
                    CUADRO_ERROR.Visible = false;
                }
            }catch (Exception EX){
                this.DDL_AGENTES.DataBind();
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }
        }

        //protected void TXT_OPERACION_TextChanged(object sender, EventArgs e){
        //    try{
        //        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
        //            SqlDataAdapter da = new SqlDataAdapter("SP_COSTOS_EXENTOS_BL_X_OP", conexion);
        //            System.Data.DataTable dt = new System.Data.DataTable();
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            da.SelectCommand.Parameters.AddWithValue("@ID_OPERACIONES", TXT_OPERACION.Text);
        //            da.Fill(dt);
        //            this.RPT_COSTOS_EXENTOS.DataSource = dt;
        //            this.RPT_COSTOS_EXENTOS.DataBind();
        //            CUADRO_ERROR.Visible = false;
        //            CUADRO_CORRECTO.Visible = false;
        //        }
        //    }catch (Exception EX){
        //        this.RPT_COSTOS_EXENTOS.DataBind();
        //        CUADRO_CORRECTO.Visible = false;
        //        CUADRO_ERROR.Visible = true;
        //        MENSAJE_ERROR.Text = EX.ToString();
        //    }
        //}

        protected void TXT_OPERACION_SelectedIndexChanged(object sender, EventArgs e)
        {
            CARGAR_CONCEPTOS();
        }


        protected void CARGAR_CONCEPTOS(){

            try{
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                    SqlDataAdapter da = new SqlDataAdapter("SP_VENTAS_EXENTAS_BL_X_OP", conexion);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ID_OPERACIONES", TXT_OPERACION.SelectedValue.ToString());
                    da.Fill(dt);
                    this.RPT_COSTOS_EXENTOS.DataSource = dt;
                    this.RPT_COSTOS_EXENTOS.DataBind();
                    CUADRO_ERROR.Visible = false;
                    CUADRO_CORRECTO.Visible = false;
                }
            }catch (Exception EX){
                this.RPT_COSTOS_EXENTOS.DataBind();
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }
        }

        protected void BTN_REGISTRAR_DETALLE_Click(object sender, EventArgs e){
            try{
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                    SqlCommand cmd = new SqlCommand("SP_CREATE_DETALLE_BL", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@N_OPERACION", TXT_OPERACION.SelectedValue.ToString()));           
                    cmd.Parameters.Add(new SqlParameter("@N_BOOKING", TXT_BOOKING.Text.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@N_BL", TXT_BL.Text.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@MARKS_NUMBER",  TXT_MARKS_NUMBER.Text.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@N_PKGS_CONTAINERS", TXT_NO_PACKAGES.Text.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@DESCRIPTION_PKGS", TXT_DESCRIPTION_PACKAGES.Text.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@GROSS_WIGHT", TXT_GROSS.Text.ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("@MEASUREMENT", TXT_MEASUREMENT.Text.ToUpper()));
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
                    if (VALOR > 0){
                             TXT_MARKS_NUMBER.Text = "";
                             TXT_NO_PACKAGES.Text = "";
                             TXT_DESCRIPTION_PACKAGES.Text = "";
                             TXT_GROSS.Text = "";
                             TXT_MEASUREMENT.Text = "";
                             LIST_DETALLE.DataBind();
                             RPT_DETALLE_BL.DataBind();
                    }else{
                        //BT_REGISTRAR_BL.Visible = true;
                        //CUADRO_CORRECTO.Visible = false;
                        //LNK_VER_BL.Visible = false;
                        //CUADRO_ERROR.Visible = true;
                        //MENSAJE_ERROR.Text = "ERROR AL REGISTRAR DATOS , REVISAR SP_CREATE_BILL_LADING DEVOLVIO RETURN 0";
                    }
                }
            }catch (Exception EX){
                CUADRO_CORRECTO.Visible = false;
                CUADRO_ERROR.Visible = true;
                MENSAJE_ERROR.Text = EX.ToString();
            }
        }

        protected void RPT_DETALLE_BL_ItemCommand(object source, RepeaterCommandEventArgs e){
            string num = Convert.ToString(e.CommandArgument);
            switch (e.CommandName){
                case "ELIMINAR":

                    try
                    {
                        using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Operaciones.Properties.Settings.CONEXION"].ToString())){
                            SqlCommand cmd = new SqlCommand("SP_DELETE_DETALLE_BILL_LADING", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@ID_BL", num));
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
                            if (VALOR > 0){
                                LIST_DETALLE.DataBind();
                                RPT_DETALLE_BL.DataBind();
                            }
                        }
                    }catch (Exception EX){
                        CUADRO_CORRECTO.Visible = false;
                        CUADRO_ERROR.Visible = true;
                        MENSAJE_ERROR.Text = EX.ToString();
                    }


                    break;
            }
        }
    }
}