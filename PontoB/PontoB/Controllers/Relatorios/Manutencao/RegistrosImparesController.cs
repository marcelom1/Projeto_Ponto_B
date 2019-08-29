using PontoB.Controllers.RegrasDeNegocios.RRelatorios;
using PontoB.DAO;
using PontoB.Models.ViewModels.VRelatorios.Manutencao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PontoB.Controllers.Relatorios.Manutencao
{
    public class RegistrosImparesController : Controller
    {
        private EmpresaDAO dbEmpresa = new EmpresaDAO();
        // GET: RegistrosImpares
        public ActionResult TabelaRegistrosImpares(DateTime dataInicio, DateTime dataFim, int empresaId = 0)
        {
            return View("Manutencao/RegistrosImpares/TabelaRegistrosImpares");
        }


        public ActionResult RelatorioRegistrosImpares(DateTime dataInicio, DateTime dataFim, int empresaId = 0)
        {

            var model = new RegistrosImpares
            {
                Empresa = empresaId.Equals(0) ? null : dbEmpresa.BuscarPorId(empresaId),
                Periodo = dataInicio.ToShortDateString() + " Até " + dataFim.ToShortDateString(),


            };

            if (dataFim > DateTime.Now.Date)
            {
                ModelState.AddModelError("erro", "O período escolhido não pode ser maior que a data corrente");
                model.Registros = new List<ViewModelRelatorioRegistroImpares>();
            }
            else
            {
                model.Registros = new RegistrosEmNumeroImpares().RegistrosImpares(dataInicio, dataFim, empresaId);

            }

            return View("Manutencao/RegistrosImpares/RelatorioRegistrosImpares", model);
        }
    }
}