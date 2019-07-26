using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using Microsoft.EntityFrameworkCore;

namespace PontoB.Filtros.FRegistroPonto
{
    public class FiltroRegistroPontoEntreDataColaborador : IFiltro<RegistroPonto>
    {
        /*public IList<RegistroPonto> Filtrar(IQueryable<RegistroPonto> query , DateTime dataInicio, DateTime dataFim, Colaborador colaborador )
        {

            return query.Include(c => c.Colaborador).OrderByDescending(r => r.DataRegistro).Where(e => e.Colaborador.Id == colaborador.Id && e.DataRegistro>=dataInicio && e.DataRegistro<dataFim.AddDays(1)).ToList();

        }*/

        public IList<RegistroPonto> Filtrar(IQueryable<RegistroPonto> query, string filtro)
        {
            string[] paramentros = filtro.Split('|');
            DateTime dataInicial = DateTime.Parse(paramentros[0]);
            DateTime dataFinal = DateTime.Parse(paramentros[1]);
            int.TryParse(paramentros[2], out int colaboradorId);

            return query.Include(c => c.Colaborador).OrderByDescending(r => r.DataRegistro).Where(e => e.Colaborador.Id == colaboradorId && e.DataRegistro >= dataInicial && e.DataRegistro < dataFinal.AddDays(1)).ToList();
        }
    }
}