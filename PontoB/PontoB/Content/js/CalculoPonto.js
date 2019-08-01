$(document).ready(function () {
    $('#Select2Colaborador').select2({

        language: "pt-BR",
        id: function (e) { return e.Id; },
        placeholder: "Selecione colaborador...",
        allowClear: true,
        minimumInputLength: 2,

        ajax: {
            url: "/CalculoPonto/GetColaboradores",
            datatype: 'json',
            type: 'POST',

            params: {
                contentType: 'application/json; charset=utf-8'
            },
            quietMillis: 100,
            data: function (params) {
                return {
                    searchTerm: params.term,
                    idEmpresa: $("#Select2Empresa").val(),
                    dataInicio: $("#dataInicio").val(),
                    dataFim: $("#dataFim").val()
                };
            },

            processResults: function (data, params) {
                
                return {

                    results: data
                }
            }
        },


    });

    $('#Select2Empresa').select2({

        language: "pt-BR",
        id: function (e) { return e.Id; },
        placeholder: "Selecione a empresa desejada...",
        allowClear: true,
        minimumInputLength: 2,

        ajax: {
            url: "/CalculoPonto/GetEmpresas",
            datatype: 'json',
            type: 'POST',

            params: {
                contentType: 'application/json; charset=utf-8'
               

            },
            quietMillis: 100,
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },

            processResults: function (data, params) {
                return {
                    results: data
                }
            }
        },


    });
});


$("#Select2Empresa").on('select2:close', function () {
    $("#Empresa_id").val($("#Select2Empresa").val());
});

$("#Select2Colaborador").on('select2:close', function () {
    var colaboradorId = $("#Select2Colaborador").val();
    if (colaboradorId == '')
        colaboradorId = 0;
    var dataInicial = $("#dataInicio").val();
    var dataFinal = $("#dataFim").val();
    $("#Colaborador_id").val(colaboradorId);
    $("#ParcialViewTabelaCalculo").load("/CalculoPonto/TabelaCalculo/", { idColaborador: colaboradorId, dataInicio: dataInicial, dataFim: dataFinal});
});

$(document).on('click', '.EditarRegistroPonto', function () {
    var id = $(this).data("value");
    $("#ModalTabelaCalculo").load("/CalculoPonto/ModalTabelaCalculo/" + id, function () {
        console.log("2");
        $("#ModalEditar").modal("show");
    });
});




$(document).on('click', '#AddAusencia', function () {
    var data = $(this).closest('tr').find('td[data-dia]').data('dia');
    AddAusencia($(this).data("value"),data);
});

function AddAusencia(id, data) {
    var idColaborador = $("#Select2Colaborador").val();
    var nomeColaborador = $("#Select2Colaborador").text();
 
    $.ajax({
        type: "POST",
        url: "/Ausencia/DetalhesAusencia/"+id,
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (resposta) {

            ModalAlert("", "Ok", resposta, "", "", data);
            $("#labelTodosColaboradores").addClass("Oculto");
            $(".divData").addClass("Oculto");
           
            $("#Data_inicio").val(new Date(moment(new Date(data)).format('YYYY/MM/DD H:mm:ss'))).addClass("Oculto");
            $("#Data_fim").val(new Date(moment(new Date(data)).format('YYYY/MM/DD H:mm:ss'))).addClass("Oculto");
            $("#TodosColaboradores").attr("disabled", true).addClass("Oculto");
            $("#Select2_Colaborador").attr("disabled", true);
            $("#TodasEmpresas").attr("disabled", true);
            $("#Select2_Empresa").attr("disabled", true);
            $("#SelectColaborador").val(idColaborador).text(nomeColaborador);
            
            DetalhesAusencia();

        },
        error: function (json) {
            alert("Erro de conexão com o servidor!");
            Console.log(json);
        }
    });
};
