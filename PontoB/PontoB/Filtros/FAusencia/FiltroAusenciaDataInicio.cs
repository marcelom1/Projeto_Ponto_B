using System.Collections.Generic;
using System.Linq;
using PontoB.Models;

namespace PontoB.Filtros.FAusencia
{
    internal class FiltroAusenciaDataInicio : IFiltro<AusenciaColaboradores>
    {
        public IList<AusenciaColaboradores> Filtrar(IQueryable<AusenciaColaboradores> query, string filtro)
        {
            return query.OrderByDescending(e => e.DataInicio).Where(e => e.DataInicio.Equals(filtro)).ToList();

        }
    }
}