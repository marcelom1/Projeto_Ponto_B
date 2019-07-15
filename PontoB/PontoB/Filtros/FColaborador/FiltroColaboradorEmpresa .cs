using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FColaborador
{
    public class FiltroColaboradorEmpresa : IFiltro<Colaborador>
    {
        public IList<Colaborador> Filtrar(IQueryable<Colaborador> query, string filtro)
        {

            return query.OrderBy(e => e.NomeCompleto).Where(e => e.Empresa.RazaoSocial.Contains(filtro)).ToList();
        }
    }
}