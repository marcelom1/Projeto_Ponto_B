using Microsoft.EntityFrameworkCore;
using PontoB.Business.Utils;
using PontoB.DAO;
using PontoB.Models.RegistroPontoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FRegistroPonto
{
    public class FiltroRegistroPontoTodosRegistros : IFiltro<RegistroPonto>
    {
        private RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();
        public IList<RegistroPonto> Filtrar(IQueryable<RegistroPonto> query, string filtro)
        {

            var valores = FiltroPeriodoValores.FromString(filtro);
            if (DateTime.TryParse(valores.Fim.ToString(), out DateTime dataFim))
                return query.Include(c => c.Colaborador).OrderByDescending(r => r.DataRegistro).Where(e => e.DataRegistro >= valores.Inicio && e.DataRegistro < dataFim.AddDays(1)).ToList();
            return dbRegistroPonto.Filtro("Colaborador", valores.Id.ToString());
        }

    }
}