using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PontoB.Test.Cadastro
{
    public class EmpresaTeste
    {
        private static EmpresaDAO dbEmpresa = new EmpresaDAO();
        private static EstadosUFDAO dbEstado = new EstadosUFDAO();

        public Empresa CriarEmpresaPadrao()
        {
            var empresa = new Empresa
            {
                Cnpj = "11.352.722/0001-23",
                RazaoSocial = "Empresa Teste",
                EnderecoEmpresa = new Endereco
                {
                    Logradouro = "Logradouro Teste",
                    Bairro = "Bairro Teste",
                    Cidade = "Cidade Teste",
                    Estado = dbEstado.BuscarPorId(1)
                    
                    
                }
                
            };
            

            return empresa;
        }
    }
}
