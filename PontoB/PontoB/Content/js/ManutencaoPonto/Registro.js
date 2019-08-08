
$(document).on('click', '.EditarRegistroPonto', function () {
    var idColaborador = $(this).data("colaborador");
    var idEscala = $(this).data("escala");
    var data = $(this).data("dia");
    $("#ModalTabelaCalculo").load("/ManutencaoPonto/ModalTabelaCalculo/", { data: data, idEscala: idEscala, idColaborador: idColaborador }, function () {

        $("#ModalEditar").modal("show");
        AtualizarTabelaManutencaoRegistro(data, idColaborador);
    });
});

$(document).on('click', '.AdicionarRegistroPonto', function () {
    var idColaborador = $(this).data("colaborador");
    var idEscala = $(this).data("escala");
    var data = $(this).data("dia");
    $("#ModalTabelaCalculo").load("/ManutencaoPonto/AdicionarRegistroManualmente/", { data: data, idEscala: idEscala, idColaborador: idColaborador }, function () {

        $("#ModalEditar").modal("show");
    });
});


$(document).on('click', '#EnvioFormularioManutencao', function () {


    var formularioManutencaoMarcacao = $("#TabelaManutencaoMarcacao");
    var form_data = new FormData(formularioManutencaoMarcacao[0]);
    var x = $('form').serializeArray();


    var erro = 0;
    $.each(x, function (i, field) {
        if (field.name == "observacao") {
            if ((field.value.match(/^(\s)+$/)) || (field.value.length == 0))
                erro++;
        }

    });



    if (erro == 0) {
        $.ajax({
            type: "POST",
            url: "/ManutencaoPonto/DesconsiderarMarcacao",
            processData: false,
            contentType: false,
            data: form_data,
            dataType: "html",
            success: function (resposta) {
                $('#ModalEditar').modal('hide');
            },
            error: function (json) {
                alert("Erro de conexão com o servidor!");
                Console.log(json);
            }
        });
    } else {
        $("#erro").text("Campo Observação não pode ficar em branco")
    }

});

function AtualizarTabelaManutencaoRegistro(data, idColaborador) {
    $("#TabelaManutencao").load("/ManutencaoPonto/TabelaManutencao/", { data: data, idColaborador: idColaborador });

}


$(document).on('click', '#ExcluirRegistroPontoManualmente', function () {
    var id = $(this).data("id");
    var colaboradorId = $(this).data("idcolaborador");
    var data = moment($(this).data("data"), "DD/MM/YYYY").format('DD/MM/YYYY');


    $.ajax({
        type: "POST",
        url: "/ManutencaoPonto/RemoverRegistroManual/",
        data: JSON.stringify({ idRegistro: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {
            AtualizarTabelaManutencaoRegistro(data, colaboradorId);

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
});

$(document).on('click', '#desconsidera', function () {
    var id = $(this).data("id");
    if ($(this).is(":checked")) {
        $(".check-" + id).prop('disabled', false);
    } else {
        $(".check-" + id).prop('disabled', true);
    }

});


$(document).on('click', '#RegistroAdicionar', function () {

    var data = moment($("#DiaManutencao").val(), "DD/MM/YYYY").format('DD/MM/YYYY');
    var hora = $("#HoraRegistroManutencao").val();
    var colaboradorId = $("#ColaboradorIdManutencao").val();
    var observacao = $("#ObservacaoManutencao").val();

    $.ajax({
        type: "POST",
        url: "/ManutencaoPonto/AdicionarRegistroManutencao/",
        data: JSON.stringify({ data: data, hora: hora, colaboradorId: colaboradorId, motivo: observacao }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {
            $('#ModalEditar').modal('hide');

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
});


$('#ModalEditar').on('hidden.bs.modal', function (e) {
    buscaTabela();
})