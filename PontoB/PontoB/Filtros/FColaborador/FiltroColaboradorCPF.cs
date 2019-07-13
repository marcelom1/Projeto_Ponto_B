using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PontoB.Models;

namespace PontoB.Filtros.FColaborador
{
    public class FiltroColaboradorCPF : IFiltro<Colaborador>
    {
        public IList<Colaborador> Filtrar(IQueryable<Colaborador> query , string filtro)
        {

            return query.OrderBy(e=>e.CPF).Where(e => e.CPF.Equals(filtro)).ToList();
        }
    }
}