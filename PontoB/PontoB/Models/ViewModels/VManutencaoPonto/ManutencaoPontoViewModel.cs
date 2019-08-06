using PontoB.Models.RegistroPontoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models.ViewModels.VManutencaoPonto
{
    public class ManutencaoPontoViewModel
    {
        public string EscalaHorario { get; set; }
        public DateTime Dia { get; set; }
        public string DiaDaSemana { get; set; }
        public IOrderedEnumerable <RegistroPonto> Registros { get; set; }
        public int colaboradorId { get; set; }

    }
}