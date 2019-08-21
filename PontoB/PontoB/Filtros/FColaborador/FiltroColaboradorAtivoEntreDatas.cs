using Microsoft.EntityFrameworkCore;
using PontoB.Business.Utils;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FColaborador
{

    public class FiltroColaboradorAtivoEntreDatas : IFiltro<Colaborador>
    {
        public IList<Colaborador> Filtrar(IQueryable<Colaborador> query, string filtro)
        {

            var valores = FiltroPeriodoValores.FromString(filtro);
            if (DateTime.TryParse(valores.Fim.ToString(), out DateTime dataFim))
            {
                var result = query.OrderByDescending(r => r.NomeCompleto).Where(e => (e.DataDemissao==null || e.DataDemissao >= valores.Inicio) && e.DataAdmissao < dataFim.AddDays(1)).ToList();
                return result;
            }
                return new List<Colaborador>();

        }
    }
    
}