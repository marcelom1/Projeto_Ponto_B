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
            else if (coluna.Equals("PontuacaoEntreDataColaborador", StringComparison.OrdinalIgnoreCase))
                return new PontuacaoEntreDataColaborador();
            else if (coluna.Equals("PontuacaoEntreDataEmpresa", StringComparison.OrdinalIgnoreCase))
                return new PontuacaoEntreDataEmpresa();

            return null;

        }

    }
}