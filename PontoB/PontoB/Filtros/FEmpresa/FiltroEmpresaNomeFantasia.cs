using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PontoB.Models;

namespace PontoB.Filtros.FEmpresa
{
    public class FiltroEmpresaNomeFantasia : IFiltro<Empresa>
    {
        public IList<Empresa> Filtrar(IQueryable<Empresa> query , string filtro)
        {

            return query.Where(e => e.NomeFantasia.Contains(filtro)).ToList();
        }
    }
}