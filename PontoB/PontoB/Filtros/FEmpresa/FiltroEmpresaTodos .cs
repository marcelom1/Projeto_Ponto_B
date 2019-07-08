using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PontoB.Models;

namespace PontoB.Filtros.FEmpresa
{
    public class FiltroEmpresaTodos : IFiltro<Empresa>
    {
        public IList<Empresa> Filtrar(IQueryable<Empresa> query , string filtro)
        {
            var resultado = query.Where(e => e.RazaoSocial.Contains(filtro) || e.NomeFantasia.Contains(filtro) || e.Cnpj.Contains(filtro)).ToList();

            if (int.TryParse(filtro, out int numero))
            {
                resultado.AddRange(query.Where(e => e.Id.Equals(numero)).ToList());

            }

            return resultado;
        }
    }
}