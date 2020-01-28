<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Listado_Facturas.aspx.cs" Inherits="Operaciones.Listado_Facturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

        <div class="container">
    <div class="panel-group">
    <div class="panel panel-primary">
    <div class="panel-heading text-bold"><h4> LISTADO DE FACTURAS  </h4> </div>
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
   <%---    LISTADO DE FACTURAS                           -----%>
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
              <tr>
                <th class="small col-sm-1">PDF</th> 
                <th class="small col-sm-1">N° FACTURA</th>
                <th class="small col-sm-1">N° OPERACION</th>
                <th class="small col-sm-2">FECHA EMISION</th>
                <th class="small col-sm-1">TIPO FACTURA</th>
                <th class="small col-sm-4">NOMBRE</th>
                <th class="small col-sm-1">RUT</th>
                <th class="small col-sm-1">ACCIONES</th>
              </tr>
              </thead>
              <tbody id="myTable">
              <asp:Repeater ID="RPT_FACTURAS"  OnItemCommand="RPT_FACTURAS_ItemCommand" runat="server"   DataSourceID="LIST_FACTURAS">
              <ItemTemplate>
              <tr>
               <td class="small  col-sm-1">
               <asp:Button ID="BTN_DESCARGAR_PDF" CommandName="DESCARGAR" CommandArgument='<%# Eval("ID_FACTURA") %>'  ToolTip="¿ DESEA DESCARGAR FACTURA ?" data-toggle="tooltip"  class=" btn btn-primary" runat="server" Text='D'/>  
               </td>
               <td class="small text-bold  col-sm-1"><%# Eval("ID_FACTURA") %></td>
               <td class="small  col-sm-1"><%# Eval("NRO_OPERACION") %></td>
               <td class="small  col-sm-2"><%# Eval("FEC_EMI") %></td>
               <td class="small  col-sm-1"><%# Eval("TIPO_FACTURA") %></td>
               <td class="small  col-sm-4"><%# Eval("NOMBRE") %></td>
               <td class="small  col-sm-1"><%# Eval("RUT") %></td>
               <td class="small hidden-sm  col-sm-1">
               <asp:Button ID="BTN_VER_VALORACION" CommandName="VALORACION" CommandArgument='<%# Eval("NRO_OPERACION") %>'  ToolTip="¿ VER DETALLE DE VALORACION ?" data-toggle="tooltip"   class=" btn  btn-success" runat="server" Text='V' /> 
               <asp:Button ID="BTN_VER_OPERACION" CommandName="OPERACION" CommandArgument='<%# Eval("NRO_OPERACION") %>'  ToolTip="¿ VER DETALLE DE OPERACION  ?" data-toggle="tooltip"  class=" btn btn-primary" runat="server" Text='O'/>  
               </td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
              <asp:SqlDataSource runat="server" ID="LIST_FACTURAS" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommandType="StoredProcedure" SelectCommand="SP_READ_LISTADO_FACTURAS"></asp:SqlDataSource>
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




</asp:Content>
