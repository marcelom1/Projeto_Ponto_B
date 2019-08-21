
$(document).ready(function () {
    DadosGraficosHoje();

    

});


function DadosGraficosHoje() {
    $.ajax({
        type: "POST",
        url: "/Relatorio/GraficosHome",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (temp) {
            var resposta = JSON.parse(temp);
            console.log(resposta);
            atualizaGraficos(resposta);

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
}


function atualizaGraficos(objetoGrafico) {
    var ctx = document.getElementById('myChart');
    var myChart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            datasets: [{
                data: objetoGrafico.Hoje,
                backgroundColor: ["#93bf85", 'rgb(255, 99, 132)', 'rgb(55, 99, 132)']
            }],

            // These labels appear in the legend and in the tooltips when hovering different arcs
            labels: [
                "Registraram o ponto",
                'Não registraram o ponto',
                'Ausêntes por motivos'
            ]
        },
        options: {
            title: {
                display: true,
                text: "Hoje " + objetoGrafico.DataHoje,
                fontSize: 18,
                fontColor: '#000000'
            }
        }
    });
    
    var ctx = document.getElementById('myChart2');
    var myChart = new Chart(ctx, {
        type: 'doughnut',
        data: {

            datasets: [{
                data: objetoGrafico.Ontem,
                backgroundColor: ["#93bf85", 'rgb(255, 99, 132)', 'rgb(55, 99, 132)']
            }],

            // These labels appear in the legend and in the tooltips when hovering different arcs
            labels: [
                "Registraram o ponto",
                'Não registraram o ponto',
                'Ausêntes por motivos'
            ]
        },
        options: {
            title: {
                display: true,
                text: "Ontem " + objetoGrafico.DataOntem,
                fontSize: 18,
                fontColor: '#000000'
            }
        }


    });


};