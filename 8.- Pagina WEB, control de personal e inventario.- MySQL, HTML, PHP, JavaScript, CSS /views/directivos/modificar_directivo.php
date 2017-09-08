<?php
require_once 'class/class.altas.php';
//session_start();
if(isset($_SESSION['user']) && $_SESSION['permiso']=='directivo' && isset($_POST['btn_modificar_usuario'])){

?>

  <body class="skin-blue sidebar-mini">

	<style type="text/css">
		.label{margin-bottom: 7px;}
	</style>

	<script LANGUAGE="JavaScript">
			

	$(function(){//Funciones para validar fotografía
	  //Oculta contenedor	
	    contenedor = $('#foto_preview'); 
		contenedor.hide();	


	  //recuperar datos	
	  r_datos = function(options){

          options=(options==typeof(undefined))?'':options;
          params=options;
        var datos = $.ajax({
          url:'class/class.altas.php',
    		  type:'post',
    		  dataType:'json',
          data:params,
    		  async:false    		
    	 }).responseText;
        return datos
      }
		 
	  //Preview Imagen
	  function readURL(input) {
		contenedor = $('#foto_preview');  
      	if (input.files && input.files[0]) {
        	var reader = new FileReader();
        	reader.onload = function (e) {
           		contenedor.attr('src', e.target.result);
				contenedor.show()
        	}
        	reader.readAsDataURL(input.files[0]);
    	}
	  }	
	  
	  //Limpiar Input
	  function limpiar_input(input, contenedor) {
		var control_input = input;
      	control_input.replaceWith(control_input.val('').clone(true));
		contenedor.attr('src', '');
		contenedor.hide()
		$('#true_foto').val(false);
	  };
		
		//Valida el tamaño de la imagen
	   valida_tamano = function(input){
		var control_input = input; 
	   	size=(control_input.files[0].size/1024/1024).toFixed(2);
		return size
	   }
	   	//Validar extensión
	   valida_extension = function(input){
   		var control_input = input; 
    	var ext = control_input.value.match(/\.(.+)$/)[1].toLowerCase();
    	switch (ext) {
        	case 'jpg':
        	case 'jpeg':
        	case 'png':
        	case 'gif':
            	return true
        	default:
            	return false;
    	}
	   };
	   
	   mensajes_error = function(clave){
		  switch (clave) {
        	case 1: alert("La imagen debe pesar máximo 2Mb");break;
			case 2: alert("La extensión de la imagen es invalida");break;
        	default:
            	return false;
    	} 
	   }

	    can_mod_directivo = function(){
			if (confirm("Si cancelas ahora no se guardarán los cambios, deseas salir?")){
				document.location.href="?command=perfil_directivo";
			}
		}

	   function get_municipios(estado){
			var valoresm = r_datos({'get_estados':estado})
			//console.log(valores[0].replace("",""));

			m_valores = JSON.parse(valoresm)
			if ($("#estado").val()!='0'){$("#municipio").html(m_valores[0])}
	   }

		
		
      $('#image-file').on('change', function() {
		  size=valida_tamano(this);
		  extension=valida_extension(this)
		  console.log(valida_extension(this));
		  if (size<2&&extension==true){
			readURL(this);
			$('#btnClearFoto').show();
			$('#name_foto').val('');
		  }else{
			limpiar_input($('#image-file'),$('#foto_preview'))   
		  	if(extension==false){mensajes_error(2);}
			else{ 
				if(size>2){mensajes_error(1);}
			}
		  }
	  });

      $('#cont-image_preview').click(function(){
      	$('#image-file').click();
      })

      $('#btnClearFoto').hide();

      $('#btnClearFoto').click(function(){
      	limpiar_input($('#image-file'),$('#foto_preview'));
      	$('#btnClearFoto').hide();
      });

		//Validar los campos obligatorios
		 $("#form-alta").submit(function() {
			var x1 = $("#usuario").val();
			var x2 = $("#contrasena").val();
			var x3 = $("#permiso").val();
			if (x1==''||x2==''||x3=='0') {
				alert("Debes llenar todos los campos obligatorios");
				return false;
			} else
				return true;
		});


		//Recuperar estados
		$("#estado").on('change',function(){
			get_municipios($("#estado").val());
		})


		function get_perfil(usuario){
			var valores = r_datos({'get_perfil_usuario':usuario})
			//console.log(valores[0].replace("",""));
			v_valores = JSON.parse(valores)
			if (usuario!=0){
				$("#usuario").val(v_valores[0]);
				$("#contrasena").val(v_valores[1]);
				//console.log('$("#permiso option[value="'+ v_valores[21] +'"]")')
				$("#permiso option[value="+ v_valores[21] +"]").attr("selected",true);
				$("#telefono-local").val(v_valores[3]);
				$("#telefono-celular").val(v_valores[4]);
				$("#correo").val(v_valores[5]);
					if(v_valores[6]!=''){
						$("#foto_preview").attr('src',"uploads/usuarios/thumbs/"+v_valores[6]);
						$("#foto_preview").show();
						$('#btnClearFoto').show();
						$('#name_foto').val(v_valores[6]);
					}else{
						$("#foto_preview").attr('src',"images/no-image.jpg");
						$("#foto_preview").hide();
						$('#btnClearFoto').hide();
						$('#name_foto').val('');
					}
				$("#apellido-paterno").val(v_valores[7]);
				$("#apellido-materno").val(v_valores[8]);
				$("#nombre").val(v_valores[9]);
				$("#direccion").val(v_valores[10]);
				$("#estado option[value="+ v_valores[22] +"]").attr("selected",true);
				get_municipios(v_valores[22]);
				$("#municipio option[value="+ v_valores[23] +"]").attr("selected",true);
				$("#f-nacimiento").val(v_valores[13]);
				$("#l-nacimiento").val(v_valores[14]);
				$("#nacionalidad").val(v_valores[15]);
				$("#sex[value="+ v_valores[16] +"]").attr("checked",true);
				$("#nombre-emergencia1").val(v_valores[17]);
				$("#numero-emergencia1").val(v_valores[18]);
				$("#nombre-emergencia2").val(v_valores[19]);
				$("#numero-emergencia2").val(v_valores[20]);
				$("#anexo1").val(v_valores[24]);
				//Anexo 2 Forms extras
				v_valores_f_ext = (v_valores[25] != '') ? JSON.parse(v_valores[25] ) : '';
				var z = v_valores_f_ext;
					for(var i in z) {
						console.log(i)
						f_cont='#'+i;
						$(f_cont).val(v_valores_f_ext[i]);
					}
				$("#no_seguro_social").val(v_valores[27]);
				$("#no_cuenta").val(v_valores[28]);
				$('#usuario_activo').prop('checked',v_valores[26]==1 ? true : false);
			}
			
		}

		get_perfil(<?php echo isset($_POST['usuario'])?$_POST['usuario']:0; ?>);

		
			

	})
	
		
						
		</script>

<div class="wrapper">
      
      

      <?php require "views/directivos/menu_vertical.php" ?>      
      <?php require "views/directivos/header_directivo.php" ?>


      <div class="content-wrapper">

      
       <!-- Main content -->
        <section class="content"> 

			<section class="content-header">
				<h1>
					Modificar Usuario
				</h1>
				<ol class="breadcrumb">
            		<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
            		<li><a href="#">Modificar Usuario</a></li>
          		</ol>
			</section>

			<div id="" class="">

			<form action="index.php?command=perfil_directivo" method="POST" enctype="multipart/form-data" id="form-alta" role="form">

					<br />
					<div class="box box-primary">
						<div class="box-header"><h4>Datos generales</h4></div>
						<div class="box-body">						
							<div class="col-md-10 col-sm-10 col-xs-7">	
								<div class="form-group row">
									<input type="hidden" id="id_usuario" name="id_usuario" value="<?php echo isset($_POST['usuario'])?$_POST['usuario']:0; ?>">
									<label class="col-md-2 col-sm-2 col-xs-12">Usuario:*</label>
									<div class="col-md-5 col-sm-8 col-xs-12">
										<div class="input-group">
											<span class="input-group-addon">@</span>
											<input name="usuario" id="usuario"  placeholder="Usuario" class="form-control">
										</div>
									</div>
								</div>

								<div class="form-group row">							
									<div class="col-md-2 col-sm-2 col-xs-12" >Contrase&ntilde;a:*</div>
									<div class="col-md-5 col-sm-8 col-xs-12">
										<div class="input-group">
											<span class="input-group-addon"><i class="fa fa-key"></i></span>
											<input name="contrasena" type="password" id="contrasena" class="form-control" placeholder="Contrase&ntilde;a">
										</div>
									</div>					
								</div>

								<div class="form-group row">
									<label class="col-md-2 col-sm-2 col-xs-12" style="">Permiso*</label>
									<div class="col-md-5 col-sm-8 col-xs-12">	
										<select name="permiso" id="permiso" class="form-control">
											<option value="0">Selecciona...</option>
											<option value="1">Directivo</option>
											<option value="2">Auditor</option>
											<option value="3">Promotor</option>
										</select>
									</div>
								</div>

								<div class="form-group row">
									<label class="label col-md-2 col-sm-2 col-xs-2">Tel. Casa</label>
									<div class="col-md-5 col-sm-8 col-xs-12">
										<div class="input-group">
											<span class="input-group-addon"><i class="fa fa-phone"></i></span>
											<input type="text" class="form-control" name="telefono-local" id="telefono-local">
										</div>
									</div>
								</div>
								<div class="form-group row">
									<label class="label col-md-2 col-sm-2 col-xs-2">Tel. Celular</label>
									<div class="col-md-5 col-sm-8 col-xs-12">
										<div class="input-group">
											<span class="input-group-addon"><i class="fa fa-mobile-phone"></i></span>
											<input type="text" class="form-control" name="telefono-celular" id="telefono-celular">
										</div>
									</div>
								</div>	
								<div class="form-group row">	
									<label class="label col-md-2 col-sm-2 col-xs-2">Tel. Correo(s)</label>
									<div class="col-md-5 col-sm-8 col-xs-12">
										<div class="input-group">
											<span class="input-group-addon"><i class="fa fa-envelope"></i></span>
											<input type="text" class="form-control" name="correo" id="correo">
										</div>
									</div>
								</div>
								<div class="form-group row">									
									<label class="col-md-2 col-sm-2 col-xs-12">No. de seguridad social</label>
									<div class="col-md-5 col-sm-8 col-xs-12">
										<div class="input-group">
											<span class="input-group-addon"><i class="fa fa-plus"></i></span>
											<input type="text" class="form-control" name="no_seguro_social" id="no_seguro_social">
										</div>
									</div>
								</div>
								<div class="form-group row">
									<label class="col-md-2 col-sm-2 col-xs-12">No. de cuenta</label>
									<div class="col-md-5 col-sm-8 col-xs-12">
										<div class="input-group">
											<span class="input-group-addon"><i class="fa fa-credit-card"></i></span>
											<input type="text" class="form-control" name="no_cuenta" id="no_cuenta">
										</div>		
									</div>							
								</div>
								<div class="form-group row">
								<label class="col-md-2 col-sm-4 col-xs-12" style="">Usuario activo</label>
								<div class="col-md-5 col-sm-8 col-xs-12">
									<div class="input-group">
										<input class="minimal" name="usuario_activo" type="checkbox" id="usuario_activo" >
									</div>
								</div>								
							</div>
							</div>

							<div class="col-md-2 col-sm-2 col-xs-5">
								<div id="marco-foto">
									<div class="label" style=""> Foto:</div>

									<div id="cont-image_preview"><img src="" id="foto_preview"/></div>
									<button class="btn btn-block btn-primary" id="btnClearFoto">Borrar foto</button>
									<div class="">
										<input name="Foto1" id="image-file" type="file" class="" >
										<input name="name_foto" id="name_foto" type="hidden" class="" >
									</div>
								</div>
							</div>
						</div>
					</div>
						
					

					<div class="box box-primary">
						<div class="box-header"><h4>Nombre</h4></div>
						<div class="box-body">
							<div class="col-md-12 col-sm-12 col-xs-12">
								<div class="form-group row">
									<label class="col-md-2 col-sm-2 col-xs-2">Apellido Paterno</label>
									<div class="col-md-5 col-sm-8 col-xs-12">
										<input class="form-control" name="apellido-paterno" id="apellido-paterno" placeholder="Apellido Paterno" type="campo">							
									</div>
								</div>
	 							<div class="form-group row">
		 							<label class="col-md-2 col-sm-2 col-xs-2">Apellido Materno</label>
		 							<div class="col-md-5 col-sm-8 col-xs-12">
	 									<input class="form-control" name="apellido-materno" id="apellido-materno" placeholder="Apellido Materno" type="campo">
	 								</div>						
								</div>
								<div class="form-group row">
									<label class="col-md-2 col-sm-2 col-xs-2">Nombre(s)</label>
									<div class="col-md-5 col-sm-8 col-xs-12">
										<div class="input-group">
											<span class="input-group-addon"><i class="fa fa-user"></i></span>
											<input class="form-control" name="nombre" id="nombre" placeholder="Nombre" type="campo">						
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>	
					
					<div class="box box-primary">
						<div class="box-header"><h4>Dirección actual</h4></div>
						<div class="caja-campos row">
							<div class="col-md-12 col-sm-12 col-xs-12">
								<div class="row grid">
									<label class="label col-md-2 col-sm-2 col-xs-2">Calle, Número</label>
									<div class="col-md-5 col-sm-8 col-xs-12">
										<div class="input-group">
											<span class="input-group-addon"><i class="fa fa-map-marker"></i></span>
											<input class="campo col-md-5 col-sm-8 col-xs-12" name="direccion" id="direccion" placeholder="Dirección" type="campo">
										</div>
									</div>
								</div>
								<div class="row grid">
									<label class="label col-md-2 col-sm-2 col-xs-2" style="">Estado:</label>
									<div class="col-md-5 col-sm-8 col-xs-12">
										<select name="estado" id="estado" class="campo col-md-5 col-sm-8 col-xs-12">
				 							<option value="0">Selecciona...</option>
											<?php
											global $obj_db;
  		                     				$consulta2 = "SELECT id_estado, nombre FROM estados ORDER BY nombre ASC";
      		                 				$result2 = $obj_db->consulta($consulta2);
           	            					if($result2!=false){
			   		                      		foreach($result2 as $fila2){
               		          						echo "<option class='text-danger text-center' value ='".$fila2['id_estado']."'>".utf8_encode($fila2['nombre'])."</option>";
                   		       					}
                       		 				}
                       						?>
										</select>							
									</div>
								</div>
								<div class="row grid">
									<label class="label col-md-2 col-sm-2 col-xs-2" style="">Municipio:</label>
									<div class="col-md-5 col-sm-8 col-xs-12">
										<select name="municipio" id="municipio" class="campo col-md-5 col-sm-8 col-xs-12">
											<option value="0">Selecciona...</option>
										</select>
									</div>
								</div>
							</div>
						</div>
					</div>
					
					<div class="box box-primary">
						<div class="box-header"><h4>Otros</h4></div>
						<div class="box-body">
							<div class="col-md-12 col-sm-12 col-xs-12">
								<div class="form-group row">
									<div class="col-md-6 col-sm-6" >										
										<label class="col-md-4 col-sm-4 col-xs-12">Fecha de Nacimiento</label>
										<div class="col-md-8 col-sm-8 col-xs-12">
											<div class="input-group">
												<span class="input-group-addon"><i class="fa fa-calendar"></i></span>
												<input class="form-control" name="f-nacimiento" placeholder="Fecha de Nacimento" type="campo" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask>
											</div>
										</div>
									</div>
									<div class="col-md-6 col-sm-6">
										<label class="col-md-4 col-sm-4 col-xs-2">Lugar de Nacimiento</label>
										<div class="col-md-8 col-sm-8 col-xs-12">
											<div class="input-group">
												<span class="input-group-addon"><i class="fa fa-dot-circle-o"></i></span>
												<input class="form-control" name="l-nacimiento" placeholder="Lugar de Nacimento" type="campo">
											</div>
										</div>
									</div>
									
								</div>
								<div class="form-group row">
								
								
								</div>
								<div class="form-group row">
									<div class="col-md-6 col-sm-6" >
										<label class="col-md-4 col-sm-4 col-xs-2">Nacionalidad</label>
										<div class="col-md-8 col-sm-8 col-xs-12">
											<div class="input-group">
												<span class="input-group-addon"><i class="fa fa-map-marker"></i></span>
												<input class="form-control" name="nacionalidad" placeholder="Nacionalidad" type="campo">
											</div>
										</div>
									</div>
									<div class="col-md-6 col-sm-6" >
										<label class="col-md-4 col-sm-4 col-xs-2">Genero </label>
										<div class="col-md-8 col-sm-8 col-xs-12">
											<label class="radio-inline"><input type="radio" name="sex" value="1" checked class="minimal">Hombre</label>
											<label class="radio-inline"><input type="radio" name="sex" value="2" class="minimal">Mujer</label>
										</div>
									</div>
								</div>
								
								<div class="box-header"><h4>Encaso de emergencia llamar a: </h4></div>
								<div class="form-group row">	
									<div class="col-md-7 col-sm-7" >		
										<label class="col-md-3 col-sm-4 col-xs-2">Nombre </label>
										<div class="col-md-8 col-sm-8 col-xs-12">
											<div class="input-group">
												<span class="input-group-addon"><i class="fa fa-user"></i></span>		
												<input class="form-control" name="nombre-emergencia1" placeholder="Nombre" type="campo">
											</div>
										</div>
									</div>
									<div class="col-md-5 col-sm-5">
										<label class="col-md-3 col-sm-4 col-xs-2">Teléfono: </label>
										<div class="col-md-8 col-sm-8 col-xs-12">
											<div class="input-group">
												<span class="input-group-addon"><i class="fa fa-phone"></i></span>	
												<input class="form-control" name="numero-emergencia1" placeholder="Número" type="campo">
											</div>
										</div>
									</div>
								
								</div>
							
								<div class="form-group row">
									<div class="col-md-7 col-sm-7" >
										<label class="col-md-3 col-sm-4 col-xs-2">Nombre </label>
										<div class="col-md-8 col-sm-8 col-xs-12">
											<div class="input-group">
												<span class="input-group-addon"><i class="fa fa-user"></i></span>	
												<input class="form-control" name="nombre-emergencia2" placeholder="Nombre" type="campo">
											</div>
										</div>
									</div>
									<div class="col-md-5 col-sm-5" >
										<label class="col-md-3 col-sm-4 col-xs-2">Teléfono: </label>
										<div class="col-md-8 col-sm-8 col-xs-12">
											<div class="input-group">
												<span class="input-group-addon"><i class="fa fa-phone"></i></span>		
												<input class="form-control" name="numero-emergencia2" placeholder="Número" type="campo">
											</div>
										</div>
									</div>
								</div>	
								<div class="form-group row">
									<div class="col-md-7 col-sm-7" >
										<label class="col-md-3 col-sm-4 col-xs-2">Comentarios: </label>
										<div class="col-md-8 col-sm-8 col-xs-12">
											<textarea class="form-control" name="anexo1" id="anexo1" placeholder="Anexo1" type="campo"></textarea>
										</div>
									</div>								
								</div>	

							</div>
						</div>
					</div>


					<?php
						global $obj_db;
                    	$consulta3 = "SELECT id_form, f_alias, f_label, f_default FROM form_extra WHERE id_seccion=1 ORDER BY f_alias ASC";
                        $result3 = $obj_db->consulta($consulta3);
                       	if($result3!=false){
                    ?>
                    <div class="box box-primary">
                    	<div class="box-header"><h4>Extras</h4></div>
						<div class="box-body">
							<div class="col-md-12 col-sm-12 col-xs-12">
								<input id="form_ext" name="form_ext" type="hidden" value="true">
						<?php
		                    	foreach($result3 as $fila3){
		            	?>
                         	
                         			<div class="form-group row">
										<div class="col-md-6 col-sm-6" >
											<label class="col-md-4 col-sm-4 col-xs-2"><?php echo $fila3['f_label']?></label>
											<div class="col-md-8 col-sm-8 col-xs-12">
												<input class="form-control" id="<?php echo "form_ext_".$fila3['id_form']?>" name="<?php echo "form_ext_".$fila3['id_form']?>" placeholder="<?php echo $fila3['f_default']?>" type="campo">
											</div>
										</div>	
									</div>	
                   		<?php
                   				}
                    	?>
                   			</div>
						</div>
                   		<?php		
	                   	   	}
                    	?>
                    </div>
					
					<div class="box-footer">

							<button type="submit" value="Guardar" name="btn_modificar_directivo" class="btn btn-primary">Guardar</button>
							<button type="button" value="Cancelar" name="btn_cancelar_modificar_directivo" class="btn btn-primary" onclick="can_mod_directivo()">Cancelar</button> 

					</div>

					<br />


			</form>
			</div>
		</section>
	
	</div>
		<?php require "views/directivos/footer_directivo.php"; ?>
</div>
<script>
		$(function(){

			//iCheck for checkbox and radio inputs
	        $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
	          checkboxClass: 'icheckbox_minimal-blue',
	          radioClass: 'iradio_minimal-blue'
	        });

		})
	</script>
</body>
<?php

}
else{
?>
<script>
alert("tu no tienes permiso para ver este contenido");
document.location.href="index.php?command=home";
</script>
<?php
}
?>