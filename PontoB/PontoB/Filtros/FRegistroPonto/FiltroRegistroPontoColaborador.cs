using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using Microsoft.EntityFrameworkCore;

namespace PontoB.Filtros.FRegistroPonto
{
    public class FiltroRegistroPontoColaborador : IFiltro<RegistroPonto>
    {
        public IList<RegistroPonto> Filtrar(IQueryable<RegistroPonto> query , string filtro)
        {
            
            if (int.TryParse(filtro, out int numero))
            {
                return (query.Include(c=>c.Colaborador).OrderByDescending(e => e.DataRegistro).Where(e => e.Colaborador.Id.Equals(numero)).ToList());

            }
            return (query.Include(c => c.Colaborador).OrderByDescending(e => e.DataRegistro).Where(e => e.Colaborador.Id.Equals(numero)).ToList());
        }
    }
}