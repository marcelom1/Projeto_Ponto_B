
//Ao fazer o click no botão excluir na tabela Index o click não se propagar para a tela de consulta
$(".Botao_Excluir").click(function (e) {
    e.stopPropagation();
});

//Para enviar o ID do formulario preciso remover o Disabled.
$("#Botao_Salvar").click(function () {
    var empresaId = $("#Empresa_id");
    empresaId.attr("disabled", false);
});
$("#Botao_Excluir_Formulario_Empresa").click(ExcluirFormulario)


function ExcluirFormulario() {

    if (confirm("Confirma Exclusão do Registro " + $("#Empresa_id").val() + "?")) {
        var formularioEscala = $("#CadastroEmpresa");
        var EscalaId = $("#Empresa_id");
        EscalaId.attr("disabled", false);
        formularioEscala.attr("action", "/Empresa/Excluir");
        formularioEscala.submit();
    }
};

if ($("#Empresa_id").val() == 0) {
    $("#Botao_Excluir_Formulario_Empresa").hide("slow");

}


//Limpar os campos do fomulário
$("#Botao_Nova_Empresa").click(function () {
    console.log("Teste");
    $(':input', '#CadastroEmpresa').not(':button, :submit, :reset, :hidden')
        .val('')
        .removeAttr('checked')
        .removeAttr('selected');
})


//Máscara CNPJ e CEP
$(document).ready(function () {
    $('#CNPJ').mask('00.000.000/0000-00', { reverse: false });
    $('#inputCEP').mask('00000-000');
});

//Máscara Telefone
var SPMaskBehavior = function (val) {
    return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
},
    spOptions = {
        onKeyPress: function (val, e, field, options) {
            field.mask(SPMaskBehavior.apply({}, arguments), options);
        }
    };
$('#Telefone').mask(SPMaskBehavior, spOptions);


//Ativa e Desativa o Envio do Formulário caso o CNPJ não seja Valido.
$("#CNPJ").blur(function () {
    var valorcnpj = $("#CNPJ").val().replace(/[^\d]+/g, "");
    var valido = validarCNPJ(valorcnpj);
    if (!valido) {
        $("#Erro_Cnpj").text("Cnpj Inválido").show();
        $("#Botao_Salvar").attr("type", "button");
        var empresaId = $("#Empresa_id");
        empresaId.attr("disabled", true);
    } else {
        $("#Erro_Cnpj").hide();
        $("#Botao_Salvar").attr("type", "submit");
    }
});


//Validador CNPJ
function validarCNPJ(cnpj) {

    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == '') return false;

    if (cnpj.length != 14)
        return false;

    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;

}
