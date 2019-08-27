$(document).ready(function () {

    $("#EnvioEscala").click(function () {
        $("#CadastroAusencia").submit();
    });

    if ($("#Ausencia_id").val() == 0) {
        $("#Botao_Excluir_Formulario_Ausencia").hide();

    }

    $("#Botao_Excluir_Formulario_Ausencia").click(function () {
        if (confirm("Confirma Exclusão do Registro " + $("#Ausencia_id").val() + "?")) {
            var formularioAusencia = $("#CadastroAusencia");
            var AusenciaId = $("#Ausencia_id");
            AusenciaId.attr("disabled", false);
            formularioAusencia.attr("action", "/Ausencia/Excluir");
            formularioAusencia.submit();
        }
    });

    $("#EnvioEscala").click(function () {
        var formularioAusencia = $("#CadastroAusencia");
        var AusenciaId = $("#Ausencia_id");
        AusenciaId.attr("disabled", false);
        formularioAusencia.submit()

    });


    //Controla se deve ou não exibir a tela de cadastro de ausência
    var idAusencia = $("#Ausencia_id").val();
    var Idtabela = $("#cadastroAusencia");
    if (idAusencia == 0) {
        Idtabela.toggleClass('OcultaCadastrado');

    } else {
        $("#cadastroAusencia").load("/Ausencia/DetalhesAusencia/", { id: idAusencia }, DetalhesAusencia);
    } 

    
  

});


function EnviaFormulario() {
    $("#CadastroAusenciaColaborador").submit();
};





