using PontoB.Business.Utils;
using PontoB.Controllers.RegrasDeNegocios.RCalculo;
using PontoB.Controllers.RegrasDeNegocios.RRelatorios;
using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using PontoB.Models.ViewModels.VEscala;
using PontoB.Models.ViewModels.VRelatorios.CartaoPonto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PontoB.Controllers
{
    public class RelatorioController : Controller
    {
        ColaboradorDAO dbColaborador = new ColaboradorDAO();
        // GET: Relatorio
        public ActionResult CartaoPonto(DateTime dataInicio, DateTime dataFim, int colaboradorId)
        {
            IList<CartaoPonto> model = new List<CartaoPonto>();
            model.Add( new RelatorioCartaoPonto().CartaoPonto(dataInicio, dataFim, colaboradorId));

            return View(model);
        }

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




    }
}