﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PontoB.Models
{
    public class Empresa
    {
        public int Id { get;  set; }
        [Required(ErrorMessage = "Razão Social é um campo obrigatório.")]
        public string RazaoSocial { get; set; }
        [Required(ErrorMessage = "CNPJ é um campo obrigatório.")]
        public string Cnpj { get;  set; }
        public string NomeFantasia { get;  set; }
        public Endereco EnderecoEmpresa { get; set; }
        public string Email { get;  set; }
        public string Telefone { get;  set; }

        
    }
}