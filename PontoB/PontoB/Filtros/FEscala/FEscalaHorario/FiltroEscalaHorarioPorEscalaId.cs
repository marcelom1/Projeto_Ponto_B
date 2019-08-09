using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FEscala.FEscalaHorario
{
    public class FiltroEscalaHorarioPorEscalaId : IFiltro<EscalaHorario>
    {

        public IList<EscalaHorario> Filtrar(IQueryable<EscalaHorario> query, string filtro)
        {

            if (int.TryParse(filtro, out int numero))
            {
                return (query.OrderBy(e => e.Id).Where(e => e.EscalaId.Equals(numero)).ToList());

            }
            return query.OrderBy(e => e.Id).Where(e => e.EscalaId.Equals(filtro)).ToList();
        }
    }
}