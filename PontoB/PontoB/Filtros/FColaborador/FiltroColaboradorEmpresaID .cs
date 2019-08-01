using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FColaborador
{
    public class FiltroColaboradorEmpresaID : IFiltro<Colaborador>
    {
        public IList<Colaborador> Filtrar(IQueryable<Colaborador> query, string filtro)
        {
            if (int.TryParse(filtro, out int numero))
            {
                return (query.OrderBy(e => e.NomeCompleto).Where(e => e.EmpresaId.Equals(numero)).ToList());

            }
            return query.OrderBy(e => e.NomeCompleto).Where(e => e.EmpresaId.Equals(filtro)).ToList();

        }
    }
}