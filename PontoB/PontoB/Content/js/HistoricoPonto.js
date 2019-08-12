$("#VisualizarEscala").click(function () {
    var idEscala = $(this).data("escala");
    ModalAlert("", "", "", "", "", "Escala");
    $("#conteudoModal").load("/RegistroPonto/ModalEscalaColaborador/", { escalaId: idEscala }, function () {

    });

});