﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PontoB.Models;

namespace PontoB.Filtros.FEmpresa
{
    public class FiltroEmpresaCodigo : IFiltro<Empresa>
    {
        public IList<Empresa> Filtrar(IQueryable<Empresa> query , string filtro)
        {
            
            if (int.TryParse(filtro, out int numero))
            {
                return(query.OrderBy(e=>e.Id).Where(e => e.Id.Equals(numero)).ToList());

            }
            return query.OrderBy(e => e.Id).Where(e=>e.Id.Equals(filtro)).ToList();
        }
    }
}