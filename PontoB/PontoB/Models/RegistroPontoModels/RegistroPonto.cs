using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models.RegistroPontoModels
{
    public class RegistroPonto
    {
        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public virtual Colaborador Colaborador { get; set; }
        public DateTime DataRegistro { get; set; }
        public int HoraRegistro { get; set; }
        public int MinutoRegistro { get; set; }
        public bool RegistroManual { get; set; }
        public string Observacao { get; set; }
        public bool DesconsiderarMarcacao { get; set; }

        
    }
}