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
    public class FiltroRegistroPontoUltimoREgistro : IFiltro<RegistroPonto>
    {
        public IList<RegistroPonto> Filtrar(IQueryable<RegistroPonto> query , string filtro)
        {
            
            if (int.TryParse(filtro, out int numero))
            {
                
                return(query.Include(e=>e.Colaborador).OrderByDescending(e => e.DataRegistro).Where(e=>e.DataRegistro.Date.ToString("MM/dd/yyyy").Equals(DateTime.Now.Date.ToString("MM/dd/yyyy"))&& e.Colaborador.Id.ToString().Equals(filtro)).ToList());

            }
            return query.OrderBy(e => e.Id).Where(e=>e.Id.Equals(filtro)).ToList();
        }
    }
}