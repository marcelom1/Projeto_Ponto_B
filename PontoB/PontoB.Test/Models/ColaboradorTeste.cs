﻿using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PontoB.Test.Cadastro
{
    public  class ColaboradorTeste
    {
        private static ColaboradorDAO dbColaborador = new ColaboradorDAO();

        public  Colaborador CriarColaboradorPadrao(DateTime dataAdmissao, DateTime? dataDemissao)
        {
            var colaborador = new Colaborador
            {
                CPF = "637.955.130-14",
                NomeCompleto = "Colaborador Teste",
                Email = "teste@colaborador.com",
                DataAdmissao = dataAdmissao,
                DataDemissao = dataDemissao,
                Pis = "120.8827.603-5",
                EscalaId = 0,
                
                EmpresaId = 0,
                
                Senha = "123"
            };

           
            return colaborador;
        }
    }
}
