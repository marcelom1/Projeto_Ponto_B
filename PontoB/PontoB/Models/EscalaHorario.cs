using System;
using System.Timers;

namespace PontoB.Models
{
    public class EscalaHorario
    {
        public int Id { get; set; }
        public int EscalaId { get; set; }
        public string DiaSemana { get; set; }
        public int EntradaHora { get; set; }
        public int EntradaMinuto { get; set; }
        public int SaidaHora { get; set; }
        public int SaidaMinuto { get; set; }
    }
}