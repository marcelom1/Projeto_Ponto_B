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
            else if (coluna.Equals("UltimoRegistroDia", StringComparison.OrdinalIgnoreCase))
                return new FiltroRegistroPontoUltimoREgistro();
            else if (coluna.Equals("Colaborador", StringComparison.OrdinalIgnoreCase))
                return new FiltroRegistroPontoColaborador();
            else if (coluna.Equals("RegistroPontoEntreDatas", StringComparison.OrdinalIgnoreCase))
                return new FiltroRegistroPontoEntreDataColaborador();
            else if (coluna.Equals("FiltroRegistroPontoTodosRegistros", StringComparison.OrdinalIgnoreCase))
                return new FiltroRegistroPontoTodosRegistros();

            

            return null;

        }

    }
}