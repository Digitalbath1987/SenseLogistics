<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Listado_Bill_Lading.aspx.cs" Inherits="Operaciones.Listado_bill_lading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
            window.location.href = '#modalHtml';
        }
</script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

<%--    <div class="container">--%>
    <div class="panel-group">
    <div class="panel panel-primary">
    <div class="panel-heading text-bold"><h4>  <i class="fab fa-docker"></i> LISTA DE BILL OF LADING</h4> </div>
    <div class="panel-body">

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

   <%----------------------------------------------------------%>
   <%---                  LISTADO DE B/L                  -----%>
   <%----------------------------------------------------------%>

     <div class="row">
           <div class="col-sm-4"></div>
            <div class="col-sm-4"></div>
            <div class="col-sm-4"><h4>BUSCAR.. <i class="fas fa-search"></i>  <input class="form-control btn-sm" id="myInput" type="text" placeholder="Search.."></h4></div>
     </div>

     <div class="row">
              <div class="table-responsive">          
              <table class="table table-sm table-striped  table-hover">
              <thead>
              <tr>   <th class="small">#</th>
                    <th class="small">N° OPERACION</th>
                    <th class="small">N° BOOKING</th>
                    <th class="small">N° BL</th>
                    <th class="small">PORT LOADING</th>
                    <th class="small">PORT DISCHARGE</th>
                    <th class="small">ACCIONES</th>
              </tr>
              </thead>
              <tbody id="myTable">
              <asp:Repeater ID="RPT_BL" OnItemCommand="RPT_BL_ItemCommand"  runat="server"   DataSourceID="LIST_BL">
              <ItemTemplate>
              <tr>
                     <td >
                     <asp:LinkButton ID="BTN_DESCARGAR_BL" CommandName="DECARGAR_BL" CommandArgument='<%# Eval("N_BL") %>'  ToolTip="¿ DESEA DESCARGAR B/L ?" data-toggle="tooltip"   class=" btn btn-success" runat="server"><i class="fas fa-file-pdf"></i></asp:LinkButton>
                     </td>
                     <td class="small text-bold "><%# Eval("N_OPERACION") %></td>
                     <td class="small text-bold "><%# Eval("N_BOOKING") %></td>
                     <td class="small text-bold "><%# Eval("N_BL") %></td>
                     <td class="small "><%# Eval("PORT_LOADING") %></td>
                     <td class="small "><%# Eval("PORT_DISCHARGE") %></td>
                     <td class="small">
                     <asp:LinkButton ID="BTN_ELIMINAR_COMERCIAL" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID_BILL_LADING") %>'  ToolTip="¿ DESEA DESHABILITAR B/L ?" data-toggle="tooltip"   class=" btn btn-danger" runat="server"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                  <%--   <asp:LinkButton ID="BTN_MODIFICAR_COMERCIAL" CommandName="EDITAR" CommandArgument='<%# Eval("ID_BILL_LADING") %>'  ToolTip="¿ DESEA MODIFICAR EL B/L ?" data-toggle="tooltip"  class=" btn btn-primary" runat="server"><i class="fas fa-file-signature"></i></asp:LinkButton> --%>    </td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
              <asp:SqlDataSource runat="server" ID="LIST_BL" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommandType="StoredProcedure" SelectCommand="SP_READ_LISTADO_BL"></asp:SqlDataSource>
              </tbody>
              </table>
              </div>
        </div>

    </div>
    </div>
    </div>
<%--    </div> --%>



<%----------------------------------------------------------%>
<%---               MODAL IMPRIMIR BL                  -----%>
<%----------------------------------------------------------%>

<div id="modalHtml" class="modalDialog2" role="dialog">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" onserverclick="BTN_CERRAR_ServerClick"  runat="server" id="Button1"  data-dismiss="modal">&times;</button>
        <h4 class="modal-title">IMPRIMIR BILL LADING</h4>
          </div>
            <div class="modal-body">

                <asp:Label ID="LBL_ID_BL" runat="server" Visible="false" Text=""></asp:Label>

                <div class="row">
                <div class="col-sm-12"> 
                    <div class="form-group">
                     <label for="pwd">TIPO DOCUMENTO:</label>
                     <div class="input-group">
                         <asp:RadioButtonList ID="RBL_TIPO_DOC" runat="server">
                             <asp:ListItem Selected="True">ORIGINAL</asp:ListItem>
                             <asp:ListItem>SEAWAYBILL</asp:ListItem>
                             <asp:ListItem>NON NEGOTIABLE COPY</asp:ListItem>
                             <asp:ListItem>TELEX RELEASE </asp:ListItem>
                         </asp:RadioButtonList>
                     </div>
                     </div>
                </div>
                </div>
    
               </div>
               <div class="modal-footer">
                <button type="button"  id="BTN_IMPRIMIR_BL"  onserverclick="BTN_IMPRIMIR_BL_ServerClick"  runat="server" ToolTip="¿ DESEA DESCARGAR B/L ?" data-toggle="tooltip"   class=" btn btn-success"  data-dismiss="modal"><i class="fas fa-file-pdf"></i> IMPRIMIR</button>
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

</asp:Content>
