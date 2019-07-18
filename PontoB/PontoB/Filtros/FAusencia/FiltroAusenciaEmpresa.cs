using System.Collections.Generic;
using System.Linq;
using PontoB.Models;

namespace PontoB.Filtros.FAusencia
{
    internal class FiltroAusenciaEmpresa : IFiltro<AusenciaColaboradores>
    {
        public IList<AusenciaColaboradores> Filtrar(IQueryable<AusenciaColaboradores> query, string filtro)
        {
            return query.OrderBy(e => e.Colaborador.Empresa.RazaoSocial).Where(e => e.Colaborador.Empresa.RazaoSocial.Contains(filtro)).ToList();
        }
    }
}