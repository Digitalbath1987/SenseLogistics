<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Crear_Bill_Lading.aspx.cs" Inherits="Operaciones.Crear_Bill_Lading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
   <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/js/bootstrap-select.js"></script>
   <link href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" rel="stylesheet">
   <script src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
 
   <link href="css/Form_Wizard.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    $(document).ready(

       function () {
            var navListItems = $('div.setup-panel div a'),
                allWells = $('.setup-content'),
                allNextBtn = $('.nextBtn');

            allWells.hide();

            navListItems.click(function (e) {
                e.preventDefault();
                var $target = $($(this).attr('href')),
                    $item = $(this);

                if (!$item.hasClass('disabled')) {
                    navListItems.removeClass('btn-success').addClass('btn-default');
                    $item.addClass('btn-success');
                    allWells.hide();
                    $target.show();
                    $target.find('input:eq(0)').focus();
                }
            });

            allNextBtn.click(function () {
                var curStep = $(this).closest(".setup-content"),
                    curStepBtn = curStep.attr("id"),
                    nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
                    curInputs = curStep.find("input[type='text'],input[type='url']"),
                    isValid = true;

                $(".form-group").removeClass("has-error");
                for (var i = 0; i < curInputs.length; i++) {
                    if (!curInputs[i].validity.valid) {
                        isValid = false;
                        $(curInputs[i]).closest(".form-group").addClass("has-error");
                    }
                }

                if (isValid) nextStepWizard.removeAttr('disabled').trigger('click');
            });

            $('div.setup-panel div a.btn-success').trigger('click');

        }
     );

    function FMOE(valor) {
        var TITULO;
        if (valor == "1") {
            TITULO = 'SHIPPER'
        } else if (valor == "2") {
            TITULO = 'CONSIGNEE'
        } else if (valor == "3") {
            TITULO = 'NOTIFY PARTY'
        } else if (valor == "4") {
            TITULO = 'FOR DELIVERY APPLY TO'
        }

        document.getElementById("<%= LBL_TIPO_AGENTE.ClientID %>").value = TITULO;
        FormularioModalJS('MODAL_AGENTES');
        Sys.Application.remove_load(FMOE);
    }

    Sys.Application.add_load(FMOE);

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

   
    <div class="container">
        <div class ="row">
            <div class="col-sm-12">
                      <h1 class="panel panel-primary text-center text-bold  panel-heading">Generar Bill Of Lading</h1>    
            </div>
         

        </div>
        <hr />


    <div class="stepwizard">
        <div class="stepwizard-row setup-panel">
            <div class="stepwizard-step col-xs-3"> 
                <a href="#step-1" type="button" class="btn btn-success btn-circle">1</a>
                <p><small>Documento</small></p>
            </div>
            <div class="stepwizard-step col-xs-3"> 
                <a href="#step-2" type="button" class="btn btn-default btn-circle" disabled="disabled">2</a>
                <p><small>Mercancia</small></p>
            </div>
            <div class="stepwizard-step col-xs-3"> 
                <a href="#step-3" type="button" class="btn btn-default btn-circle" disabled="disabled">3</a>
                <p><small>Conceptos</small></p>
            </div>
            <div class="stepwizard-step col-xs-3"> 
                <a href="#step-4" type="button" class="btn btn-default btn-circle" disabled="disabled">4</a>
                <p><small>Clientes</small></p>
            </div>
        </div>
    </div>
    
   <%--==============================================================================================================--%>
   <%--                           PASO 1 FORM WIZARD                                                                 --%>
   <%--==============================================================================================================--%>

    <div class="panel panel-primary setup-content" id="step-1">
            <div class="panel-heading">
                 <h3 class="panel-title">Documento</h3>
            </div>
            <div class="panel-body">
                <div class="row" >
                    <div class="col-sm-4">
                          <div class="form-group">
                          <label class="control-label">N° Operacion</label>
                              <asp:DropDownList ID="TXT_OPERACION"  class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TXT_OPERACION_SelectedIndexChanged" DataSourceID="OPERACIONES_FACTURA_PENDIENTES" DataTextField="ID_OPERACIONES" DataValueField="ID_OPERACIONES"></asp:DropDownList>
                              <asp:SqlDataSource runat="server" ID="OPERACIONES_FACTURA_PENDIENTES" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_OPERACIONES_SIN_BL" SelectCommandType="StoredProcedure"></asp:SqlDataSource> 
                              
                              
                           <%--   <asp:TextBox ID="TXT_OPERACION" required="required" AutoPostBack="true" OnTextChanged="TXT_OPERACION_TextChanged" runat="server" class="form-control" name="password"></asp:TextBox>--%>
                          </div>
                    </div>
                    <div class="col-sm-4">
                          <div class="form-group">
                          <label class="control-label">N° Booking</label>
                                <asp:TextBox ID="TXT_BOOKING" required="required" runat="server" class="form-control" name="password" ></asp:TextBox>
                          </div>
                    </div>
                    <div class="col-sm-4">
                          <div class="form-group">
                          <label class="control-label">N° B/L</label>
                               <asp:TextBox ID="TXT_BL" runat="server"  required="required" class="form-control" name="password"></asp:TextBox>
                          </div>
                    </div>
                </div>
                  
                <div class="row" >
                    <div class="col-sm-4">
                          <div class="form-group">
                          <label class="control-label">Vessel & Voy. No.:</label>
                                <asp:TextBox ID="TXT_VESSEL" runat="server" required="required" class="form-control" name="password"></asp:TextBox>
                          </div>
                    </div>
                    <div class="col-sm-4">
                          <div class="form-group">
                          <label class="control-label">Port Of Loading</label>
                               <asp:TextBox ID="TXT_PORT_LOADING" required="required" runat="server" class="form-control" name="password"></asp:TextBox>
                          </div>
                    </div>
                    <div class="col-sm-4">
                          <div class="form-group">
                          <label class="control-label">Port Of Discharge</label>
                               <asp:TextBox ID="TXT_PORT_DISCHARGE" required="required" runat="server" class="form-control" name="password" ></asp:TextBox>
                          </div>
                    </div>
                </div>

                <div class="row" >
                    <div class="col-sm-3">
                          <div class="form-group">
                          <label class="control-label">Place of Delivery</label>
                               <asp:TextBox ID="TXT_PLACE_DELIVERY" runat="server" required="required" class="form-control" name="password"></asp:TextBox>
                          </div>
                    </div>
                    <div class="col-sm-6">
                          <div class="form-group">
                          <label class="control-label">Point And Country of Orig.</label>
                                <asp:TextBox ID="TXT_POINT_COUNTRY" runat="server" required="required" class="form-control" name="password" ></asp:TextBox>
                          </div>
                    </div>
                     <div class="col-sm-3">
                          <div class="form-group">
                          <label class="control-label">On Boar Date</label>
                                <asp:TextBox ID="TXT_BOAR_DATE" runat="server" required="required" class="form-control" TextMode="Date" name="password" ></asp:TextBox>
                          </div>
                    </div>
                </div>
                <hr />
                <button class="btn btn-primary nextBtn pull-right" type="button">Siguiente</button>
            </div>
        </div>
   
    <%--==============================================================================================================--%>
    <%--                         PASO 2 FORM WIZARD                                                                   --%>
    <%--==============================================================================================================--%>

    <div class="panel panel-primary setup-content" id="step-2">
            <div class="panel-heading">
                 <h3 class="panel-title">Mercancia</h3>
            </div>
            <div class="panel-body">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="BTN_REGISTRAR_DETALLE" EventName="Click"  />
    </Triggers>
    <ContentTemplate>

            <div class="row">
                    <div class="col-sm-3">
                      <div class="form-group">
                      <label class="control-label">Marks and Number</label>
                         <asp:TextBox ID="TXT_MARKS_NUMBER" runat="server" TextMode="MultiLine" Rows="7" class="form-control" name="password"></asp:TextBox>
                      </div>
                    </div>
                    <div class="col-sm-3">
                      <div class="form-group">
                      <label class="control-label">No. Of Pkgs or Containers</label>
                         <asp:TextBox ID="TXT_NO_PACKAGES" runat="server" TextMode="MultiLine" Rows="7" class="form-control" name="password"></asp:TextBox>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="form-group">
                      <label class="control-label">Description of Packages and Goods</label>
                         <asp:TextBox ID="TXT_DESCRIPTION_PACKAGES" runat="server" TextMode="MultiLine" Rows="7" class="form-control" name="password"></asp:TextBox>
                      </div>
                    </div>
            </div>
            <div class="row">
                   <div class="col-sm-3">
                      <div class="form-group">
                      <label class="control-label">Gross Wight Kgs</label>
                         <asp:TextBox ID="TXT_GROSS" runat="server" TextMode="MultiLine" Rows="7" class="form-control" name="password"></asp:TextBox>
                      </div>
                    </div>
                    <div class="col-sm-3">
                      <div class="form-group">
                      <label class="control-label">Measurement m<sup>3</sup></label>
                         <asp:TextBox ID="TXT_MEASUREMENT" runat="server" TextMode="MultiLine" Rows="7" class="form-control" name="password"></asp:TextBox>
                      </div>
                    </div>
                    <div class="col-sm-3">
                         <div class="form-group">
                             <hr />
                             <label class="control-label"> </label>

                        <asp:Button ID="BTN_REGISTRAR_DETALLE" OnClick="BTN_REGISTRAR_DETALLE_Click"  CssClass="btn btn-primary" runat="server" Text="REGISTRAR" />
                    </div>
                 </div>
            </div>
            <hr />
            <div class="row">
<div class="col-lg-12">




      <div class="table-responsive">          
              <table class="table table-sm table-striped  table-hover">
              <thead>
              <tr>
                <th class="small">#</th>
                <th class="small">MARKS_NUMBER</th>
                <th class="small">N_PKGS_CONTAINERS</th>
                <th class="small">DESCRIPTION_PKGS</th>
                <th class="small">GROSS_WIGHT</th>
                <th class="small">MEASUREMENT</th>
                <th class="small">ACCIONES</th>
              </tr>
              </thead>
              <tbody>
              <asp:Repeater ID="RPT_DETALLE_BL" runat="server" OnItemCommand="RPT_DETALLE_BL_ItemCommand"  DataSourceID="LIST_DETALLE">
              <ItemTemplate>
              <tr>
               <td class="small text-bold"></td>
               <td class="small" ><%# Eval("MARKS_NUMBER") %></td>
               <td class="small" ><%# Eval("N_PKGS_CONTAINERS") %></td>
               <td class="small" ><%# Eval("DESCRIPTION_PKGS") %></td>
               <td class="small" ><%# Eval("GROSS_WIGHT") %></td>
               <td class="small" ><%# Eval("MEASUREMENT") %></td>   
               <td class="small hidden-sm">
               <asp:Button ID="BTN_ELIMINAR_DETALLE" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID_DETALLE") %>'  ToolTip="¿ DESEA DESHABILITAR EL REGISTRO ?" data-toggle="tooltip"   class=" btn btn-danger" runat="server" Text='X' />
               </td>   

              </tr>
              </ItemTemplate>
              </asp:Repeater>
              <asp:SqlDataSource runat="server" ID="LIST_DETALLE" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommandType="StoredProcedure" SelectCommand="SP_READ_DETALLE_BILL_LADING">
              <SelectParameters>
              <asp:ControlParameter ControlID="TXT_OPERACION" PropertyName="SelectedValue" DefaultValue="0" Name="N_OPERACION"  Type="String"></asp:ControlParameter>
              </SelectParameters>
              </asp:SqlDataSource>
              </tbody>
              </table>
              </div>


</div>
</div>

     </ContentTemplate>
     </asp:UpdatePanel>

            <hr /> 
            <button class="btn btn-primary nextBtn pull-right" type="button">Siguiente</button>

        </div>
        </div>
        
   <%--==============================================================================================================--%>
   <%--                          PASO 3 FORM WIZARD                                                                  --%>
   <%--==============================================================================================================--%>

    <div class="panel panel-primary setup-content" id="step-3">
            <div class="panel-heading">
            <h3 class="panel-title">Conceptos</h3>
            </div>
            <div class="panel-body">
              <div class="row">
              <div class="col-sm-12">
              <div class="table-responsive">          
              <table class="table table-sm table-striped  table-hover">
                  <thead>
                      <tr>
                        <th class="small col-sm-1">#</th>
                        <th class="small col-sm-6">DETALLE</th>
                        <th class="small col-sm-2">MONEDA</th>
                        <th class="small col-sm-1">MONTO</th>
                        <th class="small col-sm-1">MONTO_USD</th>  
                        <th class="small col-sm-1">MONTO_CLP</th>
                      </tr>
                  </thead>
              <tbody id="myTable">
              <asp:Repeater ID="RPT_COSTOS_EXENTOS"   runat="server">
                  <ItemTemplate>
                      <tr>
                       <td class="small text-bold col-sm-1"><%# Eval("ID") %></td>
                       <td class="small col-sm-6"><%# Eval("DETALLE") %></td>
                       <td class="small col-sm-2"><%# Eval("DETALLE_MONEDA") %></td>
                       <td class="small col-sm-1"><%# Eval("MONTO") %></td>
                       <td class="small col-sm-1"><%# Eval("MONTO_USD") %></td>
                       <td class="small col-sm-1"><%# Eval("MONTO_CLP") %></td>
                      </tr>
                  </ItemTemplate>
              </asp:Repeater>
              </tbody>
              </table>
              </div>
              </div>   
        </div>

        <hr />
        <button class="btn btn-primary nextBtn pull-right" type="button">Siguiente</button>
        </div>
        </div>
        
   <%--==============================================================================================================--%>
   <%--                          PASO 4 FORM WIZARD                                                                  --%>
   <%--==============================================================================================================--%>

     <div class="panel panel-primary setup-content" id="step-4">
            <div class="panel-heading">
                 <h3 class="panel-title">Clientes</h3>
            </div>
            <div class="panel-body">
                
                <asp:UpdatePanel ID="UPDATE_PANEL_FORMULARIO_WIZARD" runat="server">
               <Triggers>
               </Triggers>
               <ContentTemplate>
                
                <div class="row">
                    <div class="col-sm-6">
                           <div class="form-group">
                          <label class="control-label h3">Shipper</label>
                          <div class="panel panel-default">
                          <div class="panel-body">
                                                          
                              
                                 <asp:Label ID="LBL_SHIPPER" Visible="false" runat="server" Text="0"></asp:Label>
                              <asp:Label ID="LBL_SHIPPER_C_A" Visible="false" runat="server" Text="0"></asp:Label>

                                  <div class="row">
                                      <div class="col-lg-12">
                                          <h4 class="card-title text-bold">
                                            <asp:Label ID="LBL_NOMBRE_SHIPPER"  runat="server" Text=""></asp:Label>
                                          </h4>
                                      </div>
                                  </div>


                                  <div class="row">
                                      <div class="col-lg-12">
                                          <p class="card-text">
                                              <asp:Label ID="LBL_DIRECCION_SHIPPER" runat="server" Text=""></asp:Label>
                                               
                                              <asp:Label ID="LBL_SHIPPER_CIUDAD" runat="server" Text=""></asp:Label>
                                          </p>
                                      </div>
                                  </div>


                                  <div class="row">
                                      <div class="col-lg-12">
                                          <p class="card-text">
                                              <asp:Label ID="LBL_COD_POSTAL_SHIPPER" runat="server" Text=""></asp:Label>
                                          </p>
                                      </div>
                                  </div>

                                  <div class="row">
                                      <div class="col-lg-12">
                                          <p class="card-text">
                                              <asp:Label ID="LBL_SHIPPER_PAIS" runat="server" Text=""></asp:Label>
                                          </p>
                                      </div>
                                  </div>

                                  <div class="row">
                                      <div class="col-lg-12 text-right">
                                        <a href="#" class="stretched-link"  onclick="FMOE(1);">Cambiar</a>    
                                      </div>
                                  </div>

                          </div>
                          </div>
                          </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">
                          <label class="control-label h3">Consignee</label>
                          <div class="panel panel-default">
                          <div class="panel-body">
                                                           
                              
                                 <asp:Label ID="LBL_CONSIGNEE" Visible="false" runat="server" Text="0"></asp:Label>
                                <asp:Label ID="LBL_CONSIGNEE_C_A" Visible="false" runat="server" Text="0"></asp:Label>

                                  <div class="row">
                                  <div class="col-lg-12">
                                   <h4 class="card-title text-bold">
                                      <asp:Label ID="LBL_NOMBRE_CONSIGNEE" runat="server" Text=""></asp:Label>
                                  </h4>
                                  </div>
                                  </div>


                                  <div class="row">
                                  <div class="col-lg-12">
                                  <p class="card-text">
                                      <asp:Label ID="LBL_DIRECCION_CONSIGNEE" runat="server" Text=""></asp:Label>
                                      
                                      <asp:Label ID="LBL_CIUDAD_CONSIGNEE" runat="server" Text=""></asp:Label>
                                  </p>
                                  </div>
                                  </div>


                                  <div class="row">
                                  <div class="col-lg-12">
                                  <p class="card-text">
                                      <asp:Label ID="LBL_COD_POSTAL_CONSIGNEE" runat="server" Text=""></asp:Label>
                                  </p>
                                  </div>
                                  </div>

                                  <div class="row">
                                  <div class="col-lg-12">
                                  <p class="card-text">
                                      <asp:Label ID="LBL_PAIS_CONSIGNEE" runat="server" Text=""></asp:Label>
                                  </p>
                                  </div>
                                  </div>                              

                                  <div class="row">
                                  <div class="col-lg-12 text-right">
                                  <a href="#" class="stretched-link" onclick="FMOE(2);">Cambiar</a>    
                                  </div>
                                  </div>

                          </div>
                          </div>
                          </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-6">

                        <div class="form-group">
                          <label class="control-label h3">Notify Party</label>
                          <div class="panel panel-default">
                          <div class="panel-body">
                              
                                 <asp:Label ID="LBL_NOTIFY" Visible="false" runat="server" Text="0"></asp:Label>
                              <asp:Label ID="LBL_NOTIFY_C_A" Visible="false" runat="server" Text="0"></asp:Label>

                                  <div class="row">
                                  <div class="col-lg-12">
                                  <h4 class="card-title text-bold">
                                      <asp:Label ID="LBL_NOMBRE_NOTIFY" runat="server" Text=""></asp:Label>
                                  </h4>
                                  </div>
                                  </div>

                                  <div class="row">
                                  <div class="col-lg-12">
                                  <p class="card-text">
                                      <asp:Label ID="LBL_DIRECCION_NOTIFY" runat="server" Text=""></asp:Label>
                                      
                                      <asp:Label ID="LBL_CIUDAD_NOTIFY" runat="server" Text=""></asp:Label>
                                  </p>
                                  </div>
                                  </div>

                                  <div class="row">
                                  <div class="col-lg-12">
                                  <p class="card-text">
                                      <asp:Label ID="LBL_COD_POSTAL_NOTIFY" runat="server" Text=""></asp:Label>
                                  </p>
                                  </div>
                                  </div>
                              
                                  <div class="row">
                                  <div class="col-lg-12">
                                  <p class="card-text">
                                      <asp:Label ID="LBL_PAIS_NOTIFY" runat="server" Text=""></asp:Label>
                                  </p>
                                  </div>
                                  </div>
                                 
                                  <div class="row">
                                  <div class="col-lg-12 text-right">
                                  <a href="#" class="stretched-link"  onclick="FMOE(3);">Cambiar</a>    
                                  </div>
                                  </div>

                          </div>
                          </div>
                          </div>
                </div>
                <div class="col-sm-6">
                        <div class="form-group">
                          <label class="control-label h3">For Delivery Apply To</label>
                          <div class="panel panel-default"> 
                          <div class="panel-body">
                                                           
                                  <asp:Label ID="LBL_DELIVERY" Visible="false" runat="server" Text="0"></asp:Label>
                              <asp:Label ID="LBL_DELIVERY_C_A" Visible="false" runat="server" Text="0"></asp:Label>
                                  <div class="row">
                                  <div class="col-lg-12">
                                  <h4 class="card-title text-bold">
                                      <asp:Label ID="LBL_NOMBRE_DELIVERY" runat="server" Text=""></asp:Label>
                                  </h4>
                                  </div>
                                  </div>

                                  <div class="row">
                                  <div class="col-lg-12">
                                  <p class="card-text">
                                      <asp:Label ID="LBL_DIRECCION_DELIVERY" runat="server" Text=""></asp:Label>
                                       
                                      <asp:Label ID="LBL_CIUDAD_DELIVERY" runat="server" Text=""></asp:Label>
                                  </p>
                                  </div>
                                 </div>

                                  <div class="row">
                                  <div class="col-lg-12">
                                  <p class="card-text">
                                      <asp:Label ID="LBL_COD_POSTAL_DELIVERY" runat="server" Text=""></asp:Label>
                                  </p>
                                  </div>
                                  </div>
                               
                                  <div class="row">
                                  <div class="col-lg-12">
                                  <p class="card-text">
                                      <asp:Label ID="LBL_PAIS_DELIVERY" runat="server" Text=""></asp:Label>
                                  </p>
                                  </div>
                                  </div>

                                  <div class="row">
                                  <div class="col-lg-12 text-right">
                                  <a href="#" class="stretched-link"  onclick="FMOE(4);">Cambiar</a>    
                                  </div>
                                  </div>

                          </div>
                          </div>
                          </div>
                    </div>
                </div>

               <hr />
                   <asp:LinkButton ID="LNK_VER_BL" PostBackUrl="~/Listado_Bill_Lading.aspx"  Visible="false" runat="server">VER B/L <i class="fas fa-arrow-circle-right"></i>   </asp:LinkButton>
                <asp:Button ID="BT_REGISTRAR_BL" OnClick="BT_REGISTRAR_BL_Click" class="btn btn-success pull-right" runat="server" Text="Crear B/L" />
        
                <div class="row">
     
                <div class="col-md-12">
                   <%---------------------------------------------%>
                   <%--          MENSAJE DE ERROR                --%>
                   <%---------------------------------------------%>
                    <br />
                   <div runat="server" id="CUADRO_ERROR" visible="false" class="alert alert-danger alert-dismissible fade in">
                   <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                   <strong>ERROR DE REGISTRO!</strong> <asp:Label ID="MENSAJE_ERROR" runat="server" Text="Label"></asp:Label> <i class="fas fa-exclamation-triangle"></i> .
                   </div>

                   <%---------------------------------------------%>
                   <%--          MENSAJE DE CORRECTO            --%>
                   <%---------------------------------------------%>
                   <div runat="server" id="CUADRO_CORRECTO" visible="false" class="alert alert-success alert-dismissible fade in">
                   <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                   <h5><strong>REGISTRO CORRECTO!</strong>  <asp:Label ID="MENSAJE_CORRECTO" runat="server" Text="Label"></asp:Label>.</h5>
                   </div>
               </div>
        </div>
               </ContentTemplate>
               </asp:UpdatePanel>

           

             </div>
     </div>

</div>
       

    <%--==============================================================================================================--%>
    <%-- INICIO MODAL AGENTES                                                                                          --%>
    <%--==============================================================================================================--%>
    <div class="modal fade" id="MODAL_AGENTES" role="dialog">
      <asp:UpdatePanel ID="UPDATE_PANEL_MODAL_AGENTES" runat="server">
            <ContentTemplate>
                <div class="modal-dialog">
                        
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="LBL_TITULO_GRUPO_CARGA" runat="server" Text="Seleccionar"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="LBL_TIPO_AGENTE" class="form-control" data-width="100%" Width="100%"  runat="server" DISABLED></asp:TextBox>
                                 </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-12">
                                   <div class="form-group">     
                                       <asp:DropDownList ID="DDL_AGENTES" data-width="100%" Width="100%"  class="form-control"  runat="server">
                                       </asp:DropDownList>
                                   </div> 
                                </div>
                            </div>



                <div class="row">
     
                <div class="col-md-12">
                   <%---------------------------------------------%>
                   <%--          MENSAJE DE ERROR MODAL          --%>
                   <%---------------------------------------------%>
                    <br />
                   <div runat="server" id="DIV_MODAL_MENSAJE_OK" visible="false" class="alert alert-success  alert-dismissible fade in">
                   <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                   <strong>REGISTRO CORRECTO!</strong> <asp:Label ID="DIV_LABEL_OK" runat="server" Text=""></asp:Label> <i class="fas fa-exclamation-triangle"></i> .
                   </div>

                   <%---------------------------------------------%>
                   <%--          MENSAJE DE CORRECTO MODAL      --%>
                   <%---------------------------------------------%>
                   <div runat="server" id="DIV_MODAL_MENSAJE_ERROR" visible="false" class="alert alert-danger  alert-dismissible fade in">
                   <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                   <h5><strong>ERROR DE REGISTRO!</strong>  <asp:Label ID="DIV_LABEL_ERROR" runat="server" Text=""></asp:Label>.</h5>
                   </div>
               </div>
        </div>
              </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
   
                            

                            <asp:Button ID="BTN_CARGAR_AGENTE"
                                OnClick="BTN_CARGAR_AGENTE_Click" 
                                runat="server" 
                                Text="CARGAR DATOS" 
                                CssClass="btn btn-primary"
                                 />
                      
                         </div>
                    </div>
                </div>
            </ContentTemplate>
               <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BTN_CARGAR_AGENTE" EventName="Click" />
     
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--==============================================================================================================--%>
    <%-- FIN MODAL AGENTES                                                                                            --%>
    <%--==============================================================================================================--%>

   <%-- <script type="text/javascript">
        // Material Select Initialization
        $(document).ready(function () {
            $('.mdb-select').materialSelect();
        });
</script>--%>

</asp:Content>
