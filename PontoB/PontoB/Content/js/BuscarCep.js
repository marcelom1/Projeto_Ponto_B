
var botaoBuscarCep = document.querySelector("#buscarCEP");
var logradouro = document.querySelector("#Logradouro");
var bairro = document.querySelector("#LogradouroBairro");
var cidade = document.querySelector("#LogradouroCidade");
var estado = document.querySelector("#LogradouroEstado");

botaoBuscarCep.addEventListener("click", function () {
    var valorCEP = $("#inputCEP").val().replace(/[^\d]+/g, "");
    var xhr = new XMLHttpRequest();
    

    xhr.open("GET", "https://viacep.com.br/ws/" + valorCEP +"/json/");

    xhr.addEventListener("load", function () {

        var resposta = xhr.responseText;
        var endereco = JSON.parse(resposta);
        console.log(endereco.logradouro);

        $("#Logradouro").val(endereco.logradouro);
        $("#LogradouroBairro").val(endereco.bairro);
        $("#LogradouroCidade").val(endereco.localidade);
        $("#LogradouroEstado option:contains("+endereco.uf+")").attr('selected', true);
        //$("#LogradouroEstado").val($('option:contains('endereco.uf')').val());
        //$("#LogradouroEstado").text(endereco.uf);  
    
    });

    xhr.send();
});