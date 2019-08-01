using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models
{
    public class ManutencaoPonto
    {
        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public virtual Colaborador Colaborador { get; set; }
        public DateTime DataRegistro { get; set; }
        public int HoraRegistro { get; set; }
        public int MinutoRegistro { get; set; }
        public string Motivo { get; set; }
    }
}