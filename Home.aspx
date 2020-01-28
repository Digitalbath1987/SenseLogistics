<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Operaciones.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script>
         window.onload = function () {

             var chart = new CanvasJS.Chart("chartContainer", {
                 theme: "light2", // "light1", "light2", "dark1", "dark2"
                 exportEnabled: true,
                 animationEnabled: true,
                 title: {
                     text: "VALORACIONES PENDIENTES V/S LIQUIDADAS",
                     fontSize: 18
                 },
                 data: [{
                     type: "doughnut",
                     startAngle: 140,
                     toolTipContent: "<b>{label}</b>: {y} ",
                     indexLabel: "{label} - {y} ",
                     dataPoints: <%= CARGAR_CHART() %>
                      }]
             });
        
             chart.render();

            var chart2 = new CanvasJS.Chart("chartContainer2", {
                animationEnabled: true,
                 exportEnabled: true,
	            theme: "light2", // "light1", "light2", "dark1", "dark2"
	            title: {
                    text: "CANTIDAD DE OPERACIONES X MES EN EL ULTIMO AÑO",
                    fontSize: 18
	            },
	            axisY: {
		            prefix: "CANT:",
		            scaleBreaks: {
			            customBreaks: [{
				            startValue: 90,
				            endValue: 100
			            }]
		            }
	            },
	            data: [{
		            type: "column",
		           // yValueFormatString: "##000",
                    dataPoints: <%= CARGAR_CHART_OPERACIONES_X_MES() %>
	            }]
            });
            chart2.render();

         }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <div class="container">
      <div class="row">
          
          <div class="col-sm-4">
              <div class="small-box bg-light-blue-active ">
                <div class="inner">
                <div class="row">
                    <div class="col-sm-12">
                         <h2>CLIENTES</h2>
                    </div>
                   </div>
                  <p>
        <asp:LinkButton ID="LnkB_Sol_Vacaciones"  class="btn btn-link text-black" Font-Size="XX-Large"   Font-Bold="true" data-toggle="tooltip" title="Ver Clientes Registrados"   runat="server" PostBackUrl="~/Listado_Cliente.aspx"><i class="fas fa-arrow-circle-right"></i></asp:LinkButton></p>
                </div>
                <div class="icon">
                  <i class="fas fa-user-check"></i>
                </div>
               </div>
            </div>

          <div class="col-sm-4">
              <div class="small-box bg-light-blue-active">
                <div class="inner">
              <h2>OPERACIONES</h2>
                  <p>
            <asp:LinkButton ID="Lnkb_Sol_Certificados" class="btn btn-link text-black"  Font-Size="XX-Large"  data-toggle="tooltip" title="Ver Operaciones Registradas"   Font-Bold="true" PostBackUrl="~/Listado_Operaciones.aspx" runat="server"><i class="fas fa-arrow-circle-right"></i></asp:LinkButton></p>
                </div>
                <div class="icon">
                  <i class="fas fa-folder-open"></i>
                </div>
               </div>
            </div>

           <div class="col-sm-4">
              <div class="small-box bg-light-blue-active">
                <div class="inner">
              <h2>BILL OF LADING</h2>
                  <p>
            <asp:LinkButton ID="LinkButton1" class="btn btn-link text-black"  Font-Size="XX-Large"  data-toggle="tooltip" title="Ver Operaciones Registradas"   Font-Bold="true" PostBackUrl="~/Listado_Bill_Lading.aspx" runat="server"><i class="fas fa-arrow-circle-right"></i></asp:LinkButton></p>
                </div>
                <div class="icon">
                <i class="fab fa-docker"></i>
                </div>
               </div>
            </div>

   
   </div>
      <div class="row">
            <div class="col-sm-4">
              <div class="small-box bg-light-blue-active">
                <div class="inner">
                  <h2>CUSTOMERS</h2>
                  <p><asp:LinkButton ID="Lnkb_Sol_Liqui" class="btn btn-link text-black" Font-Size="XX-Large"  Font-Bold="true" data-toggle="tooltip" title="Ver Customers Registrados"  PostBackUrl="~/Listado_Customer.aspx" runat="server"><i class="fas fa-arrow-circle-right"></i></asp:LinkButton></p>
                </div>
                <div class="icon">
                  <i class="fas fa-user-edit"></i>
                </div>
               </div>
            </div>

          <div class="col-sm-4">
              <div class="small-box bg-light-blue-active">
                <div class="inner">
                  <h2>COMERCIALES</h2>
                  <p>
              <asp:LinkButton ID="Lnkb_Asistencia" Font-Bold="true" class="btn btn-link text-black" Font-Size="XX-Large" data-toggle="tooltip" title="Ver Comerciales Registrados"    PostBackUrl="~/Listado_Comercial.aspx" runat="server"><i class="fas fa-arrow-circle-right"></i></asp:LinkButton></p>
                </div>
                <div class="icon">
                  <i class="fas fa-user-tag"></i>
                </div>
               </div>
            </div>

             <div class="col-sm-4">
              <div class="small-box bg-light-blue-active">
                <div class="inner">
                  <h2>FACTURAS</h2>
                  <p>
              <asp:LinkButton ID="LinkButton2" Font-Bold="true" class="btn btn-link text-black" Font-Size="XX-Large" data-toggle="tooltip" title="Ver Comerciales Registrados"    PostBackUrl="~/Listado_Facturas.aspx" runat="server"><i class="fas fa-arrow-circle-right"></i></asp:LinkButton></p>
                </div>
                <div class="icon">
                  <i class="fas fa-file-invoice-dollar"></i>
                </div>
               </div>
            </div>


   </div>

    <div class="row">
            <div class="col-sm-6">
                 <div id="chartContainer" style="height: 370px; width: 100%;"></div>
            </div>
            <div class="col-sm-6">
                 <div id="chartContainer2" style="height: 370px; width: 100%;"></div>
            </div>
        </div>
    </div>

<script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
<script src="https://canvasjs.com/assets/script/jquery-1.11.1.min.js"></script>



</asp:Content>
