
$(document).ready(function () {
    ExibirHoraAtual();
    $("#registrar").click(EnviarRegistro);
});
function ExibirHoraAtual() {
  
    $.ajax({
        type: "POST",
        url: "/RegistroPonto/DataHoraAtual",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: atualizaRelogio,
       
        
    });
}

function EnviarRegistro() {
    console.log("test");
    $.ajax({
        type: "POST",
        url: "/RegistroPonto/Registro",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {
            if (alert("Registrado com sucesso!"))

        },
        error: function (json) {
            console.log(json);
        }
    });
};

function ExibirHoraAtual() {

    $.ajax({
        type: "POST",
        url: "/RegistroPonto/DataHoraAtual",
        data: "a",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: atualizaRelogio,

    });
}

function atualizaRelogio(hora) {


        var momentoAtual = new Date(moment(new Date(hora)).add(1, 'seconds').format('YYYY MM DD h:mm:ss'));
    
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
    
    setTimeout(function () { atualizaRelogio(momentoAtual) }, 1000);
   
   
}