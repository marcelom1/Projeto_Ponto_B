using PontoB.Business.Utils;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FAusencia
{
    public class FiltroAusenciaMotivoEntreData : IFiltro<AusenciaColaboradores>
    {
        public IList<AusenciaColaboradores> Filtrar(IQueryable<AusenciaColaboradores> query, string filtro)
        {
            var valores = FiltroPeriodoValores.FromString(filtro);
            return query.OrderBy(e => e.MotivoAusencia.Descricao).Where(e => ( valores.Id == 0|| e.MotivoAusencia.Id.Equals(valores.Id)) && (e.DataFim >= valores.Inicio && e.DataInicio < valores.Fim.Value.AddDays(1))).ToList();

        }
    }
}