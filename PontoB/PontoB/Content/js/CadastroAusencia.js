$(document).ready(function () {
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
});
$("#EnvioEscala").click(function () {
    $("#CadastroAusencia").submit();
});

$("#Botao_Excluir_Formulario_Ausencia").click(function () {
    if (confirm("Confirma Exclusão do Registro " + $("#Ausencia_id").val() + "?")) {
        var formularioAusencia = $("#CadastroAusencia");
        var AusenciaId = $("#Ausencia_id");
        AusenciaId.attr("disabled", false);
        formularioAusencia.attr("action", "/Ausencia/Excluir");
        formularioAusencia.submit();
    }
});

$("#NovaLinhaEscala").click(function () {
    var colaboradorId = $("#Select2_Colaborador").val();
    var empresaId = $("#Select2_Empresa").val();
    var DataInicio = $("#Data_inicio").val();
    var HoraInicio = $("#hora_inicio").val();
    var DataFim = $("#Data_fim").val();
    var HoraFim = $("#hora_fim").val();
    var MotivoId = $("#Select2_MotivoAusencia").val();
    var erro = 0;
    if (DataInicio > DataFim)
        erro++;
    if (DataInicio <= DataFim)
        if (HoraInicio > HoraFim)
            erro++;
    if (MotivoId == null)
        erro++;
    if ($("#TodosColaboradores").is(":checked") == false)
        if (colaboradorId == null)
            erro++;
    if ($("#TodosColaboradores").is(":checked") == true)
        if ($("#TodasEmpresas").is(":checked") == false)
            if (empresaId == null)
                erro++;
    if (erro == 0)
        $("#CadastroAusenciaColaborador").submit();
       
});

 //Controla se deve ou não exibir a tela de cadastro de ausência
var idAusencia = $("#Ausencia_id").val();
var Idtabela = $("#cadastroAusencia");
if (idAusencia == 0) {
    Idtabela.toggleClass('OcultaCadastrado');
} 

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