using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PontoB.Models;

namespace PontoB.Filtros.FEscala
{
    public class FiltroEscalaDescricao : IFiltro<Escala>
    {
        public IList<Escala> Filtrar(IQueryable<Escala> query , string filtro)
        {

            return query.OrderBy(e => e.Descricao).Where(e => e.Descricao.Contains(filtro)).ToList();
        }
    }
}