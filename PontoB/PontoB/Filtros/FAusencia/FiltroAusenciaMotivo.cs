using System.Collections.Generic;
using System.Linq;
using PontoB.Models;

namespace PontoB.Filtros.FAusencia
{
    internal class FiltroAusenciaMotivo : IFiltro<AusenciaColaboradores>
    {
        public IList<AusenciaColaboradores> Filtrar(IQueryable<AusenciaColaboradores> query, string filtro)
        {
            return query.OrderBy(e => e.MotivoAusencia.Descricao).Where(e => e.MotivoAusencia.Descricao.Contains(filtro)).ToList();

        }
    }
}