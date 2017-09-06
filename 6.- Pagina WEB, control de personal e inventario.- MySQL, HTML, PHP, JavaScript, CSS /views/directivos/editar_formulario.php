<?php
require_once 'class/class.camposExtras.php';

if(isset($_SESSION['user']) && $_SESSION['permiso']=='directivo'){
?>

  <body class="skin-blue sidebar-mini">

<style>
.controls.form_extra{
	text-align: right;
}
.btn-xs{
	cursor:pointer;
	display: inline-block;
	background-color: #F11111;
	border:1px solid #FFF;
	padding:3px;
	width: 25px;
	text-align: center;
	font-weight: bold;
	color:#FFF;
	font-size: 14px;
}

.btn-xs:hover{
    background-color: #C00;
}

.t_h_label{
	display: inline-block;
	background-color: #F11111;
	margin:0 1px;
	padding:3px auto;
	text-align: center;
	font-weight: bold;
	color:#FFF;
	font-size: 11px;	
}
.row{
	margin-left:5px;
	margin-right:5px;
}
#cont_preguntas_extras .row:nth-child(2n+1){background:#FFF;}
#cont_preguntas_extras .row:nth-child(2n+1){background:#eee;}

.i_alias, .i_label, .i_tipo, .i_default{width:100%;}

</style>

<script>

$(function(){

      function ajaxPreguntas(options){
          options=(options==typeof(undefined))?'':options;
          params=options;
          params.id_form=<?php echo str_replace('p_f_', '', $_GET['formulario']); ?>;
        var datos = $.ajax({
              url:'class/class.formulariosPersonalizados.php',
              type:'post',
              async: false,
              data:params,
              success:function(data){
                    if(data==false){
                        getPreguntas('4');
                        return data;
                    }
              }          
         }).responseText;
        return datos
      }




    function construir_pregunta(){



         var s= '<div class="form-group row pre_campo">'+
                    '<div class="t_d_label col-md-2 col-sm-2 col-xs-2" id=""><input type="text" name="p_alias" id="p_alias" class="form-control" placeholder="Alias"></div>'+
                    '<div class="t_d_value col-md-3 col-sm-3 col-xs-3"><input type="text" name="p_label" id="p_label" class="form-control" placeholder="Label"></div>'+
                    '<div class="t_d_value col-md-2 col-sm-2 col-xs-2">'+
                       '<select type="text" name="p_tipo" id="p_tipo" class="form-control" placeholder="Tipo">'+
                            '<option value="0">Selecciona una opción</option>'+
                            '<option value="1">Texto</option>'+
                            '<option value="2">Multiselección</option>'+
                       '</select>'+
                    '</div>'+
                    '<div class="t_d_value col-md-2 col-sm-2 col-xs-2"><input type="text" name="p_default" id="p_default" class="form-control" placeholder="Valor por defecto"></div>'+
                    '<div class="t_d_value col-md-1 col-sm-1 col-xs-1" style="text-align:center"><button class="btn btn-primary  btn-xs-nuevo">O</button></div>'+
                    '<div class="t_d_value col-md-1 col-sm-1 col-xs-1" style="text-align:center"><button class="btn btn-primary  btn-xs-cancel">x</button></div>'+
                '</div>';                
        return (s);
    }   

    //Nuevo

    function handler(cadena,seccion){
        $('.btn-xs-nuevo').click(function(){
            if(confirm("Estas a punto de cargar una nueva pregunta,\nrealmente deseas agregar una pregunta?")){
                var opciones={};
                opciones.f=cadena;
                opciones.alias=$('#p_alias').val();
                opciones.label=$('#p_label').val();
                opciones.tipo=$('#p_tipo').val();
                opciones.default=$('#p_default').val();
                opciones.seccion=seccion
                ajaxPreguntas(opciones);
                
            }else{
                
            };
        });

        $('.btn-xs-change').unbind();
        $('.btn-xs-saveChange').unbind();
        $('.btn-xs-cancel').unbind();
        $('.btn-xs-delete').unbind();

        $('.btn-xs-change').click(function(){
            cambiar($(this));
        })

        $('.btn-xs-saveChange').click(function(){
            salvarcambios($(this));
        })

        $('.btn-xs-cancel').click(function(){
            cancelar($(this));
        });

        $('.btn-xs-delete').click(function(){
            borrar($(this));
        });

        $('.i_tipo').on('change',function(){
            cambiarInputDefault($(this));
        });
    }

    //Recupera preguntas

    function getPreguntas(seccion){
        var opciones={};
        opciones.f="getPreguntas";
        opciones.seccion=seccion;
        preguntas=ajaxPreguntas(opciones);
        if (seccion==4&&preguntas!=1){
            $('#cont_preguntas_extras').html('');
            $('#cont_preguntas_extras').html(preguntas);
            handler('altaPregunta','4');
        }
    }

    //Definimos variables auxiliares para almacenar los campos
    var aux_alias, aux_label, aux_tipo, aux_default

    function cambiar(element){
            
        //Elimina las clases para los elementos que no se modificaron
            $('btn-xs-saveChange').removeClass('btn-xs-saveChange').addClass('btn-xs-change');
            $('.btn-xs-delete').removeClass('btn-xs-delete').addClass('btn-xs-cancel');
            //Cambia la clase de los elementos a modificar
            $(element).removeClass('btn-xs-change').addClass('btn-xs-saveChange');
            $(element).parent().parent().children().children('.i_alias, .i_label, .i_tipo, .i_default').replaceWith(function() {
                //return ('<input value="' + $(this).html + '">');
                if($(this).hasClass('i_alias')){aux_alias=$(this).html()}
                if($(this).hasClass('i_label')){aux_label=$(this).html()}
                
                if($(this).hasClass('i_tipo')){
                    aux_tipo=$(this).html()

                    var a1, a2, a3;

                    a1 = $(this).html()=="Selecciona un Opción"?"selected":"";
                    a2 = $(this).html()=="Texto"?"selected":"";
                    a3 = $(this).html()=="Multiselección"?"selected":"";


                    return '<select type="text" class="'+$(this).attr('class')+'" name="'+$(this).attr('name')+'" id="'+$(this).attr('id')+'" placeholder="'+$(this).attr('placeholder')+'" >'+
                        '<option value="0" '+a1+'>Selecciona una Opción</option>'+
                        '<option value="1" '+a2+'>Texto</option>'+
                        '<option value="2" '+a3+'>Multiselección</option>'+
                    '</select>'
                }

                if($(this).hasClass('i_default')){
                    aux_default=$(this).html()
                    elementDefault=$(this);
                    if($(this).parent().prev().children().val()=='2'){
                        return '<textarea type="text" class="'+$(elementDefault).attr('class')+'" name="'+$(elementDefault).attr('name')+'" id="'+$(elementDefault).attr('id')+'" placeholder="'+$(elementDefault).attr('placeholder')+'">'+$(elementDefault).html()+'</textarea>';
                    }
                }


                return '<input type="text" class="'+$(this).attr('class')+'" name="'+$(this).attr('name')+'" id="'+$(this).attr('id')+'" placeholder="'+$(this).attr('placeholder')+'" value="'+$(this).html()+'">'
            });            
        handler('','');
    }

    function salvarcambios(element){
        var nomAlias=$(element).parent().parent().children().children('.i_alias').val()
        var nomLabel=$(element).parent().parent().children().children('.i_label').val()
        var nomTipo=$(element).parent().parent().children().children('.i_tipo').val()
        var nomDefault=$(element).parent().parent().children().children('.i_default').val()
        var attrID=$(element).parent().next().children().attr('id');

         if(confirm("Estas a punto de cambiar la pregunta '"+nomAlias+"',\nrealmente deseas cambiarlo?")){
                var opciones={};
                opciones.f='cambiarPregunta';
                opciones.indice_p=attrID;
                opciones.alias=nomAlias
                opciones.label=nomLabel;
                opciones.tipo=nomTipo;
                opciones.default=nomDefault;
                ajaxPreguntas(opciones);                
          }  
    }

    function cancelar(element){
        if(confirm("Si cancelas ahora no se guardarán los cambios,\nrealmente deseas cancelar?")){
               //Remueve los preguntas no guardados
                $('.pre_campo').remove();
                //Devuelve las clases a la normalidad
                //$(element).parent().prev().children('.btn-xs-saveChange').removeClass('btn-xs-saveChange').addClass('btn-xs-change');
                $('input.i_alias, input.i_label, select.i_tipo, input.i_default').replaceWith(function() {
                    //return ('<input value="' + $(this).html + '">');
                var valor_control_inicial='';

                if($(this).hasClass('i_alias')){valor_control_inicial=aux_alias}
                if($(this).hasClass('i_label')){valor_control_inicial=aux_label}
                if($(this).hasClass('i_default')){valor_control_inicial=aux_default}
                if($(this).hasClass('i_tipo')){valor_control_inicial=aux_tipo}
                
                    return '<div class="'+$(this).attr('class')+'" name="'+$(this).attr('name')+'" id="'+$(this).attr('id')+'" placeholder="'+$(this).attr('placeholder')+'" >'+valor_control_inicial+'</div>'
                });
                $('.btn-xs-saveChange').removeClass('btn-xs-saveChange').addClass('btn-xs-change');
                $(element).removeClass('btn-xs-cancel').addClass('btn-xs-delete');
        };
        handler('','');
    }

    function borrar(element){
        var attrID=$(element).attr('id');
        if(confirm("Realmente deseas eliminar el elemento "+$(element).attr('id')+"?")){
            var opciones={};
            opciones.f='borrarPregunta';
            opciones.indice_p=attrID;
            ajaxPreguntas(opciones);
               // borrarCampo($(element).attr('id'));
        };
        handler('','');
    }

    function cambiarInputDefault(element){

        elementDefault=$(element).parent().next().children();
        if($(element).val()=='1'){
            elementDefault.replaceWith(function() {
                //alert("Texto");
                return '<input type="text" class="'+$(elementDefault).attr('class')+'" name="'+$(elementDefault).attr('name')+'" id="'+$(elementDefault).attr('id')+'" placeholder="'+$(elementDefault).attr('placeholder')+'" value="'+$(elementDefault).val()+'">'
            })
        }
        if($(element).val()=='2'){
            elementDefault.replaceWith(function() {
                //alert("Multiselección");
                return '<textarea type="text" class="'+$(elementDefault).attr('class')+'" name="'+$(elementDefault).attr('name')+'" id="'+$(elementDefault).attr('id')+'" placeholder="'+$(elementDefault).attr('placeholder')+'">'+$(elementDefault).val()+'</textarea>'
            })
        }
    }


    
    //Crea un campo para usuarios
  $('#btn_form_f_add').click(function(){
    var pregunta = construir_pregunta();
    $('.pre_campo').remove();
    $('#cont_preguntas_extras').append(pregunta);
    handler('altaPregunta','4');
  });






    function inic(){
        getPreguntas(4);
    }

    inic();
    
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
                    Informacion extra para preguntas
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                    <li><a href="#">Formularios personalizados</a></li>
                </ol>
            </section>

            <div id="" class="">

                <div class="box box-primary">
                    <label class="" style="text-align:center;">Agregar preguntas:</label>

                    <div class="box-header">
                            <h4>Preguntas del formulario <?php echo $_GET['formulario'] ?> </h4>   
                            <span class="pull-right">
                                <button class="btn btn-primary" title="Agregar preguntas" data-toggle="tooltip" data-placement="bottom"  id="btn_form_f_add">+</button>
                            </span>                         
                    </div>
                    <div class="box-body">
                    	<div class="box-header with-border bg-light-blue">
                            <div class="box-title col-md-2 col-sm-2 col-xs-2"><h5>Alias</h5></div>
                            <div class="box-title col-md-3 col-sm-3 col-xs-3"><h5>Label</h5></div>
                            <div class="box-title col-md-2 col-sm-2 col-xs-2"><h5>Tipo</h5></div>
                            <div class="box-title col-md-2 col-sm-2 col-xs-2"><h5>Valor por Defecto</h5></div>
                            <div class="box-title col-md-1 col-sm-1 col-xs-1"><h5>Editar/<br>Guardar</h5></div>
                            <div class="box-title col-md-1 col-sm-1 col-xs-1"><h5>Eliminar/<br>Cancelar</h5></div>
                    	</div>
                        <div id="cont_preguntas_extras">
                        </div>
                    </div>	
                    

                    <br />
                

            </div>
        </section>
        
    </div>
        <?php require "views/directivos/footer_directivo.php" ?>
</div>
</body>
<?php
}
else if($_SESSION['permiso']!='directivo'){
?>
<script>
alert("tu no tienes permiso para ver este contenido");
document.location.href="index.php?command=home";
</script>
<?php
}
?>