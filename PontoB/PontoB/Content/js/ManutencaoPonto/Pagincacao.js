
function BuscaIndiceColaborador() {
    var EmpresaId = $("#Select2Empresa").val();
    var ColaboradorId = $("#Select2Colaborador").val();

    $.ajax({
        type: "POST",
        url: "/ManutencaoPonto/BuscaIndiceColaborador",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        data: JSON.stringify({
            empresaId: EmpresaId,
            dataInicio: $("#DataCarregadaInicio").text(),
            dataFim: $("#DataCarregadaFim").text(),
            colaboradorId: ColaboradorId
        }),
        success: function (temp) {

            var resposta = JSON.parse(temp);
            console.log(resposta.indice);
            AtualizaAnteriorProximo(resposta.indice, resposta.qtdLista);
        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
};


$(document).on('click', '#BuscarColaboradores', function () {
    var datafim = $("#dataFim").val();
    var datainicio = $("#dataInicio").val()
    var EmpresaId = $("#Select2Empresa").val();
    if (EmpresaId != 0 && datainicio != '' && datafim != '') {
        if (validacaoData()) {
            ColaboradoresPagincao(0);
            
        }
    } else {
        ModalAlert("", "", "Todos os campos são obrigatórios!", "", "","Erro");
    }

});



function ColaboradoresPagincao(indice) {
    var empresaId = $("#Select2Empresa").val();

    $.ajax({
        type: "POST",
        url: "/ManutencaoPonto/ColaboradoresPagincao/",
        data: JSON.stringify({
            empresaId: empresaId,
            dataInicio: $("#dataInicio").val(),
            dataFim: $("#dataFim").val(),
            indice: indice
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {
            var obj = JSON.parse(resposta);
            $("#calculo").removeClass("Oculto");
            $("#CartaoPonto").removeClass("Oculto");
            $('#Select2Colaborador').val(null).trigger('change');
            var newOption = new Option(obj.Nome, obj.id, false, true);
            $('#Select2Colaborador').append(newOption).trigger('change');

            AtualizaSelectColaborador();
            AtualizaAnteriorProximo(indice, obj.qtd);

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
}



function AtualizaAnteriorProximo(indiceAtual, qtdLista) {
    var btAnterior = $("#btAnterior");
    var btProximo = $("#btProximo");


    btAnterior.data("indice", (indiceAtual > 0 ? (indiceAtual - 1) : 0));
    btProximo.data("indice", (indiceAtual < (qtdLista - 1) ? (indiceAtual + 1) : (qtdLista - 1)));

    console.log("Bt Anterior: " + btAnterior.data("indice"));
    console.log("Bt Proximo: " + btProximo.data("indice"));
}

$(document).on('click', '#btProximo', function () {
    var btProximo = $("#btProximo").data("indice");
    $("#ParcialViewTabelaCalculo").html("");
    ColaboradoresPagincao(btProximo);
});

$(document).on('click', '#btAnterior', function () {
    var btAnterior = $("#btAnterior").data("indice");
    $("#ParcialViewTabelaCalculo").html("");
    ColaboradoresPagincao(btAnterior);

});