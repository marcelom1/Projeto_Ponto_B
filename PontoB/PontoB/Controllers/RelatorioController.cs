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

        // GET: Relatorio
        public ActionResult CartaoPonto(DateTime dataInicio, DateTime dataFim, int colaboradorId)
        {
            CartaoPonto model = new RelatorioCartaoPonto().CartaoPonto(dataInicio, dataFim, colaboradorId);

            return View(model);
        }

        


    }
}