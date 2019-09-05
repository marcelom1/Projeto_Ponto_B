using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PontoB.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string CEP { get; set; }
        [Required(ErrorMessage = "Logradouro é um campo obrigatório.")]
        public string Logradouro { get; set; }
       
        public int Numero { get; set; }
        public string Complemento { get; set; }
        [Required(ErrorMessage = "Bairro é um campo obrigatório.")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Estado é um campo obrigatório.")]
        public virtual EstadosUF Estado { get; set; }
        [Required(ErrorMessage = "Cidade é um campo obrigatório.")]
        public string Cidade { get; set; }

        public override string ToString()
        {
            return Logradouro + " " + Numero + ", " + Bairro + " - " + Cidade; 
        }
    }
}