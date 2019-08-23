using PontoB.Business.Utils;
using PontoB.Controllers.RegrasDeNegocios.RCalculo;
using PontoB.DAO;
using PontoB.Models.ViewModels.VRelatorios.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Controllers.RegrasDeNegocios.RRelatorios
{
    public class TopPontuacao
    {
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private OcorrenciaDiaDAO dbOcrrenciaDia = new OcorrenciaDiaDAO();
        private PontuacaoDAO dbPontuacao = new PontuacaoDAO();


        public IList<RelatorioTopHorasViewModels> TopFive(DateTime DataInicio, DateTime DataFim)
        {
            var valores = new FiltroPeriodoValores
            {
                Inicio = DataInicio,
                Fim = DataFim,
                Id = 0
            };
            var pontuacoes = dbPontuacao.Filtro("PontuacaoEntreData", valores.ToString());
            var colaboradores = dbColaborador.Lista().Where(x => x.DataAdmissao <= valores.Fim && (x.DataDemissao == null || x.DataDemissao >= valores.Inicio));

            var model = new List<RelatorioTopHorasViewModels>();

            foreach (var colaborador in colaboradores)
            {
                model.Add(new RelatorioTopHorasViewModels
                {

                    NomeColaborador = colaborador.NomeCompleto,
                    HorasPontuacao = pontuacoes.Select(x => new { x.ColaboradorId, x.Ponto })
                                               .Where(x => x.ColaboradorId.Equals(colaborador.Id))
                                               .Sum(x => x.Ponto)
                });
            }





            return model.OrderBy(x => x.HorasPontuacao).ToList();
        }
    }
}