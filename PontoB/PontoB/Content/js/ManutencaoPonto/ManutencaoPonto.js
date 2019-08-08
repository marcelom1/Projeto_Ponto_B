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
    
    if (colaboradorId != null && EmpresaId != null && dataInicial!='' && dataFinal!='') {
        $("#spninner").removeClass("Oculto");
        $("#ParcialViewTabelaCalculo").load("/ManutencaoPonto/TabelaCalculo/", { idColaborador: colaboradorId, dataInicio: dataInicial, dataFim: dataFinal }, function () {
            $("#spninner").addClass("Oculto");
        });
    } else {
        $("#ParcialViewTabelaCalculo").html("");
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
    
    buscaTabela();
});



$("#Select2Colaborador").on('select2:close', function () {
    AtualizaSelectColaborador();
    BuscaIndiceColaborador();
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
    var datainicio = $("#dataInicio").val()
    if (datafim)
        if (datainicio)
            if (datafim < datainicio) {
                $("#Erro_DataInicio").text("Data inicial não pode ser maior que data final!").show();
                $("#GridManutencao").addClass("Oculto");
            } else {
                $("#Erro_DataInicio").hide();
                buscaTabela();
            }
}








