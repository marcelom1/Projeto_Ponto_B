﻿$(document).ready(function () {
    $('#Select2Colaborador').select2({

        language: "pt-BR",
        id: function (e) { return e.Id; },
        placeholder: "Selecione colaborador...",
        allowClear: true,
        minimumInputLength: 2,

        ajax: {
            url: "/CalculoPonto/GetColaboradores",
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
            url: "/CalculoPonto/GetEmpresas",
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

    
    
    if (colaboradorId != null && EmpresaId != null && dataInicial != '' && dataFinal!='') {
        $("#spninner").removeClass("Oculto");
        $("#ParcialViewTabelaCalculo").load("/CalculoPonto/TabelaCalculo/", { idColaborador: colaboradorId, dataInicio: dataInicial, dataFim: dataFinal }, function () {
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
    }
    
    buscaTabela();
});

$("#Select2Colaborador").on('select2:close', function () {
    var colaboradorId = $("#Select2Colaborador").val();
    if (colaboradorId)
        $("#Colaborador_id").val(colaboradorId);
    else
        $("#Colaborador_id").val("");
    buscaTabela();
});

$(document).on('click', '.EditarRegistroPonto', function () {
    var idColaborador = $(this).data("colaborador");
    var idEscala = $(this).data("escala");
    var data = $(this).data("dia");
    $("#ModalTabelaCalculo").load("/CalculoPonto/ModalTabelaCalculo/", { data, idEscala, idColaborador }, function () {
        $("#ModalEditar").modal("show");
    });
});

$("#dataInicio").blur(function () {
    var datafim = $("#dataFim").val();
    var datainicio = $("#dataInicio").val()
    if (datafim)
        if (datafim < datainicio) {
            $("#Erro_DataInicio").text("Data final não pode ser menor que data inicial!").show();
        } else {
            $("#Erro_DataInicio").hide();
            buscaTabela();
        }
});

$("#dataFim").blur(function () {
    var datafim = $("#dataFim").val();
    var datainicio = $("#dataInicio").val()
    if (datainicio)
        if (datafim < datainicio) {
            $("#Erro_DataFim").text("Data final não pode ser menor que data inicial!").show();
        }
        else
        {
            $("#Erro_DataFim").hide();
            buscaTabela();
        }
    });

$(document).on('click', '#AddAusencia', function () {
    var data = $(this).closest('tr').find('td[data-dia]').data('dia');
    AddAusencia($(this).data("value"),data);
});

function AddAusencia(id, data) {
    var idColaborador = $("#Select2Colaborador").val();
    var nomeColaborador = $("#Select2Colaborador").text();
 
    $.ajax({
        type: "POST",
        url: "/Ausencia/DetalhesAusencia/",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {

            ModalAlert("", "", resposta, "", "", data);
            $("#labelTodosColaboradores").addClass("Oculto");
            $(".divData").addClass("Oculto");
           
            $("#Data_inicio").val(moment(data,"DD/MM/YYYY").format('YYYY-MM-DD')).addClass("Oculto");
            $("#Data_fim").val(moment(data, "DD/MM/YYYY").format('YYYY-MM-DD')).addClass("Oculto");
            $("#TodosColaboradores").attr("disabled", true).addClass("Oculto");
            $("#Select2_Colaborador").attr("disabled", true);
            $("#TodasEmpresas").attr("disabled", true);
            $("#Select2_Empresa").attr("disabled", true);
            $("#SelectColaborador").val(idColaborador).text(nomeColaborador);
            
            DetalhesAusencia();
            AtualizaAusencia(data)

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
};

function EnviaFormulario() {
    
    var data = moment($("#Data_inicio").val()).format('DD/MM/YYYY');
    
    
    $.ajax({
        type: "POST",
        url: "/CalculoPonto/AdicionarGrupoAusencia/",
        data: JSON.stringify({ data: data}),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {
            $("#ID_Ausencia").val(resposta);
            EnvioFormularioAusencia(data);

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
};

function AtualizaAusencia(data) {

    $("#spninner").removeClass("Oculto");
    var idColaborador = $("#Select2Colaborador").val();
    $("#GridAusencia").load("/Ausencia/TabelaAusenciaPorDia", { data: data, colaboradorId: idColaborador }, function () {
        $("#spninner").addClass("Oculto");

    });
    $("#Select2_Colaborador").attr("disabled", true);
    $("#Select2_MotivoAusencia").val("").text("");
    $("#hora_inicio").val("00:00");
    $("#hora_fim").val("00:00");
    $("#Observacao").val("");

    
};

function EnvioFormularioAusencia(data) {
    $("#Select2_Colaborador").attr("disabled", false);
    var formularioDetalheAusencia = $("#CadastroAusenciaColaborador");
    var form_data = new FormData(formularioDetalheAusencia[0]);

    $.ajax({
        type: "POST",
        url: "/Ausencia/AdicionaAusenciaColaboradorPelaManutencao",
        processData: false,
        contentType: false,
        data: form_data,
        dataType: "html",
        success: function (resposta) {
            AtualizaAusencia(data);
        }
    });
};