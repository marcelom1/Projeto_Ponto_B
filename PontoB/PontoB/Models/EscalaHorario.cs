using System;
using System.ComponentModel.DataAnnotations;
using System.Timers;

namespace PontoB.Models
{
    public class EscalaHorario
    {
        public int Id { get; set; }
        public virtual int EscalaId { get; set; }
        [Required(ErrorMessage = "Dia da semana não pode ficar em branco")]
        public string DiaSemana { get; set; }
        public int EntradaHora { get; set; }
        public int EntradaMinuto { get; set; }
        public int SaidaHora { get; set; }
        public int SaidaMinuto { get; set; }
        public int TotalEmMinutos { get; set; }
        
    }
}