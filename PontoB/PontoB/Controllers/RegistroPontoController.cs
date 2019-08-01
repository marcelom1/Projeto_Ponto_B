using PagedList;
using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using PontoB.Models.ViewModels.VRegistroPonto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PontoB.Business.Utils;

namespace PontoB.Controllers
{
    [Authorize]
    public class RegistroPontoController : Controller
    {
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();
       
        public ActionResult Index()
        {
            
            var model = new RegistroPonto();
            model.Colaborador = dbColaborador.BuscarEmail(User.Identity.Name);
            ViewBag.UltimoRegistro = UltimoRegistro();
            return View(model);
        }

        public DateTime DataHoraAtual()
        {
            return DateTime.Now;
        }

        public string UltimoRegistro()
        {
            var model = new RegistroPonto();
            model.Colaborador = dbColaborador.BuscarEmail(User.Identity.Name);
            var filtro = dbRegistroPonto.Filtro("UltimoRegistroDia", model.Colaborador.Id.ToString()).FirstOrDefault();
            if (filtro == null)
                return "Sem Registro no dia!";
            else
                return filtro.DataRegistro.ToString("HH:mm");

        }
        public string Registro()
        {
            var buscaColaborador = dbColaborador.BuscarEmail(User.Identity.Name);
            if (buscaColaborador.DataDemissao == null || buscaColaborador.DataDemissao >= DateTime.Now)
            {

                RegistroPonto registro = new RegistroPonto();
                registro.Colaborador = dbColaborador.BuscarEmail(User.Identity.Name);
                var filtro = dbRegistroPonto.Filtro("UltimoRegistroDia", registro.Colaborador.Id.ToString()).FirstOrDefault();
               
                if (filtro==null || filtro.DataRegistro.ToShortTimeString() != DateTime.Now.ToShortTimeString())
                {
                    registro.ColaboradorId = registro.Colaborador.Id;
                    registro.DataRegistro = DateTime.Now;
                    registro.HoraRegistro = registro.DataRegistro.Hour;
                    registro.MinutoRegistro = registro.DataRegistro.Minute;
                    dbRegistroPonto.Adiciona(registro);
                    return "True";
                }
                else
                {
                    return "Marcação duplicada - Registro ignorado";
                }
            }
            return "Erro - Data de registro superior a data de demissão";
        }

        public ActionResult Historico(DateTime? dataInicio, DateTime? dataFim, int pagina = 1)
        {

            //Aplica paginação no Filtro
            if (dataInicio != null && dataFim!= null)
            {
                return RedirectToAction("Filtro", new {dataInicio, dataFim, pagina });

            }
            //Caso não tenha Filtro, set VieBag vazia 
            ViewBag.DataInicio = "";
            ViewBag.DataFim = "";

            //retorna o colaborador logado
            var colaboradorLogado = dbColaborador.BuscarEmail(User.Identity.Name);

            IList<RegistroPonto> filtro = dbRegistroPonto.Filtro("Colaborador", colaboradorLogado.Id.ToString());

            var Lista = ListaPontoRegistroViewModel(filtro);
            var model = new HistoricoRegistroPontoComFiltro
            {
                HistoricoRegistroPonto = Lista.ToPagedList(pagina, 10),
                FiltroDataInicio = dataInicio,
                FiltroDataFim = dataFim
            };


            return View(model);
           
        }

        public ActionResult Filtro(DateTime? dataInicio, DateTime? dataFim, int pagina = 1)
        {
            var colaboradorLogado = dbColaborador.BuscarEmail(User.Identity.Name);

            var valores = new FiltroPeriodoValores
            {
                Inicio = dataInicio,
                Fim = dataFim,
                ColaboradorId = colaboradorLogado.Id

            };
            var texto = valores.ToString();
                        

            //Faz a Busca do filtro
            IList<RegistroPonto> filtro = dbRegistroPonto.Filtro("RegistroPontoEntreDatas",texto);

            var Lista = ListaPontoRegistroViewModel(filtro);
            var model = new HistoricoRegistroPontoComFiltro
            {
                HistoricoRegistroPonto = Lista.ToPagedList(pagina, 10),
                FiltroDataInicio = dataInicio,
                FiltroDataFim = dataFim
            };

            //Preenche as ViewBag com os resultado do filtro
            return View("Historico", model);
        }

        public List<HistoricoRegistroPontoViewModels> ListaPontoRegistroViewModel(IList<RegistroPonto> filtro)
        {
            var colaboradorLogado = dbColaborador.BuscarEmail(User.Identity.Name);
            List<HistoricoRegistroPontoViewModels> model = new List<HistoricoRegistroPontoViewModels>();

            foreach (var registro in filtro.OrderByDescending(d => d.DataRegistro).Select(x => x.DataRegistro.Date).Distinct())
            {
                model.Add(new HistoricoRegistroPontoViewModels
                {
                    Data = registro.Date.ToShortDateString(),
                    Registros = string.Join(" - ", filtro.OrderBy(x => x.DataRegistro).Where(x => x.DataRegistro.Date == registro.Date).Select(x => x.HoraRegistro.ToString("00") + ":" + x.MinutoRegistro.ToString("00"))),
                    colaborador = colaboradorLogado
                });
            }

            return model;

        }
    }
}