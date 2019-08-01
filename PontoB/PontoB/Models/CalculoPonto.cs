using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models
{
    public class CalculoPonto
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int ColaboradorId { get; set; }
        public virtual Colaborador Colaborador { get; set; }
        public int TotalPrevistoEmMinutos { get; set; }
        public int TotalRealizadoEmMinutos { get; set; }
        public int SaldoEmMinutos { get; set; }


    }
}