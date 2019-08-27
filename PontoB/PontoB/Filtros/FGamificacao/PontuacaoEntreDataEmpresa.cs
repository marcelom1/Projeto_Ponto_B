﻿using Microsoft.EntityFrameworkCore;
using PontoB.Business.Utils;
using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FGamificacao
{
    public class PontuacaoEntreDataEmpresa : IFiltro<Pontuacao>
    {
        private PontuacaoDAO dbPontuacao = new PontuacaoDAO();

        public IList<Pontuacao> Filtrar(IQueryable<Pontuacao> query, string filtro)
        {

            var valores = FiltroPeriodoValores.FromString(filtro);
            if (DateTime.TryParse(valores.Fim.ToString(), out DateTime dataFim))
                if(valores.Id!=0)
                return query.Include(c => c.Colaborador).OrderByDescending(r => r.DataRegistro).Where(e => e.Colaborador.EmpresaId.Equals(valores.Id) && e.DataRegistro >= valores.Inicio.Value.Date && e.DataRegistro < dataFim.Date.AddDays(1)).ToList();

            return query.Include(c => c.Colaborador).OrderByDescending(r => r.DataRegistro).Where(e=>e.DataRegistro >= valores.Inicio.Value.Date && e.DataRegistro < dataFim.Date.AddDays(1)).ToList();
        }

    }
}