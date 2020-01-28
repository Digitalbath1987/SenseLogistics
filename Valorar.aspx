<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Valorar.aspx.cs" Inherits="Operaciones.Valorar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

   <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
   <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/js/bootstrap-select.js"></script>
   <link href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" rel="stylesheet">
   <script src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>

    <style type="text/css">
            .modalDialog2
             {
             position: fixed;
             top: 0;
             right: 0;
             bottom: 0;
             left: 0;
             background: rgba(180,180,180,0.8);
             z-index: 99999;
             opacity: 0;
             -webkit-transition: opacity 600ms ease-in;
             -moz-transition: opacity 600ms ease-in;
             transition: opacity 600ms ease-in;
             pointer-events: none;
             }
            .modalDialog2:target
             {
             opacity: 1;
             pointer-events: auto;
             }
            .modalDialog2 > div
             {
             width: 30%;
             position: center;
             margin: 5% auto;
             padding: 5px 20px 13px 20px;
             border-radius: 10px;
             background: #fff;
             background: -moz-linear-gradient(#fff, #999);
             background: -webkit-linear-gradient(#fff, #999);
             background: #fff;
                top: 0px;
                left: -21px;
            }
    </style>

    <script type="text/javascript">
    function MostrarModalJS() {
    window.location.href = '#modalHtml2';
        }
 function MostrarModalJS() {
    window.location.href = '#modalHtml';
        }
</script>



<div class="container">
    <div class="col-sm-10">

   <%----------------------------------------------------------%>
   <%---           BUSCAR OPERACION                       -----%>
   <%----------------------------------------------------------%>
      <div class="panel panel-primary">
          <div class="panel-heading text-bold"><h4> VALORIZAR OPERACION </h4> </div>
      <div class="panel-body">
      <div class="row">
          <div class="col-sm-2"></div>
          <div class="col-sm-2">
              <label for="pwd">NRO OPERACION:</label>  
            </div>
          <div class="col-sm-2">
             <asp:TextBox ID="TXT_OPERACION" TextMode="Number" CssClass="form-control" Text="0" placeholder="NRO OPERACION" runat="server"></asp:TextBox>
          </div>
          <div class="col-sm-2">
              <button type="button" class="btn btn-primary" runat="server" id="BTN_BUSCAR_OPERACION" onserverclick="BTN_BUSCAR_OPERACION_ServerClick" data-toggle="tooltip" title="CARGAR DATOS"><i class="fas fa-search"></i> BUSCAR  </button>
          </div>
          
          <div class="col-sm-2"></div>
          <div class="col-sm-2"></div>
       </div>
       </div>
       </div>

      <div class="row">
<div class="col-lg-9">    
   <%----------------------------------------------------------%>
   <%---           DATOS DEL CLIENTE                      -----%>
   <%----------------------------------------------------------%>
      <div class="panel panel-primary">
      
      <div class="panel-body">
                 <h3><label> DATOS DEL CLIENTE</label></h3>
          <div class="row">
             <div class="col-sm-8">
               <div class="form-group">
                     <label for="pwd">CLIENTE:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-user"></i></span>
                     <asp:TextBox ID="TXT_NOMBRE"  runat="server" class="form-control"  name="password"  DISABLED></asp:TextBox>
                     </div>
                </div>
            </div>

          <div class="col-sm-4">
                <div class="form-group">
                     <label for="pwd">RUT:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-at"></i></span>
                     <asp:TextBox ID="TXT_RUT" runat="server"  class="form-control"   name="password"  DISABLED></asp:TextBox>
                     </div>
                </div>
           </div>
      </div>
          <div class="row">
          <div class="col-sm-4">
              <div class="form-group">
                     <label for="pwd">CIUDAD:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-map-marked-alt"></i></span>
                     <asp:TextBox ID="TXT_CIUDAD" runat="server"  class="form-control"   name="password"  DISABLED></asp:TextBox>
                     </div>
              </div>
          </div>
    
          <div class="col-sm-8">
                <div class="form-group">
                     <label for="pwd">DIRECCION:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-map-marked-alt"></i></span>
                     <asp:TextBox ID="TXT_DIRECCION" runat="server"  class="form-control" name="password"   DISABLED></asp:TextBox>
                     </div>
                </div>
          </div>
      </div>

          <div class="row">

       <div class="col-sm-4">
           <div class="form-group">
                     <label for="pwd">FECHA CREACION:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-map-marked-alt"></i></span>
                     <asp:TextBox ID="TXT_FECHA_CREACION" runat="server"  TextMode="Date" class="form-control" name="password"  REQUIRED></asp:TextBox>
                     </div>
           </div>
    </div>
       
       <div class="col-sm-4">
           <div class="form-group">
                     <label for="pwd">FECHA PAGO:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-map-marked-alt"></i></span>
                     <asp:TextBox ID="TXT_FECHA_PAGO" runat="server" TextMode="Date" class="form-control" name="password"></asp:TextBox>
                     </div>
           </div>
           </div>
       <div class="col-sm-4">
           <div class="form-group">
                     <label for="pwd"></label>
                     <div class="input-group">
                     <span ></span>
                     <button type="button" class="btn btn-primary" data-toggle="tooltip" id="BTN_VALORAR" runat="SERVER" visible="false"  title="GENERAR VALORACION" onserverclick="BTN_VALORAR_ServerClick"> VALORAR    <i class="fas fa-hand-holding-usd"></i> </button>
                     <button type="button" class="btn btn-danger small" data-toggle="tooltip" id="BTN_MODIFICAR" runat="SERVER" visible="false"  title="MODIFICAR VALORACION " onserverclick="BTN_MODIFICAR_ServerClick"><i class="fas fa-pen-square"></i></button>
           </div>
           </div>
           </div>
</div>


       </div>
       </div>
</div>
<div class="col-lg-3">
   <%----------------------------------------------------------%>
   <%---           DATOS DEL MONEDAS                      -----%>
   <%----------------------------------------------------------%>

<div class="panel panel-primary">
      <div class="panel-body">
          <h3><label> VALOR MONEDA</label></h3>
            <div class="row">
                <div class="col-sm-12">
                      <div class="form-group">
                                 <label for="pwd">TASA CAMBIO USD:</label>
                                 <div class="input-group">
                                 <span class="input-group-addon"><i class="fas fa-dollar-sign"></i></span>
                                 <asp:TextBox ID="TXT_VALOR_USD" runat="server" class="form-control"  placeholder="USD" name="password"  REQUIRED></asp:TextBox>
                                 </div>
                          </div>
                 </div>
            </div>

            <div class="row">
                 <div class="col-sm-12">
                      <div class="form-group">
                                 <label for="pwd">TASA CAMBIO EUR:</label>
                                 <div class="input-group">
                                 <span class="input-group-addon"><i class="fas fa-euro-sign"></i></span>
                                 <asp:TextBox ID="TXT_VALOR_EUR" runat="server" class="form-control" placeholder="EUR"  name="password"  REQUIRED></asp:TextBox>
                                 </div>
                      </div>
                 </div>
            </div>

            <div class="row">
                 <div class="col-sm-12">
                      <div class="form-group">
                                 <label for="pwd">TASA CAMBIO GBP:</label>
                                 <div class="input-group">
                                 <span class="input-group-addon"><i class="fas fa-pound-sign"></i></span>
                                 <asp:TextBox ID="TXT_VALOR_GBP" runat="server" class="form-control" name="password"  placeholder="GBP"  REQUIRED></asp:TextBox>
                                 </div>
                      </div>
                 </div>
            </div>

       </div>
   </div>
</div>

</div>

  
<hr />


<div id="PANEL_COSTOS" runat="server" visible="false">

   <%----------------------------------------------------------%>
   <%---               COSTOS AFECTOS                     -----%>
   <%----------------------------------------------------------%>

<div class="row">
<div  class="col-sm-3">

</div>
<div  class="col-sm-3">
</div>
<div  class="col-sm-3">
</div>
<div  class="col-sm-3 text-right">
<button type="button" class="btn btn-primary"   data-toggle="tooltip" runat="server" id="BTN_REGISTRAR_COSTO_AFECTO" onserverclick="BTN_REGISTRAR_COSTO_AFECTO_ServerClick"  title="Registrar Costo Afecto">AGREGAR <i class="fas fa-plus-circle"></i> </button>
</div>
</div>

<br />

<div class="row">
<div  class="col-sm-12">

<div class="panel panel-primary">
      <div class="panel-body">
 <h3><label> COSTOS AFECTOS</label></h3>
       <div class="row">
            <div class="col-sm-12">  
             <asp:GridView ID="GV_COSTOS_AFECTOS" CssClass="table-responsive col-sm-12" OnRowCommand="GV_COSTOS_AFECTOS_RowCommand"  DataKeyNames="REGISTRO" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Sin Registros" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="COSTOS_AFECTOS" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                 <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                      <Columns>
                           <%----------------------------------------------------------%>
                           <%---               BOTONES ACCION                     -----%>
                           <%----------------------------------------------------------%>
                          <asp:TemplateField HeaderText="ACCION" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1">
                           <ItemTemplate>
                              <asp:LinkButton ID="BTN_ELI_CA" runat="server" CssClass="btn btn-danger btn-xs" CommandName="ELIMINAR" CommandArgument='<%# Eval("REGISTRO") %>' ToolTip="¿DESEA ELIMINAR REGISTRO?"> 
                              <i class="fas fa-trash-alt"></i>
                              </asp:LinkButton>
                              <asp:LinkButton ID="BTN_MOD_CA" runat="server" CssClass="btn btn-primary btn-xs" CommandName="MODIFICAR" CommandArgument='<%# Eval("REGISTRO") %>' ToolTip="¿DESEA MODIFICAR REGISTRO?"> 
                              <i class="fas fa-edit"></i>
                              </asp:LinkButton>
                          </ItemTemplate>
                          </asp:TemplateField>

                     <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="REGISTRO" Visible="false" ></asp:BoundField>
                     <asp:BoundField DataField="ID" HeaderText=" NRO" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="ID" ReadOnly="True"></asp:BoundField>
                     <asp:BoundField DataField="DETALLE" HeaderText=" DETALLE" HeaderStyle-CssClass="col-sm-4 text text-center" ItemStyle-CssClass="col-sm-4" SortExpression="DETALLE"></asp:BoundField>
                     <asp:BoundField DataField="DETALLE_MONEDA" HeaderText=" MONEDA" HeaderStyle-CssClass="col-sm-2 text text-center" ItemStyle-CssClass="col-sm-2" SortExpression="DETALLE_MONEDA"></asp:BoundField>
                     <asp:BoundField DataField="MONTO" HeaderText=" MONTO" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="MONTO"></asp:BoundField>
                     <asp:BoundField DataField="MONTO_USD" HeaderText=" MONTO USD" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="MONTO_USD"></asp:BoundField>
                     <asp:BoundField DataField="MONTO_CLP"  ItemStyle-CssClass="col-sm-1" HeaderStyle-CssClass="col-sm-1 text text-center" HeaderText=" MONTO CLP" SortExpression="MONTO_CLP"></asp:BoundField>
                 </Columns>
                 <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                 <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>
                 <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>
                 <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                 <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>
                 <SortedAscendingHeaderStyle BackColor="#808080"></SortedAscendingHeaderStyle>
                 <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>
                 <SortedDescendingHeaderStyle BackColor="#383838"></SortedDescendingHeaderStyle>
             </asp:GridView>
               <asp:SqlDataSource runat="server" ID="COSTOS_AFECTOS" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_COSTOS_AFECTOS_X_OP" SelectCommandType="StoredProcedure">
               <SelectParameters>
               <asp:ControlParameter ControlID="TXT_OPERACION" PropertyName="Text" Name="ID_OPERACIONES" Type="Int32"></asp:ControlParameter>
               </SelectParameters>
               </asp:SqlDataSource>
</div>
      </div>
      </div>
</div>

</div>
</div>
    
   <%----------------------------------------------------------%>
   <%---               COSTOS EXENTOS                     -----%>
   <%----------------------------------------------------------%>

<div class="row">
<div  class="col-sm-3">
</div>
<div  class="col-sm-3">
</div>
<div  class="col-sm-3">
</div>
<div  class="col-sm-3 text-right">
<button type="button" class="btn btn-primary" data-toggle="tooltip" runat="server" id="BTN_REGISTRAR_COSTO_EXENTO" onserverclick="BTN_REGISTRAR_COSTO_EXENTO_ServerClick" title="Registrar Costo Exento">AGREGAR <i class="fas fa-plus-circle"></i> </button>
</div>
</div>

<br />

<div class="row">
    <div  class="col-sm-12">
         <div class="panel panel-primary">
      <div class="panel-body">

           <h3><label> COSTOS EXENTO</label></h3>
              <div class="row">
            <div class="col-sm-12">  
             <asp:GridView ID="GV_COSTO_EXENTO" CssClass="table-responsive col-sm-12" DataKeyNames="REGISTRO" OnRowCommand="GV_COSTO_EXENTO_RowCommand"  runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Sin Registros" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="COSTOS_EXENTOS" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                 <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                 <Columns>
                        <%----------------------------------------------------------%>
                        <%---               BOTONES ACCION                     -----%>
                        <%----------------------------------------------------------%>
                        <asp:TemplateField HeaderText="ACCION" HeaderStyle-CssClass="col-sm-2 text text-center" ItemStyle-CssClass="col-sm-2">
                        <ItemTemplate>
                        <asp:LinkButton ID="BTN_ELI_CE" runat="server" CssClass="btn btn-danger btn-xs" CommandName="ELIMINAR" CommandArgument='<%# Eval("REGISTRO") %>' ToolTip="¿DESEA ELIMINAR REGISTRO?"> 
                        <i class="fas fa-trash-alt"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="BTN_MOD_CE" runat="server" CssClass="btn btn-primary btn-xs" CommandName="MODIFICAR" CommandArgument='<%# Eval("REGISTRO") %>' ToolTip="¿DESEA MODIFICAR REGISTRO?"> 
                        <i class="fas fa-edit"></i>
                        </asp:LinkButton>
                        </ItemTemplate>
                        </asp:TemplateField>
                     <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="REGISTRO" Visible="false" ></asp:BoundField>
                     <asp:BoundField DataField="ID" HeaderText=" NRO" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="ID" ReadOnly="True"></asp:BoundField>
                     <asp:BoundField DataField="DETALLE" HeaderText=" DETALLE" HeaderStyle-CssClass="col-sm-3 text text-center" ItemStyle-CssClass="col-sm-3" SortExpression="DETALLE"></asp:BoundField>
                     <asp:BoundField DataField="DETALLE_MONEDA" HeaderText=" MONEDA" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-2" SortExpression="DETALLE_MONEDA"></asp:BoundField>
                     <asp:BoundField DataField="MONTO" HeaderText=" MONTO" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="MONTO"></asp:BoundField>
                     <asp:BoundField DataField="MONTO_USD" HeaderText=" MONTO USD" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="MONTO_USD"></asp:BoundField>
                     <asp:BoundField DataField="MONTO_CLP"  ItemStyle-CssClass="col-sm-1" HeaderStyle-CssClass="col-sm-1 text text-center" HeaderText=" MONTO CLP" SortExpression="MONTO_CLP"></asp:BoundField>
                 </Columns>
                 <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                 <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>
                 <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>
                 <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                 <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>
                 <SortedAscendingHeaderStyle BackColor="#808080"></SortedAscendingHeaderStyle>
                 <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>
                 <SortedDescendingHeaderStyle BackColor="#383838"></SortedDescendingHeaderStyle>
             </asp:GridView>
             <asp:SqlDataSource runat="server" ID="COSTOS_EXENTOS" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_COSTOS_EXENTOS_X_OP" SelectCommandType="StoredProcedure">
               <SelectParameters>
                   <asp:ControlParameter ControlID="TXT_OPERACION" PropertyName="Text" Name="ID_OPERACIONES" Type="Int32"></asp:ControlParameter>
               </SelectParameters>
           </asp:SqlDataSource>
          </div>
        </div>
      </div>
     </div>
   </div>
</div>
            
   <%----------------------------------------------------------%>
   <%---               VENTA AFECTA                       -----%>
   <%----------------------------------------------------------%>
<div class="row">
<div  class="col-sm-3">
    <button type="button" class="btn btn-success"   data-toggle="tooltip" runat="server" id="BTN_CREAR_NUEVA_FACTURA_AFECTA" onserverclick="BTN_CREAR_NUEVA_FACTURA_AFECTA_ServerClick"   title="¿Desea Crear Nueva Factura Afecta?">FACTURAS AFECTA 
    <span class="badge badge-light">
    <asp:Label ID="LBL_CANTIDAD_FACTURAS_AFECTA" runat="server" Text="0"></asp:Label> 
    </span>  
    </button>
</div>
<div  class="col-sm-3">
</div>
<div  class="col-sm-3">
</div>
<div  class="col-sm-3 text-right">
<button type="button" class="btn btn-primary" data-toggle="tooltip" runat="server" id="BTN_REGISTRAR_VENTA_AFECTA" onserverclick="BTN_REGISTRAR_VENTA_AFECTA_ServerClick"  title="Registrar Venta Afecta">AGREGAR <i class="fas fa-plus-circle"></i> </button>
</div>
</div>

<br />
<div class="row">
    <div  class="col-sm-12">
         <div class="panel panel-primary">
      <div class="panel-body">
          <h3><label>VENTA AFECTA</label></h3>
          <div class="row">
            <div class="col-sm-12">  
             <asp:GridView ID="GV_VENTA_AFECTA" CssClass="table-responsive col-sm-12" DataKeyNames="REGISTRO" OnRowCommand="GV_VENTA_AFECTA_RowCommand"  runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Sin Registros" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="VENTAS_AFECTAS" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                 <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                 <Columns>
                        <%----------------------------------------------------------%>
                        <%---               BOTONES ACCION                     -----%>
                        <%----------------------------------------------------------%>
                        <asp:TemplateField HeaderText="ACCION" HeaderStyle-CssClass="col-sm-2 text text-center" ItemStyle-CssClass="col-sm-2">
                        <ItemTemplate>
                        <asp:LinkButton ID="BTN_ELI_VA" runat="server" CssClass="btn btn-danger btn-xs" CommandName="ELIMINAR" CommandArgument='<%# Eval("REGISTRO") %>' ToolTip="¿DESEA ELIMINAR REGISTRO?"> 
                        <i class="fas fa-trash-alt"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="BTN_MOD_VA" runat="server" CssClass="btn btn-primary btn-xs" CommandName="MODIFICAR" CommandArgument='<%# Eval("REGISTRO") %>' ToolTip="¿DESEA MODIFICAR REGISTRO?"> 
                        <i class="fas fa-edit"></i>
                        </asp:LinkButton>
                        </ItemTemplate>
                        </asp:TemplateField>
                     <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="REGISTRO" Visible="false" ></asp:BoundField>
                     <asp:BoundField DataField="ID" HeaderText=" NRO" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="ID" ReadOnly="True"></asp:BoundField>
                     <asp:BoundField DataField="DETALLE" HeaderText=" DETALLE" HeaderStyle-CssClass="col-sm-3 text text-center" ItemStyle-CssClass="col-sm-3" SortExpression="DETALLE"></asp:BoundField>
                     <asp:BoundField DataField="DETALLE_MONEDA" HeaderText=" MONEDA" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-2" SortExpression="DETALLE_MONEDA"></asp:BoundField>
                     <asp:BoundField DataField="MONTO" HeaderText=" MONTO" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="MONTO"></asp:BoundField>
                     <asp:BoundField DataField="MONTO_USD" HeaderText=" MONTO USD" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="MONTO_USD"></asp:BoundField>
                     <asp:BoundField DataField="MONTO_CLP"  ItemStyle-CssClass="col-sm-1" HeaderStyle-CssClass="col-sm-1 text text-center" HeaderText=" MONTO CLP" SortExpression="MONTO_CLP"></asp:BoundField>
                     <asp:BoundField DataField="GRUPO_FACTURA"  ItemStyle-CssClass="col-sm-1" HeaderStyle-CssClass="col-sm-1 text text-center" HeaderText="FACTURA" SortExpression="GRUPO_FACTURA"></asp:BoundField>
                 </Columns>
                 <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                 <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>
                 <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>
                 <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                 <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>
                 <SortedAscendingHeaderStyle BackColor="#808080"></SortedAscendingHeaderStyle>
                 <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>
                 <SortedDescendingHeaderStyle BackColor="#383838"></SortedDescendingHeaderStyle>
             </asp:GridView>
             <asp:SqlDataSource runat="server" ID="VENTAS_AFECTAS" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_VENTAS_AFECTAS_X_OP" SelectCommandType="StoredProcedure">
               <SelectParameters>
                   <asp:ControlParameter ControlID="TXT_OPERACION" PropertyName="Text" Name="ID_OPERACIONES" Type="Int32"></asp:ControlParameter>
               </SelectParameters>
           </asp:SqlDataSource>
       
</div>
      </div>

      </div>
</div>
   </div>
</div>

   <%----------------------------------------------------------%>
   <%---               VENTA EXENTA                       -----%>
   <%----------------------------------------------------------%>

<div class="row">
<div  class="col-sm-3">
       <button type="button" class="btn btn-success"   data-toggle="tooltip" runat="server" id="BTN_CREAR_NUEVA_FACTURA_EXENTA"  onserverclick="BTN_CREAR_NUEVA_FACTURA_EXENTA_ServerClick"  title="¿Desea Crear Nueva Factura Exenta?">FACTURAS EXENTA 
    <span class="badge badge-light">
    <asp:Label ID="LBL_CANTIDAD_FACTURAS_EXENTA" runat="server" Text="0"></asp:Label> 
    </span>  
    </button>
</div>
<div  class="col-sm-3">
</div>
<div  class="col-sm-3">
</div>
<div  class="col-sm-3 text-right">
<button type="button" class="btn btn-primary" data-toggle="tooltip" runat="server" id="BTN_REGISTRAR_VENTA_EXENTA" onserverclick="BTN_REGISTRAR_VENTA_EXENTA_ServerClick" title="Registrar Venta Exenta">AGREGAR <i class="fas fa-plus-circle"></i> </button>
</div>
</div>

<br />

<div class="row">
    <div  class="col-sm-12">
         <div class="panel panel-primary">
      <div class="panel-body">
            <h3><label>VENTA EXENTA</label></h3>
          <div class="row">
            <div class="col-sm-12">  
             <asp:GridView ID="GV_VENTA_EXENTA" CssClass="table-responsive col-sm-12" OnRowCommand="GV_VENTA_EXENTA_RowCommand" DataKeyNames="REGISTRO" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Sin Registros" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="VENTAS_EXENTAS" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                 <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                 <Columns>
                        <%----------------------------------------------------------%>
                        <%---               BOTONES ACCION                     -----%>
                        <%----------------------------------------------------------%>
                        <asp:TemplateField HeaderText="ACCION" HeaderStyle-CssClass="col-sm-2 text text-center" ItemStyle-CssClass="col-sm-2">
                        <ItemTemplate>
                        <asp:LinkButton ID="BTN_ELI_VA" runat="server" CssClass="btn btn-danger btn-xs" CommandName="ELIMINAR" CommandArgument='<%# Eval("REGISTRO") %>' ToolTip="¿DESEA ELIMINAR REGISTRO?"> 
                        <i class="fas fa-trash-alt"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="BTN_MOD_VA" runat="server" CssClass="btn btn-primary btn-xs" CommandName="MODIFICAR" CommandArgument='<%# Eval("REGISTRO") %>' ToolTip="¿DESEA MODIFICAR REGISTRO?"> 
                        <i class="fas fa-edit"></i>
                        </asp:LinkButton>
                        </ItemTemplate>
                        </asp:TemplateField>
                     <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="REGISTRO" Visible="false" ></asp:BoundField>
                     <asp:BoundField DataField="ID" HeaderText=" NRO" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="ID" ReadOnly="True"></asp:BoundField>
                     <asp:BoundField DataField="DETALLE" HeaderText=" DETALLE" HeaderStyle-CssClass="col-sm-3 text text-center" ItemStyle-CssClass="col-sm-3" SortExpression="DETALLE"></asp:BoundField>
                     <asp:BoundField DataField="DETALLE_MONEDA" HeaderText=" MONEDA" HeaderStyle-CssClass="col-sm-2 text text-center" ItemStyle-CssClass="col-sm-2" SortExpression="DETALLE_MONEDA"></asp:BoundField>
                     <asp:BoundField DataField="MONTO" HeaderText=" MONTO" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="MONTO"></asp:BoundField>
                     <asp:BoundField DataField="MONTO_USD" HeaderText=" MONTO USD" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1" SortExpression="MONTO_USD"></asp:BoundField>
                     <asp:BoundField DataField="MONTO_CLP"  ItemStyle-CssClass="col-sm-1" HeaderStyle-CssClass="col-sm-1 text text-center" HeaderText=" MONTO CLP" SortExpression="MONTO_CLP"></asp:BoundField>
                     <asp:BoundField DataField="GRUPO_FACTURA"  ItemStyle-CssClass="col-sm-1" HeaderStyle-CssClass="col-sm-1 text text-center" HeaderText="FACTURA" SortExpression="GRUPO_FACTURA"></asp:BoundField>
                     <asp:BoundField DataField="BL"  ItemStyle-CssClass="col-sm-1" HeaderStyle-CssClass="col-sm-1 text text-center" HeaderText="BL" SortExpression="BL"></asp:BoundField>
                     <asp:BoundField DataField="PRE_COL"  ItemStyle-CssClass="col-sm-1" HeaderStyle-CssClass="col-sm-1 text text-center" HeaderText="PRE/COL" SortExpression="PRE_COL"></asp:BoundField>
                     
                 </Columns>
                 <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                 <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>
                 <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>
                 <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                 <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>
                 <SortedAscendingHeaderStyle BackColor="#808080"></SortedAscendingHeaderStyle>
                 <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>
                 <SortedDescendingHeaderStyle BackColor="#383838"></SortedDescendingHeaderStyle>
             </asp:GridView>
             <asp:SqlDataSource runat="server" ID="VENTAS_EXENTAS" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_VENTAS_EXENTAS_X_OP" SelectCommandType="StoredProcedure">
               <SelectParameters>
                   <asp:ControlParameter ControlID="TXT_OPERACION" PropertyName="Text" Name="ID_OPERACIONES" Type="Int32"></asp:ControlParameter>
               </SelectParameters>
           </asp:SqlDataSource>
       
</div>
      </div>

      </div>
</div>
   </div>
</div>
    

   <%----------------------------------------------------------%>
   <%---               COMISIONES                         -----%>
   <%----------------------------------------------------------%>

<div class="row">
<div  class="col-sm-3">
</div>
<div  class="col-sm-3">
</div>
<div  class="col-sm-3">
</div>
<div  class="col-sm-3 text-right">
<button type="button" class="btn btn-primary" data-toggle="tooltip" runat="server" id="BTN_COMISION"  onserverclick="BTN_COMISION_ServerClick"  title="Registrar Comision">AGREGAR <i class="fas fa-plus-circle"></i> </button>
</div>
</div>

<br />

<div class="row">
    <div  class="col-sm-12">
         <div class="panel panel-primary">
      <div class="panel-body">
         <h3><label>COMISIONES</label></h3>
          <div class="row">
            <div class="col-sm-12">  
             <asp:GridView ID="GV_COMISIONES" CssClass="table-responsive col-sm-12" OnRowCommand="GV_COMISIONES_RowCommand"  DataKeyNames="REGISTRO" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Sin Registros" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="COMISIONES" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                 <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                 <Columns>
                        <%----------------------------------------------------------%>
                        <%---               BOTONES ACCION                     -----%>
                        <%----------------------------------------------------------%>
                        <asp:TemplateField HeaderText="ACCION" HeaderStyle-CssClass="col-sm-1 text text-center" ItemStyle-CssClass="col-sm-1">
                        <ItemTemplate>
                        <asp:LinkButton ID="BTN_ELI_VA" runat="server" CssClass="btn btn-danger btn-xs" CommandName="ELIMINAR" CommandArgument='<%# Eval("REGISTRO") %>' ToolTip="¿DESEA ELIMINAR REGISTRO?"> 
                        <i class="fas fa-trash-alt"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="BTN_MOD_VA" runat="server" CssClass="btn btn-primary btn-xs" CommandName="MODIFICAR" CommandArgument='<%# Eval("REGISTRO") %>' ToolTip="¿DESEA MODIFICAR REGISTRO?"> 
                        <i class="fas fa-edit"></i>
                        </asp:LinkButton>
                        </ItemTemplate>
                        </asp:TemplateField>
                     <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO"  ItemStyle-CssClass="col-sm-1" SortExpression="REGISTRO" Visible="false" ></asp:BoundField>
                     <asp:BoundField DataField="ID" HeaderText=" NRO"  ItemStyle-CssClass="col-sm-2" SortExpression="ID" ReadOnly="True"></asp:BoundField>
                     <asp:BoundField DataField="NOMBRE_COMERCIAL" HeaderText="COMERCIAL"  ItemStyle-CssClass="col-sm-6" SortExpression="NOMBRE_COMERCIAL"></asp:BoundField>
                     <asp:BoundField DataField="PORCENTAJE" HeaderText=" PORCENTAJE"  ItemStyle-CssClass="col-sm-1" SortExpression="PORCENTAJE"></asp:BoundField>
                     <asp:BoundField DataField="MONTO_CLP"  ItemStyle-CssClass="col-sm-1" HeaderText=" MONTO CLP" SortExpression="MONTO_CLP"></asp:BoundField>
                 </Columns>
                 <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                 <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>
                 <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>
                 <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                 <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>
                 <SortedAscendingHeaderStyle BackColor="#808080"></SortedAscendingHeaderStyle>
                 <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>
                 <SortedDescendingHeaderStyle BackColor="#383838"></SortedDescendingHeaderStyle>
             </asp:GridView>
             <asp:SqlDataSource runat="server" ID="COMISIONES" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_COMISIONES_X_OP" SelectCommandType="StoredProcedure">
               <SelectParameters>
                   <asp:ControlParameter ControlID="TXT_OPERACION" PropertyName="Text" Name="ID_OPERACIONES" Type="Int32"></asp:ControlParameter>
               </SelectParameters>
           </asp:SqlDataSource>
       
</div>
      </div>

      </div>
</div>
   </div>
</div>


</div>
        
        
 <div class="row">
       <div class="col-md-8">
            <%---------------------------------------------%>
            <%--          MENSAJE DE ERROR               --%>
            <%---------------------------------------------%>
            <div runat="server" id="CUADRO_ERROR" visible="false" class="alert alert-danger alert-dismissible fade in">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <strong>ERROR DE REGISTRO!</strong> <asp:Label ID="MENSAJE_ERROR" runat="server" Text="Label"></asp:Label> <i class="fas fa-exclamation-triangle"></i> .
            </div>
       </div>
 </div>
        


   <%----------------------------------------------------------%>
   <%---               TOTAL DETALLES                     -----%>
   <%----------------------------------------------------------%>

        <div class="panel panel-primary">
             <div class="panel-body">
                 <h3><label> VALORES TOTALES (CLP)</label></h3>
    
                  <div class="row">
                <div class="col-sm-3">
                    <label>COSTOS EXENTOS :</label> <asp:Label ID="LBL_COSTOS_EXENTOS" Font-Bold="true"  runat="server" Text="0"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <label>COSTOS AFECTOS :</label><asp:Label ID="LBL_COSTOS_AFECTAS" Font-Bold="true"  runat="server" Text="0"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <label>IVA COSTOS(19%):</label> <asp:Label ID="LBL_IVA_COSTOS" runat="server"  Font-Bold="true" Text="0"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <label>TOTAL COSTOS :</label><asp:Label ID="LBL_TOTAL_COSTOS" runat="server" Font-Bold="true"  Text="0"></asp:Label>
                </div>
            </div>
            
                  <div class="row">
                <div class="col-sm-3">
                    <label>VENTAS EXENTAS :</label><asp:Label ID="LBL_VENTAS_EXENTAS" runat="server" Font-Bold="true"  Text="0"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <label>VENTAS AFECTAS :</label><asp:Label ID="LBL_VENTAS_AFECTAS" runat="server" Font-Bold="true"  Text="0"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <label>IVA VENTAS(19%) :</label><asp:Label ID="LBL_IVA_VENTAS" runat="server" Font-Bold="true"  Text="0"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <label>TOTAL VENTAS :</label><asp:Label ID="LBL_TOTAL_VENTAS" runat="server" Font-Bold="true"  Text="0"></asp:Label>
                </div>
            </div>
                 <hr />
                <div class="row">
               <div class="col-sm-3">
                    <label></label>
                </div>
                <div class="col-sm-3">
                    <label></label>
                </div>
                <div class="col-sm-3 text-right">
                    <label>            PROFIT :</label>
                </div>
                <div class="col-sm-3">
                             <asp:TextBox ID="LBL_PROFIT" Enabled= "false" CssClass="form-control" Text="0" runat="server"></asp:TextBox> 
                </div>
            </div>
                 <br />
<div class="row">
    <div class="col-sm-3"><button type="button" class="btn btn-primary" data-toggle="tooltip" id="BTN_IMPRIMIR"  runat="SERVER" visible="false" onserverclick="BTN_IMPRIMIR_ServerClick"  title="IMPRIMIR">IMPRIMIR<i class="fas fa-print"></i></button></div>
    <div class="col-sm-3"><button type="button" class="btn btn-primary" data-toggle="tooltip" id="BTN_LIQUIDAR"  runat="server" visible="false" onserverclick="BTN_LIQUIDAR_ServerClick"  title="LIQUIDAR OPERACION">LIQUIDAR<i class="fas fa-tags"></i></button></div>
    <div class="col-sm-3"><button type="button" class="btn btn-success" data-toggle="tooltip" id="BTN_FACTURAR"  runat="server" visible="false" onserverclick="BTN_FACTURAR_ServerClick"  title="FACTURAR VENTAS">FACTURAR<i class="fas fa-tags"></i></button></div>
    <div class="col-sm-3"><button type="button" class="btn btn-danger" data-toggle="tooltip" id="BTN_HABILITAR" runat="server" visible="false" onserverclick="BTN_HABILITAR_ServerClick"  title="HABILITAR OPERACION">HABILITAR <i class="fas fa-tags"></i></button></div> 
</div>
</div>
</div>
</div>
</div>
           
<script type="text/javascript">
// Material Select Initialization
$(document).ready(function() {
$('.mdb-select').materialSelect();
});
</script>

    
<%----------------------------------------------------------%>
<%---               MODAL REGISTRAR CONCEPTO           -----%>
<%----------------------------------------------------------%>
<div id="modalHtml2" class="modalDialog2" role="dialog">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" runat="server" id="BTN_CERRAR" onserverclick="BTN_CERRAR_ServerClick"  data-dismiss="modal">&times;</button>
        <h4 class="modal-title"><asp:Label ID="LBL_TITULO_MODAL" runat="server" Text="Label"></asp:Label></h4>
      </div>
      <div class="modal-body">
           <asp:Label ID="LBL_TIPO_CONCEPTO" TEXT="0" Visible="false" runat="server" ></asp:Label>
           <asp:Label ID="LBL_NRO_REGISTRO" TEXT="0" Visible="false" runat="server" ></asp:Label>
   <div class="row">
    <div class="col-sm-12">    
        <div class="form-group">
        <label for="pwd">CONCEPTO:</label>
        <div class="input-group">
        <span class="input-group-addon"><i class="fas fa-clipboard-list"></i></span>
        <asp:DropDownList ID="DDL_CONCEPTO" data-live-search="true" data-live-search-style="startsWith" class="form-control selectpicker" runat="server" DataSourceID="LIST_CONCEPTOS" DataTextField="DETALLE" DataValueField="ID_CONCEPTO"></asp:DropDownList>
        <asp:SqlDataSource runat="server" ID="LIST_CONCEPTOS" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_CONCEPTO" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="LBL_TIPO_CONCEPTO" PropertyName="Text" Name="TIPO_CONCEPTO" Type="Int32"></asp:ControlParameter>
            </SelectParameters>
        </asp:SqlDataSource>
        </div>
        </div>
    </div>
    </div>
    
    <div class="row">
    <div class="col-sm-12"> 
                     <div class="form-group">
                     <label for="pwd">MONEDA:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-user-edit"></i></span>
                     <asp:DropDownList ID="DDL_MONEDA" data-live-search="true" data-live-search-style="startsWith" class="form-control selectpicker" runat="server" DataSourceID="LIST_MONEDAS" DataTextField="DETALLE_MONEDA" DataValueField="ID_MONEDA"></asp:DropDownList>
                     <asp:SqlDataSource runat="server" ID="LIST_MONEDAS" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SELECT ID_MONEDA,DETALLE_MONEDA FROM MONEDA WITH(NOLOCK) ORDER BY DETALLE_MONEDA ASC"></asp:SqlDataSource>
                     </div>
                     </div>
    </div>
    </div>
    
    <div class="row">
         <div class="col-sm-12"><div class="form-group">
                     <label for="pwd">MONTO:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-globe-asia"></i></span>
                     <asp:TextBox ID="TXT_MONTO" runat="server" class="form-control" name="password" placeholder="(OBLIGATORIO)" REQUIRED></asp:TextBox>
                     </div>
                     </div>
         </div>
    </div>
 
   
    <div class="row">
    <div class="col-sm-12">
    <div runat="server" visible="false" id="DIV_ASOCIAR_FACTURA">    
              <div class="form-group">
              <label for="pwd">ASOCIAR FACTURA:</label>
              <div class="input-group">
              <span class="input-group-addon"><i class="fas fa-user-edit"></i></span>
                  <asp:DropDownList ID="DDL_FACTURA" data-live-search="true" data-live-search-style="startsWith" class="form-control selectpicker" DataTextField="GRUPO_FACTURA" DataValueField="ID_FACTURA" runat="server"></asp:DropDownList>
              </div>
              </div>
    </div>
    </div>
    </div>

    <div class="row">
    <div class="col-sm-12">
      <div runat="server" visible="false" id="DIV_CHK_BL">    
      <div class="form-group">

<div class="row">
    <div class="col-sm-4">
        <label for="pwd">ASIGNAR A BL:</label>
        <div class="input-group">
        <input runat="server" id="CHK_BL" data-on="SI" data-off="NO" type="checkbox"  data-toggle="toggle" data-style="slow">
        </div>
    </div>
    <div class="col-sm-4">
        <label for="pwd">PREPAID/COLLECT</label>
        <div class="input-group">
        <input runat="server" id="CHK_PREPAID_COLLECT" data-on="PREPAID" data-off="COLLECT" type="checkbox"  data-toggle="toggle" data-style="slow">
        </div>
    </div>
</div>
      



      </div>
      </div>
    </div>
    </div>

      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" id="BTN_REGISTRAR_CONCEPTO"  runat="server" onserverclick="BTN_REGISTRAR_CONCEPTO_ServerClick" data-dismiss="modal">REGISTRAR</button>
      </div>
    </div>

  </div>
</div>


<%----------------------------------------------------------%>
<%---               MODAL REGISTRAR COMISION           -----%>
<%----------------------------------------------------------%>

<div id="modalHtml" class="modalDialog2" role="dialog">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" runat="server" id="Button1" onserverclick="BTN_CERRAR_ServerClick"  data-dismiss="modal">&times;</button>
        <h4 class="modal-title">REGISTRAR COMISION</h4>
          </div>
            <div class="modal-body">
                <div class="row">
                <div class="col-sm-12"> 
                    <div class="form-group">
                     <label for="pwd">COMERCIAL:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-user-tag"></i></span>
                     <asp:DropDownList ID="DDL_COMERCIAL" data-live-search="true" data-live-search-style="startsWith" class="form-control selectpicker" runat="server" DataSourceID="LIST_COMERCIAL" DataTextField="NOMBRE_COMERCIAL" DataValueField="ID_COMERCIAL"></asp:DropDownList>
                     <asp:SqlDataSource runat="server" ID="LIST_COMERCIAL" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SELECT ID_COMERCIAL,NOMBRE_COMERCIAL FROM COMERCIAL WITH(NOLOCK) WHERE ESTADO = 1 ORDER BY NOMBRE_COMERCIAL ASC"></asp:SqlDataSource>
                     </div>
                     </div>
                </div>
                </div>
    
                <div class="row">
                <div class="col-sm-12"> 
                     <div class="form-group">
                     <label for="pwd">PORCENTAJE:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-globe-asia"></i></span>
                     <asp:TextBox ID="TXT_PORCENTAJE" runat="server" class="form-control" name="password" TextMode="Number" placeholder="(OBLIGATORIO)" REQUIRED></asp:TextBox>
                     </div>
                     </div>
                </div>
                </div>
    
               </div>
               <div class="modal-footer">
                <button type="button" class="btn btn-default" id="BTN_REGISTRAR_COMISION" onserverclick="BTN_REGISTRAR_COMISION_ServerClick"  runat="server"  data-dismiss="modal">REGISTRAR</button>
               </div>
    </div>
  </div>
</div>
</asp:Content>
