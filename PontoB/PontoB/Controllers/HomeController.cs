using PontoB.Controllers.RegrasDeNegocios.RRelatorios;
using PontoB.Models.ViewModels.VRelatorios.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;

namespace PontoB.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Master")]
        // GET: Home
        public ActionResult Index()
        {
          var model = TopHome();
            

            return View(model);
        }

        [Authorize]
        public ActionResult IndexColaborador()
        {
            


            return View();
        }

        public TopHomeViewModels TopHome()
        {

            var dataInicio = DateTime.Now.AddDays(-30);
            var dataFim = DateTime.Now;
            var topHoras = new TopHoras().TopFive(dataInicio, dataFim);

            var model = new TopHomeViewModels
            {
                DataInicio = dataInicio.ToShortDateString(),
                DataFim = dataFim.ToShortDateString(),
                HorasFalta = topHoras.Where(x=>x.HorasPontuacao<0).Take(5).ToList(),
                HorasExcedentes = topHoras.Reverse().Where(x=>x.HorasPontuacao>0).Take(5).ToList()

            };

            return model;


        }
    }
}