<%@ Page Title="" Language="C#" MasterPageFile="~/Home_Master.Master" AutoEventWireup="true" CodeBehind="Crear_Comercial.aspx.cs" Inherits="Operaciones.Crear_Comercial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      
   <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
   <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/js/bootstrap-select.js"></script>
   <link href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" rel="stylesheet">
   <script src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>


     <style type="text/css">

 .slow .toggle-group { transition: left 0.7s; -webkit-transition: left 0.7s; }
.modalDialog2
 {
 position: fixed;
 top: 0;
 right: 0;
 bottom: 0;
 left: 0;
 background: rgba(180,180,180,0.8);
 z-index: 99999;
 opacity: 0;
 -webkit-transition: opacity 600ms ease-in;
 -moz-transition: opacity 600ms ease-in;
 transition: opacity 600ms ease-in;
 pointer-events: none;
 }
 .modalDialog2:target
 {
 opacity: 1;
 pointer-events: auto;
 }
 
 .modalDialog2 > div
 {
 width: 52%;
 position: center;
 margin: 5% auto;
 padding: 5px 20px 13px 20px;
 border-radius: 10px;
 background: #fff;
 background: -moz-linear-gradient(#fff, #999);
 background: -webkit-linear-gradient(#fff, #999);
 background: #fff;
    top: 0px;
    left: -21px;
}

.modalDialog
 {
 position: fixed;
 top: 0;
 right: 0;
 bottom: 0;
 left: 0;
 background: rgba(180,180,180,0.8);
 z-index: 99999;
 opacity: 0;
 -webkit-transition: opacity 600ms ease-in;
 -moz-transition: opacity 600ms ease-in;
 transition: opacity 600ms ease-in;
 pointer-events: none;
 }
 .modalDialog:target
 {
 opacity: 1;
 pointer-events: auto;
 }
 
 .modalDialog > div
 {
 width: 90%;
 position: center;
 margin: 10% auto;
 padding: 5px 20px 13px 20px;
 border-radius: 10px;
 background: #fff;
 background: -moz-linear-gradient(#fff, #999);
 background: -webkit-linear-gradient(#fff, #999);
 background: #fff;
    top: 0px;
    left: -21px;
}
 
 .close
 {
 background: #606061;
 color: #FFFFFF;
 line-height: 25px;
 position: absolute;
 right: -12px;
 text-align: center;
 top: -10px;
 width: 24px;
 text-decoration: none;
 font-weight: bold;
 -webkit-border-radius: 12px;
 -moz-border-radius: 12px;
 border-radius: 12px;
 -moz-box-shadow: 1px 1px 3px #000;
 -webkit-box-shadow: 1px 1px 3px #000;
 box-shadow: 1px 1px 3px #000;
 }
 
 .close:hover
 {
 background: #00d9ff;
 }

 .DataGridFixedHeader
{
position: relative;
top: expression(this.offsetParent.scrollTop-3); /*this works fine with IE only, but FireFox seems to be ignoring this*/
}
   .scrolling-table-container {
    height: 378px;
    overflow-y: scroll;
    overflow-x: hidden;
}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

   
    <div class="container">

   <%----------------------------------------------------------%>
   <%---    REGISTRAR COMERCIAL                           -----%>
   <%----------------------------------------------------------%>

  <div class="panel-group">
    <div class="panel panel-primary">
      <div class="panel-heading text-bold"><h4> CREAR COMERCIAL  </h4> </div>
      <div class="panel-body">
         <asp:Label ID="ID_COMERCIAL" Visible="false"  runat="server" Text="0"></asp:Label>

         <div class="row">
       
              <div class="col-md-4">
                     <div class="form-group">
                     <label for="pwd">NOMBRE COMPLETO:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-user"></i></span>
                     <asp:TextBox ID="TXT_NOMBRE" runat="server" class="form-control"  name="password" placeholder="(OBLIGATORIO)" REQUIRED></asp:TextBox>
                     </div>
                     </div>
                  </div>

              <div class="col-md-4">
                     <div class="form-group">
                     <label for="pwd">EMAIL:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-at"></i></span>
                     <asp:TextBox ID="TXT_EMAIL" runat="server" class="form-control" name="password" placeholder="(NO OBLIGATORIO)" REQUIRED></asp:TextBox>
                     </div>
                     </div>
                  </div>

                 <div class="col-md-4">
                     <div class="form-group">
                     <label for="pwd">TELEFONO:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-phone"></i></span>
                     <asp:TextBox ID="TXT_TELEFONO" runat="server" class="form-control" name="password" placeholder="(NO OBLIGATORIO)" REQUIRED></asp:TextBox>
                     </div>
                     </div>
                  </div>
             
         </div>

          <br />
          <div class="row">
          <div class="col-md-4">
                     <div class="form-group">
                     <label for="pwd">RUT:</label>
                     <div class="input-group">
                     <span class="input-group-addon"><i class="fas fa-phone"></i></span>
                     <asp:TextBox ID="TXT_RUT" runat="server" class="form-control" name="password" placeholder="(NO OBLIGATORIO)" REQUIRED></asp:TextBox>
                     </div>
                     </div>
                  </div>
          </div>
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
                    <asp:Button ID="BTN_GUARDAR" Visible="false" runat="server" Text="CREAR COMERCIAL"  OnClick="BTN_GUARDAR_Click" class="btn btn-primary" data-toggle="tooltip" title="Click Para Guardar" /> 
                    <asp:Button ID="BTN_ACTUALIZAR" Visible="false" runat="server" Text="ACTUALIZAR COMERCIAL" OnClick="BTN_ACTUALIZAR_Click"  class="btn btn-primary"  data-toggle="tooltip" title="Click Para Actualizar" />
               </div>
       
        </div>
  
</div>
</div>
</div>
                 

</div>
    
<script type="text/javascript">
// Material Select Initialization
$(document).ready(function() {
$('.mdb-select').materialSelect();
});
</script>



</asp:Content>
