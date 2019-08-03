﻿using PontoB.Business.Utils;
using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using PontoB.Models.ViewModels.VCalculoPonto;
using PontoB.Models.ViewModels.VRegistroPonto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;

namespace PontoB.Controllers
{
    public class CalculoPontoController : Controller
    {
        private ColaboradorDAO dbColaborador        = new ColaboradorDAO();
        private EmpresaDAO dbEmpresa                = new EmpresaDAO();
        private CalculoPontoDAO dbCalculoPonto      = new CalculoPontoDAO();
        private RegistroPontoDAO dbRegistroPonto    = new RegistroPontoDAO();
        private AusenciaDAO dbAusencia              = new AusenciaDAO();
        private EscalaDAO dbEscala                  = new EscalaDAO();

        // GET: CalculoPonto
        public ActionResult Index()
        {

            return View();
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetColaboradores(string searchTerm, int idEmpresa, DateTime dataInicio, DateTime dataFim)
        {
            var empresa = dbColaborador.Filtro("Nome Completo", searchTerm);


            var modifica = empresa.Where(e=>e.EmpresaId==idEmpresa && (e.DataDemissao == null || e.DataDemissao>= dataInicio) && (e.DataAdmissao<= dataFim)).Select(x => new
            {
                id = x.Id,
                text = x.NomeCompleto
            });

            return Json(modifica, JsonRequestBehavior.AllowGet);


        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetEmpresas(string searchTerm)
        {
            var empresa = dbEmpresa.Filtro("Razão Social", searchTerm);

            var modifica = empresa.Select(x => new
            {
                id = x.Id,
                text = x.RazaoSocial
            });

            return Json(modifica, JsonRequestBehavior.AllowGet);


        }

        public ActionResult TabelaCalculo(int idColaborador, DateTime dataInicio, DateTime dataFim)
        {
            IList<TabelaCalculoViewModels> model = new List<TabelaCalculoViewModels>();


            var valores = new FiltroPeriodoValores
            {
                Inicio = dataInicio,
                Fim = dataFim,
                ColaboradorId = idColaborador

            };
            var texto = valores.ToString();


            //Faz a Busca do filtro
            var Colaborador = dbColaborador.BuscarPorId(idColaborador);
            var RegistrosPontos = dbRegistroPonto.Filtro("RegistroPontoEntreDatas", texto);
            var HistoricoDeRegistro = ListaPontoRegistroViewModel(RegistrosPontos);
            var AusenciaColaborador = dbAusencia.Filtro("ColaboradorEntreData", texto);
            var EscalaColaborador = dbEscala.BuscarPorId(Colaborador.EscalaId);
            IList<EscalaHorario> EscalaTotalDiaSemana = GetHorasDiaDaSemana(EscalaColaborador);

            while (dataInicio <= dataFim)
            {
                var diasemana = GetDiaDaSemana(dataInicio);

                model.Add(new TabelaCalculoViewModels
                {
                    DiaDaSemana = diasemana,
                    Data = dataInicio


                });
                dataInicio = dataInicio.AddDays(1);
            }

            foreach (var item in model)
            {
                foreach (var registro in RegistrosPontos.Where(x => x.DataRegistro.Date == item.Data.Date))
                {
                    item.RegistroPonto.Add(registro);
                }

                foreach (var historico in HistoricoDeRegistro)
                {
                    if (item.Data.Date == DateTime.Parse(historico.Data))
                    {
                        item.Registros.Add(historico);
                        break;
                    }

                }

                foreach (var ausencia in AusenciaColaborador)
                {
                    if (item.Data.Date >= ausencia.DataInicio && item.Data.Date <= ausencia.DataFim)
                        item.Ausencia.Add(ausencia);
                }

                foreach (var escala in EscalaTotalDiaSemana.Where(x=>x.DiaSemana==item.DiaDaSemana))
                {
                    item.TotalEscalaMinutos = escala.TotalEmMinutos;
                    break;
                }
            }

            return PartialView(model);
        }

        public int AdicionarGrupoAusencia(string data)
        {
            var ausencia = new Ausencia{
                Descricao = "Lançamento Manual - "+data
            };

            try
            {
                dbAusencia.Adiciona(ausencia);
            }
            catch (Exception e)
            {

                throw e;
            }

            return ausencia.Id;
        }


        private static string GetDiaDaSemana(DateTime dataInicio)
        {
            switch ((int)dataInicio.DayOfWeek)
            {
                case 0:
                    return "Domingo";
                case 1:
                    return "Segunda";
                case 2:
                    return "Terça";
                case 3:
                    return "Quarta";
                case 4:
                    return "Quinta";
                case 5:
                    return "Sexta";
                case 6:
                    return "Sábado";
                default:
                    return "";

            }
        }

        private static IList<EscalaHorario> GetHorasDiaDaSemana(Escala EscalaColaborador)
        {



            IList<EscalaHorario> EscalaTotalDiaSemana = new List<EscalaHorario>();
            foreach (var diaSemana in EscalaColaborador.EscalasHorario.Select(x => x.DiaSemana).Distinct())
            {
                EscalaTotalDiaSemana.Add(new EscalaHorario
                {
                    DiaSemana = diaSemana.ToString(),
                    TotalEmMinutos = EscalaColaborador.EscalasHorario.Where(x => x.DiaSemana == diaSemana).Select(x => x.TotalEmMinutos).Sum()
                });

            }

            return EscalaTotalDiaSemana;
        }

        public ActionResult ModalTabelaCalculo(int? idColaborador, DateTime? dataInicial, DateTime? dataFim)
        {

            return PartialView();
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