using PagedList;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros
{
    public interface IFiltro<T>
    {
        IList<T> Filtrar(IQueryable<T> query,string filtro);
    }
}