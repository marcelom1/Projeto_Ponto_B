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
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public virtual EstadosUF Estado { get; set; }
        public string Cidade { get; set; }

        public override string ToString()
        {
            return Logradouro + " " + Numero + ", " + Bairro + " - " + Cidade; 
        }
    }
}