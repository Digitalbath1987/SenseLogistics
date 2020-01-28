<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Listado_Operaciones.aspx.cs" Inherits="Operaciones.Listado_Operaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
     .scrolling-table-container {
    height: 500px;
    overflow-y: scroll;
    overflow-x: hidden;
}
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">



  
    <div class="panel-group">
    <div class="panel panel-primary">
    <div class="panel-heading text-bold"><h4> LISTADO DE OPERACIONES  </h4> </div>
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

        <div class="row">
           <div class="col-sm-4"></div>
            <div class="col-sm-4"></div>
            <div class="col-sm-4"><h4>BUSCAR.. <i class="fas fa-search"></i>  <input class="form-control btn-sm" id="myInput" type="text" placeholder="Search.."></h4></div>
       
        </div>
    
        <div class="row">
         <div class="col-sm-12">
              <div class="table-responsive">          
              <table class="table table-sm table-striped  table-hover">
              <thead>
              <tr>
              <th class="small" >N° OP</th>
              <th class="small" >FECHA</th>
              <th class="small" >MARITIMO</th>
              <th class="small" >AEREO</th>
              <th class="small" >T.I.</th>
              <th class="small" >T.N.</th> 
              <th class="small" >SEGURO</th>
              <th class="small" >A.G.A.</th>
              <th class="small" >OTROS</th>
              <th class="small" >CLIENTE</th>
              <th class="small" >ORIGEN</th>
              <th class="small" >DESTINO</th>
              <th class="small" >INCOTERM</th>
              <th class="small" >UNIDAD</th>
              <th class="small" >REFERENCIA</th>
              <th class="small" >COMERCIAL</th>
              <th class="small" >CUSTOMER</th>
              <th class="small  hidden-sm" >USUARIO</th>
              <th class="small col-sm-1" >ACCIONES</th>   
              </tr>
              </thead>
              <tbody id="myTable">
              <asp:Repeater ID="RPT_OPERACIONES" runat="server" OnItemCommand="RPT_OPERACIONES_ItemCommand" DataSourceID="LIST_OPERACIONES">
              <ItemTemplate>
              <tr>
                <td class="small text-bold sm" ><%# Eval("NUM_OPE") %></td>
                <td class="small" ><%# Eval("FECHA_REGISTRO") %></td>
                <td class="small" ><asp:CheckBox Enabled="false"  Checked='<%# Eval("MARITIMO") %>' runat="server" /></td>
                <td class="small" ><asp:CheckBox Enabled="false"  Checked='<%# Eval("AEREO") %>' runat="server" /></td>
                <td class="small" ><asp:CheckBox Enabled="false"  Checked='<%# Eval("TI") %>' runat="server" /></td>
                <td class="small" ><asp:CheckBox Enabled="false"  Checked='<%# Eval("TN") %>' runat="server" /></td>
                <td class="small" ><asp:CheckBox Enabled="false"  Checked='<%# Eval("SEGURO") %>' runat="server" /></td>
                <td class="small" ><asp:CheckBox Enabled="false"  Checked='<%# Eval("AGA") %>' runat="server" /></td>
                <td class="small" ><asp:CheckBox Enabled="false"  Checked='<%# Eval("OTROS") %>' runat="server" /></td>
                <td class="small"><%# Eval("CLIENTE") %></td>
                <td class="small" ><%# Eval("ORIGEN") %></td>
                <td class="small" ><%# Eval("DESTINO") %></td>
                <td class="small" ><%# Eval("INCOTERM") %></td>
                <td class="small" ><%# Eval("UNIDAD") %></td>
                <td class="small" ><%# Eval("REFERENCIA") %></td>
                <td class="small" ><%# Eval("COMERCIAL") %></td>
                <td class="small" ><%# Eval("CUSTOMER") %></td>
                <td class="small hidden-sm"><%# Eval("USUARIO_REGISTRO") %></td>
                <td class="small hidden-sm col-sm-1"><asp:Button ID="BTN_ELIMINAR_OPERACION" CommandName="ELIMINAR" CommandArgument='<%# Eval("NUM_OPE") %>'  ToolTip="¿ DESEA ELIMINAR ELIMINAR LA OPERACION ?" data-toggle="tooltip"   class=" btn btn-danger" runat="server" Text='X' /> <asp:Button ID="Button1" CommandName="EDITAR" CommandArgument='<%# Eval("NUM_OPE") %>'  ToolTip="¿ DESEA MODIFICAR LA OPERACION ?" data-toggle="tooltip"  class=" btn btn-primary" runat="server" Text='M'/>  </td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
              <asp:SqlDataSource runat="server" ID="LIST_OPERACIONES" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommandType="StoredProcedure" SelectCommand="SP_READ_OPERACIONES"></asp:SqlDataSource>
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
