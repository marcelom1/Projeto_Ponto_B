using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PontoB.Models;

namespace PontoB.Filtros.FEmpresa
{
    public class FiltroEmpresaCnpj : IFiltro<Empresa>
    {
        public IList<Empresa> Filtrar(IQueryable<Empresa> query , string filtro)
        {

            return query.Where(e => e.Cnpj.Equals(filtro)).ToList();
        }
    }
}