using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PontoB.Models;

namespace PontoB.Filtros.FEmpresa
{
    public class FiltroEmpresaCodigo : IFiltro<Empresa>
    {
        public IList<Empresa> Filtrar(IQueryable<Empresa> query , string filtro)
        {
            
            if (int.TryParse(filtro, out int numero))
            {
                return(query.Where(e => e.Id.Equals(numero)).ToList());

            }
            return query.Where(e=>e.Id.Equals(filtro)).ToList();
        }
    }
}