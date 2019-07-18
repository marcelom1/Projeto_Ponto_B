using System.Collections.Generic;
using System.Linq;
using PontoB.Models;

namespace PontoB.Filtros.FAusencia
{
    internal class FiltroAusenciaColaborador : IFiltro<AusenciaColaboradores>
    {
        public IList<AusenciaColaboradores> Filtrar(IQueryable<AusenciaColaboradores> query, string filtro)
        {
            return query.OrderBy(e => e.Colaborador.NomeCompleto).Where(e => e.Colaborador.NomeCompleto.Contains(filtro)).ToList();
        }
    }
}