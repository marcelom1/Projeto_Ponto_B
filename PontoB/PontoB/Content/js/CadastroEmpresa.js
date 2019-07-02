
//Ao fazer o click no botão excluir na tabela Index o click não se propagar para a tela de consulta
$(".Botao_Excluir").click(function (e) {
    e.stopPropagation();
});

//Para enviar o ID do formulario preciso remover o Disabled.
$("#Botao_Salvar").click(function () {
    var empresaId = $("#Empresa_id");
    empresaId.attr("disabled", false);
});


$("#Botao_Excluir_Formulario").click(function () {
    var empresaId = $("#Empresa_id");
    empresaId.attr("disabled", false);
});

$("#Botao_Nova_Empresa").click(function () {
    console.log("Teste");
    $(':input', '#CadastroEmpresa').not(':button, :submit, :reset, :hidden')
        .val('')
        .removeAttr('checked')
        .removeAttr('selected');
})
