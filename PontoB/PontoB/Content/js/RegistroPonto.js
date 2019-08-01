var registro = true;
$(document).ready(function () {
    $('#dataInicio').mask("99/99/9999");
    $('#dataFim').mask("99/99/9999");
    ExibirHoraAtual();
    $("#registrar").click(EnviarRegistro);
   
});
function ExibirHoraAtual() {
    registro = true;
    $.ajax({
        type: "POST",
        url: "/RegistroPonto/DataHoraAtual",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: atualizaRelogio,
       
        
    });
}

function UltimoRegistro() {
    $.ajax({
        type: "POST",
        url: "/RegistroPonto/UltimoRegistro",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function(resposta) {
            $("#ultimoRegistro").text("\u00daltimo Registro: " + resposta);

        }
    });
}

function EnviarRegistro() {
   
    $.ajax({
        type: "POST",
        url: "/RegistroPonto/Registro",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {
          
            if (resposta == "True") {
                ModalAlert("Hist\u00f3rico", "Sair", "Registrado com sucesso! Deseja visualizar o hist\u00f3rico ou sair?", "/RegistroPonto/Historico", "/Login/Logout", "Registro");
                UltimoRegistro();
            } else {
                ModalAlert("", "Ok", resposta, "", "", "Erro");
            }

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
};




function atualizaRelogio(hora) {

       
        var momentoAtual = new Date(moment(new Date(hora)).add(1, 'seconds').format('YYYY/MM/DD H:mm:ss'));
   
        var vhora = momentoAtual.getHours();
        var vminuto = momentoAtual.getMinutes();
        var vsegundo = momentoAtual.getSeconds();

        var vdia = momentoAtual.getDate();
        var vmes = momentoAtual.getMonth() + 1;
        var vano = momentoAtual.getFullYear();

        if (vdia < 10) { vdia = "0" + vdia; }
        if (vmes < 10) { vmes = "0" + vmes; }
        if (vhora < 10) { vhora = "0" + vhora; }
        if (vminuto < 10) { vminuto = "0" + vminuto; }
    if (vsegundo < 10) { vsegundo = "0" + vsegundo; }
   
        dataFormat = vdia + " / " + vmes + " / " + vano;
        horaFormat = vhora + " : " + vminuto + " : " + vsegundo;

        document.getElementById("data").innerHTML = dataFormat;
        document.getElementById("hora").innerHTML = horaFormat;

    if (registro==true)
        setTimeout(function () { atualizaRelogio(momentoAtual) }, 1000);
    
}