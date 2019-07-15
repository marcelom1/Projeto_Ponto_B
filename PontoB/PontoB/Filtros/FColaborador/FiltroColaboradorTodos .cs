using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PontoB.Models;

namespace PontoB.Filtros.FColaborador
{
    public class FiltroColaboradorTodos : IFiltro<Colaborador>
    {
        public IList<Colaborador> Filtrar(IQueryable<Colaborador> query , string filtro)
        {
            var resultado = query.Where(e => e.CPF.Contains(filtro) || e.NomeCompleto.Contains(filtro) || e.Empresa.RazaoSocial.Contains(filtro)).ToList();

            if (int.TryParse(filtro, out int numero))
            {
                resultado.AddRange(query.Where(e => e.Id.Equals(numero)).ToList());

            }

            return resultado;
        }
    }
}