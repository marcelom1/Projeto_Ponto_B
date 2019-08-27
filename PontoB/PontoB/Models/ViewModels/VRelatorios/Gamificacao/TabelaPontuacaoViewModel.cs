using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models.ViewModels.VRelatorios.Gamificacao
{
    public class TabelaPontuacaoViewModel
    {
        public Empresa Empresa { get; set; }
        public Colaborador Colaborador { get; set; }
        public IList<Pontuacao> Pontuacao { get; set; }
    }
}