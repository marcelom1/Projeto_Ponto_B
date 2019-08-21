using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models.ViewModels.VRelatorios.Home
{
    public class TopHomeViewModels
    {
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public IList<RelatorioTopHorasViewModels> HorasFalta { get; set; }
        public IList<RelatorioTopHorasViewModels> HorasExcedentes { get; set; }
    }
}