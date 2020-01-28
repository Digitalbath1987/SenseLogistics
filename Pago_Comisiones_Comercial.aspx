<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Pago_Comisiones_Comercial.aspx.cs" Inherits="Operaciones.Pago_Comisiones_Comercial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/js/bootstrap-select.js"></script>
    <link href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" rel="stylesheet">
    <script src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <div class="container">

        <%---------------------------------------------%>
        <%--          CUADRO BUSCAR COMERCIAL        --%>
        <%---------------------------------------------%>
        <div class="row">
            <div class="panel panel-primary">
                <div class="panel-heading">PAGO DE COMISIONES X COMERCIAL</div>
                <div class="panel-body">
                    <div class="col-sm-3">
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label for="pwd">COMERCIAL:</label>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fas fa-user-tag"></i></span>
                                <asp:DropDownList ID="DDL_COMERCIAL" data-live-search="true" data-live-search-style="startsWith" class="form-control selectpicker" runat="server" DataSourceID="LIST_COMERCIAL" DataTextField="NOMBRE_COMERCIAL" DataValueField="ID_COMERCIAL"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="LIST_COMERCIAL" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SELECT ID_COMERCIAL,NOMBRE_COMERCIAL FROM COMERCIAL WITH(NOLOCK) WHERE ESTADO = 1 ORDER BY NOMBRE_COMERCIAL ASC"></asp:SqlDataSource>
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

        <%---------------------------------------------%>
        <%--       CUADRO LISTADO DE COMISIONES      --%>
        <%---------------------------------------------%>
        <div class="row">
            <div class="panel panel-primary">
            <div class="panel-heading">LISTADO COMISIONES X COMERCIAL</div>
            <div class="panel-body">
            <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#menu2">MOVIMIENTOS DE PAGOS</a></li>   
            <li><a data-toggle="tab" href="#menu1">COMISIONES PAGADAS</a></li>
            </ul>

              <div class="tab-content">
              <div id="menu1" class="tab-pane fade">
              <%---------------------------------------------%>
              <%--       COMISIONES PAGADAS                --%>
              <%---------------------------------------------%>
              <div class="row">
              <div class="col-sm-4">
              <button type="button" class="btn btn-primary btn-lg" visible="false" data-toggle="tooltip" title="Descargar a .csv" runat="server" id="Button1"> DESCARGAR <i class="fas fa-file-csv"></i></button>
              </div>
              <div class="col-sm-4"></div>
              <div class="col-sm-4"><h4>BUSCAR.. <i class="fas fa-search"></i>  <input class="form-control btn-sm" id="myInput_2" type="text" placeholder="Search.."></h4></div>
              </div>
              <div class="row">
                  <div class="col-sm-12">
              <div class="table-responsive">          
              <table class="table table-sm table-striped  table-hover">
              <thead>
              <tr>
              <th class="small col-sm-1" >NRO OP.</th>
              <th class="small col-sm-1" >COMERCIAL</th>
              <th class="small col-sm-2" >CLIENTE</th>
              <th class="small col-sm-1" >RUT</th>
              <th class="small col-sm-1" >FECHA</th>
              <th class="small col-sm-3" >DETALLE</th>
              <th class="small col-sm-1" >MONTO</th>
              <th class="small col-sm-2" >ACCIONES</th>
              </tr>
              </thead>
              <tbody id="myTable_2">
              <asp:Repeater ID="RPT_COMISIONES_PAGADAS_X_COMERCIAL" OnItemCommand="RPT_COMISIONES_PAGADAS_X_COMERCIAL_ItemCommand"  runat="server" DataSourceID="COMISIONES_PAGADAS_X_COMERCIAL">
              <ItemTemplate>
              <tr>
              <td class="small text-bold  col-sm-1" ><%# Eval("ID_OPERACIONES") %></td>
              <td class="small col-sm-1" ><%# Eval("NOMBRE_COMERCIAL") %></td>
              <td class="small col-sm-2" ><%# Eval("NOMBRE_CLIENTE") %></td>
              <td class="small col-sm-1" ><%# Eval("RUT") %></td>
              <td class="small col-sm-1" ><%# Eval("FECHA_PAGO") %></td>
              <td class="small col-sm-3" ><%# Eval("DETALLE") %></td>
              <td class="small col-sm-1" ><%# Eval("MONTO_PAGO") %></td>
               <td class="small hidden-sm col-sm-2">
                  <asp:Button ID="BTN_VER_VALORACION" CommandName="VALORACION" CommandArgument='<%# Eval("ID_OPERACIONES") %>'  ToolTip="¿ VER DETALLE DE VALORACION ?" data-toggle="tooltip"   class=" btn  btn-success" runat="server" Text='V' /> 
                  <asp:Button ID="BTN_VER_OPERACION" CommandName="OPERACION" CommandArgument='<%# Eval("ID_OPERACIONES") %>'  ToolTip="¿ VER DETALLE DE OPERACION  ?" data-toggle="tooltip"  class=" btn btn-primary" runat="server" Text='O'/> 
                  <asp:Button ID="BTN_ELIMINAR_PAGO" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID_PAGO") %>'  ToolTip="¿ELIMINAR PAGO?" data-toggle="tooltip"   class=" btn btn-danger" runat="server" Text='X' />
             </td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
              </tbody>
              </table>
              <asp:SqlDataSource runat="server" ID="COMISIONES_PAGADAS_X_COMERCIAL" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_COMISIONES_PAGADAS_X_COMERCIAL" SelectCommandType="StoredProcedure">
              <SelectParameters>
              <asp:ControlParameter ControlID="DDL_COMERCIAL" PropertyName="SelectedValue" DefaultValue="1" Name="ID_COMERCIAL" Type="Int32"></asp:ControlParameter>
              </SelectParameters>
              </asp:SqlDataSource>
              </div>
              </div>   
               </div>     
            </div>
           
                <div id="menu2" class="tab-pane fade  in active">

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
                           <div class="col-sm-4"> 

                                 <div class="form-group">
                                 <label for="pwd">SELECCIONES OP A PAGAR:</label>
                                 <div class="input-group">
                                 <span class="input-group-addon"><i class="fas fa-map-marked-alt"></i></span>
                                 <asp:DropDownList ID="DDL_OPERACION_A_PAGAR" data-size="5"  data-live-search="true" AutoPostBack="true"  OnSelectedIndexChanged="DDL_OPERACION_A_PAGAR_SelectedIndexChanged"   data-live-search-style="startsWith" class="form-control selectpicker" runat="server" DataSourceID="COMISIONES_PENDIENTES_A_PAGAR" DataTextField="ID_OPERACIONES" DataValueField="ID_OPERACIONES"></asp:DropDownList>
                                     <asp:SqlDataSource runat="server" ID="COMISIONES_PENDIENTES_A_PAGAR" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_COMISIONES_PENDIENTES_X_COMERCIAL" SelectCommandType="StoredProcedure">
                                         <SelectParameters>
                                             <asp:ControlParameter ControlID="DDL_COMERCIAL" PropertyName="SelectedValue" Name="ID_COMERCIAL" Type="Int32"></asp:ControlParameter>
                                         </SelectParameters>
                                     </asp:SqlDataSource>
                                 </div>
                                 </div>
                           </div>
                           <div class="col-sm-8">
                               <div class ="row">
                                     <div class="col-sm-6">
                                         <div class="form-group">
                                         <label for="pwd">CLIENTE:</label>
                                         <div class="input-group">
                                         <span class="input-group-addon"><i class="fas fa-user"></i></span>
                                         <asp:TextBox ID="TXT_NOMBRE"  runat="server" class="form-control"  name="password"  DISABLED></asp:TextBox>
                                         </div>
                                         </div>
                                     </div>
                                     <div class="col-sm-6">
                                          <div class="form-group">
                                         <label for="pwd">RUT:</label>
                                         <div class="input-group">
                                         <span class="input-group-addon"><i class="fas fa-at"></i></span>
                                         <asp:TextBox ID="TXT_RUT" runat="server"  class="form-control"   name="password"  DISABLED></asp:TextBox>
                                         </div>
                                         </div>
                                     </div>
                               </div>
                               <div class ="row">
                                     <div class="col-sm-6">
                                          <div class="form-group">
                                         <label for="pwd">CIUDAD:</label>
                                         <div class="input-group">
                                         <span class="input-group-addon"><i class="fas fa-map-marked-alt"></i></span>
                                         <asp:TextBox ID="TXT_CIUDAD" runat="server"  class="form-control"   name="password"  DISABLED></asp:TextBox>
                                         </div>
                                         </div>
                                     </div>
                                     <div class="col-sm-6">
                                         <div class="form-group">
                                         <label for="pwd">DIRECCION:</label>
                                         <div class="input-group">
                                         <span class="input-group-addon"><i class="fas fa-map-marked-alt"></i></span>
                                         <asp:TextBox ID="TXT_DIRECCION" runat="server"  class="form-control" name="password"   DISABLED></asp:TextBox>
                                         </div>
                                         </div>
                                     </div>
                               </div>
                               <div class ="row">
                                    <div class="col-sm-6">
                                     <asp:Button ID="BTN_VER_VALORACION" ToolTip="¿ VER DETALLE DE VALORACION ?" data-toggle="tooltip" OnClick="BTN_VER_VALORACION_Click"  class=" btn  btn-success" runat="server" Text='VALORACION' /> 
                                    </div>
                                    <div class="col-sm-6">
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
                    
                    
              <%---------------------------------------------%>
              <%--      REGISTRAR  PAGO                    --%>
              <%---------------------------------------------%>
                 
          <hr />         
          <div class="row">
          <div class="col-sm-12">
          <div class="panel panel-default">
          <div class="panel-heading">REGISTRAR PAGO DE COMISIONES</div>
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
    <hr />      
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


              <div class="row">
              <div class="col-sm-4">
              <button type="button" class="btn btn-primary btn-lg" visible="false" data-toggle="tooltip" title="Descargar a .csv" runat="server" id="Button2"> DESCARGAR <i class="fas fa-file-csv"></i></button>
              </div>
              <div class="col-sm-4"></div>
              <div class="col-sm-4"><h4>BUSCAR.. <i class="fas fa-search"></i>  <input class="form-control btn-sm" id="myInput_3" type="text" placeholder="Search.."></h4></div>
              </div>
              <div class="row">
                  <div class="col-sm-12">
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
              <tbody id="myTable_3">
              <asp:Repeater ID="RPT_PAGO_COMISIONES_X_COMERCIAL" OnItemCommand="RPT_PAGO_COMISIONES_X_COMERCIAL_ItemCommand"   runat="server" DataSourceID="PAGO_COMISIONES_X_COMERCIAL">
              <ItemTemplate>
              <tr>
              <td class="small col-sm-2" ><%# Eval("FECHA_PAGO") %></td>
              <td class="small col-sm-8" ><%# Eval("DETALLE") %></td>
              <td class="small col-sm-1" ><%# Eval("MONTO") %></td>   
              <td class="small col-sm-1"><asp:Button ID="BTN_VER_VALORACION" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID_PAGO") %>'  ToolTip="¿ELIMINAR PAGO?" data-toggle="tooltip"   class=" btn btn-danger" runat="server" Text='X' /></td>
             </tr>
              </ItemTemplate>
              </asp:Repeater>
              </tbody>
              </table>
              <asp:SqlDataSource runat="server" ID="PAGO_COMISIONES_X_COMERCIAL" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_PAGO_COMISIONES_X_COMERCIAL" SelectCommandType="StoredProcedure">
              <SelectParameters>
              <asp:ControlParameter ControlID="DDL_OPERACION_A_PAGAR" PropertyName="SelectedValue" DefaultValue="1" Name="ID_OPERACION" Type="Int32"></asp:ControlParameter>
              </SelectParameters>
              </asp:SqlDataSource>
              </div>
              </div>  
              </div>

                </div>
                  
              </div>
              </div>
            </div>
        </div>

        <div class="row">
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
                          <label for="pwd">MONTO TOTAL DE COMISIONES:</label>
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
