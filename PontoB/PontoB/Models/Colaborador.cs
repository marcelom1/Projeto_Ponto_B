using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PontoB.Models
{
    public class Colaborador
    {
        public int Id { get; set; }

       [Required(ErrorMessage = "Nome completo é um campo obrigatório.")]
        public string NomeCompleto { get; set; }

       [Required(ErrorMessage = "CPF é um campo obrigatório.")]
        public string CPF { get; set; }

        public string RG { get; set; }
        public string Cargo { get; set; }

       [Required(ErrorMessage = "E-mail é um campo obrigatório.")]
        public string Email { get; set; }

        public string Telefone { get; set; }
       
        [Required(ErrorMessage = "Data de Admissão é um campo obrigatório.")]
        public DateTime? DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }
        public DateTime? DataNascimento  { get; set; }

        [Required(ErrorMessage = "PIS é um campo obrigatório.")]
        public string Pis { get; set; }

       
        public virtual Endereco EnderecoColaborador { get; set; }

       [Required(ErrorMessage = "Escala é um campo obrigatório.")]
        public int EscalaId { get; set; }
        public Escala Escala { get; set; }
            

        [Required(ErrorMessage = "1Empresa é um campo obrigatório.")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        [Required(ErrorMessage = "1Senha é um campo obrigatório.")]
        public string Senha { get; set; }

        public bool Master { get; set; }
    }
}