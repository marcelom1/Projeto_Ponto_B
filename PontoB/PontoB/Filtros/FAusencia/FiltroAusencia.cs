using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FAusencia
{
    public class FiltroAusencia
    {
        public static IFiltro<AusenciaColaboradores> ObterFiltroColuna(string coluna)
        {
            if (coluna.Equals("Colaborador", StringComparison.OrdinalIgnoreCase))
                return new FiltroAusenciaColaborador();
            else if (coluna.Equals("Data Inicio", StringComparison.OrdinalIgnoreCase))
                return new FiltroAusenciaDataInicio();
            else if (coluna.Equals("Data Fim", StringComparison.OrdinalIgnoreCase))
                return new FiltroAusenciaDataFim();
            if (coluna.Equals("Empresa", StringComparison.OrdinalIgnoreCase))
                return new FiltroAusenciaEmpresa();
            else if (coluna.Equals("Motivo", StringComparison.OrdinalIgnoreCase))
                return new FiltroAusenciaMotivo();
            else if (coluna.Equals("Grupo", StringComparison.OrdinalIgnoreCase))
                return new FiltroAusenciaGrupo();
            else if (coluna.Equals("Todos", StringComparison.OrdinalIgnoreCase))
                return new FiltroAusenciaTodos();
            else if (coluna.Equals("ColaboradorEntreData", StringComparison.OrdinalIgnoreCase))
                return new FiltroAusenciaColaboradorEntreData();

            return null;

        }

    }
}