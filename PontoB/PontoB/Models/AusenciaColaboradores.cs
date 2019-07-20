using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PontoB.Models
{
    public class AusenciaColaboradores
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Data Inicio é um campo obrigatório.")]
        public DateTime? DataInicio { get; set; }
        [Required(ErrorMessage = "Hora inicial é um campo obrigatório.")]
        public int HoraInicio { get; set; }
        [Required(ErrorMessage = "Minuto inicial é um campo obrigatório.")]
        public int MinutoInicio { get; set; }
        [Required(ErrorMessage = "Data fim é um campo obrigatório.")]
        public DateTime? DataFim { get; set; }
        [Required(ErrorMessage = "Hora fim é um campo obrigatório.")]
        public int HoraFim { get; set; }
        [Required(ErrorMessage = "Minuto fim é um campo obrigatório.")]
        public int MinutoFim { get; set; }
        public string Observacao { get; set; }

        [Required(ErrorMessage = "Colaborador é um campo obrigatório.")]
        public int ColaboradorId { get; set; }
        public Colaborador Colaborador { get; set; }

        [Required(ErrorMessage = "Motivo é um campo obrigatório.")]
        public int MotivoAusenciaId { get; set; }
        public MotivoAusencia MotivoAusencia { get; set; }

        [Required(ErrorMessage = "Motivo é um campo obrigatório.")]
        public int AusenciaId { get; set; }
        public Ausencia Ausencia { get; set; }
    }
}