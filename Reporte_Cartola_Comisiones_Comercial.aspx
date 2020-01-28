<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Reporte_Cartola_Comisiones_Comercial.aspx.cs" Inherits="Operaciones.Reporte_Cartola_Comisiones_Comercial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/js/bootstrap-select.js"></script>
    <link href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" rel="stylesheet">
    <script src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

 
    <div class="panel-group">
    <div class="panel panel-primary">
    <div class="panel-heading text-bold"><h4> CARTOLA MOVIMIENTOS DE COMISIONES X COMERCIAL</h4> </div>
    <div class="panel-body">
    <hr/>    
    
    <%---------------------------------------------%>
        <%--          CUADRO BUSCAR COMERCIAL        --%>
        <%---------------------------------------------%>
        <div class="row">
            <div class="col-sm-12">

            <div class="panel panel-default">
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
                            <button type="button" id="BTN_FILTRAR_CC" runat="server"  onserverclick="BTN_FILTRAR_CC_ServerClick" class="btn btn-primary">FILTRAR <i class="fas fa-search"></i></button>
                        </div>
                    </div>
                    <div class="col-sm-3"></div>
                </div>
            </div>
        </div>
        </div>
     <hr/>    


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
           <div class="col-sm-4">
                   <button type="button" class="btn btn-primary btn-lg" visible="false" data-toggle="tooltip" title="Descargar a .csv" runat="server" id="BTN_DESCARGAR_CSV"  onserverclick="BTN_DESCARGAR_CSV_ServerClick" > DESCARGAR <i class="fas fa-file-csv"></i></button>
           </div>
           <div class="col-sm-4">
           </div>
           <div class="col-sm-4">
                   <h4>BUSCAR.. <i class="fas fa-search"></i>  <input class="form-control btn-sm" id="myInput" type="text" placeholder="Search.."></h4>
           </div>
      </div>
      <div class="row">
          <div class="col-sm-12">
           <div class="table-responsive">          
              <table class="table table-sm table-striped  table-hover">
              <thead>
              <tr>
              <th class="small col-sm-1" >OPERACION</th>
              <th class="small col-sm-2" >FECHA</th>
              <th class="small col-sm-6" >DETALLE</th>
              <th class="small col-sm-1" >MONTO</th>
              <th class="small col-sm-2" >TIPO MOVIMIENTO</th>
              <th class="small col-sm-2" >ACCIONES</th>    
             </tr>
              </thead>
              <tbody id="myTable">
              <asp:Repeater ID="RPT_REPORTE" OnItemCommand="RPT_REPORTE_ItemCommand"  runat="server" >
              <ItemTemplate>
              <tr>
                <td class="small text-bold col-sm-1" ><%# Eval("OPERACION") %></td>
                <td class="small text-bold col-sm-2" ><%# Eval("FECHA") %></td>
                <td class="small col-sm-6" ><%# Eval("DETALLE") %></td>
                <td class="small col-sm-1" ><%# Eval("MONTO") %></td>
                <td class="small col-sm-2" ><%# Eval("TIPO") %></td>
                  <td class="small col-sm-2" >
                  <asp:Button ID="BTN_VER_VALORACION" CommandName="VALORACION" CommandArgument='<%# Eval("OPERACION") %>'  ToolTip="¿ VER DETALLE DE VALORACION ?" data-toggle="tooltip"   class=" btn  btn-success" runat="server" Text='V' /> 
                  <asp:Button ID="BTN_VER_OPERACION" CommandName="OPERACION" CommandArgument='<%# Eval("OPERACION") %>'  ToolTip="¿ VER DETALLE DE OPERACION  ?" data-toggle="tooltip"  class=" btn btn-primary" runat="server" Text='O'/> 
                 </td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
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
