using Microsoft.EntityFrameworkCore;
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
                if (numero!=0)
                    return (query.OrderBy(e => e.NomeCompleto).Include(x=>x.Empresa.EnderecoEmpresa).Where(e => e.EmpresaId.Equals(numero)).ToList());
                

            }
            return query.OrderBy(e => e.NomeCompleto).ToList();

        }
    }
}