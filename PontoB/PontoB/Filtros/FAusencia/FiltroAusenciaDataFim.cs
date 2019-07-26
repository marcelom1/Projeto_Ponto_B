using System.Collections.Generic;
using System.Linq;
using PontoB.Models;

namespace PontoB.Filtros.FAusencia
{
    internal class FiltroAusenciaDataFim : IFiltro<AusenciaColaboradores>
    {
        public IList<AusenciaColaboradores> Filtrar(IQueryable<AusenciaColaboradores> query, string filtro)
        {
            // 01/01/209|10/
            var datas = filtro.Split('|');
            
            return query.OrderByDescending(e => e.DataFim).Where(e => e.DataFim.Equals(filtro)).ToList();
        }
    }
}