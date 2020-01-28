<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Pago_Comisiones_Proveedores.aspx.cs" Inherits="Operaciones.Pago_Comisiones_Proveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/js/bootstrap-select.js"></script>
    <link href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" rel="stylesheet">
    <script src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <div class="container">

           <%---------------------------------------------%>
           <%--     BUSCAR CC  PENDIENTES               --%>
           <%---------------------------------------------%>      
            <div class="row">
            <div class="col-sm-12">
            <div class="panel panel-primary">
            <div class="panel-heading">PAGO COMISIONES X PROVEEDOR</div>
            <div class="panel-body">
                <div class="row">
                <div class="col-sm-2"></div>
                <div class="col-sm-4">
                     <div class="form-group">
                     <label for="pwd">CLIENTES CON CC PROVEEDORES PENDIENTES:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-user-check"></i></span>
                     <asp:DropDownList ID="DDL_CLIENTE" data-live-search="true" data-live-search-style="startsWith" class="form-control selectpicker" runat="server" DataSourceID="LISTADO_CLIENTES_CC_PENDIENTES" DataTextField="CLIENTE" DataValueField="ID_CLIENTE"></asp:DropDownList>
                     <asp:SqlDataSource runat="server" ID="LISTADO_CLIENTES_CC_PENDIENTES" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_OP_CLIENTES_COMISIONES_PENDIENTES" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                     </div>
                     </div>
                </div>
                <div class="col-sm-3">
                     <label for="pwd"></label>
                     <div class="input-group">
                     <button type="button" id="BTN_FILTRAR_CC" runat="server" onserverclick="BTN_FILTRAR_CC_ServerClick" class="btn btn-primary">FILTRAR <i class="fas fa-search"></i></button>
                     </div>
                </div>
                <div class="col-sm-3"></div>
                </div>
            </div>
            </div>
            </div>
            </div>

            <div class="row">
            <div class="col-sm-12">
            <div class="panel panel-primary">
            <div class="panel-heading">LISTADO DE COMISIONES X PROVEEDOR</div>
            <div class="panel-body">
            <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#menu1">PAGOS PENDIENTES</a></li>   
            <li><a data-toggle="tab" href="#menu2">HISTORIAL DE PAGOS</a></li>
            </ul>
            <div class="tab-content">
            <div id="menu1" class="tab-pane fade in active">
             

              <%---------------------------------------------%>
              <%--      COMISIONES PENDIENTES              --%>
              <%---------------------------------------------%>
              <hr />
                <div class="row">
                <div class="col-sm-12">
                <div class="panel panel-default">
                <div class="panel-heading">COMISIONES PENDIENTES </div>
                <div class="panel-body">      
                    <div runat="server" id="SELECCIONAR_OPERACION" visible="false">
                      <div class="row">
                           <div class="col-sm-3">
                                 <div class="form-group">
                                 <label for="pwd">OPERACIONES CON CC PENDIENTES:</label>
                                 <div class="input-group">
                                 <span class="input-group-addon"><i class="fas fa-user-check"></i></span>
                                     <asp:DropDownList ID="DDL_NRO_OPERACION" runat="server" OnSelectedIndexChanged="DDL_NRO_OPERACION_SelectedIndexChanged" data-live-search="true" AutoPostBack="true" data-live-search-style="startsWith" class="form-control selectpicker" DataSourceID="LISTADO_OP_CC_PENDIENTES" DataTextField="ID_OPERACIONES" DataValueField="ID_OPERACIONES"></asp:DropDownList>
                                     <asp:SqlDataSource runat="server" ID="LISTADO_OP_CC_PENDIENTES" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_OP_COMISIONES_PENDIENTES" SelectCommandType="StoredProcedure">
                                         <SelectParameters>
                                             <asp:ControlParameter ControlID="DDL_CLIENTE" PropertyName="SelectedValue" Name="ID_CLIENTE" Type="Int32"></asp:ControlParameter>
                                         </SelectParameters>
                                     </asp:SqlDataSource>
                                 </div>
                                 </div>
                            </div>
                         
                           <div class="col-sm-8">
                               <div class ="row">
                                     <div class="col-sm-6">
                                         <div class="form-group">
                                         <label for="pwd"> </label>
                                         <div class="input-group">
                                            <asp:Button ID="BTN_VER_VALORACION" ToolTip="¿ VER DETALLE DE VALORACION ?" data-toggle="tooltip" OnClick="BTN_VER_VALORACION_Click"  class=" btn  btn-success" runat="server" Text='VALORACION' /> 
                                         </div>
                                         </div>
                                     </div>
                                     <div class="col-sm-6">
                                          <div class="form-group">
                                         <label for="pwd"> </label>
                                         <div class="input-group">
                                         <asp:Button ID="BTN_VER_OPERACION" ToolTip="¿ VER DETALLE DE OPERACION  ?" data-toggle="tooltip" OnClick="BTN_VER_OPERACION_Click"  class=" btn btn-primary" runat="server" Text='OPERACION'/>  
                                         </div>
                                         </div>
                                     </div>
                               </div>
                           </div>
                       </div>   
                    </div> 
                </div>
                </div>
                </div>
                </div>

            <hr />
     
      
    

                <%---------------------------------------------%>
                <%--      REGISTRAR  PAGO CC                 --%>
                <%---------------------------------------------%>
                <hr />         
                <div class="row">
          <div class="col-sm-12">
          <div class="panel panel-default">
          <div class="panel-heading">REGISTRAR PAGO DE COMISIONES PROVEEDORES</div>
          <div class="panel-body">      
              <div class="row">                   
                      <div class="col-sm-6">
                          <div class="form-group">
                          <label for="pwd">FECHA PAGO:</label>
                          <div class="input-group">
                          <span class="input-group-addon"><i class="fas fa-calendar-alt"></i></span>
                          <asp:TextBox ID="TXT_FECHA_PAGO" runat="server" class="form-control" TextMode="Date"  placeholder="(OBLIGATORIO)" ></asp:TextBox>
                          </div>
                          </div>
                      </div> 
                      <div class="col-sm-6">
                          <div class="form-group">
                          <label for="pwd">MONTO A PAGAR:</label>
                          <div class="input-group">
                          <span class="input-group-addon"><i class="fas fa-hand-holding-usd"></i></span>
                          <asp:TextBox ID="TXT_MONTO" runat="server" class="form-control" TextMode="Number"  placeholder="(OBLIGATORIO)"></asp:TextBox>
                          </div>
                          </div>
                      </div> 
              </div> 
              <div class="row">
                      <div class="col-sm-12">
                          <div class="form-group">
                          <label for="pwd">DETALLE DE PAGO:</label>
                          <div class="input-group">
                          <span class="input-group-addon"><i class="fas fa-file-alt"></i></span>
                          <asp:TextBox ID="TXT_DETALLE_PAGO" runat="server" Rows="6" class="form-control" TextMode="MultiLine" placeholder="(OBLIGATORIO)"></asp:TextBox>
                          </div>
                          </div>   
                      </div> 
                   </div>
              <div class="row">
              <div class="col-sm-4"></div>  
              <div class="col-sm-4"></div>   
              <div class="col-sm-4">
                     <label for="pwd"></label>
                     <div class="input-group">
                     <button type="button" id="BTN_REGISTRAR_PAGO" onserverclick="BTN_REGISTRAR_PAGO_ServerClick" runat="server" ToolTip="REGISTRAR PAGO" data-toggle="tooltip"  class="btn btn-primary">REGISTRAR PAGO <i class="fas fa-money-check-alt"></i></button>
                     </div>  
              </div>                     
              </div>  

          </div>
          </div>
          </div>
          </div>


                <div class="row">
                  <div class="col-md-12">
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






                <hr />
                <div class="row">
            <div class="col-sm-12">
            <div class="panel panel-default">
            <div class="panel-heading">LISTADO DE ABONOS</div>
            <div class="panel-body">

              <div class="row">
              <div class="col-sm-4">
              <button type="button" class="btn btn-primary btn-lg" visible="false" data-toggle="tooltip" title="Descargar a .csv" runat="server" id="Button2"> DESCARGAR <i class="fas fa-file-csv"></i></button>
              </div>
              <div class="col-sm-4"></div>
              <div class="col-sm-4"><h4>BUSCAR.. <i class="fas fa-search"></i>  <input class="form-control btn-sm" id="myInput_1" type="text" placeholder="Search.."></h4></div>
              </div>
              <div class="row">
              <div class="table-responsive">          
              <table class="table table-sm table-striped  table-hover">
              <thead>
              <tr>
              <th class="small col-sm-2" >FECHA PAGO</th>
              <th class="small col-sm-8" >DETALLE</th>
              <th class="small col-sm-1" >MONTO</th>     
              <th class="small col-sm-1" >ACCIONES</th>
              </tr>
              </thead>
              <tbody id="myTable_1">
                  <asp:Repeater ID="RPT_LISTADO_ABONOS" runat="server" OnItemCommand="RPT_LISTADO_ABONOS_ItemCommand" DataSourceID="PAGOS_ABONOS_PROVEEDOR_X_OP">
                      <ItemTemplate>
                          <tr>
                              <td class="small col-sm-2"><%# Eval("FECHA_PAGO") %></td>
                              <td class="small col-sm-8"><%# Eval("DETALLE") %></td>
                              <td class="small col-sm-1"><%# Eval("MONTO") %></td>
                              <td class="small col-sm-1">
                                  <asp:Button ID="BTN_ELIMINAR_ABONO" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID_PAGO") %>' ToolTip="¿ELIMINAR ABONO?" data-toggle="tooltip" class=" btn btn-danger" runat="server" Text='X' /></td>
                          </tr>
                      </ItemTemplate>
                  </asp:Repeater>
                  <asp:SqlDataSource runat="server" ID="PAGOS_ABONOS_PROVEEDOR_X_OP" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_PAGOS_COMISIONES_PENDIENTES_PROVEEDOR_X_OP" SelectCommandType="StoredProcedure">
                      <SelectParameters>
                          <asp:ControlParameter ControlID="DDL_NRO_OPERACION" PropertyName="SelectedValue" DefaultValue="0" Name="ID_OPERACIONES" Type="Int32"></asp:ControlParameter>
                      </SelectParameters>
                  </asp:SqlDataSource>
              </tbody>
              </table>
          
              </div>
     </div>  

            </div>
            </div>
            </div>
 </div>





     </div>


     <div id="menu2" class="tab-pane fade">
           <%---------------------------------------------%>
           <%--      HISTORIAL  PAGO CC                 --%>
           <%---------------------------------------------%>
         <hr />
            <div class="row">
            <div class="col-sm-12">
            <div class="panel panel-default">
            <div class="panel-heading">LISTADO DE LOS ULTIMOS PAGOS</div>
            <div class="panel-body">
            <div class="row">
            <div class="col-sm-4">
            <button type="button" class="btn btn-primary btn-lg" visible="false" data-toggle="tooltip" title="Descargar a .csv" runat="server" id="Button1"> DESCARGAR <i class="fas fa-file-csv"></i></button>
            </div>
            <div class="col-sm-4"></div>
            <div class="col-sm-4"><h4>BUSCAR.. <i class="fas fa-search"></i>  <input class="form-control btn-sm" id="myInput_2" type="text" placeholder="Search.."></h4></div>
            </div>
            <div class="row">
            <div class="table-responsive">          
            <table class="table table-sm table-striped  table-hover">
            <thead>
            <tr>
            <th class="small col-sm-1" >VER</th>
            <th class="small col-sm-1" >OPERACION</th> 
            <th class="small col-sm-2" >FECHA PAGO</th>
            <th class="small col-sm-5" >DETALLE</th>
            <th class="small col-sm-1" >MONTO</th>     
            <th class="small col-sm-1" >ACCIONES</th>
            </tr>
            </thead>
            <tbody id="myTable_2">
                <asp:Repeater ID="RPT_ULTIMOS_PAGOS_PROVEEDOR" OnItemCommand="RPT_ULTIMOS_PAGOS_PROVEEDOR_ItemCommand" runat="server" DataSourceID="ULTIMOS_PAGOS_PROVEEDOR">
                    <ItemTemplate>
                        <tr>
                            <td class="small col-sm-1">
                                <asp:Button ID="BTN_VER_VALORACION" CommandName="VALORACION" CommandArgument='<%# Eval("OPERACION") %>'  ToolTip="¿ VER DETALLE DE VALORACION ?" data-toggle="tooltip"   class=" btn  btn-success" runat="server" Text='V' /> 
                                <asp:Button ID="BTN_VER_OPERACION" CommandName="OPERACION" CommandArgument='<%# Eval("OPERACION") %>'  ToolTip="¿ VER DETALLE DE OPERACION  ?" data-toggle="tooltip"  class=" btn btn-primary" runat="server" Text='O'/>  
                            </td>
                            <td class="small col-sm-1"><%# Eval("OPERACION") %></td>
                            <td class="small col-sm-2"><%# Eval("FECHA") %></td>
                            <td class="small col-sm-5"><%# Eval("DETALLE") %></td>
                            <td class="small col-sm-1"><%# Eval("MONTO") %></td>
                            <td class="small col-sm-1">
                                <asp:Button ID="BTN_ELIMINAR_PAGO" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID_PAGO") %>' ToolTip="¿ELIMINAR PAGO?" data-toggle="tooltip" class=" btn btn-danger" runat="server" Text='X' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:SqlDataSource runat="server" ID="ULTIMOS_PAGOS_PROVEEDOR" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_PAGO_COMISIONES_PROVEEDOR_X_FECHA" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
            </tbody>
            </table>
            </div>
            </div>  
            </div>
            </div>
            </div>
            </div>
     </div>
              
            
                   



   </div>





   </div>


                




            </div>



                 <div class="row">
               <div class="col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">DETALLE DE MONTOS TOTALES</div>
                <div class="panel-body">
                  <div class="row">
                    <div class="col-sm-4">
                           <div class="form-group">
                          <label for="pwd">MONTO TOTAL DE PAGOS:</label>
                          <div class="input-group">
                          <span class="input-group-addon"><i class="fas fa-file-alt"></i></span>
                          <asp:TextBox ID="TXT_MONTO_PAGOS" runat="server"  class="form-control"  placeholder="(OBLIGATORIO)" DISABLED></asp:TextBox>
                          </div>
                          </div> 
                    </div>  
                         <div class="col-sm-4">
                           <div class="form-group">
                          <label for="pwd">MONTO TOTAL DE COMISION:</label>
                          <div class="input-group">
                          <span class="input-group-addon"><i class="fas fa-file-alt"></i></span>
                          <asp:TextBox ID="TXT_MONTO_COMISIONES" runat="server"  class="form-control" placeholder="(OBLIGATORIO)" DISABLED></asp:TextBox>
                          </div>
                          </div> 
                    </div> 
                         <div class="col-sm-4">
                           <div class="form-group">
                          <label for="pwd">SALDO PENDIENTE:</label>
                          <div class="input-group">
                          <span class="input-group-addon"><i class="fas fa-file-alt"></i></span>
                          <asp:TextBox ID="TXT_SALDOS" runat="server" class="form-control"  placeholder="(OBLIGATORIO)" DISABLED></asp:TextBox>
                          </div>
                          </div> 
                    </div> 

                  </div>  

                </div>
            </div>
        </div>
</div>






            </div>
  
        
    
    </div>










    

    </div>







    <script>
        $(document).ready(function(){
          $("#myInput_1").on("keyup", function() {
            var value = $(this).val().toLowerCase();
            $("#myTable_1 tr").filter(function() {
              $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
          });
        });
        </script>

 <script>
        $(document).ready(function(){
          $("#myInput_2").on("keyup", function() {
            var value = $(this).val().toLowerCase();
            $("#myTable_2 tr").filter(function() {
              $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
          });
        });
        </script>
     <script>
        $(document).ready(function(){
          $("#myInput_3").on("keyup", function() {
            var value = $(this).val().toLowerCase();
            $("#myTable_3 tr").filter(function() {
              $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
          });
        });
        </script>

            
    
</asp:Content>
