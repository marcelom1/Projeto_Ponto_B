using PontoB.Business.Utils;
using PontoB.Controllers.RegrasDeNegocios.RRelatorios;
using PontoB.DAO;
using PontoB.Models.RegistroPontoModels;
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
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();
        private PontuacaoDAO dbPontuacao = new PontuacaoDAO();

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
            var model = new List<HomeColaboradorViewModel>();
            var colaboradorLogado = dbColaborador.BuscarEmail(User.Identity.Name);
            var dataInicio = DateTime.Now.AddDays(-4);
            var dataFim = DateTime.Now;

            var valores = new FiltroPeriodoValores
            {
                Inicio = dataInicio,
                Fim = dataFim,
                Id = colaboradorLogado.Id

            };
            var texto = valores.ToString();


            //Faz a Busca do filtro
            var registros = dbRegistroPonto.Filtro("RegistroPontoEntreDatas", texto);

            valores.Inicio = DateTime.Now.AddDays(-30);
            var pontos = dbPontuacao.Filtro("PontuacaoEntreDataColaborador", texto);



            while (dataInicio <= dataFim)
            {

                model.Add(new HomeColaboradorViewModel
                {
                    Data = dataInicio.Date,
                    Registros = StringRegistros(registros.Where(x => x.DataRegistro.Date.Equals(dataInicio.Date) && x.DesconsiderarMarcacao==false).ToList()),
                    Pontos = pontos.Where(x=>x.DataRegistro.Date.Equals(dataInicio.Date)).Sum(x=>x.Ponto),
                 
                    

                });

                dataInicio = dataInicio.AddDays(1);
            }
            ViewBag.TotalPontos = pontos.Sum(x => x.Ponto);
            ViewBag.DataPontos = valores.Inicio.Value.ToShortDateString() + " até " + DateTime.Now.ToShortDateString();




            return View(model.OrderByDescending(x=>x.Data).ToList());
        }

        


        public TopHomeViewModels TopHome()
        {

            var dataInicio = DateTime.Now.AddDays(-30);
            var dataFim = DateTime.Now;
            var topHoras = new TopHoras().TopFive(dataInicio, dataFim);
            var topPontuacao = new TopPontuacao().TopFive(dataInicio, dataFim);

            var model = new TopHomeViewModels
            {
                DataInicio = dataInicio.ToShortDateString(),
                DataFim = dataFim.ToShortDateString(),
                HorasFalta = topHoras.Where(x => x.HorasPontuacao < 0).Take(5).ToList(),
                HorasExcedentes = topHoras.Reverse().Where(x => x.HorasPontuacao > 0).Take(5).ToList(),
                TopPontos = topPontuacao.OrderBy(x => x.HorasPontuacao).Where(x=>x.HorasPontuacao>0).ToList()
                

            };

            return model;


        }

        public string StringRegistros(IList<RegistroPonto> filtro)
        {
            string resultado = "";

            resultado = string.Join(" - ", filtro.OrderBy(x=>x.DataRegistro).Select(x => x.HoraRegistro.ToString("00") + ":" + x.MinutoRegistro.ToString("00")));

            return resultado;

        }


    }
}