
function EnviarSolicitacao() {
    var email = $("#email").val();
    $.ajax({
        type: "POST",
        url: "/Login/RecuperarSenhaEmail/",
        data: JSON.stringify({ email: email }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (temp) {
            $('#conteudoModal').html(temp);
           
           console.log(resposta)

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");

            Console.log(json);
        }
    });
};




$(document).on('click', '#EnivarAlteracao', function () {
    var token = $("#Redefinicao").val();
    var email = $("#Email").val();
    var NovaSenha = $("#NovaSenha").val();
    var ConfirmaSenha = $("#ConfirmaSenha").val();
    $("#MsgServidor").text("");
    ValidaSenhaEmBranco(token, NovaSenha, ConfirmaSenha, email);

});

function ValidaSenhaEmBranco(token, NovaSenha, ConfirmaSenha,email) {
    if ((NovaSenha.split(/\s+/).length > 1) || (NovaSenha == "")) {
        $(".Erro_ConfirmaSenha").text("Campo senha não pode conter espaços ou ficar em branco!").show();
        return true;
    } else {
        $(".Erro_ConfirmaSenha").hide();
        ValidaSenhaConfirmação(token, NovaSenha, ConfirmaSenha, email)
        return false;
    }
};

function ValidaSenhaConfirmação(token, NovaSenha, ConfirmaSenha,email) {

    if (NovaSenha != ConfirmaSenha) {
        $(".Erro_ConfirmaSenha").text("As senhas não coincidem!").show()
        return true;
    } else {
        $("#Erro_ConfirmaSenha").hide();
        EnviarNovaSenha(token, NovaSenha, ConfirmaSenha,email)
        return false;
    }
}


function EnviarNovaSenha(token, NovaSenha, ConfirmaSenha,email) {
    $.ajax({
        type: "POST",
        url: "/Login/AlterarSenhaViaEmail/",
        data: JSON.stringify({ token: token, novaSenha: NovaSenha, confirmacaoSenha: ConfirmaSenha, email }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (temp) {
            var resposta = JSON.parse(temp);
            if (resposta == "Senha alterada com sucesso!") {
                $("#MsgServidor").removeClass("erro").addClass("sucess");
                $("#MsgServidor").text(resposta);
                $("#conteudoModal").html($("#MsgServidor"));

            }
            $("#MsgServidor").text(resposta);
            console.log(resposta)

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");

            Console.log(json);
        }
    });
};

