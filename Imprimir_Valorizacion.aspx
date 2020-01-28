<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Imprimir_Valorizacion.aspx.cs" Inherits="Operaciones.Imprimir_Valorizacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>

   </head>
<body>
<form id="form1" runat="server">
<div class="container">
     <div class="row">
      <div class="col-sm-4">
          <img class="img-responsive" src="Imagenes/Logo_Mediano.PNG" width="700" height="170"  alt="Chania">
      </div>
      <div class="col-sm-8">
            <h3><label>CIERRE FILE : </label> <asp:Label ID="TXT_OPERACION" runat="server" ></asp:Label></h3>
      </div>
      </div>
           
    <br />
    <div class="row">
       <div class="col-sm-8">
       <div class="panel panel-primary">
       <div class="panel-body">
       <h3><label>DATOS DEL CLIENTE</label></h3>
       <div class="row">
       <div class="col-sm-4">
       <h5>RUT CLIENTE  :  <asp:Label ID="TXT_RUT" runat="server" ></asp:Label></h5>
       </div> 
       <div class="col-sm-8">
       <h5>NOMBRE CLIENTE : <asp:Label ID="TXT_NOMBRE" runat="server" ></asp:Label></h5>
       </div>  
       </div>
       <div class="row">
       <div class="col-sm-4">
       <h5>CIUDAD : <asp:Label ID="TXT_CIUDAD" runat="server" ></asp:Label></h5>
       </div>
       <div class="col-sm-8">
       <h5>DIRECCION :  <asp:Label ID="TXT_DIRECCION" runat="server" ></asp:Label> </h5>
       </div>
       </div>
       <div class="row">
       <div class="col-sm-4">
       <h5>FECHA CREACION :   <asp:Label ID="TXT_FECHA_CREACION" runat="server" ></asp:Label></h5>
       </div>
       <div class="col-sm-4">
       <h5>FECHA PAGO :   <asp:Label ID="TXT_FECHA_PAGO" runat="server" ></asp:Label></h5> 
       </div>
       </div>
       </div>
       </div>
       </div>
       <div class="col-sm-4">
       <div class="panel panel-primary">
       <div class="panel-body">
       <h3><label>TASA DE CAMBIO</label></h3>
       <div class="row">
       <div class="col-sm-12">
       <h5>TASA CAMBIO USD : <asp:Label ID="TXT_VALOR_USD" runat="server" ></asp:Label></h5> 
       </div>
       </div>  
           
       <div class="row">
       <div class="col-sm-12">
       <h5>TASA CAMBIO EUR : <asp:Label ID="TXT_VALOR_EUR" runat="server" ></asp:Label></h5> 
       </div>
       </div> 
       <div class="row">
       <div class="col-sm-12">
        <h5>TASA CAMBIO GBP :  <asp:Label ID="TXT_VALOR_GBP" runat="server" ></asp:Label></h5> 
       </div>
       </div>
       </div>
       </div>
        </div>
    </div>

  
       <div class="panel panel-primary">
       <div class="panel-body">
       <h3><label>COSTOS AFECTOS</label></h3>
       <asp:GridView ID="GV_COSTOS_AFECTOS" CssClass="table-responsive col-sm-12" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Sin Registros" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="COSTOS_AFECTOS" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
       <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
       <Columns>
       <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO"  ItemStyle-CssClass="col-sm-1" SortExpression="REGISTRO" Visible="false" ></asp:BoundField>
       <asp:BoundField DataField="ID" HeaderText=" NRO"  ItemStyle-CssClass="col-sm-1" SortExpression="ID" ReadOnly="True"></asp:BoundField>
       <asp:BoundField DataField="DETALLE" HeaderText=" DETALLE"  ItemStyle-CssClass="col-sm-5" SortExpression="DETALLE"></asp:BoundField>
       <asp:BoundField DataField="DETALLE_MONEDA" HeaderText=" MONEDA"  ItemStyle-CssClass="col-sm-2" SortExpression="DETALLE_MONEDA"></asp:BoundField>
       <asp:BoundField DataField="MONTO" HeaderText=" MONTO"  ItemStyle-CssClass="col-sm-2" SortExpression="MONTO"></asp:BoundField>
            <asp:BoundField DataField="MONTO_USD" HeaderText=" MONTO USD"  ItemStyle-CssClass="col-sm-2" SortExpression="MONTO_USD"></asp:BoundField>
       <asp:BoundField DataField="MONTO_CLP"  ItemStyle-CssClass="col-sm-2" HeaderText=" MONTO CLP" SortExpression="MONTO_CLP"></asp:BoundField>
       </Columns>
       <FooterStyle BackColor="#CCCCCC"></FooterStyle>
       <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>
       <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>
       <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
       <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>
       <SortedAscendingHeaderStyle BackColor="#808080"></SortedAscendingHeaderStyle>
       <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>
       <SortedDescendingHeaderStyle BackColor="#383838"></SortedDescendingHeaderStyle>
       </asp:GridView>
       <asp:SqlDataSource runat="server" ID="COSTOS_AFECTOS" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_COSTOS_AFECTOS_X_OP" SelectCommandType="StoredProcedure">
       <SelectParameters>
       <asp:ControlParameter ControlID="TXT_OPERACION" PropertyName="Text" Name="ID_OPERACIONES" Type="Int32"></asp:ControlParameter>
       </SelectParameters>
       </asp:SqlDataSource>
       </div>
       </div>

       <div class="panel panel-primary">
       <div class="panel-body">
       <h3><label>COSTOS EXENTO</label></h3>
       <asp:GridView ID="GV_COSTO_EXENTO" CssClass="table-responsive col-sm-12" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Sin Registros" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="COSTOS_EXENTOS" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
       <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
       <Columns>
       <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO"  ItemStyle-CssClass="col-sm-1" SortExpression="REGISTRO" Visible="false" ></asp:BoundField>
       <asp:BoundField DataField="ID" HeaderText=" NRO"  ItemStyle-CssClass="col-sm-1" SortExpression="ID" ReadOnly="True"></asp:BoundField>
       <asp:BoundField DataField="DETALLE" HeaderText=" DETALLE"  ItemStyle-CssClass="col-sm-5" SortExpression="DETALLE"></asp:BoundField>
       <asp:BoundField DataField="DETALLE_MONEDA" HeaderText=" MONEDA"  ItemStyle-CssClass="col-sm-2" SortExpression="DETALLE_MONEDA"></asp:BoundField>
       <asp:BoundField DataField="MONTO" HeaderText=" MONTO"  ItemStyle-CssClass="col-sm-2" SortExpression="MONTO"></asp:BoundField>
            <asp:BoundField DataField="MONTO_USD" HeaderText=" MONTO USD"  ItemStyle-CssClass="col-sm-2" SortExpression="MONTO_USD"></asp:BoundField>
       <asp:BoundField DataField="MONTO_CLP"  ItemStyle-CssClass="col-sm-2" HeaderText=" MONTO CLP" SortExpression="MONTO_CLP"></asp:BoundField>
       </Columns>
       <FooterStyle BackColor="#CCCCCC"></FooterStyle>
       <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>
       <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>
       <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
       <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>
       <SortedAscendingHeaderStyle BackColor="#808080"></SortedAscendingHeaderStyle>
       <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>
       <SortedDescendingHeaderStyle BackColor="#383838"></SortedDescendingHeaderStyle>
       </asp:GridView>
       <asp:SqlDataSource runat="server" ID="COSTOS_EXENTOS" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_COSTOS_EXENTOS_X_OP" SelectCommandType="StoredProcedure">
       <SelectParameters>
       <asp:ControlParameter ControlID="TXT_OPERACION" PropertyName="Text" Name="ID_OPERACIONES" Type="Int32"></asp:ControlParameter>
       </SelectParameters>
       </asp:SqlDataSource>
       </div>
       </div>

       <div class="panel panel-primary">
       <div class="panel-body">
       <h3><label>VENTA AFECTA</label></h3>
       <asp:GridView ID="GV_VENTA_AFECTA" CssClass="table-responsive col-sm-12" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Sin Registros" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="VENTAS_AFECTAS" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
       <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
       <Columns>
       <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO"  ItemStyle-CssClass="col-sm-1" SortExpression="REGISTRO" Visible="false" ></asp:BoundField>
       <asp:BoundField DataField="ID" HeaderText=" NRO"  ItemStyle-CssClass="col-sm-1" SortExpression="ID" ReadOnly="True"></asp:BoundField>
       <asp:BoundField DataField="DETALLE" HeaderText=" DETALLE"  ItemStyle-CssClass="col-sm-5" SortExpression="DETALLE"></asp:BoundField>
       <asp:BoundField DataField="DETALLE_MONEDA" HeaderText=" MONEDA"  ItemStyle-CssClass="col-sm-2" SortExpression="DETALLE_MONEDA"></asp:BoundField>
       <asp:BoundField DataField="MONTO" HeaderText=" MONTO"  ItemStyle-CssClass="col-sm-2" SortExpression="MONTO"></asp:BoundField>
            <asp:BoundField DataField="MONTO_USD" HeaderText=" MONTO USD"  ItemStyle-CssClass="col-sm-2" SortExpression="MONTO_USD"></asp:BoundField>
       <asp:BoundField DataField="MONTO_CLP"  ItemStyle-CssClass="col-sm-2" HeaderText=" MONTO CLP" SortExpression="MONTO_CLP"></asp:BoundField>
       </Columns>
       <FooterStyle BackColor="#CCCCCC"></FooterStyle>
       <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>
       <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>
       <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
       <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>
       <SortedAscendingHeaderStyle BackColor="#808080"></SortedAscendingHeaderStyle>
       <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>
       <SortedDescendingHeaderStyle BackColor="#383838"></SortedDescendingHeaderStyle>
       </asp:GridView>
       <asp:SqlDataSource runat="server" ID="VENTAS_AFECTAS" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_VENTAS_AFECTAS_X_OP" SelectCommandType="StoredProcedure">
       <SelectParameters>
       <asp:ControlParameter ControlID="TXT_OPERACION" PropertyName="Text" Name="ID_OPERACIONES" Type="Int32"></asp:ControlParameter>
       </SelectParameters>
       </asp:SqlDataSource>
       </div>
       </div>

       <div class="panel panel-primary">
       <div class="panel-body">
       <h3><label>VENTA EXENTA</label></h3>
       <asp:GridView ID="GV_VENTA_EXENTA" CssClass="table-responsive col-sm-12" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Sin Registros" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="VENTAS_EXENTAS" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
       <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
       <Columns>
       <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO"  ItemStyle-CssClass="col-sm-1" SortExpression="REGISTRO" Visible="false" ></asp:BoundField>
       <asp:BoundField DataField="ID" HeaderText=" NRO"  ItemStyle-CssClass="col-sm-1" SortExpression="ID" ReadOnly="True"></asp:BoundField>
       <asp:BoundField DataField="DETALLE" HeaderText=" DETALLE"  ItemStyle-CssClass="col-sm-5" SortExpression="DETALLE"></asp:BoundField>
       <asp:BoundField DataField="DETALLE_MONEDA" HeaderText=" MONEDA"  ItemStyle-CssClass="col-sm-2" SortExpression="DETALLE_MONEDA"></asp:BoundField>
       <asp:BoundField DataField="MONTO" HeaderText=" MONTO"  ItemStyle-CssClass="col-sm-2" SortExpression="MONTO"></asp:BoundField>
            <asp:BoundField DataField="MONTO_USD" HeaderText=" MONTO USD"  ItemStyle-CssClass="col-sm-2" SortExpression="MONTO_USD"></asp:BoundField>
       <asp:BoundField DataField="MONTO_CLP"  ItemStyle-CssClass="col-sm-2" HeaderText=" MONTO CLP" SortExpression="MONTO_CLP"></asp:BoundField>
       </Columns>
       <FooterStyle BackColor="#CCCCCC"></FooterStyle>
       <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>
       <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>
       <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
       <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>
       <SortedAscendingHeaderStyle BackColor="#808080"></SortedAscendingHeaderStyle>
       <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>
       <SortedDescendingHeaderStyle BackColor="#383838"></SortedDescendingHeaderStyle>
       </asp:GridView>
       <asp:SqlDataSource runat="server" ID="VENTAS_EXENTAS" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_VENTAS_EXENTAS_X_OP" SelectCommandType="StoredProcedure">
       <SelectParameters>
       <asp:ControlParameter ControlID="TXT_OPERACION" PropertyName="Text" Name="ID_OPERACIONES" Type="Int32"></asp:ControlParameter>
       </SelectParameters>
       </asp:SqlDataSource>
       </div>
       </div>

   <%--    <div class="panel panel-primary">
       <div class="panel-body">
       <h3><label>COMISIONES</label></h3>
       <asp:GridView ID="GV_COMISIONES" CssClass="table-responsive col-sm-12" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Sin Registros" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="COMISIONES" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
       <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
       <Columns>
       <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO"  ItemStyle-CssClass="col-sm-1" SortExpression="REGISTRO" Visible="false" ></asp:BoundField>
       <asp:BoundField DataField="ID" HeaderText=" NRO"  ItemStyle-CssClass="col-sm-2" SortExpression="ID" ReadOnly="True"></asp:BoundField>
       <asp:BoundField DataField="TIPO_COMISION" HeaderText=" COMISION"  ItemStyle-CssClass="col-sm-2" SortExpression="TIPO_COMISION"></asp:BoundField>
       <asp:BoundField DataField="NOMBRE" HeaderText=" NOMBRE"  ItemStyle-CssClass="col-sm-3" SortExpression="NOMBRE"></asp:BoundField>
       <asp:BoundField DataField="PORCENTAJE" HeaderText=" PORCENTAJE"  ItemStyle-CssClass="col-sm-2" SortExpression="PORCENTAJE"></asp:BoundField>
       <asp:BoundField DataField="MONTO_CLP"  ItemStyle-CssClass="col-sm-2" HeaderText=" MONTO CLP" SortExpression="MONTO_CLP"></asp:BoundField>
       </Columns>
       <FooterStyle BackColor="#CCCCCC"></FooterStyle>
       <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>
       <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>
       <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
       <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>
       <SortedAscendingHeaderStyle BackColor="#808080"></SortedAscendingHeaderStyle>
       <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>
       <SortedDescendingHeaderStyle BackColor="#383838"></SortedDescendingHeaderStyle>
       </asp:GridView>
       <asp:SqlDataSource runat="server" ID="COMISIONES" ConnectionString='<%$ ConnectionStrings:Operaciones.Properties.Settings.CONEXION %>' SelectCommand="SP_COMISIONES_X_OP" SelectCommandType="StoredProcedure">
       <SelectParameters>
       <asp:ControlParameter ControlID="TXT_OPERACION" PropertyName="Text" Name="ID_OPERACIONES" Type="Int32"></asp:ControlParameter>
       </SelectParameters>
       </asp:SqlDataSource>
       </div>
       </div>--%>


    
       <div class="panel panel-primary">
       <div class="panel-body">
       <h3><label> VALORES TOTALES</label></h3>
    
       <div class="row">
       <div class="col-sm-3">
       <label>COSTOS EXENTOS :</label> <asp:Label ID="LBL_COSTOS_EXENTOS" Font-Bold="true"  runat="server" Text="0"></asp:Label>
       </div>
       <div class="col-sm-3">
       <label>COSTOS AFECTOS :</label><asp:Label ID="LBL_COSTOS_AFECTAS" Font-Bold="true"  runat="server" Text="0"></asp:Label>
       </div>
       <div class="col-sm-3">
       <label>IVA COSTOS :</label> <asp:Label ID="LBL_IVA_COSTOS" runat="server"  Font-Bold="true" Text="0"></asp:Label>
       </div>
       <div class="col-sm-3">
       <label>TOTAL COSTOS :</label><asp:Label ID="LBL_TOTAL_COSTOS" runat="server" Font-Bold="true"  Text="0"></asp:Label>
       </div>
       </div>
            <hr />
       <div class="row">
       <div class="col-sm-3">
       <label>VENTAS EXENTAS :</label><asp:Label ID="LBL_VENTAS_EXENTAS" runat="server" Font-Bold="true"  Text="0"></asp:Label>
       </div>
       <div class="col-sm-3">
       <label>VENTAS AFECTAS :</label><asp:Label ID="LBL_VENTAS_AFECTAS" runat="server" Font-Bold="true"  Text="0"></asp:Label>
       </div>
       <div class="col-sm-3">
       <label>IVA VENTAS :</label><asp:Label ID="LBL_IVA_VENTAS" runat="server" Font-Bold="true"  Text="0"></asp:Label>
       </div>
       <div class="col-sm-3">
       <label>TOTAL VENTAS :</label><asp:Label ID="LBL_TOTAL_VENTAS" runat="server" Font-Bold="true"  Text="0"></asp:Label>
       </div>
       </div>

             <hr />
       <div class="row">
       <div class="col-sm-3">
       <label></label>
       </div>
       <div class="col-sm-3">
       <label></label>
       </div>
       <div class="col-sm-6">
       PROFIT :
       
      <asp:TextBox ID="LBL_PROFIT"  CssClass="form-control small" Font-Bold="true" runat="server"></asp:TextBox> 
       </div>
       </div>
       <br />
</div>
</div>




</div>



  






    </form>
</body>
</html>
