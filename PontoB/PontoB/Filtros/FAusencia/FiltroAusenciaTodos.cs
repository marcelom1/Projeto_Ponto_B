using System.Collections.Generic;
using System.Linq;
using PontoB.Models;

namespace PontoB.Filtros.FAusencia
{
    internal class FiltroAusenciaTodos : IFiltro<AusenciaColaboradores>
    {
        public IList<AusenciaColaboradores> Filtrar(IQueryable<AusenciaColaboradores> query, string filtro)
        {
            var resultado = query.Where(e => e.Colaborador.NomeCompleto.Contains(filtro) || e.DataFim.Equals(filtro) || e.DataInicio.Equals(filtro) || e.Colaborador.Empresa.RazaoSocial.Contains(filtro) || e.Ausencia.Id.Equals(filtro) || e.MotivoAusencia.Descricao.Contains(filtro)).ToList();
            return resultado;
        }
    }
}