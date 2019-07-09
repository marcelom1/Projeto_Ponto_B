
$(document).ready(function () {
    $("#NovaLinhaEscala").click(AdicionarLinhaEscala);
    $("#Botao_Salvar_Escala").click(SalvarFormulario);
    $("#NovaLinhaEscala").click(SalvarNovoHorarioEscala);
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
});

function ExcluirFormulario() {
    var formularioEscala = $("#CadastroEscala");
    var EscalaId = $("#Escala_id");
    EscalaId.attr("disabled", false);
    formularioEscala.attr("action", "Excluir");
    console.log("TESTE");
    formularioEscala.submit();
 
}

function SalvarNovoHorarioEscala() {
    var formularioEscala = $("#CadastroEscalaHorario");
    formularioEscala.submit();
  
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
}

function scrollEscala() {
    $(".EscalaCadastro").slideDown(500);
    var posicaoEscala = $(".EscalaCadastro").offset().top;
    $("body").animate({
        scrollTop: posicaoEscala + "px"
    }, 1000);
}

function AdicionarLinhaEscala() {
    var diaDaSemana = $("#DiasDaSemana").val();
    var horaEntrada = $("#NovoHoraEntrada").val();
    var horaSaida = $("#NovoHoraSaida").val();
    console.log("Dia da Semana: "+diaDaSemana);
    console.log("Hora Entrada: "+horaEntrada);
    console.log("Hora Saida" + horaSaida);

   /* var linha = $("<tr>");
    var colunaAcao = $("<td>").text(usuario)
    var colunaPalavras = $("<td>").text(numPalavras)
    var colunaRemove = $("<td>");

    var link = $("<a>").addClass("botao_remover").attr("href", "#");
    var icone = $("<i>").addClass("small").addClass("material-icons").text("delete");


    link.append(icone);

    colunaRemove.append(link);

    linha.append(colunaUsuario);
    linha.append(colunaPalavras);
    linha.append(colunaRemove);
    */
};