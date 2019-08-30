
$("#AlterarSenha").click(function () {
    $.ajax({
        type: "POST",
        url: "/Login/ModalAlterarSenha/",
        data: JSON.stringify({
           
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {
            ModalAlert("", "", resposta, "", "", "Alterar Senha");
        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
    
});



