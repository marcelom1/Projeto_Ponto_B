$(document).ready(function () {

    $('.tooltip2').tooltipster({
        trigger: "custom"
    });


    $('#Select2Empresa').select2({

        language: "pt-BR",
        id: function (e) { return e.Id; },
        placeholder: "Selecione a empresa desejada...",
        allowClear: true,
        minimumInputLength: 2,

        ajax: {
            url: "/ManutencaoPonto/GetEmpresas",
            datatype: 'json',
            type: 'POST',

            params: {
                contentType: 'application/json; charset=utf-8'


            },
            quietMillis: 100,
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },

            processResults: function (data, params) {
                return {
                    results: data
                }
            }
        },


    });
});



var AntesEmpresaSelect2 = '';
$("#Select2Empresa").on('select2:close', function () {


    var EmpresaId = $("#Select2Empresa").val();
    if (EmpresaId != AntesEmpresaSelect2) {
        $("#Select2Colaborador").val("").text("");
        $("#Colaborador_id").val("");

    }
    if (EmpresaId) {

        $("#Empresa_id").val(EmpresaId);
        AntesEmpresaSelect2 = $("#Empresa_id").val();
    } else {
        $("#Empresa_id").val("");
        $("#Grid").addClass("Oculto");
    }

});

$("#dataInicio").blur(function () {
    validacaoData();
});
$("#dataFim").blur(function () {
    validacaoData();
});

function validacaoData() {
    var datafim = $("#dataFim").val();
    var datainicio = $("#dataInicio").val()
    if (datafim)
        if (datainicio)
            if (datafim < datainicio && datafim != '' && datainicio != '') {
                $("#Erro_DataInicio").text("Data inicial não pode ser maior que data final!").show();
                $("#GridManutencao").addClass("Oculto");
                return false;
            } else {
                $("#Erro_DataInicio").hide();
                return true;
            }
};

$("#Buscar").click(function () {


    var datafim = $("#dataFim").val();
    var datainicio = $("#dataInicio").val()
    var EmpresaId = $("#Select2Empresa").val();
    if (datainicio != '' && datafim != '') {
        if (validacaoData()) {
            $(".tooltip2").tooltipster("content", "Processando...").tooltipster("open");

            AtualizarGrid();
            $("#Grid").removeClass("Oculto");

        }
    } else {
        ModalAlert("", "", "Todos os campos são obrigatórios!", "", "", "Erro");

    }

});


function AtualizarGrid() {
    var empresaId = $("#Select2Empresa").val();
    var controller = $("#Buscar").data("controller");
    var relatorio = $("#Buscar").data("relatorio");

    $.ajax({
        type: "POST",
        url: "/" + "Relatorio" + "/" + controller +"/",
        data: JSON.stringify({
            relatorio: relatorio,
            empresaId: empresaId,
            dataInicio: $("#dataInicio").val(),
            dataFim: $("#dataFim").val(),

        }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {

            $("#Grid").html(resposta)
            $("#ErrosResumoDeCalculo").text("");
            if ($("#erroSpan").text() != '') {
                var erro = $("#erroSpan").text()
                $("#Grid").html('');
                $("#ErrosResumoDeCalculo").text(erro);
            }
            $(".tooltip2").tooltipster("close");
            //inicializacaoTooltip();

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
}




