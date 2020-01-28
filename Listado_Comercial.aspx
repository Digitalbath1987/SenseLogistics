<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Listado_Comercial.aspx.cs" Inherits="Operaciones.Listado_Comercial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
     
    <div class="container">
    <div class="panel-group">
    <div class="panel panel-primary">
    <div class="panel-heading text-bold"><h4> LISTADO DE CUSTOMER  </h4> </div>
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
   <%---    LISTADO DE CUSTOMER                           -----%>
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
              <th class="small">N° COMERCIAL</th>
                <th class="small">NOMBRE</th>
                <th class="small">TELEFONO</th>
                <th class="small">EMAIL</th>
                <th class="small">IDENTIFICADOR</th>  
              <th class="small">ACCIONES</th>
              </tr>
              </thead>
              <tbody id="myTable">
              <asp:Repeater ID="RPT_COMERCIAL"  OnItemCommand="RPT_COMERCIAL_ItemCommand" runat="server"   DataSourceID="LIST_COMERCIAL">
              <ItemTemplate>
              <tr>
               <td class="small text-bold"><%# Eval("ID_COMERCIAL") %></td>
               <td class="small"><%# Eval("NOMBRE_COMERCIAL") %></td>
               <td class="small"><%# Eval("TELEFONO_COMERCIAL") %></td>
               <td class="small"><%# Eval("EMAIL_COMERCIAL") %></td>
               <td class="small"><%# Eval("IDENTIFICADOR") %></td>
               <td class="small hidden-sm"><asp:Button ID="BTN_ELIMINAR_COMERCIAL" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID_COMERCIAL") %>'  ToolTip="¿ DESEA DESHABILITAR CUSTOMER ?" data-toggle="tooltip"   class=" btn btn-danger" runat="server" Text='X' /> <asp:Button ID="BTN_MODIFICAR_COMERCIAL" CommandName="EDITAR" CommandArgument='<%# Eval("ID_COMERCIAL") %>'  ToolTip="¿ DESEA MODIFICAR EL COMERCIAL ?" data-toggle="tooltip"  class=" btn btn-primary" runat="server" Text='M'/>  </td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
              <asp:SqlDataSource runat="server" ID="LIST_COMERCIAL" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommandType="StoredProcedure" SelectCommand="SP_READ_COMERCIAL"></asp:SqlDataSource>
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
