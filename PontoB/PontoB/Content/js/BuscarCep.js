

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
        
            $("#Logradouro").val(endereco.logradouro);
            $("#LogradouroBairro").val(endereco.bairro);
            $("#LogradouroCidade").val(endereco.localidade);
            $("#LogradouroEstado option:contains(" + endereco.uf + ")").attr('selected', true);
      

    });
    
    xhr.send();
    
    setTimeout(function () {
        $(".tooltip").tooltipster("close");
    }, 1200);
});