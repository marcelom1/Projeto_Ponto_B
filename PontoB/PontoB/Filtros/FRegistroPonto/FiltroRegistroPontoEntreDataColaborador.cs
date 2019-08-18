using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using Microsoft.EntityFrameworkCore;
using PontoB.Business.Utils;
using PontoB.DAO;

namespace PontoB.Filtros.FRegistroPonto
{
    public class FiltroRegistroPontoEntreDataColaborador : IFiltro<RegistroPonto>
    {
        private RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();
        /*public IList<RegistroPonto> Filtrar(IQueryable<RegistroPonto> query , DateTime dataInicio, DateTime dataFim, Colaborador colaborador )
        {

            return query.Include(c => c.Colaborador).OrderByDescending(r => r.DataRegistro).Where(e => e.Colaborador.Id == colaborador.Id && e.DataRegistro>=dataInicio && e.DataRegistro<dataFim.AddDays(1)).ToList();

        }*/

        public IList<RegistroPonto> Filtrar(IQueryable<RegistroPonto> query, string filtro)
        {
            
            var valores = FiltroPeriodoValores.FromString(filtro);
            if (DateTime.TryParse(valores.Fim.ToString(), out DateTime dataFim))
                return query.Include(c => c.Colaborador).OrderByDescending(r => r.DataRegistro).Where(e => e.Colaborador.Id == valores.Id && e.DataRegistro >= valores.Inicio && e.DataRegistro < dataFim.AddDays(1)).ToList();
            return dbRegistroPonto.Filtro("Colaborador", valores.Id.ToString());
        }
    }
}