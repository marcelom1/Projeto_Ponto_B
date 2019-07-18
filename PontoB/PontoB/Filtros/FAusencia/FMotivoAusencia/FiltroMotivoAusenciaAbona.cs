using System.Collections.Generic;
using System.Linq;
using PontoB.Models;

namespace PontoB.Filtros.FAusencia.FMotivoAusencia
{
    internal class FiltroMotivoAusenciaAbona : IFiltro<MotivoAusencia>
    {
        public IList<MotivoAusencia> Filtrar(IQueryable<MotivoAusencia> query, string filtro)
        {
            return query.OrderByDescending(e => e.Descricao).Where(e => e.Abonar.Equals(filtro)).ToList();
        }
    }
}