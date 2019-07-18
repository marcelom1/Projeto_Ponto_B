//Para enviar o ID do formulario preciso remover o Disabled.
$("#Botao_Salvar").click(function () {
   //verifica se existe espaços ou a senha está em branco ao adicionar novo colaborador
    var senha = $("#ColaboradorSenha").val();
    var confirmaSenha = $("#ConfirmaSenha").val();
    var valorCPF = $("#CPF").val().replace(/[^\d]+/g, "");
    var valorPIS = $("#PIS").val().replace(/[^\d]+/g, "");
    var dataAdmissao = $("#Data_Admissao").val();
    var dataDemissao = $("#Data_Demissao").val();
    var dataNascimento = $("#Data_Nascimento").val();

    var erro = 0
    if ($("#colaborador_Id").val() == 0) {
        if (ValidaSenhaEmBranco(senha)) {
            erro = 1;
        };
        if (ValidaSenhaConfirmação(senha, confirmaSenha)) {
            erro = 1;
        }
    }
    if (!ValidaPIS(valorPIS)) {
        $("#Erro_PIS").text("PIS Inválido").show();
        erro = 1;
    }
    if (!TestaCPF(valorCPF)) {
        $("#Erro_CPF").text("CPF Inválido").show();
        erro = 1;
    }
    if (ValidaDataAdmissaoMaiorDataDemissao(dataAdmissao, dataDemissao)) {
        erro = 1;
    }
    if (ValidaDataNascimentoMaiorDataAdmissao(dataAdmissao, dataNascimento)) {
        erro = 1;
    }
    if (erro == 0) {
        
        SalvarFormulario();
    }
    
});

function ValidaDataAdmissaoMaiorDataDemissao(admissao, demissao) {
    if (demissao != "") {
        if (admissao > demissao) {
            $("#Erro_DataDemissao").text("Data de demissão menor que data de admissão").show();
            console.log("Erro demissao");
            return true;
        }
    }
    return false
};

function ValidaDataNascimentoMaiorDataAdmissao(admissao, nascimento) {
    if (nascimento != null) {
        if (admissao < nascimento) {
            $("#Erro_DataNascimento").text("Data de nascimento maior que data de admissão ").show();
            console.log("Erro nascimento");
            return true;
        }
    }
    return false;
};

function ValidaSenhaEmBranco(senha) {
    if ((senha.split(/\s+/).length > 1) || (senha == "")) {
        $("#Erro_Senha").text("Campo senha não pode conter espaços ou ficar em branco!").show();
        return true;
    } else {
        $("#Erro_Senha").hide();
        return false;
    }
};

function ValidaSenhaConfirmação(senha, confirmaSenha) {
    
    if (senha != confirmaSenha) {
        $("#Erro_ConfirmaSenha").text("As senhas não coincidem!").show()
        return true;
    } else {
        $("#Erro_ConfirmaSenha").hide();
        return false;        
    }
}


$("#ColaboradorSenha").blur(function () {
    if ($("#colaborador_Id").val()==0)
        ValidaSenhaEmBranco($("#ColaboradorSenha").val());
});

$("#ConfirmaSenha").blur(function () {
    if ($("#colaborador_Id").val()==0)
        ValidaSenhaConfirmação($("#ColaboradorSenha").val(), $("#ConfirmaSenha").val());
});

function SalvarFormulario() {
    var formularioColaborador = $("#CadastroColaborador");
    var colaboradorId = $("#colaborador_Id");
    colaboradorId.attr("disabled", false);
    formularioColaborador.submit()
        
};


function ExcluirFormulario() {
   
    if (confirm("Confirma Exclusão do Registro " + $("#colaborador_Id").val() + "?")) {
        var formularioEscala = $("#CadastroColaborador");
        var EscalaId = $("#colaborador_Id");
        EscalaId.attr("disabled", false);
        formularioEscala.attr("action", "/Colaborador/Excluir");
        formularioEscala.submit();
    }
};



$(document).ready(function () {

    if ($("#colaborador_Id").val() == 0) {
        $("#Botao_Excluir_Formulario_Colaborador").hide("slow");
        
    }
    $("#Botao_Excluir_Formulario_Colaborador").click(ExcluirFormulario)
    $('#Select2_Empresa').select2({
        
        language: "pt-BR",
        id: function (e) { return e.Id; },
        placeholder: "",
        allowClear: true,
        minimumInputLength: 2,
        
        ajax: {
            url: "/Colaborador/getEmpresas",
            datatype: 'json',
            type: 'POST',
            
            params: {
                contentType:'application/json; charset=utf-8'
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
    
    

    $('#Select2_Escalas').select2({

        language: "pt-BR",
        id: function (e) { return e.Id; },
        allowClear: true,
        minimumInputLength: 2,
        

        ajax: {
            url: "/Colaborador/getEscala",
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

    $('#CPF').mask('000.000.000-00', { reverse: false });
    $('#inputCEP').mask('00000-000');
    $('#PIS').mask('000.00000.00-0', { reverse: false });
    $('#Select2_Empresa')

    //Máscara Telefone
    var SPMaskBehavior = function (val) {
        return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
    },
        spOptions = {
            onKeyPress: function (val, e, field, options) {
                field.mask(SPMaskBehavior.apply({}, arguments), options);
            }
        };
    $('#Telefone').mask(SPMaskBehavior, spOptions);


});

//Ao fazer o click no botão excluir na tabela Index o click não se propagar para a tela de consulta
$(".Botao_Excluir").click(function (e) {
    e.stopPropagation();
});
$("#CPF").blur(function () {
    var valorCPF = $("#CPF").val().replace(/[^\d]+/g, "");
    var valido = TestaCPF(valorCPF);
    if (!valido) {
        $("#Erro_CPF").text("CPF Inválido").show();
    } else {
        $("#Erro_CPF").hide(); 
    }
});

//Validador CPF
function TestaCPF(strCPF) {

    strCPF = strCPF.replace(/[^\d]+/g, '');
    var Soma;
    var Resto;
    Soma = 0;
    
    // Elimina CNPJs invalidos conhecidos
    if (strCPF == "00000000000" ||
        strCPF == "11111111111" ||
        strCPF == "22222222222" ||
        strCPF == "33333333333" ||
        strCPF == "44444444444" ||
        strCPF == "55555555555" ||
        strCPF == "66666666666" ||
        strCPF == "77777777777" ||
        strCPF == "88888888888" ||
        strCPF == "99999999999")
        return false;


    for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(9, 10))) return false;

    Soma = 0;
    for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(10, 11))) return false;
    return true;
}


$("#PIS").blur(function () {
    var valorPIS = $("#PIS").val().replace(/[^\d]+/g, "");
    var valido = ValidaPIS(valorPIS);
    if (!valido) {
        $("#Erro_PIS").text("PIS Inválido").show();
    } else {
        $("#Erro_PIS").hide();
    }
});


var ftap = "3298765432";
var total = 0;
var i;
var resto = 0;
var numPIS = 0;
var strResto = "";

function ValidaPIS(pis) {

    total = 0;
    resto = 0;
    numPIS = 0;
    strResto = "";

    numPIS = pis;

    if (numPIS == "" || numPIS == null) {
        return false;
    }

    for (i = 0; i <= 9; i++) {
        resultado = (numPIS.slice(i, i + 1)) * (ftap.slice(i, i + 1));
        total = total + resultado;
    }

    resto = (total % 11)

    if (resto != 0) {
        resto = 11 - resto;
    }

    if (resto == 10 || resto == 11) {
        strResto = resto + "";
        resto = strResto.slice(1, 2);
    }

    if (resto != (numPIS.slice(10, 11))) {
        return false;
    }

    return true;
}