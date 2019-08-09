using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FEscala.FEscalaHorario
{
    public class FiltroEscalaHorario
    {
        public static IFiltro<EscalaHorario> ObterFiltroColuna(string coluna)
        {
            if (coluna.Equals("Código", StringComparison.OrdinalIgnoreCase))
                return new FiltroEscalaHorarioPorEscalaId();
            return null;
        }
    }
}