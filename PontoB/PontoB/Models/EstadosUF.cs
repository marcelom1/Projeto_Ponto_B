using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PontoB.Models
{
    public class EstadosUF
    {
        [Required(ErrorMessage = "Estado é um campo obrigatório.")]
        public int Id { get; set; }
        public string Sigla { get; set; }
        

    }
}