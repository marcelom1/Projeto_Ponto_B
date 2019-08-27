using PontoB.Business.Utils;
using PontoB.DAO;
using PontoB.Models.ViewModels.VRelatorios.Gamificacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Controllers.RegrasDeNegocios.RRelatorios
{
    public class ResumoPontosGamificacao
    {
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private PontuacaoDAO dbPontuacao = new PontuacaoDAO();

        public IList<TabelaPontuacaoViewModel> ResumoGamificacao(int empresaId, DateTime dataInicio, DateTime dataFim, List<TabelaPontuacaoViewModel> model)
        {
            var valores = new FiltroPeriodoValores
            {
                Inicio = dataInicio,
                Fim = dataFim,
                Id = empresaId
            }.ToString();

            var colaboradores = dbColaborador.Filtro("EmpresaId", empresaId.ToString()).Where(x => x.DataAdmissao <= dataFim && (x.DataDemissao == null || x.DataDemissao >= dataInicio));
            var pontos = dbPontuacao.Filtro("PontuacaoEntreDataEmpresa", valores);


            foreach (var colaborador in colaboradores)
            {
                model.Add(new TabelaPontuacaoViewModel
                {
                    Colaborador = colaborador,
                    Empresa = colaborador.Empresa,
                    Pontuacao = pontos.Where(x => x.ColaboradorId.Equals(colaborador.Id)).ToList()

                });
            }

            return model.ToList();
        }
    }
}