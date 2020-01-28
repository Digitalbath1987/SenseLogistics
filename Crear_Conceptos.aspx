<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Crear_Conceptos.aspx.cs" Inherits="Operaciones.Conceptos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
   <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/js/bootstrap-select.js"></script>
   <link href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" rel="stylesheet">
   <script src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    
   <%----------------------------------------------------------%>
   <%---    REGISTRAR CONCEPTO                            -----%>
   <%----------------------------------------------------------%>

  <div class="panel-group">
    <div class="panel panel-primary">
      <div class="panel-heading text-bold"><h4> CREAR CONCEPTO </h4> </div>
      <div class="panel-body">
         <asp:Label ID="ID_USUARIO" Visible="false"  runat="server" Text="0"></asp:Label>

         <div class="row">
       
              <div class="col-md-4">
                     <div class="form-group">
                     <label for="pwd">CONCEPTO DE PAGO</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-user"></i></span>
                     <asp:TextBox ID="TXT_CONCEPTO" runat="server" class="form-control"  name="password" placeholder="(OBLIGATORIO)" REQUIRED></asp:TextBox>
                     </div>
                     </div>
                  </div>

                <div class="col-md-4">
                      <div class="form-group">
                      <label for="pwd">TIPO DE CONCEPTO:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-user-check"></i></span>
                     <asp:DropDownList ID="DDL_TIPO_CONCEPTO" data-live-search="true" data-live-search-style="startsWith" class="form-control selectpicker" runat="server">
                     <asp:ListItem Value="1">AFECTO</asp:ListItem>
                     <asp:ListItem Value="2">EXENTO</asp:ListItem>
                     </asp:DropDownList>
                     </div>
                     </div>
                 </div>
         </div>
         <br />
          <br />
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
           
               <div class="col-md-4">
                    <asp:Button ID="BTN_GUARDAR"  runat="server" Text="CREAR CONCEPTOS"  class="btn btn-primary" OnClick="BTN_GUARDAR_Click" data-toggle="tooltip" title="Click Para Guardar" /> 
                    <asp:Button ID="BTN_ACTUALIZAR" Visible="false" runat="server" Text="ACTUALIZAR USUARIO" class="btn btn-primary"  data-toggle="tooltip" title="Click Para Actualizar" />
               </div>
       
        </div>
  
</div>
</div>
</div>


   <%----------------------------------------------------------%>
   <%---    LISTADO DE REGISTROS                          -----%>
   <%----------------------------------------------------------%>

    <div class="panel-group">
        <div class="panel panel-primary">
        <div class="panel-heading text-bold"><h4> LISTADOS DE REGISTROS </h4> </div>
            <div class="panel-body">
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
              <th class="small" >ID_CONCEPTO</th>
              <th class="small" >DETALLE</th>
              <th class="small" >TIPO_CONCEPTO</th>
              <th class="small" >ACCIONES</th>   
              </tr>
              </thead>
              <tbody id="myTable">
              <asp:Repeater ID="RPT_OPERACIONES" runat="server"  DataSourceID="LIST_CONCEPTOS">
              <ItemTemplate>
              <tr>
                <td class="small"><%# Eval("ID_CONCEPTO") %></td>
                <td class="small" ><%# Eval("DETALLE") %></td>
                <td class="small" ><%# Eval("TIPO_CONCEPTO") %></td>
                <td class="small hidden-sm"><asp:Button ID="BTN_ELIMINAR_OPERACION" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID_CONCEPTO") %>'  ToolTip="¿ DESEA ELIMINAR ELIMINAR LA OPERACION ?" data-toggle="tooltip"   class=" btn btn-danger" runat="server" Text='X' /> <asp:Button ID="Button1" CommandName="EDITAR" CommandArgument='<%# Eval("ID_CONCEPTO") %>'  ToolTip="¿ DESEA MODIFICAR LA OPERACION ?" data-toggle="tooltip"  class=" btn btn-primary" runat="server" Text='M'/>  </td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
              <asp:SqlDataSource runat="server" ID="LIST_CONCEPTOS" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommandType="StoredProcedure" SelectCommand="SP_READ_DETALLE_CONCEPTOS"></asp:SqlDataSource>
              </tbody>
              </table>
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
