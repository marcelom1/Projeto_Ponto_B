using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PontoB.Models;

namespace PontoB.Filtros.FEscala
{
    public class FiltroEscalaCodigo : IFiltro<Escala>
    {
        public IList<Escala> Filtrar(IQueryable<Escala> query , string filtro)
        {
            
            if (int.TryParse(filtro, out int numero))
            {
                return(query.OrderBy(e => e.Id).Where(e => e.Id.Equals(numero)).ToList());

            }
            return query.OrderBy(e => e.Id).Where(e=>e.Id.Equals(filtro)).ToList();
        }
    }
}