
$(document).on('submit', '#formulario', function (e) {
    e.preventDefault();

});

$(document).ready(function () {
    inicializacaoTooltip();
});


function inicializacaoTooltip() {
    $('.tooltip23').tooltipster({
        trigger: "custom"
    });
  
}



function EnviarSolicitacao() {
    $(".tooltip23").tooltipster("content", "Processando...").tooltipster("open");
    var form = $("#formulario");
    
    var form_data = new FormData(form[0]);



    $.ajax({
        type: "POST",
        url: "/Login/RecuperarSenhaEmail/",
        processData: false,
        contentType: false,
        data: form_data,
        dataType: "html",
        success: function (resposta) {
            $(".tooltip23").tooltipster("close");
            if (resposta == '"Por favor resolva o captcha!"') {
                $("#MsgServidor").text(resposta);
                console.log(resposta);
            } else {
                
                $('#ModalRecuperar').html(resposta);
                console.log(resposta);
            }
        }
    });
    /*var email = $("#email").val();
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
    });*/
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


function EnviarNovaSenha(token, NovaSenha, ConfirmaSenha, email) {
   
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
                $("#ModalRecuperar").html($("#MsgServidor"));

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

