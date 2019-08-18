using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FOcorrenciaDia
{
    public class FiltroOcorrenciaDia
    {
        public static IFiltro<OcorrenciaDia> ObterFiltroColuna(string coluna)
        {
            if (coluna.Equals("OcorrenciaEntreDatas", StringComparison.OrdinalIgnoreCase))
                return new FiltroOcorrenciaDiaEntreData();
            else if (coluna.Equals("OcorrenciaDiaEntreDataPorEmpresa", StringComparison.OrdinalIgnoreCase))
                return new FiltroOcorrenciaDiaEntreDataPorEmpresa();

            
            return null;
        }
    }
}