using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FRegistroPonto 
{
    public class FiltroRegistroPonto
    {
        public static IFiltro<RegistroPonto> ObterFiltroColuna(string coluna)
        {
            if (coluna.Equals("Código", StringComparison.OrdinalIgnoreCase))
                return new FiltroRegistroPontoCodigo();
          /*  else if (coluna.Equals("Descrição", StringComparison.OrdinalIgnoreCase))
                return new FiltroEscalaDescricao();
            else if (coluna.Equals("Todos", StringComparison.OrdinalIgnoreCase))
                return new FiltroEscalaTodos();
           */

            return null;

        }

    }
}