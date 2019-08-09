using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PontoB.Models
{
    public class OcorrenciaDia
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int ColaboradorId { get; set; }

       
        public Colaborador Colaborador { get; set; }

        [Required]
        public int CodigoOcorrencia { get; set; }

        [Required]
        public int QtdMinutos { get; set; }
    }
}