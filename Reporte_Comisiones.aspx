<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Reporte_Comisiones.aspx.cs" Inherits="Operaciones.Reporte_Comisiones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">



    
  
    <div class="panel-group">
    <div class="panel panel-primary">
    <div class="panel-heading text-bold"><h4> REPORTE DE COMISIONES X FECHA DE OPERACION</h4> </div>
    <div class="panel-body">
    <hr/>    
    
    <div class="row">
                <div class="col-md-4">
                     <div class="form-group">
                     <label for="pwd">FECHAS:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><asp:Button ID="BTN_FILTRAR_FECHAS" runat="server" OnClick="BTN_FILTRAR_FECHAS_Click" Text="FILTRAR" data-toggle="tooltip" title="Click Para Filtrar" /> </span>
                     <asp:TextBox ID="TXT_FECHA_DESDE" runat="server" TextMode="Date" class="form-control" ></asp:TextBox>
                     <asp:TextBox ID="TXTFECHA_HASTA" runat="server" TextMode="Date"  class="form-control" ></asp:TextBox>
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
               <button type="button" class="btn btn-primary btn-lg" visible="false" data-toggle="tooltip" title="Descargar a .csv" runat="server" id="BTN_DESCARGAR_CSV" onserverclick="BTN_DESCARGAR_CSV_ServerClick"  > DESCARGAR <i class="fas fa-file-csv"></i></button>

           </div>
           <div class="col-sm-4"></div>
           <div class="col-sm-4"><h4>BUSCAR.. <i class="fas fa-search"></i>  <input class="form-control btn-sm" id="myInput" type="text" placeholder="Search.."></h4></div>
        </div>
        <div class="row">
              <div class="table-responsive">          
              <table class="table table-sm table-striped  table-hover">
              <thead>
              <tr>
              <th class="small" >NRO OPERACION</th>
              <th class="small" >AEREO</th>
              <th class="small" >MARITIMO</th>
              <th class="small" >T. INTERNACIONAL</th>
              <th class="small" >T. NACIONAL</th>
              <th class="small" >A.G.A.</th>
              <th class="small" >SEGURO</th>
              <th class="small" >FECHA OPERACION</th>
              <th class="small" >CLIENTE</th>
              <th class="small" >RUT</th>
              <th class="small" >COMERCIAL</th>
              <th class="small" >% COMISION</th>
              <th class="small" >MONTO(CLP)</th>
              <th class="small" >ACCIONES</th>   
              </tr>
              </thead>
              <tbody id="myTable">
              <asp:Repeater ID="RPT_REPORTE"  OnItemCommand="RPT_REPORTE_ItemCommand" runat="server" >
              <ItemTemplate>
              <tr>
                <td class="small text-bold" ><%# Eval("ID_OPERACIONES") %></td>
                <td class="small" ><%# Eval("AEREO") %></td>
                <td class="small" ><%# Eval("MARITIMO") %></td>
                <td class="small" ><%# Eval("T_INTERNACIONAL") %></td>
                <td class="small" ><%# Eval("T_NACIONAL") %></td>
                <td class="small" ><%# Eval("AGA") %></td>
                <td class="small" ><%# Eval("SEGURO") %></td>
                <td class="small" ><%# Eval("FECHA_OPERACION") %></td>
                <td class="small" ><%# Eval("CLIENTE") %></td>
                <td class="small" ><%# Eval("RUT") %></td>
                <td class="small" ><%# Eval("NOMBRE_COMERCIAL") %></td>
                <td class="small" ><%# Eval("PORCENTAJE") %></td>
                <td class="small" ><%# Eval("MONTO_CLP") %></td>
                <td class="small hidden-sm col-sm-1">
                    <asp:Button ID="BTN_VER_VALORACION" CommandName="VALORACION" CommandArgument='<%# Eval("ID_OPERACIONES") %>'  ToolTip="¿ VER DETALLE DE VALORACION ?" data-toggle="tooltip"   class=" btn  btn-success" runat="server" Text='V' /> 
                    <asp:Button ID="BTN_VER_OPERACION" CommandName="OPERACION" CommandArgument='<%# Eval("ID_OPERACIONES") %>'  ToolTip="¿ VER DETALLE DE OPERACION  ?" data-toggle="tooltip"  class=" btn btn-primary" runat="server" Text='O'/>  
             
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
