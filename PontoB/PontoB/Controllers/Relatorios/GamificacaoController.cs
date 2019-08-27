using PagedList;
using PontoB.Business.Utils;
using PontoB.Controllers.RegrasDeNegocios.RRelatorios;
using PontoB.DAO;
using PontoB.Models.ViewModels.VRelatorios.Gamificacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PontoB.Controllers.Relatorios
{
    [Authorize]
    public class GamificacaoController : Controller
    {
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private PontuacaoDAO dbPontuacao = new PontuacaoDAO();
      
        [Authorize(Roles = "Master")]
        public PartialViewResult TabelaResumoGamificacao(DateTime dataInicio, DateTime dataFim, int pagina = 1, int empresaId=0)
        {
            var model = new List<TabelaPontuacaoViewModel>();
            if (dataFim > DateTime.Now.Date)
            {
                ModelState.AddModelError("erro", "O período escolhido não pode ser maior que a data corrente");
            }
            else
            {
                model.Concat( new ResumoPontosGamificacao().ResumoGamificacao(empresaId, dataInicio, dataFim, model));
            }

            ViewBag.EmpresaId = empresaId;
            ViewBag.DataInicio = dataInicio;
            ViewBag.DataFim = dataFim;




            return PartialView(model.OrderByDescending(x=>x.Pontuacao.Sum(e=>e.Ponto)).ToPagedList(pagina, 10));
        }


        [Authorize(Roles = "Master")]
        


        [Authorize(Roles = "Master")]
        public ActionResult RelatorioResumoGamificacao(DateTime dataInicio, DateTime dataFim, int empresaId = 0)
        {
            var tabelaPontuacao = new List<TabelaPontuacaoViewModel>();
            if (dataFim > DateTime.Now.Date)
            {
                ModelState.AddModelError("erro", "O período escolhido não pode ser maior que a data corrente");
            }
            else
            {
                tabelaPontuacao.Concat(new ResumoPontosGamificacao().ResumoGamificacao(empresaId, dataInicio, dataFim, tabelaPontuacao));
            }


            var model = new List<RelatorioResumoPontuacaoViewModel>
            {
                new RelatorioResumoPontuacaoViewModel
                {
                    Pontuacao = tabelaPontuacao,
                    Periodo = dataInicio.ToShortDateString() + " Até " + dataFim.ToShortDateString(),
                    Empresa = empresaId.Equals(0) ? null : tabelaPontuacao.First().Empresa

                }
            };


            return View("Relatorios/RelatorioResumoGamificacao", model);
        }

        [Authorize(Roles = "Master")]
        public ActionResult RelatorioDetalhadoGamificacao(DateTime dataInicio, DateTime dataFim, int empresaId = 0)
        {
            var tabelaPontuacao = new List<TabelaPontuacaoViewModel>();
            if (dataFim > DateTime.Now.Date)
            {
                ModelState.AddModelError("erro", "O período escolhido não pode ser maior que a data corrente");
            }
            else
            {
                tabelaPontuacao.Concat(new ResumoPontosGamificacao().ResumoGamificacao(empresaId, dataInicio, dataFim, tabelaPontuacao));
            }


            var model = new List<RelatorioResumoPontuacaoViewModel>
            {
                new RelatorioResumoPontuacaoViewModel
                {
                    Pontuacao = tabelaPontuacao,
                    Periodo = dataInicio.ToShortDateString() + " Até " + dataFim.ToShortDateString(),
                    Empresa = empresaId.Equals(0) ? null : tabelaPontuacao.First().Empresa

                }
            };



            return View("Relatorios/RelatorioDetalhadoGamificacao", model);
        }

        [Authorize]
        public ActionResult RelatorioDetalhadoGamificacaoUsuario()
        {
            var tabelaPontuacao = new List<TabelaPontuacaoViewModel>();
            var colaboradorLogado = dbColaborador.BuscarEmail(User.Identity.Name);
            var dataInicio = DateTime.Now.AddDays(-30);
            var dataFim = DateTime.Now;
            var empresaId = colaboradorLogado.EmpresaId;
            tabelaPontuacao.Concat(new ResumoPontosGamificacao().ResumoGamificacao(empresaId, dataInicio, dataFim, tabelaPontuacao));



            var model = new List<RelatorioResumoPontuacaoViewModel>
            {
                new RelatorioResumoPontuacaoViewModel
                {
                    Pontuacao = tabelaPontuacao.Where(x => x.Colaborador.Id.Equals(colaboradorLogado.Id)).ToList(),
                    Periodo = dataInicio.ToShortDateString() + " Até " + dataFim.ToShortDateString(),
                    Empresa = empresaId.Equals(0) ? null : tabelaPontuacao.First().Empresa

                }
            };



            return View("Relatorios/RelatorioDetalhadoGamificacao", model);
        }
    }
}