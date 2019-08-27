using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models.ViewModels.VRelatorios.Gamificacao
{
    public class RelatorioResumoPontuacaoViewModel
    {
        public IList<TabelaPontuacaoViewModel> Pontuacao { get; set; }
        public Empresa Empresa { get; set; }
        public string Periodo { get; set; }
    }
}