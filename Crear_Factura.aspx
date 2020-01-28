<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Crear_Factura.aspx.cs" Inherits="Operaciones.Crear_Factura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
   <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/js/bootstrap-select.js"></script>
   <link href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" rel="stylesheet">
   <script src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    
   <script type="text/javascript">

        <%-----------------------------------------------------------------------%>
        <%--          FORMATEA EL RUT CON FORMATO XX.XXX.XXX-X                 --%>
        <%-----------------------------------------------------------------------%>

     function formatoRut(texto, inputText) {
     objRut = document.getElementById(inputText);
     var rut_aux = "";
     for (i = 0; i < texto.length; i++)
     if (texto.charAt(i) != ' ' && texto.charAt(i) != '.' && texto.charAt(i) != '-')
     rut_aux = rut_aux + texto.charAt(i);
     largo = rut_aux.length;
     if (largo == 0) return false;
     if (largo < 2) return false;
     for (i = 0; i < largo; i++) {
     var letra = rut_aux.charAt(i);
     if (!letra.match(/^([0-9]|[kK])$/)) return false;
     }
     var rut_inv = "";
     for (i = (largo - 1), j = 0; i >= 0; i-- , j++) rut_inv = rut_inv + rut_aux.charAt(i);
     var dtexto = "";
     dtexto = dtexto + rut_inv.charAt(0);
     dtexto = dtexto + '-';
     cnt = 0;
     for (i = 1, j = 2; i < largo; i++ , j++) {
     if (cnt == 3) {
     dtexto = dtexto + '.';
     j++;
     dtexto = dtexto + rut_inv.charAt(i);
     cnt = 1;
     } else {
     dtexto = dtexto + rut_inv.charAt(i);
     cnt++;
     }
     }
     rut_inv = "";
     for (i = (dtexto.length - 1), j = 0; i >= 0; i-- , j++) rut_inv = rut_inv + dtexto.charAt(i);
     objRut.value = rut_inv.toUpperCase()
     }
  </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
 <div class="container">
        <div class="col-sm-12">
        <%-----------------------------------------------------------------------%>
        <%--          CUADRO BUSCAR OPERACIONES CON FACTURAS PENDIENTES        --%>
        <%-----------------------------------------------------------------------%>
        <div class="row">
        <div class="panel panel-primary">
                <div class="panel-heading">BUSCAR OPERACION A FACTURAR</div>
                <div class="panel-body">
                    <div class="col-sm-3">
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label for="pwd">OPERACION:</label>
                                <asp:DropDownList ID="DDL_OPERACION" data-live-search="true" data-live-search-style="startsWith"  class="form-control" runat="server" DataSourceID="OPERACIONES_FACTURA_PENDIENTES" DataTextField="NRO_OPERACION" DataValueField="NRO_OPERACION"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="OPERACIONES_FACTURA_PENDIENTES" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_FACTURAS_PENDIENTES_X_OP" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                        <label for="pwd"></label>
                        <div class="input-group">
                            <button type="button" id="BTN_CARGAR_FACTURAS" onserverclick="BTN_CARGAR_FACTURAS_ServerClick" runat="server" class="btn btn-primary">BUSCAR <i class="fas fa-search"></i></button>
                        </div>
                        </div>
                    </div>
                    <div class="col-sm-3"></div>
                </div>
            </div>
        </div>
 
        <%-----------------------------------------------------------------------%>
        <%--             LISTADO DE GRUPOS  DE FACTURAS ASOCIADAS              --%>
        <%-----------------------------------------------------------------------%>
        <div class="row">
        <div class="panel panel-primary">
            <div class="panel-heading">LISTADO DE FACTURAS ASOCIADAS A LA OPERACION</div>
            <div class="panel-body">

              <div class="row">
              <div class="col-sm-12">
              <div class="table-responsive">          
              <table class="table table-sm table-striped  table-hover">
              <thead>
              <tr>
              <th class="small">ID FACTURA</th>
              <th class="small">OPERACION</th>
              <th class="small">TIPO FACTURA</th>
              <th class="small">FACTURA</th>
              <th class="small">ESTADO</th>  
              <th class="small">ACCIONES</th>
              </tr>
              </thead>
              <tbody id="myTable">
              <asp:Repeater ID="RPT_FACTURAS"   OnItemCommand="RPT_FACTURAS_ItemCommand" runat="server" DataSourceID="SP_READ_FACTURAS_X_OP">
              <ItemTemplate>
              <tr>
              <td class="small text-bold"><%# Eval("ID_FACTURA") %></td>
              <td class="small"><%# Eval("NRO_OPERACION") %></td>
              <td class="small"><%# Eval("DETALLE") %></td>
              <td class="small"><%# Eval("GRUPO_FACTURA") %></td>
              <td class="small"><%# Eval("ESTADO") %></td>
              <td class="small hidden-sm">
              <asp:Button ID="BTN_SELECCIONAR_FACTURA" CommandName="SELECCIONAR" CommandArgument='<%# Eval("ID_FACTURA") %>' ToolTip="¿ SELECCIONE FACTURA ?" data-toggle="tooltip" class="btn btn-primary" runat="server" Text='F' />
              </td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
              <asp:SqlDataSource runat="server" ID="SP_READ_FACTURAS_X_OP" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_FACTURAS_X_OP" SelectCommandType="StoredProcedure">
              <SelectParameters>
              <asp:ControlParameter ControlID="DDL_OPERACION" PropertyName="SelectedValue" DefaultValue="0" Name="NRO_OPERACION" Type="Int32"></asp:ControlParameter>
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

        <%-----------------------------------------------------------------------%>
        <%--              FORMULARIO DE FACTURA ELECTRONICA                    --%>
        <%-----------------------------------------------------------------------%>
 
        <div class="row">
           <div class="col-sm-12">
                <div id="DIV_FORMULARIO_FACTURA" visible="false" runat="server">
        <div class="row">
            <div class="panel panel-primary">
            <div class="panel-heading">ID FACTURA <asp:Label ID="LBL_GRUPO_FACTURA" runat="server" Text="0"></asp:Label></div>
            <div class="panel-body">
     
                    <asp:Label ID="LBL_ID_FACTURA" runat="server" Visible="false" Text="0"></asp:Label>
            
                    <div class="row">
                        <div class="col-sm-12">
                        <div class="panel panel-default">
                        <div class="panel-heading">DATOS DEL CLIENTE</div>
                        <div class="panel-body">
       
                         <div class="row">
                            <div class="col-sm-6">
                            <div class="form-group">
                            <label>Nombre :</label> 
                            <asp:TextBox ID="TXT_NOMBRE_CLIENTE" CssClass="form-control"  runat="server" ></asp:TextBox>
                            </div>
                            </div>

                            <div class="col-sm-3">
                            <div class="form-group">
                            <label>Rut :</label>
                            <asp:TextBox ID="TXT_RUT_CLIENTE"  onblur="javascript:formatoRut(this.value,this.id)" CssClass="form-control"  runat="server" ></asp:TextBox>
                            </div>
                            </div>


                            <div class="col-sm-3">
                            <div class="form-group">  
                            <label>Telefono :</label>  
                            <asp:TextBox ID="TXT_TELEFONO_CLIENTE" CssClass="form-control" runat="server" ></asp:TextBox>             
                            </div>
                            </div>

                        </div>               
                             <hr />
                            <div class="row">
                            <div class="col-sm-6">
                            <div class="form-group">  
                            <label>Giro :</label>  
                            <asp:TextBox ID="TXT_GIRO_CLIENTE" CssClass="form-control" runat="server" ></asp:TextBox>             
                            </div>
                            </div>
                            <div class="col-sm-3">
                            <div class="form-group">  
                            <label>Ref. Cliente :</label>  
                            <asp:TextBox ID="TXT_REF_CLIENTE" CssClass="form-control" runat="server" ></asp:TextBox>             
                            </div>
                            </div>
                            <div class="col-sm-3">
                            <div class="form-group">  
                            <label>Doc. Transporte:</label>  
                            <asp:TextBox ID="TXT_DOC_TRANSPORTE" CssClass="form-control" runat="server" ></asp:TextBox>             
                            </div>   
                            </div>
                            </div> 
                              
                             <hr />
                           <div class="row">
                            <div class="col-sm-6">
                            <div class="form-group">   
                            <label>Direccion :</label>  
                            <asp:TextBox ID="TXT_DIRECCION_CLIENTE" CssClass="form-control" runat="server" ></asp:TextBox>   
                            </div>
                            </div>
                
                            <div class="col-sm-3">
                            <div class="form-group">   
                            <label>Comuna :</label>  
                            <asp:TextBox ID="TXT_COMUNA_CLIENTE"  CssClass="form-control" runat="server" ></asp:TextBox>                 
                            </div>
                            </div>


                            <div class="col-sm-3">
                            <div class="form-group"> 
                            <label>Ciudad :</label>  
                            <asp:TextBox ID="TXT_CIUDAD_CLIENTE"  CssClass="form-control" runat="server" ></asp:TextBox>                
                            </div>
                            </div>
                        </div>   
                             <hr />                
                             <div class="row">

                            <div class="col-sm-3">
                            <div class="form-group"> 
                            <label>Fecha Emision :</label>  
                            <asp:TextBox ID="TXT_FECHA_EMISION" TextMode="Date"  CssClass="form-control" runat="server" ></asp:TextBox>   
                            </div>
                            </div> 

                            <div class="col-sm-3">
                            <div class="form-group">                      
                            <label>Fecha Vencimiento :</label>  
                            <asp:TextBox ID="TXT_FECHA_VECIMIENTO" TextMode="Date"  CssClass="form-control" runat="server" ></asp:TextBox>                 
                            </div>
                            </div>

                            <div class="col-sm-3">
                            <div class="form-group">      
                            <label>Vendedor :</label>  
                            <asp:TextBox ID="TXT_VENDEDOR" CssClass="form-control" runat="server" ></asp:TextBox>                
                            </div>
                            </div>
                            <div class="col-sm-3">
                            <div class="form-group">     
                            <label>Condicion de Pago :</label>  
                                <asp:DropDownList ID="DDL_CONDICION_PAGO" CssClass="form-control" runat="server" DataSourceID="SP_READ_CONDICION_PAGO" DataTextField="DETALLE" DataValueField="ID_CONDICION_PAGO"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="SP_READ_CONDICION_PAGO" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_CONDICION_PAGO" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                            </div>    
                            </div>
                        </div>   
                        </div>   
                        </div>    
                        </div>
                    </div>    
                    <div class="row">
                    <div class="col-sm-12">
                    <div class="panel panel-default">
                    <div class="panel-heading">CONCEPTOS A FACTURAR</div>
                    <div class="panel-body">
                           <div class="row">
                           <div class="col-sm-12">
                           <div class="table-responsive">          
                           <table class="table table-sm table-striped  table-hover">
                           <thead>
                           <tr>
                           <th class="small col-sm-8">DESCRIPCION</th>
                           <th class="small col-sm-2">PRECIO UNITARIO</th>
                           <th class="small col-sm-2">TOTAL</th>
                           </tr>
                           </thead>
                           <tbody id="TABLA_CONCEPTOS">
                           <asp:Repeater ID="RPT_CONCEPTOS" runat="server" DataSourceID="SP_READ_CONCEPTOS_FACTURAR_X_ID_FACTURA">
                                   <ItemTemplate>
                                       <tr>
                                           <td class="small text-bold col-sm-8"><%# Eval("DESCRIPCION") %></td>
                                           <td class="small col-sm-2"><%# Eval("PRECIO_UNITARIO") %></td>
                                           <td class="small col-sm-2"><%# Eval("SALDO_TOTAL_PENDIENTE") %></td>
                                       </tr>
                                   </ItemTemplate>
                               </asp:Repeater>
                               <asp:SqlDataSource runat="server" ID="SP_READ_CONCEPTOS_FACTURAR_X_ID_FACTURA" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_READ_CONCEPTOS_FACTURAR_X_ID_FACTURA" SelectCommandType="StoredProcedure">
                                   <SelectParameters>
                                       <asp:ControlParameter ControlID="LBL_ID_FACTURA" PropertyName="Text" DefaultValue="1" Name="ID_FACTURA" Type="Int32"></asp:ControlParameter>
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
                    <div class="row">
                    <div class="col-sm-12">
                    <div class="panel panel-default">
                    <div class="panel-body">
                         <div class="col-sm-12">
                         <div class="form-group">
                         <label for="pwd">Observacion:</label>
                             <asp:TextBox ID="TXT_OBSERVACION_FACTURA" Rows="8" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox> 
                         </div>
                         </div>
                    </div>
                    </div>
                    </div>
                    </div>   
                    <div class="row">
                        <div class="col-sm-6">
                        </div> 
                        <div class="col-sm-6">
                           <div class="panel panel-default">
                           <div class="panel-heading"> <label for="pwd">MONTOS TOTALES:</label></div> 
                           <div class="panel-body"> 
                               <div class="row">
                                  <div class="col-sm-6">   
                                       <div class="form-group">
                                       <label for="pwd">NETO:</label>
                                       </div>
                                  </div>
                                  <div class="col-sm-6">
                                      <asp:Label ID="LBL_NETO" runat="server" Text="0"></asp:Label>
                                  </div>
                               </div>
                               
                               <div class="row">
                                  <div class="col-sm-6">    
                                       <div class="form-group">
                                       <label for="pwd">IVA:</label>
                                       </div>
                                  </div>
                                  <div class="col-sm-6">     
                                   <asp:Label ID="LBL_IVA" runat="server" Text="0"></asp:Label>
                                  </div>
                               </div>
                               
                               <div class="row">
                                  <div class="col-sm-6">     
                                       <div class="form-group">
                                       <label for="pwd">EXENTO:</label>
                                       </div>
                                  </div>
                                  <div class="col-sm-6">
                                       <asp:Label ID="LBL_EXENTO" runat="server" Text="0"></asp:Label>
                                  </div>
                               </div>
                               
                               <div class="row">
                                  <div class="col-sm-6">     
                                       <div class="form-group">
                                       <label for="pwd">TOTAL:</label>
                                       </div>
                                      </div>
                                  <div class="col-sm-6">    
                                         <asp:Label ID="LBL_TOTAL" runat="server" Text="0"></asp:Label>
                                  </div>
                               </div>
  
                           </div>
                           </div>
                        </div>
                    </div>  
                   
                    <div class="row">
                <div class="col-sm-12">
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
            </div> 
            </div> 
            </div> 
        <hr />
       </div> 
           </div>
       </div>
    
        <div class="row">
            <div id="ACCIONES" runat="server" visible="false">
              <div class="col-sm-4">
              <div class="small-box bg-light-blue-active ">
              <div class="inner">
              <div class="row">
              <div class="col-sm-12">
              <h4>ACCIONES</h4>
              </div>
              </div>
               <br />
             <div class="row">
              <div class="col-sm-12">
                    <asp:Button  id="BTN_VISUALIZAR" visible="false" ToolTip="Descargar factura .PDF" data-toggle="tooltip" OnClick="BTN_VISUALIZAR_Click"   runat="server" class="btn btn-default" Text="DESCARGAR .PDF"/>
                    <asp:Button id="BTN_FACTURAR" runat="server" visible="false" ToolTip="Enviar Factura a SII" data-toggle="tooltip" OnClick="BTN_FACTURAR_Click"  class="btn btn-danger" Text="FACTURAR SII"/>
              </div>  
              </div> 
               </div>
              <div class="icon">
              <i class="fas fa-file-invoice-dollar"></i>
              </div>
              </div>
              </div>
            </div>  
         </div>

       </div>
    </div>

        </ContentTemplate>
        <Triggers>
       <%--     <asp:AsyncPostBackTrigger ControlID="BTN_VISUALIZAR" EventName="Click" />--%>
            <asp:PostBackTrigger ControlID="BTN_VISUALIZAR" />
            <asp:PostBackTrigger ControlID="BTN_VISUALIZAR" />
        </Triggers>
    </asp:UpdatePanel>


   
    
    
    
    <script>
            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip();
            });
        </script>
</asp:Content>
