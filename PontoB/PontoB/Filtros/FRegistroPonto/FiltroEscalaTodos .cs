using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PontoB.Models;

namespace PontoB.Filtros.FRegistroPonto
{
   /* public class FiltroEscalaTodos : IFiltro<Escala>
    {
        public IList<Escala> Filtrar(IQueryable<Escala> query , string filtro)
        {
            var resultado = query.OrderBy(e=>e.Id).Where(e => e.Descricao.Contains(filtro)).ToList();

            if (int.TryParse(filtro, out int numero))
            {
                resultado.AddRange(query.OrderBy(e=>e.Id).Where(e => e.Id.Equals(numero)).ToList());

            }

            return resultado;
        }
    }*/
}