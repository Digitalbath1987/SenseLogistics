<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Operaciones.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
  <link href="css/Index.css" rel="stylesheet" />
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css" integrity="sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU" crossorigin="anonymous">

   <title>Sense Logistics Ltda.</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <div class="container"><br /><br /><br />
	<div class="d-flex justify-content-center h-100">
		<div class="card">
			<div class="card-header">
				<h3>Iniciar Sesión</h3>
				<div class="d-flex justify-content-end social_icon">
					<span><i class="fab fa-facebook-square" data-toggle="tooltip" title="ir a Facebook"></i></span>
					<span><i class="fab fa-twitter-square" data-toggle="tooltip" title="ir a Twitter"></i></span>
				</div>
			</div>
			<div class="card-body">
			<div class="input-group form-group">
						<div class="input-group-prepend">
							<span class="input-group-text"><i class="fas fa-user"></i></span>
						</div>
				           <asp:TextBox ID="USER" CssClass="form-control"  data-toggle="tooltip" data-placement="right" title="Ingresar Usuario" runat="server" REQUIRED></asp:TextBox>
					</div>
					<div class="input-group form-group">
						<div class="input-group-prepend">
							<span class="input-group-text"><i class="fas fa-key"></i></span>
						</div>
                        <asp:TextBox ID="CONTRASENA" TextMode="Password" CssClass="form-control" data-placement="right"  data-toggle="tooltip" title="Ingresar Contraseña"  runat="server" REQUIRED></asp:TextBox>
					</div>
					<div class="row align-items-center remember">
						<input type="checkbox" data-toggle="tooltip" title="Presiona Aqui Para Guardar Credenciales">Recordar
					</div>
					<div class="form-group">
	                <asp:Button ID="BTN_VALIDAR_USUARIO" runat="server" Text="ENTRAR" value="Entrar" data-placement="right" class="btn float-right login_btn" OnClick="BTN_VALIDAR_USUARIO_Click"/>
					</div>
			</div>
			<%--<div class="card-footer">
			
				<div class="d-flex justify-content-center">
					<a href="#" data-placement="right"  data-toggle="tooltip" title="Click Aqui Para Recordar Contraseña">Olvidaste Tu Contraseña?</a>
				</div>
			</div>--%>
		</div>
	</div>
</div>
        </div>
    </form>
 

       <div runat="server" id="CUADRO_ERROR" visible="false" class="alert alert-danger alert-dismissible fade show">
    <button type="button" class="close" data-dismiss="alert">&times;</button>
    <strong>Error de Autenticacion!</strong> <asp:Label ID="MENSAJE_ERROR" runat="server" Text="Label"></asp:Label> <i class="fas fa-exclamation-triangle"></i> .
    </div>
 

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

<footer class="text-sm-center text-white">
              <div class="container align-content-md-between col-6"> 
                 <h7> Developed by <a class="text-white" href="http://mariorosales.tk/" data-toggle="tooltip" title="Contactar"  target="_blank">Mario Rosales Figueroa</a> © Copyright 2019 All Rights Reserved , Sense Logistics Ltda.   </h7>
           </div>
</footer>


    


    <script>
$(document).ready(function(){
  $('[data-toggle="tooltip"]').tooltip();   
});
</script>


</body>
</html>
