using Microsoft.EntityFrameworkCore;
using PontoB.Business.Utils;
using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FAusencia
{
    public class FiltroAusenciaColaboradorEntreData : IFiltro<AusenciaColaboradores>
    {
        private AusenciaDAO dbAusencia = new AusenciaDAO();
        public IList<AusenciaColaboradores> Filtrar(IQueryable<AusenciaColaboradores> query, string filtro)
        {
            var valores = FiltroPeriodoValores.FromString(filtro);
            if (DateTime.TryParse(valores.Fim.ToString(), out DateTime dataFim))
                return query.Include(c => c.Colaborador).Include(c=>c.MotivoAusencia).OrderByDescending(r => r.DataInicio).Where(e => e.Colaborador.Id == valores.ColaboradorId && e.DataFim >= valores.Inicio && e.DataInicio < dataFim.AddDays(1)).ToList();


            return null;
        }
    }
}