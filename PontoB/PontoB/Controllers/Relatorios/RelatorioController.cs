using PontoB.Business.Utils;
using PontoB.Controllers.RegrasDeNegocios.RCalculo;
using PontoB.Controllers.RegrasDeNegocios.RRelatorios;
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
            if (relatorio.Equals("RelatorioRegistrosImpares", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.NomeRelatorio = "Registros Ímpares";
                ViewBag.Controller = "ManutencaoPonto";
            }
            else if (relatorio.Equals("TabelaResumoGamificacao", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.NomeRelatorio = "Gamificação";
                ViewBag.Controller = "Gamificacao";
            }
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