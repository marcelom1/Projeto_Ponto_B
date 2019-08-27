using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models.ViewModels.VRegistroPonto
{
    public class HistoricoRegistroPontoComFiltro
    {
        public IPagedList<HistoricoRegistroPontoViewModels> HistoricoRegistroPonto { get; set; }
        public DateTime? FiltroDataInicio { get; set; }
        public DateTime? FiltroDataFim { get; set; }
        public int escalaId { get; set; }
        public Colaborador Colaborador { get; set; }

    }
}