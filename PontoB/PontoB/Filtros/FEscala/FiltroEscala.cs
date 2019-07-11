using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FEscala
{
    public class FiltroEscala
    {
        public static IFiltro<Escala> ObterFiltroColuna(string coluna)
        {
            if (coluna.Equals("Código", StringComparison.OrdinalIgnoreCase))
                return new FiltroEscalaCodigo();
            else if (coluna.Equals("Descrição", StringComparison.OrdinalIgnoreCase))
                return new FiltroEscalaDescricao();
            else if (coluna.Equals("Todos", StringComparison.OrdinalIgnoreCase))
                return new FiltroEscalaTodos();
           

            return null;

        }

    }
}