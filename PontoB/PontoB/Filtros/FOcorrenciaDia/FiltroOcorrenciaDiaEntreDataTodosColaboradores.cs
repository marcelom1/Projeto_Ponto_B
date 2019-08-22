﻿using Microsoft.EntityFrameworkCore;
using PontoB.Business.Utils;
using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FOcorrenciaDia
{
    public class FiltroOcorrenciaDiaEntreDataTodosColaboradores : IFiltro<OcorrenciaDia>
    {
        private OcorrenciaDiaDAO dbRegistroPonto = new OcorrenciaDiaDAO();


        public IList<OcorrenciaDia> Filtrar(IQueryable<OcorrenciaDia> query, string filtro)
        {

            var valores = FiltroPeriodoValores.FromString(filtro);
            if (DateTime.TryParse(valores.Fim.ToString(), out DateTime dataFim))
                return query.Include(c => c.Colaborador).OrderByDescending(r => r.Date).Where(e=>e.Date >= valores.Inicio.Value.Date && e.Date < dataFim.Date.AddDays(1)).ToList();
            return dbRegistroPonto.Filtro("Colaborador", valores.Id.ToString());
        }

    }
}