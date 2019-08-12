

$(document).ready(function () {
    $('.tooltip').tooltipster({
        trigger: "custom"
    });
});

var botaoBuscarCep = document.querySelector("#buscarCEP");
var logradouro = document.querySelector("#Logradouro");
var bairro = document.querySelector("#LogradouroBairro");
var cidade = document.querySelector("#LogradouroCidade");
var estado = document.querySelector("#LogradouroEstado");

botaoBuscarCep.addEventListener("click", function () {
    
    $(".tooltip").tooltipster("open").tooltipster("content", "Buscando...");
    var valorCEP = $("#inputCEP").val().replace(/[^\d]+/g, "");
    var xhr = new XMLHttpRequest();
    

    xhr.open("GET", "https://viacep.com.br/ws/" + valorCEP +"/json/");

    xhr.addEventListener("load", function () {
        var resposta = xhr.responseText;
        var endereco = JSON.parse(resposta);

        if (xhr.status == 200 && xhr.readyState == 4) {
            if (endereco.erro) {
                $(".tooltip").tooltipster("open").tooltipster("content", "Erro - CEP não encontrado");
                 setTimeout(function () {
                     $(".tooltip").tooltipster("close");
                 }, 1200);
            }
            else {
                setTimeout(function () {
                    $(".tooltip").tooltipster("close");
                }, 350);
                $("#Logradouro").val(endereco.logradouro);
                $("#LogradouroBairro").val(endereco.bairro);
                $("#LogradouroCidade").val(endereco.localidade);
                $("#LogradouroEstado option:contains(" + endereco.uf + ")").attr('selected', true);
            }
        }

    });
    
    xhr.send();
    
   
});