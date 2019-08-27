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

function inicializacaoTooltip() {
    $('.tooltip3').tooltipster({
        trigger: "custom"
    });
    $('.tooltip4').tooltipster({
        trigger: "custom"
    });
}

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
        $("#GridManutencao").addClass("Oculto");
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
            if (datafim < datainicio && datafim != '' && datainicio !='') {
                $("#Erro_DataInicio").text("Data inicial não pode ser maior que data final!").show();
                $("#GridManutencao").addClass("Oculto");
                return false;
            } else {
                $("#Erro_DataInicio").hide();
                return true;
            }
};

$("#BuscarColaboradores").click(function () {
   

        var datafim = $("#dataFim").val();
        var datainicio = $("#dataInicio").val()
        var EmpresaId = $("#Select2Empresa").val();
        if (EmpresaId != 0 && datainicio != '' && datafim != '') {
            if (validacaoData()) {
                $(".tooltip2").tooltipster("content", "Processando...").tooltipster("open");

                ColaboradoresPagincao();
                $("#GridManutencao").removeClass("Oculto");

            } 
        } else {
            ModalAlert("", "", "Todos os campos são obrigatórios!", "", "", "Erro");
           
        }

});


function ColaboradoresPagincao() {
    var empresaId = $("#Select2Empresa").val();

    $.ajax({
        type: "POST",
        url: "/ResumoCalculo/TabelaResumoCalculo/",
        data: JSON.stringify({
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
            inicializacaoTooltip();
            
        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
}

$(document).on('click', '#printerCartaoPonto', function () {
    $("#tooltip4").tooltipster("content", "Processando...").tooltipster("open");
    var datafim = $("#dataFim").val();
    var datainicio = $("#dataInicio").val()
    var colaboradorId = $(this).data("colaboradorid");
    if (datafim > datainicio && colaboradorId != 0 && datainicio != '' && datafim != '') {
        $.ajax({
            type: "POST",
            url: "/Relatorio/CartaoPonto/",
            data: JSON.stringify({ colaboradorId: colaboradorId, dataInicio: datainicio, dataFim: datafim }),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (resposta) {
                var myWindow = window.open("", "_blank");

                myWindow.document.write(resposta);
                $("#tooltip4").tooltipster("close");

            },
            error: function (json) {
                alert("Erro de conexão com o servidor!");
                Console.log(json);
            }
        });
    } 

});

$(document).on("click", "#contentPager a", function () {
    $.ajax({
        url: $(this).attr("href"),
        type: 'GET',
        cache: false,
        success: function (result) {
            $('#Grid').html(result);
        }
    });
    return false;
});




$(document).on('click', '#ImprimirTodos', function () {
    $(".tooltip3").tooltipster("content", "Processando...").tooltipster("open");
    var datafim = $("#dataFim").val();
    var datainicio = $("#dataInicio").val()
    var empresaId = $("#Select2Empresa").val();
    if (datafim > datainicio && empresaId != 0 && datainicio != '' && datafim != '') {
        $.ajax({
            type: "POST",
            url: "/Relatorio/TodosCartaoPontoEmpresa/",
            data: JSON.stringify({ empresaId: empresaId, dataInicio: datainicio, dataFim: datafim }),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (resposta) {
                var myWindow = window.open("", "_blank");

                myWindow.document.write(resposta, function () {
                    
                });
                
                $(".tooltip3").tooltipster("close");

            },
            error: function (json) {
                alert("Erro de conexão com o servidor!");
                Console.log(json);
            }
        });
    }

});


$(document).on('click', '#CalcularTodos', function () {
    $(".tooltip4").tooltipster("content", "Calculando...").tooltipster("open");
    $("#ErrosResumoDeCalculo").text("");
    var EmpresaId = $("#Select2Empresa").val();
    var dataInicial = $("#dataInicio").val();
    var dataFinal = $("#dataFim").val();

    $.ajax({
        type: "POST",
        url: "/ResumoCalculo/CalculoTodosPontos/",
        data: JSON.stringify({ idEmpresa: EmpresaId, dataInicial: dataInicial, dataFinal: dataFinal }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {

            $("#ErrosResumoDeCalculo").text(resposta);
           
            $(".tooltip4").tooltipster("close");

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
            $(".tooltip").tooltipster("close");
        }
    });

});

