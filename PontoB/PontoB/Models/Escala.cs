using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PontoB.Models
{
    public class Escala
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Descrição é um campo obrigatório.")]
        public string Descricao { get; set; }
        public virtual IList<EscalaHorario> EscalasHorario { get; set; } 



    }
}