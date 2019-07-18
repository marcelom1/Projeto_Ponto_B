using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FAusencia.FMotivoAusencia
{
    public class FiltroMotivoAusencia
    {
        public static IFiltro<MotivoAusencia> ObterFiltroColuna(string coluna)
        {
            if (coluna.Equals("Código", StringComparison.OrdinalIgnoreCase))
                return new FiltroMotivoAusenciaCodigo();
            else if (coluna.Equals("Descricao", StringComparison.OrdinalIgnoreCase))
                return new FiltroMotivoAusenciaDescricao();
            else if (coluna.Equals("Abona", StringComparison.OrdinalIgnoreCase))
                return new FiltroMotivoAusenciaAbona();
           

            return null;

        }

    }
}