using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FGamificacao
{
    public class FiltroGamificacao
    {
        public static IFiltro<Pontuacao> ObterFiltroColuna(string coluna)
        {
            if (coluna.Equals("PontuacaoEntreData", StringComparison.OrdinalIgnoreCase))
                return new PontuacaoEntreData();
            
            return null;

        }

    }
}