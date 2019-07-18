using System.Collections.Generic;
using System.Linq;
using PontoB.Models;

namespace PontoB.Filtros.FAusencia.FMotivoAusencia
{
    internal class FiltroMotivoAusenciaDescricao : IFiltro<MotivoAusencia>
    {
        public IList<MotivoAusencia> Filtrar(IQueryable<MotivoAusencia> query, string filtro)
        {
            return query.OrderBy(e => e.Descricao).Where(e => e.Descricao.Contains(filtro)).ToList();
        }
    }
}