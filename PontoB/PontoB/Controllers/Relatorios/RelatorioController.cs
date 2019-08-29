using PontoB.Business.Utils;
using PontoB.Controllers.RegrasDeNegocios.RCalculo;
using PontoB.Controllers.RegrasDeNegocios.RRelatorios;
using PontoB.Controllers.Relatorios;
using PontoB.Controllers.Relatorios.Gamificacao;
using PontoB.Controllers.Relatorios.Manutencao;
using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using PontoB.Models.ViewModels.VEscala;
using PontoB.Models.ViewModels.VRelatorios.CartaoPonto;
using PontoB.Models.ViewModels.VRelatorios.Gamificacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;

namespace PontoB.Controllers
{
    [Authorize]
    public class RelatorioController : Controller
    {
        
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private PontuacaoDAO dbPontuacao = new PontuacaoDAO();

        // GET: Relatorio
        [Authorize(Roles = "Master")]
        public ActionResult Index(string relatorio)
        {
            ViewBag.Relatorio = relatorio;
            if (relatorio.Equals("TabelaRegistrosImpares", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.NomeRelatorio = "Registros Ímpares";
                ViewBag.Controller = "BuscaRelatorioManutencao";
            }
            else if (relatorio.Equals("TabelaResumoGamificacao", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.NomeRelatorio = "Gamificação";
                ViewBag.Controller = "BuscaRelatorioGamificacao";
            }
            return View();
        }

       


        public ActionResult BuscaRelatorioGamificacao(string relatorio, DateTime? dataInicio, DateTime? dataFim, int pagina = 1, int empresaId = 0)
        {
            if (relatorio.Equals("RelatorioResumoGamificacao", StringComparison.OrdinalIgnoreCase))
                return new GamificacaoController().RelatorioResumoGamificacao(dataInicio.Value, dataFim.Value, empresaId);
            else if (relatorio.Equals("RelatorioDetalhadoGamificacao", StringComparison.OrdinalIgnoreCase))
                return new GamificacaoController().RelatorioDetalhadoGamificacao(dataInicio.Value, dataFim.Value, empresaId);
            else if (relatorio.Equals("RelatorioDetalhadoGamificacaoUsuario", StringComparison.OrdinalIgnoreCase))
                return new GamificacaoController().RelatorioDetalhadoGamificacaoUsuario();
            else if (relatorio.Equals("TabelaResumoGamificacao", StringComparison.OrdinalIgnoreCase))
                return new GamificacaoController().TabelaResumoGamificacao(dataInicio.Value, dataFim.Value,pagina, empresaId);


            return View();
        }



        public ActionResult BuscaRelatorioManutencao(string relatorio, DateTime dataInicio, DateTime dataFim, int empresaId = 0)
        {
            if (relatorio.Equals("RelatorioRegistrosImpares", StringComparison.OrdinalIgnoreCase))
                return new RegistrosImparesController().RelatorioRegistrosImpares(dataInicio, dataFim, empresaId);
            else if (relatorio.Equals("TabelaRegistrosImpares", StringComparison.OrdinalIgnoreCase))
                return new RegistrosImparesController().TabelaRegistrosImpares(dataInicio, dataFim, empresaId);

            return View();
        }



        public ActionResult BuscaRelatorioCartaoPonto(string relatorio, DateTime dataInicio, DateTime dataFim,int colaboradorId = 0 ,int empresaId = 0)
        {
            if (relatorio.Equals("TodosCartaoPontoEmpresa", StringComparison.OrdinalIgnoreCase))
                return View();
            else if (relatorio.Equals("CartaoPonto", StringComparison.OrdinalIgnoreCase))
                return View();

            return View();
        }













        [Authorize(Roles = "Master")]
        public ActionResult CartaoPonto(DateTime dataInicio, DateTime dataFim, int colaboradorId)
        {
            IList<CartaoPonto> model = new List<CartaoPonto>();
            model.Add( new RelatorioCartaoPonto().CartaoPonto(dataInicio, dataFim, colaboradorId));

            return View(model);
        }

        [Authorize(Roles = "Master")]
        public ActionResult TodosCartaoPontoEmpresa(DateTime dataInicio, DateTime dataFim, int empresaId)
        {
            var filtro = new FiltroPeriodoValores
            {
                Inicio = dataInicio,
                Fim = dataFim,
                Id = empresaId

            }.ToString();
            var colaboradores = dbColaborador.Filtro("EmpresaId", empresaId.ToString());

            IList<CartaoPonto> model = new List<CartaoPonto>();
            foreach (var colaborador in colaboradores.Where(x => x.DataAdmissao <= dataFim && (x.DataDemissao == null || x.DataDemissao >= dataInicio)))
            {
                model.Add(new RelatorioCartaoPonto().CartaoPonto(dataInicio, dataFim, colaborador.Id));
            }
            

            return View("CartaoPonto", model);
        }

        [Authorize(Roles = "Master")]
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GraficosHome()
        {
            
            var data = DateTime.Now;
            
            var modifica = new
            {
                DataHoje = data.ToShortDateString(),
                Hoje = new Graficos().DadosGraficoDoDia(data),
                
                DataOntem = data.AddDays(-1).ToShortDateString(),
                Ontem = new Graficos().DadosGraficoDoDia(data.AddDays(-1))
            };

            return Json(modifica, JsonRequestBehavior.AllowGet);


        }


    }
}