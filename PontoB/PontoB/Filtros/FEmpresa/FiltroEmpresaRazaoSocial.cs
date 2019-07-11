using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PontoB.Models;

namespace PontoB.Filtros.FEmpresa
{
    public class FiltroEmpresaRazaoSocial : IFiltro<Empresa>
    {
        public IList<Empresa> Filtrar(IQueryable<Empresa> query , string filtro)
        {

            return query.OrderBy(e=>e.RazaoSocial).Where(e => e.RazaoSocial.Contains(filtro)).ToList();
        }
    }
}