
$(document).ready(function () {
   
    $('#NovoHoraEntrada').mask("99:99");
    $('#NovoHoraSaida').mask("99:99");


    if ($("#Escala_id").val() == 0) {
        $("#Botao_Excluir_Formulario_Escala").hide();
        
    }


    //Monitoramento de Click
    $("#Botao_Excluir_Formulario_Escala").click(ExcluirFormulario)
    $("#Botao_Salvar_Escala").click(SalvarFormulario);
    $("#NovaLinhaEscala").click(SalvarNovoHorarioEscala);
    $(".Botao_Excluir").click(function (e) {
        e.stopPropagation();
    });
    
    //Controla se deve ou não exibir a tela de cadastro de horas
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

    //Verifica se a descrição da escala está em branco ou não
    $("#DescricaoEscala").blur(function () {
        var diasSemana = $("#DescricaoEscala").val();
        if (diasSemana == "") {
            $("#ErroDescricao").text("Campo descrição não pode ficar em branco!").show();
        } else {
            $("#ErroDescricao").hide();
        }
    });

    if ($("#erroSpan").text() != "")
        ModalAlert("", "", $("#erroSpan").text() , "", "", "Erro");

});


//Excluir Formulário 
function ExcluirFormulario() {
    if (confirm("Confirma Exclusão do Registro " + $("#Escala_id").val()+"?")) {
        var formularioEscala = $("#CadastroEscala");
        var EscalaId = $("#Escala_id");
        EscalaId.attr("disabled", false);
        formularioEscala.attr("action", "/Escalas/Excluir");
        formularioEscala.submit();
    }
};

//Ao salvar novo horario na escala é efetuado algumas verificações
function SalvarNovoHorarioEscala() {
    var HoraEntrada = $("#NovoHoraEntrada").val();
    var HoraSaida = $("#NovoHoraSaida").val();
    var DiaSemana = $("#DiasDaSemana").val();
    console.log(HoraEntrada);
    if (HoraEntrada == "") {
        alert("Hora Entrada não pode ser nula!");
    }
    else if (HoraSaida == "") {
        alert("Hora Saida não pode ser nula!");
    }
    else if (HoraEntrada >= HoraSaida) {
        alert("Hora Saida Não Pode Ser Maior ou iqual que Hora Entrada!");
    } else {
        var formularioEscala = $("#CadastroEscalaHorario");
        formularioEscala.submit();
    }
};

//Salva nova escala
function SalvarFormulario() {
    var formularioEscala = $("#CadastroEscala");
    var idformulario = $("#Escala_id").val();
    var EscalaId = $("#Escala_id");
    EscalaId.attr("disabled", false);
    if (idformulario != 0) {
       // scrollEscala(); 
        formularioEscala.submit()
        
    } else {
        formularioEscala.submit()
    }
};

/*function scrollEscala() {
    $(".EscalaCadastro").slideDown(500);
    var posicaoEscala = $(".EscalaCadastro").offset().top;
    $("body").animate({
        scrollTop: posicaoEscala + "px"
    }, 1000);
};*/
