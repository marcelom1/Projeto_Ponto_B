using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PontoB.Models
{
    public class Empresa
    {
        public int Id { get; private set; }
        [Required]
        public string RazaoSocial { get; private set; }
        [Required]
        public string Cnpj { get; private set; }
        public string NomeFantasia { get; private set; }
        public int CEP { get; private set; }
        public string Logradouro { get; private set; }
        public int  Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Estado { get; private set; }
        public string Cidade { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }

        public Empresa(string razaoSocial, string cnpj, string nomeFantasia, int cEP, string logradouro, int numero, string complemento, string bairro, string estado, string cidade, string email, string telefone)
        {
         
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            NomeFantasia = nomeFantasia;
            CEP = cEP;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Estado = estado;
            Cidade = cidade;
            Email = email;
            Telefone = telefone;
        }
    }
}