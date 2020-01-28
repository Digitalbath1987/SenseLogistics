<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Reporte_Profit.aspx.cs" Inherits="Operaciones.Reporte_Profit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
   <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/js/bootstrap-select.js"></script>
   <link href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" rel="stylesheet">
   <script src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <div class="panel-group">
    <div class="panel panel-primary">
    <div class="panel-heading text-bold"><h4> REPORTE DE PROFIT X FECHA DE OPERACION</h4> </div>
    <div class="panel-body">
        <hr/>    
        <div class="row">
             <div class="col-md-4">
             <div class =" row"> 
             <div class="col-sm-12">
                          <div class="form-group">
                          <label for="pwd">FECHAS:</label>
                          <div class="input-group">
                          <span class="input-group-addon"></span>
                          <asp:TextBox ID="TXT_FECHA_DESDE" runat="server" TextMode="Date" class="form-control" ></asp:TextBox>
                          <asp:TextBox ID="TXT_FECHA_HASTA" runat="server" TextMode="Date"  class="form-control" ></asp:TextBox>
                          </div>
                          </div>
                </div> 
             </div>
             <div class="row">
             <div class="col-sm-12">
                         <div class="form-group">
                         <label for="pwd">CLIENTE:</label>
                         <div class="input-group">
                         <span class="input-group-addon"><i class="fas fa-user-check"></i></span>
                         <asp:DropDownList ID="DDL_CLIENTE" data-live-search="true" data-live-search-style="startsWith" class="form-control selectpicker" runat="server" DataSourceID="LISTADO_CLIENTES" DataTextField="NOMBRE" DataValueField="ID_CLIENTE"></asp:DropDownList>

                             <asp:SqlDataSource runat="server" ID="LISTADO_CLIENTES" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_CLIENTE_LISTADO" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                         </div>
                         </div>
                    </div>
             </div>
             <div class="row">
             <div class="col-sm-2">
                  <div class="form-group">
                    <asp:Button ID="BTN_FILTRAR_FECHAS" CssClass="btn btn-success" runat="server" OnClick="BTN_FILTRAR_FECHAS_Click" Text="FILTRAR" data-toggle="tooltip" title="Click Para Filtrar" /> 
                </div>
             </div>
             </div>
             </div>
             <div class="col-md-4"></div>
             <div class="col-md-4">
              <div class="row">
                    <div class="col-sm-6">
                           <div class="form-group">
                          <label for="pwd">V. EXENTAS:</label>
                          <div class="input-group">
                          <span class="input-group-addon"><i class="fas fa-file-alt"></i></span>
                          <asp:TextBox ID="TXT_MONTO_VENTAS_EXENTAS" runat="server"  class="form-control"  placeholder="0" DISABLED></asp:TextBox>
                          </div>
                          </div> 
                    </div> 
                    <div class="col-sm-6">
                           <div class="form-group">
                          <label for="pwd">V. AFECTAS:</label>
                          <div class="input-group">
                          <span class="input-group-addon"><i class="fas fa-file-alt"></i></span>
                          <asp:TextBox ID="TXT_MONTO_VENTAS_AFECTAS" runat="server"  class="form-control"  placeholder="0" DISABLED></asp:TextBox>
                          </div>
                          </div> 
                    </div> 
                  </div>  

                    <div class="row">
                    <div class="col-sm-6">
                           <div class="form-group">
                          <label for="pwd">C. EXENTOS:</label>
                          <div class="input-group">
                          <span class="input-group-addon"><i class="fas fa-file-alt"></i></span>
                          <asp:TextBox ID="TXT_MONTO_COSTOS_EXENTOS" runat="server"  class="form-control"  placeholder="0" DISABLED></asp:TextBox>
                          </div>
                          </div> 
                    </div> 
                    <div class="col-sm-6">
                           <div class="form-group">
                          <label for="pwd">C. AFECTOS:</label>
                          <div class="input-group">
                          <span class="input-group-addon"><i class="fas fa-file-alt"></i></span>
                          <asp:TextBox ID="TXT_MONTO_COSTOS_AFECTOS" runat="server"  class="form-control"  placeholder="0" DISABLED></asp:TextBox>
                          </div>
                          </div> 
                    </div> 
                  </div> 
              <div class="row">
                    <div class="col-sm-6">
                           <div class="form-group">
                          <label for="pwd">PROFIT TOTAL:</label>
                          <div class="input-group">
                          <span class="input-group-addon"><i class="fas fa-file-alt"></i></span>
                          <asp:TextBox ID="TXT_MONTO_PROFIT_TOTAL" runat="server"  class="form-control"  placeholder="0" DISABLED></asp:TextBox>
                          </div>
                          </div> 
                    </div> 


                  </div> 
              			




     </div>

       </div>
        <hr/>    
        <div class="row">
       <div class="col-md-8">
                  

                   <%---------------------------------------------%>
                   <%--          MENSAJE DE ERROR                --%>
                   <%---------------------------------------------%>
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

        
        <div class="row">
           <div class="col-sm-4">
               <button type="button" class="btn btn-primary btn-lg" visible="false" data-toggle="tooltip" title="Descargar a .csv" runat="server" id="BTN_DESCARGAR_CSV" onserverclick="BTN_DESCARGAR_CSV_ServerClick"  > DESCARGAR <i class="fas fa-file-csv"></i></button>

           </div>
           <div class="col-sm-4"></div>
           <div class="col-sm-4"><h4>BUSCAR.. <i class="fas fa-search"></i>  <input class="form-control btn-sm" id="myInput" type="text" placeholder="Search.."></h4></div>
        </div>
        <div class="row">
             <div class="col-sm-12">
              <div class="table-responsive">          
              <table class="table table-sm table-striped  table-hover">
              <thead>
              <tr>
              <th class="small" >NRO OPERACION</th>
              <th class="small" >AEREO</th>
              <th class="small" >MARITIMO</th>
              <th class="small" >T INTERNACIONAL</th>
              <th class="small" >T NACIONAL</th>
              <th class="small" >A.G.A.</th>
              <th class="small" >SEGURO</th>
<%--              <th class="small" >FECHA OPERACION</th>
              <th class="small" >FECHA CREACION</th>
              <th class="small" >FECHA PAGO</th>
              <th class="small" >USD</th>
              <th class="small" >EUR</th>
              <th class="small" >GBP</th>--%>
              <th class="small" >CLIENTE</th>
              <th class="small" >RUT</th>
              <th class="small" >V. EXENTAS</th>
              <th class="small" >V. AFECTAS</th>
              <th class="small" >C. EXENTOS</th>
              <th class="small" >C. AFECTOS</th>
              <th class="small" >PROFIT</th>
              <th class="small" >ACCIONES</th>    
              </tr>
              </thead>
              <tbody id="myTable">
              <asp:Repeater ID="RPT_REPORTE"  OnItemCommand="RPT_REPORTE_ItemCommand" runat="server" >
              <ItemTemplate>
              <tr>
                <td class="small text-bold" ><%# Eval("NRO_OPERACION") %></td>
                <td class="small text-bold" ><%# Eval("AEREO") %></td>
                <td class="small text-bold" ><%# Eval("MARITIMO") %></td>
                <td class="small text-bold" ><%# Eval("T_INTERNACIONAL") %></td>
                <td class="small text-bold" ><%# Eval("T_NACIONAL") %></td>
                <td class="small text-bold" ><%# Eval("AGA") %></td>
                <td class="small text-bold" ><%# Eval("SEGURO") %></td>
    <%--            <td class="small text-bold" ><%# Eval("FECHA_OPERACION") %></td>
                <td class="small text-bold" ><%# Eval("FECHA_CREACION") %></td>
                <td class="small text-bold" ><%# Eval("FECHA_PAGO") %></td>
                <td class="small text-bold" ><%# Eval("VALOR_USD") %></td>
                <td class="small text-bold" ><%# Eval("VALOR_EUR") %></td>
                <td class="small text-bold" ><%# Eval("VALOR_GBP") %></td>--%>
                <td class="small text-bold" ><%# Eval("CLIENTE") %></td>
                <td class="small text-bold" ><%# Eval("RUT") %></td>
                <td class="small text-bold" ><%# Eval("VENTAS_EXENTAS") %></td>
                <td class="small text-bold" ><%# Eval("VENTAS_AFECTAS") %></td>
                <td class="small text-bold" ><%# Eval("COSTOS_EXENTOS") %></td>
                <td class="small text-bold" ><%# Eval("COSTOS_AFECTOS") %></td>
                <td class="small text-bold" ><%# Eval("PROFIT") %></td>
                <td class="small hidden-sm col-sm-1"><asp:Button ID="BTN_VER_VALORACION" CommandName="VALORACION" CommandArgument='<%# Eval("NRO_OPERACION") %>'  ToolTip="¿ VER DETALLE DE VALORACION ?" data-toggle="tooltip"   class=" btn  btn-success" runat="server" Text='V' /> <asp:Button ID="BTN_VER_OPERACION" CommandName="OPERACION" CommandArgument='<%# Eval("NRO_OPERACION") %>'  ToolTip="¿ VER DETALLE DE OPERACION  ?" data-toggle="tooltip"  class=" btn btn-primary" runat="server" Text='O'/>  </td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
              </tbody>
              </table>
              </div>
              </div>
           </div>
        
    
    
    
    </div>
    </div>
   </div>
      <script>
        $(document).ready(function(){
          $("#myInput").on("keyup", function() {
            var value = $(this).val().toLowerCase();
            $("#myTable tr").filter(function() {
              $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
          });
        });
        </script>
       <script type="text/javascript">

        // Material Select Initialization
$(document).ready(function() {
$('.mdb-select').materialSelect();
});

    </script>

</asp:Content>
