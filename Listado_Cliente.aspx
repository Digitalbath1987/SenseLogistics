<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Listado_Cliente.aspx.cs" Inherits="Operaciones.Listado_Cliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <div class="container">
    <div class="panel-group">
    <div class="panel panel-primary">
    <div class="panel-heading text-bold"><h4> LISTADO DE CLIENTES  </h4> </div>
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
   <%---    LISTADO DE USUARIOS                           -----%>
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
              <th class="small">N° CLIENTE</th>
                <th class="small">NOMBRE</th>
                <th class="small">RUT</th>
                <th class="small">TELEFONO_1</th>
                <th class="small">TELEFONO_2</th>
                <th class="small">CIUDAD</th>
                <th class="small">DIRECCION</th>
                <th class="small">EMAIL</th>
                 <th class="small">COMERCIAL</th>
              <th class="small">ACCIONES</th>
              </tr>
              </thead>
              <tbody id="myTable">
              <asp:Repeater ID="RPT_CLIENTES" runat="server" OnItemCommand="RPT_CLIENTES_ItemCommand"  DataSourceID="LIST_CLIENTE">
              <ItemTemplate>
              <tr>
               <td class="small text-bold"><%# Eval("ID_CLIENTE") %></td>
               <td class="small" ><%# Eval("NOMBRE") %></td>
               <td class="small" ><%# Eval("RUT") %></td>
               <td class="small" ><%# Eval("TELEFONO_1") %></td>
               <td class="small" ><%# Eval("TELEFONO_2") %></td>
               <td class="small" ><%# Eval("CIUDAD") %></td>   
               <td class="small" ><%# Eval("DIRECCION") %></td>
               <td class="small" ><%# Eval("EMAIL") %></td>
               <td class="small" ><%# Eval("NOMBRE_COMERCIAL") %></td>    
               <td class="small hidden-sm"><asp:Button ID="BTN_ELIMINAR_CLIENTE" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID_CLIENTE") %>'  ToolTip="¿ DESEA DESHABILITAR EL USUARIO ?" data-toggle="tooltip"   class=" btn btn-danger" runat="server" Text='X' /> <asp:Button ID="BTN_MODIFICAR_CLIENTE" CommandName="EDITAR" CommandArgument='<%# Eval("ID_CLIENTE") %>'  ToolTip="¿ DESEA MODIFICAR EL CLIENTE ?" data-toggle="tooltip"  class=" btn btn-primary" runat="server" Text='M'/>  </td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
              <asp:SqlDataSource runat="server" ID="LIST_CLIENTE" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommandType="StoredProcedure" SelectCommand="SP_READ_CLIENTE"></asp:SqlDataSource>
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
