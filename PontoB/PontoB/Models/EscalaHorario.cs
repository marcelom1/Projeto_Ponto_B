using System;

namespace PontoB.Models
{
    public class EscalaHorario
    {
        public int Id { get; set; }
        public Escala EscalaId { get; set; }
        public string DiaSemana { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSaida { get; set; }
    }
}