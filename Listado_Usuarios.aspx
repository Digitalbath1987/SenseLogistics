<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Listado_Usuarios.aspx.cs" Inherits="Operaciones.Listado_Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <div class="container">
    <div class="panel-group">
    <div class="panel panel-primary">
    <div class="panel-heading text-bold"><h4> LISTADO DE USUARIOS  </h4> </div>
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
              <th class="small">N° USUARIO</th>
              <th class="small">NOMBRE</th>
              <th class="small">EMAIL</th>
              <th class="small">USUARIO</th>
              <th class="small">IDENTIFICADOR</th>   
              <th class="small">EDITAR</th>
              <th class="small">ELIMINAR</th>
              <th class="small">ACCIONES</th>
              </tr>
              </thead>
              <tbody id="myTable">
              <asp:Repeater ID="RPT_USUARIOS" runat="server" OnItemCommand="RPT_USUARIOS_ItemCommand"  DataSourceID="LIST_USUARIOS">
              <ItemTemplate>
              <tr>
              <td class="small text-bold"><%# Eval("ID_USUARIO") %></td>
              <td class="small" ><%# Eval("NOMBRE") %></td>
              <td class="small" ><%# Eval("EMAIL") %></td>
              <td class="small" ><%# Eval("NOMBRE_USUARIO") %></td>
              <td class="small" ><%# Eval("IDENTIFICADOR") %></td>
              <td class="small" ><asp:CheckBox Enabled="false"  Checked='<%#Eval("EDITAR") %>' runat="server" /></td>
              <td class="small" ><asp:CheckBox Enabled="false"  Checked='<%#Eval("ELIMINAR") %>' runat="server" /></td>
              <td class="small hidden-sm"><asp:Button ID="BTN_ELIMINAR_OPERACION" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID_USUARIO") %>'  ToolTip="¿ DESEA DESHABILITAR EL USUARIO ?" data-toggle="tooltip"   class=" btn btn-danger" runat="server" Text='X' /> <asp:Button ID="Button1" CommandName="EDITAR" CommandArgument='<%# Eval("ID_USUARIO") %>'  ToolTip="¿ DESEA MODIFICAR EL USUARIO ?" data-toggle="tooltip"  class=" btn btn-primary" runat="server" Text='M'/>  </td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
              <asp:SqlDataSource runat="server" ID="LIST_USUARIOS" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommandType="StoredProcedure" SelectCommand="SP_READ_TOTAL_USUARIO"></asp:SqlDataSource>
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
