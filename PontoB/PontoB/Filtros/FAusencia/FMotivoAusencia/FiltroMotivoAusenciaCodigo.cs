using System.Collections.Generic;
using System.Linq;
using PontoB.Models;

namespace PontoB.Filtros.FAusencia.FMotivoAusencia
{
    internal class FiltroMotivoAusenciaCodigo : IFiltro<MotivoAusencia>
    {
        public IList<MotivoAusencia> Filtrar(IQueryable<MotivoAusencia> query, string filtro)
        {
            if (int.TryParse(filtro, out int numero))
            {
                return (query.OrderBy(e => e.Id).Where(e => e.Id.Equals(numero)).ToList());

            }
            return query.OrderBy(e => e.Id).Where(e => e.Id.Equals(filtro)).ToList();
        }
    }
}