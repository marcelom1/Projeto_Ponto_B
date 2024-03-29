﻿using PontoB.Models.RegistroPontoModels;
using PontoB.Models.ViewModels.VRegistroPonto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models.ViewModels.VCalculoPonto
{
    public class TabelaCalculoViewModels
    {
        public TabelaCalculoViewModels()
        {
            RegistroPonto = new List<RegistroPonto>();
            Registros = new List<HistoricoRegistroPontoViewModels>();
            Ausencia = new List<AusenciaColaboradores>();
        }

        public string DiaDaSemana { get; set; }
        public DateTime Data { get; set; }
        public IList<RegistroPonto> RegistroPonto { get; set; }
        public List<HistoricoRegistroPontoViewModels> Registros { get; set; }
        public IList<AusenciaColaboradores> Ausencia  { get; set; }
        public int TotalEscalaMinutos { get; set; }
      
      
        public int EscalaId { get; set; }
        public int ColaboradorId { get; set; }
        public Colaborador Colaborador { get; set; }
        public int Saldo { get; set; }
        public int HorasTrabalhadas { get; set; }
    }

    
}