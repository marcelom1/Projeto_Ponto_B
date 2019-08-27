$(document).ready(function () {
    inicializacaoTooltip();
   
});

function inicializacaoTooltip() {
    $('.tooltip3').tooltipster({
        trigger: "custom"
    });
    $('.tooltip4').tooltipster({
        trigger: "custom"
    });
}



$(document).on('click', '#ImprimirDetalhado', function () {
    $(".tooltip4").tooltipster("content", "Processando...").tooltipster("open");
    var datafim = $("#dataFim").val();
    var datainicio = $("#dataInicio").val()
    var empresaId = $("#Select2Empresa").val();
    if (datafim > datainicio && datainicio != '' && datafim != '') {
        $.ajax({
            type: "POST",
            url: "/Gamificacao/RelatorioDetalhadoGamificacao/",
            data: JSON.stringify({ empresaId: empresaId, dataInicio: datainicio, dataFim: datafim }),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (resposta) {
                var myWindow = window.open("", "_blank");

                myWindow.document.write(resposta);

                $(".tooltip4").tooltipster("close");

            },
            error: function (json) {
                alert("Erro de conexão com o servidor!");
                $(".tooltip4").tooltipster("close");
                Console.log(json);
            }
        });
    }

});



$(document).on('click', '#ImprimirRelatorio', function () {
    $(".tooltip3").tooltipster("content", "Processando...").tooltipster("open");
    var datafim = $("#dataFim").val();
    var datainicio = $("#dataInicio").val()
    var empresaId = $("#Select2Empresa").val();
    if (datafim > datainicio && datainicio != '' && datafim != '') {
        $.ajax({
            type: "POST",
            url: "/Gamificacao/RelatorioResumoGamificacao/",
            data: JSON.stringify({ empresaId: empresaId, dataInicio: datainicio, dataFim: datafim }),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (resposta) {
                var myWindow = window.open("", "_blank");

                myWindow.document.write(resposta);

                $(".tooltip3").tooltipster("close");

            },
            error: function (json) {
                alert("Erro de conexão com o servidor!");
                Console.log(json);
                $(".tooltip3").tooltipster("close");
            }
        });
    }

});

