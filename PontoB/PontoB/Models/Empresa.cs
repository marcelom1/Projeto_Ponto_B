using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PontoB.Models
{
    public class Empresa
    {
        public int Id { get;  set; }
        [Required]
        public string RazaoSocial { get;  set; }
        [Required]
        public string Cnpj { get;  set; }
        public string NomeFantasia { get;  set; }
        public int CEP { get;  set; }
        public string Logradouro { get;  set; }
        public int  Numero { get;  set; }
        public string Complemento { get;  set; }
        public string Bairro { get;  set; }
        public string Estado { get;  set; }
        public string Cidade { get;  set; }
        public string Email { get;  set; }
        public string Telefone { get;  set; }

      
    }
}