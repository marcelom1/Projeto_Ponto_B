﻿function DetalhesAusencia() {
    $('#hora_inicio').mask("99:99");
    $('#hora_fim').mask("99:99");


    $("#NovaLinhaAusencia").click(function () {
        var colaboradorId = $("#Select2_Colaborador").val();
        var empresaId = $("#Select2_Empresa").val();
        var DataInicio = $("#Data_inicio").val();
        var HoraInicio = $("#hora_inicio").val();
        var DataFim = $("#Data_fim").val();
        var HoraFim = $("#hora_fim").val();
        var MotivoId = $("#Select2_MotivoAusencia").val();
        var erro = 0;

        if (DataInicio=='') {
            erro++;
            $("#Erro_Ausencia_DataInicio").text("Data inicial não pode ficar em branco").show();
        } else
            $("#Erro_Ausencia_DataInicio").hide();


        if (DataFim=='') {
            erro++;
            $("#Erro_Ausencia_DataFim").text("Data final não pode ficar em branco").show();
        } else
            $("#Erro_Ausencia_DataFim").hide();

        if (HoraInicio=='') {
            erro++;
            $("#Erro_Ausencia_HoraInicio").text("Hora inicial não pode ficar em branco").show();
        } else
            $("#Erro_Ausencia_HoraInicio").hide();

        if (HoraFim=='') {
            erro++;
            $("#Erro_Ausencia_HoraFim").text("Hora final não pode ficar em branco").show();
        } else
            $("#Erro_Ausencia_HoraFim").hide();

        if (DataInicio > DataFim) {
            erro++;
            $("#Erro_Ausencia_Datas").text("Data inicial não pode ser maior que data final").show();
        } else
            $("#Erro_Ausencia_Datas").hide();

        if (DataInicio == DataFim)
            if (HoraInicio > HoraFim) {
                erro++;
                $("#Erro_Ausencia_HoraInicio").text("Hora inicial não pode ser maior que hora final").show();
            } else
                $("#Erro_Ausencia_HoraInicio").hide();

        if (MotivoId == 0) {
            erro++;
            $("#Erro_Ausencia_Motivo").text("Motivo é um campo obrigatório").show();
        } else
            $("#Erro_Ausencia_Motivo").hide();

        if ($("#TodosColaboradores").is(":checked") == false)
            if (colaboradorId == 0) {
                erro++;
                $("#Erro_Ausencia_Colaborador").text("Colaborador é um campo obrigatório").show();
            } else
                $("#Erro_Ausencia_Colaborador").hide();

        if ($("#TodosColaboradores").is(":checked") == true)
            if ($("#TodasEmpresas").is(":checked") == false)
                if (empresaId == 0) {
                    erro++;
                    $("#Erro_Ausencia_EmpresaId").text("Empresa é um campo obrigatório").show();
                }
                else
                    $("#Erro_Ausencia_EmpresaId").hide();
            else
                $("#Erro_Ausencia_EmpresaId").hide();

        if (erro == 0)
            EnviaFormulario();//Cada página deve implementar o enviar Formulário com a rota de envio desejada.
            

    });


    $('#Select2_MotivoAusencia').select2({

        language: "pt-BR",
        id: function (e) { return e.Id; },
        placeholder: "Selecione o motivo da ausência...",
        allowClear: true,
        minimumInputLength: 2,

        ajax: {
            url: "/Ausencia/GetMotivoAusencia",
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

    $('#Select2_Colaborador').select2({

        language: "pt-BR",
        id: function (e) { return e.Id; },
        placeholder: "Selecione colaborador ausênte...",
        allowClear: true,
        minimumInputLength: 2,

        ajax: {
            url: "/Ausencia/GetColaboradores",
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

    $('#Select2_Empresa').select2({

        language: "pt-BR",
        id: function (e) { return e.Id; },
        placeholder: "Selecione a empresa desejada...",
        allowClear: true,
        minimumInputLength: 2,

        ajax: {
            url: "/Colaborador/getEmpresas",
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


    //Habilita ou desabilita o select2 Colaborador
    $("#TodosColaboradores").click(function () {
        if ($("#TodosColaboradores").is(":checked") == true)
            $("#Select2_Colaborador").attr("disabled", true);
        else
            $("#Select2_Colaborador").attr("disabled", false);
        $("#buscaEmpresa").toggleClass('Oculto');
    });




    //Habilita ou desabilita o select2 Empresa
    $("#TodasEmpresas").click(function () {
        if ($("#TodasEmpresas").is(":checked") == true)
            $("#Select2_Empresa").attr("disabled", true);
        else
            $("#Select2_Empresa").attr("disabled", false);
    });
    //Limitador de caracters na  texarea    
    $('#observacaoAusencia').keypress(function () {
        var length = $(this).val().length - 1;
        var maxlength = $(this).attr('maxlength') - 1;
        if (length >= maxlength) {
            $(this).val($(this).val().substring(length - maxlength, length));
        }
    })

    //Ao fazer o click no botão excluir na tabela Index o click não se propagar para a tela de consulta
    $(".Botao_Excluir").click(function (e) {
        e.stopPropagation();
    });
}

$(document).on('click', '#botaoExcluirLinha', function () {
    var id = $(this).data("id");
    var idColaborador = $("#Select2Colaborador").val();
    var data = moment($("#Data_inicio").val()).format('DD/MM/YYYY');
    if (confirm('Confirma Exclusão do Registro ' + id + "?")) {
        $("#GridAusencia").load("/Ausencia/ExcluirAusenciaColaborador", { AusenciaColaboradorId: id, ViewOrigem: "TabelaAusenciaPorDia", data: data, idColaborador: idColaborador });
    }

});