$(document).ready(function () {
    $('#Select2Colaborador').select2({

        language: "pt-BR",
        id: function (e) { return e.Id; },
        placeholder: "Selecione colaborador...",
        allowClear: true,
        minimumInputLength: 2,

        ajax: {
            url: "/ManutencaoPonto/GetColaboradores",
            datatype: 'json',
            type: 'POST',

            params: {
                contentType: 'application/json; charset=utf-8'
            },
            quietMillis: 100,
            data: function (params) {
                return {
                    searchTerm: params.term,
                    idEmpresa: $("#Select2Empresa").val(),
                    dataInicio: $("#dataInicio").val(),
                    dataFim: $("#dataFim").val()
                };
            },

            processResults: function (data, params) {
                
                return {

                    results: data
                }
            }
        },


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

function buscaTabela() {
    var EmpresaId = $("#Select2Empresa").val();
    var colaboradorId = $("#Select2Colaborador").val();
    var dataInicial = $("#dataInicio").val();
    var dataFinal = $("#dataFim").val();
    
    if (colaboradorId != null && EmpresaId != '' && dataInicial != '' && dataFinal != '') {
        $("#ErrosManutencaoPonto").text("");
        $("#spninner").removeClass("Oculto");
        $("#ParcialViewTabelaCalculo").load("/ManutencaoPonto/TabelaCalculo/", { idColaborador: colaboradorId, dataInicio: dataInicial, dataFim: dataFinal }, function () {
            var erro = $("#erroSpan").text();
            if (erro != '') {
                $("#GridManutencao").addClass("Oculto");
                $("#ErrosManutencaoPonto").text(erro);
            } else {
                $("#GridManutencao").removeClass("Oculto");
                $("#DataCarregadaInicio").text(dataInicial);
                $("#DataCarregadaFim").text(dataFinal);
                $("#spninner").addClass("Oculto");
            }
        });
    } else {
        $("#ParcialViewTabelaCalculo").html("");
        $("#GridManutencao").addClass("Oculto");
    }
};

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
    
    validacaoData();
});



$("#Select2Colaborador").on('select2:close', function () {
    AtualizaSelectColaborador();
    if ($("#Select2Colaborador").val() != null) {
        $("#calculo").removeClass("Oculto");
        $("#CartaoPonto").removeClass("Oculto");
        BuscaIndiceColaborador();
    } else {
        $("#calculo").addClass("Oculto");
        $("#CartaoPonto").addClass("Oculto");
    }
});

function AtualizaSelectColaborador() {
    var colaboradorId = $("#Select2Colaborador").val();
    if (colaboradorId)
        $("#Colaborador_id").val(colaboradorId);
    else
        $("#Colaborador_id").val("");
   
    buscaTabela();
};


$("#dataInicio").blur(function () {
        validacaoData();
});
$("#dataFim").blur(function () {
    validacaoData();
});

function validacaoData() {
    var datafim = $("#dataFim").val();
    var datainicio = $("#dataInicio").val();
    var dataAtual = new Date();
    console.log(dataAtual);
    if (datafim) {
        if (datainicio) {
            if (datafim < datainicio) {
                $("#Erro_DataInicio").text("Data inicial não pode ser maior que data final!").show();
                $("#GridManutencao").addClass("Oculto");
                return false;
            } else if (datafim > dataAtual) {
                $("#Erro_DataFim").text("Data final não pode ser maior que a data corrente").show();
                $("#GridManutencao").addClass("Oculto");
                return false;
            } else {
                $("#Erro_DataInicio").hide();
                buscaTabela();
                return true;
            }
        }
    }
    $("#GridManutencao").addClass("Oculto");
};

$(document).ready(function () {
    $('.tooltip').tooltipster({
        trigger: "custom"
    });
});

$("#calculo").click(function () {
    $(".tooltip").tooltipster("open").tooltipster("content", "Calculando...");
    var colaboradorId = $("#Select2Colaborador").val();
    var dataInicial = $("#DataCarregadaInicio").text();
    var dataFinal = $("#DataCarregadaFim").text();

    $.ajax({
        type: "POST",
        url: "/ManutencaoPonto/CalculoPonto/",
        data: JSON.stringify({ idColaborador: colaboradorId, dataInicial: dataInicial, dataFinal: dataFinal }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {
            $("#erro-calculo").text(resposta);
            console.log(resposta);
            buscaTabela();
            $(".tooltip").tooltipster("close");

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });

});

$("#CartaoPonto").click(function () {
    var colaboradorId = $("#Select2Colaborador").val();
    var dataInicial = $("#DataCarregadaInicio").text();
    var dataFinal = $("#DataCarregadaFim").text();

    $.ajax({
        type: "POST",
        url: "/Relatorio/CartaoPonto/",
        data: JSON.stringify({ colaboradorId: colaboradorId, dataInicio: dataInicial, dataFim: dataFinal }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {
            var myWindow = window.open("", "_blank");

            myWindow.document.write(resposta);
            

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });

});








