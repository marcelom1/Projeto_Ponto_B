
$(document).ready(function () {
    
    $("#Botao_Salvar_Escala").click(SalvarFormulario);
    $("#NovaLinhaEscala").click(SalvarNovoHorarioEscala);
    $("#Botao_Nova_Escala").click(Nova_Escala);
    $(".Botao_Excluir").click(function (e) {
        e.stopPropagation();
    });
    $("#Botao_Excluir_Formulario_Escala").click(ExcluirFormulario)

    var idEscala = $("#Escala_id").val();
    var Idtabela = $("#EscalaCaddastro");

    if (idEscala == 0) {
        Idtabela.toggleClass('EscalaCadastro');
    } else {
        $(".EscalaCadastro").slideDown(500);
    }

    //Verifica se o Campo Dia da Semana está em Branco ou não
    $("#DiasDaSemana").blur(function () {
        var diasSemana = $("#DiasDaSemana").val();
        if (diasSemana == "") {
            $("#ErroDiaSemana").text("Campo Obrigatório!").show();
        } else {
            $("#ErroDiaSemana").hide();
        }
    });

    $("#DescricaoEscala").blur(function () {
        var diasSemana = $("#DescricaoEscala").val();
        if (diasSemana == "") {
            $("#ErroDescricao").text("Campo descrição não pode ficar em branco!").show();
        } else {
            $("#ErroDescricao").hide();
        }
    });



});

function Nova_Escala() {

}


function ExcluirFormulario() {
    var formularioEscala = $("#CadastroEscala");
    var EscalaId = $("#Escala_id");
    EscalaId.attr("disabled", false);
    formularioEscala.attr("action", "/Escalas/Excluir");
    formularioEscala.submit();
 
    };

function SalvarNovoHorarioEscala() {
    var HoraEntrada = $("#NovoHoraEntrada").val();
    var HoraSaida = $("#NovoHoraSaida").val();
    var DiaSemana = $("#DiasDaSemana").val();
    console.log(HoraEntrada);
    if (DiaSemana == "") {
        alert("Dia da semana não pode ficar em branco!");
    }
    else if (HoraEntrada == null) {
        alert("Hora Entrada não pode ser nula!");
    }
    else if (HoraSaida == null) {
        alert("Hora Saida não pode ser nula!");
    }
    else if (HoraEntrada >= HoraSaida) {
        alert("Hora Saida Não Pode Ser Maior ou iqual que Hora Entrada!");
    } else {
        var formularioEscala = $("#CadastroEscalaHorario");
        formularioEscala.submit();
    }
};


function SalvarFormulario() {
    var formularioEscala = $("#CadastroEscala");
    var idformulario = $("#Escala_id").val();
    var EscalaId = $("#Escala_id");
    EscalaId.attr("disabled", false);
    if (idformulario != 0) {
        formularioEscala.submit()
        scrollEscala();
    } else {
        formularioEscala.submit()
    }
    };

function scrollEscala() {
    $(".EscalaCadastro").slideDown(500);
    var posicaoEscala = $(".EscalaCadastro").offset().top;
    $("body").animate({
        scrollTop: posicaoEscala + "px"
    }, 1000);
    };
