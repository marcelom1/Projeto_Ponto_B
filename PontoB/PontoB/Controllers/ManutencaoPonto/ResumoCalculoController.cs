using PagedList;
using PontoB.Business.Utils;
using PontoB.Controllers.RegrasDeNegocios.RCalculo;
using PontoB.Controllers.RegrasDeNegocios.ROcorrenciaDia;
using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.ViewModels.VResumoCalculo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PontoB.Controllers
{
    [Authorize(Roles = "Master")]
    public class ResumoCalculoController : Controller
    {

        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private EmpresaDAO dbEmpresa = new EmpresaDAO();
        private ManutencaoPontoDAO dbManutencaoPonto = new ManutencaoPontoDAO();
        private RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();
        private AusenciaDAO dbAusencia = new AusenciaDAO();
        private EscalaDAO dbEscala = new EscalaDAO();
        private OcorrenciaDiaDAO dbOcorrenciaDia = new OcorrenciaDiaDAO();

        // GET: ResumoCalculo
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult TabelaResumoCalculo(int empresaId, DateTime dataInicio, DateTime dataFim, int pagina = 1)
        {
            var model = new List<ResumoCalculoViewModel>();
            if (dataFim > DateTime.Now.Date)
            {
                ModelState.AddModelError("erro", "O periodo de apuração não pode ser maior que a data corrente");
            }
            else
            {
                

                var valores = new FiltroPeriodoValores
                {
                    Inicio = dataInicio,
                    Fim = dataFim,
                    Id = empresaId

                }.ToString();
                var ocorrencias = dbOcorrenciaDia.Filtro("OcorrenciaDiaEntreDataPorEmpresa", valores);
                var colaboradores = dbColaborador.Filtro("EmpresaId", empresaId.ToString()).Where(x => x.DataAdmissao <= dataFim && (x.DataDemissao == null || x.DataDemissao >= dataInicio));

                foreach (var colaborador in colaboradores)
                {
                    model.Add(new ResumoCalculoViewModel
                    {
                        Colaborador = colaborador,
                        CalculoPonto = new CalculoPonto(ocorrencias.Where(x => x.ColaboradorId.Equals(colaborador.Id)).ToList()),
                    });
                }
            }

            ViewBag.EmpresaId = empresaId;
            ViewBag.DataInicio = dataInicio;
            ViewBag.DataFim = dataFim;




            return PartialView(model.ToPagedList(pagina, 10));
        }

        public string CalculoTodosPontos(int idEmpresa, DateTime dataInicial, DateTime dataFinal)
        {
            var calculo = new RegrasOcorrenciaDia();
            var colaboradores = dbColaborador.Filtro("EmpresaId", idEmpresa.ToString());

            foreach (var colaborador in colaboradores.Where(e => (e.DataDemissao == null || e.DataDemissao >= dataInicial) && e.DataAdmissao < dataFinal.AddDays(1)))
            {
                try
                {
                    calculo.CalculoPonto(colaborador.Id, dataInicial, dataFinal);
                }
                catch (Exception e)
                {

                    return e.Message;
                }
            }
           
            return "";

        }
    }
}