using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;

namespace PontoB.Filtros.FRegistroPonto
{
    public class FiltroRegistroPontoCodigo : IFiltro<RegistroPonto>
    {
        public IList<RegistroPonto> Filtrar(IQueryable<RegistroPonto> query , string filtro)
        {
            
            if (int.TryParse(filtro, out int numero))
            {
                return(query.OrderBy(e => e.Id).Where(e => e.Id.Equals(numero)).ToList());

            }
            return query.OrderBy(e => e.Id).Where(e=>e.Id.Equals(filtro)).ToList();
        }
    }
}