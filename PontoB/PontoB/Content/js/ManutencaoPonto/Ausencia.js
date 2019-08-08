
$(document).on('click', '#AddAusencia', function () {
    var data = $(this).closest('tr').find('td[data-dia]').data('dia');
    AddAusencia($(this).data("value"), data);
});


function AddAusencia(id, data) {
    var idColaborador = $("#Select2Colaborador").find(':selected').val();
    var nomeColaborador = $("#Select2Colaborador").find(':selected').text();

    $.ajax({
        type: "POST",
        url: "/Ausencia/DetalhesAusencia/",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {

            ModalAlert("", "", resposta, "", "", data);
            $("#labelTodosColaboradores").addClass("Oculto");
            $(".divData").addClass("Oculto");

            $("#Data_inicio").val(moment(data, "DD/MM/YYYY").format('YYYY-MM-DD')).addClass("Oculto");
            $("#Data_fim").val(moment(data, "DD/MM/YYYY").format('YYYY-MM-DD')).addClass("Oculto");
            $("#TodosColaboradores").attr("disabled", true).addClass("Oculto");
            $("#Select2_Colaborador").attr("disabled", true);
            $("#TodasEmpresas").attr("disabled", true);
            $("#Select2_Empresa").attr("disabled", true);
            $("#SelectColaborador").val(idColaborador).text(nomeColaborador);

            DetalhesAusencia();
            AtualizaAusencia(data)

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
};


function EnviaFormulario() {

    var data = moment($("#Data_inicio").val()).format('DD/MM/YYYY');


    $.ajax({
        type: "POST",
        url: "/ManutencaoPonto/AdicionarGrupoAusencia/",
        data: JSON.stringify({ data: data }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {
            $("#ID_Ausencia").val(resposta);
            EnvioFormularioAusencia(data);

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
};




function AtualizaAusencia(data) {

    $("#spninner").removeClass("Oculto");
    var idColaborador = $("#Select2Colaborador").val();
    $("#GridAusencia").load("/Ausencia/TabelaAusenciaPorDia", { data: data, colaboradorId: idColaborador }, function () {
        $("#spninner").addClass("Oculto");

    });
    $("#Select2_Colaborador").attr("disabled", true);
    $("#Select2_MotivoAusencia").val("").text("");
    $("#hora_inicio").val("00:00");
    $("#hora_fim").val("00:00");
    $("#Observacao").val("");


};



function EnvioFormularioAusencia(data) {
    $("#Select2_Colaborador").attr("disabled", false);
    var formularioDetalheAusencia = $("#CadastroAusenciaColaborador");
    var form_data = new FormData(formularioDetalheAusencia[0]);

    $.ajax({
        type: "POST",
        url: "/Ausencia/AdicionaAusenciaColaboradorPelaManutencao",
        processData: false,
        contentType: false,
        data: form_data,
        dataType: "html",
        success: function (resposta) {
            AtualizaAusencia(data);
        }
    });
};