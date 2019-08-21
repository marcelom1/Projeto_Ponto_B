using PontoB.Business.Utils;
using PontoB.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Controllers.RegrasDeNegocios.RRelatorios
{
    public class Graficos
    {
        private RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private AusenciaDAO dbAusencia = new AusenciaDAO();
        public int[] DadosGraficoDoDia(DateTime dia)
        {
            int[] dadosGrafico = new int[3];
            var valores = new FiltroPeriodoValores
            {
                Inicio = dia.Date,
                Fim = dia.Date,
                Id =0


            }.ToString();

            var registros = dbRegistroPonto.Filtro("FiltroRegistroPontoTodosRegistros", valores).Select(x=>x.ColaboradorId).Distinct().ToList();
            var colaboradores = dbColaborador.Filtro("FiltroColaboradorAtivoEntreDatas", valores).Select(x => x.Id).Distinct().ToList();
            
            var ferias = dbAusencia.Filtro("FiltroAusenciaMotivoEntreData",valores).Select(x=>x.ColaboradorId).Distinct().ToList();

            dadosGrafico[0] = registros.Count();
            dadosGrafico[1] = colaboradores.Count() - registros.Count() - ferias.Count();
            dadosGrafico[2] = ferias.Count();

            return dadosGrafico;
        }
    }
}