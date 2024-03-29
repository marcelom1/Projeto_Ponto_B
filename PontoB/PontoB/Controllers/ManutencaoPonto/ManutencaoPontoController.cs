﻿using PontoB.Business.Utils;
using PontoB.Controllers.RegrasDeNegocios.ROcorrenciaDia;
using PontoB.Controllers.RegrasDeNegocios.RRelatorios;
using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using PontoB.Models.ViewModels.VCalculoPonto;
using PontoB.Models.ViewModels.VManutencaoPonto;
using PontoB.Models.ViewModels.VRegistroPonto;
using PontoB.Models.ViewModels.VRelatorios.Manutencao;
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
    [Authorize(Roles = "Master")]
    public class ManutencaoPontoController : Controller
    {
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private EmpresaDAO dbEmpresa = new EmpresaDAO();
       
        private RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();
        private AusenciaDAO dbAusencia = new AusenciaDAO();
        private EscalaDAO dbEscala = new EscalaDAO();
        private OcorrenciaDiaDAO dbOcorrenciaDia = new OcorrenciaDiaDAO();


        // GET: ManutencaoPonto
        public ActionResult Index()
        {

            return View();
        }



        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetColaboradores(string searchTerm, int idEmpresa, DateTime dataInicio, DateTime dataFim)
        {
            var colaborador = dbColaborador.Filtro("Nome Completo", searchTerm);


            var modifica = colaborador.Where(e => e.EmpresaId == idEmpresa && (e.DataDemissao == null || e.DataDemissao >= dataInicio) && (e.DataAdmissao <= dataFim)).Select(x => new
            {
                id = x.Id,
                text = x.NomeCompleto,
                data = x.EmpresaId
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

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult ColaboradoresPagincao(string empresaId, DateTime dataInicio, DateTime dataFim, int indice = 0)
        {
            var colaborador = dbColaborador.Filtro("EmpresaId", empresaId);

            var lista = colaborador.Where(e => e.EmpresaId == Convert.ToInt32(empresaId) && (e.DataDemissao == null || e.DataDemissao >= dataInicio) && (e.DataAdmissao <= dataFim));

            var modifica = lista.Select(x => new
            {
                id = x.Id,
                Nome = x.NomeCompleto,
                qtd = lista.Count()

            }).OrderBy(x => x.Nome).ElementAt(indice);



            return Json(modifica, JsonRequestBehavior.AllowGet);


        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult BuscaIndiceColaborador(string empresaId, DateTime dataInicio, DateTime dataFim, int colaboradorId)
        {
            var colaborador = dbColaborador.Filtro("EmpresaId", empresaId);

            var lista = colaborador.Where(e => e.EmpresaId == Convert.ToInt32(empresaId) && (e.DataDemissao == null || e.DataDemissao >= dataInicio) && (e.DataAdmissao <= dataFim));

            // var colaboradorSelecionado = dbColaborador.BuscarPorId(colaboradorId);

            var indice = lista.OrderBy(x => x.NomeCompleto).ToList().FindLastIndex(x => x.Id.Equals(colaboradorId));

            var qtdLista = lista.Count();

            return Json(new { indice, qtdLista }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TabelaCalculo(int idColaborador, DateTime dataInicio, DateTime dataFim)
        {
            IList<TabelaCalculoViewModels> model = new List<TabelaCalculoViewModels>();
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
                    Id = idColaborador

                };
                var texto = valores.ToString();


                //Faz a Busca do filtro
                var Colaborador = dbColaborador.BuscarPorId(idColaborador);
                var RegistrosPontos = dbRegistroPonto.Filtro("RegistroPontoEntreDatas", texto);
                var HistoricoDeRegistro = ListaPontoRegistroViewModel(RegistrosPontos);
                var AusenciaColaborador = dbAusencia.Filtro("ColaboradorEntreData", texto);
                var EscalaColaborador = dbEscala.BuscarPorId(Colaborador.EscalaId);
                IList<EscalaHorario> EscalaTotalDiaSemana = GetHorasDiaDaSemana(EscalaColaborador);
                var Ocorrencias = dbOcorrenciaDia.Filtro("OcorrenciaEntreDatas", texto);

                while (dataInicio <= dataFim)
                {
                    var diasemana = dataInicio.ToDiaDaSemana();

                    model.Add(new TabelaCalculoViewModels
                    {
                        DiaDaSemana = diasemana,
                        Data = dataInicio,
                        ColaboradorId = Colaborador.Id,
                        EscalaId = EscalaColaborador.Id,
                        Colaborador = Colaborador




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

                    item.Ausencia = AusenciaColaborador.Where(e => e.DataFim >= item.Data && e.DataInicio < item.Data.AddDays(1)).ToList();

                    foreach (var escala in EscalaTotalDiaSemana.Where(x => x.DiaSemana == item.DiaDaSemana))
                    {
                        item.TotalEscalaMinutos = escala.TotalEmMinutos;
                        break;
                    }

                    item.Saldo = 0;
                    item.HorasTrabalhadas = 0;
                    foreach (var horas in Ocorrencias.Where(x => (x.Date.Date == item.Data.Date) && (x.CodigoOcorrencia.Equals(5) || x.CodigoOcorrencia.Equals(6) || x.CodigoOcorrencia.Equals(2))))
                    {
                        if (horas.CodigoOcorrencia == 2)
                            item.HorasTrabalhadas = horas.QtdMinutos;
                        else
                            if (horas.QtdMinutos > 0)
                        {
                            if (horas.CodigoOcorrencia.Equals(6))
                                item.Saldo = horas.QtdMinutos * (-1);
                            else
                                item.Saldo = horas.QtdMinutos;
                        }
                    }
                }
            }
            return PartialView(model);
        }

        public int AdicionarGrupoAusencia(string data)
        {
            var ausencia = new Ausencia
            {
                Descricao = "Lançamento Manual - " + data
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



        public ActionResult ModalTabelaCalculo(int idColaborador, int idEscala, DateTime data)
        {
            var escala = dbEscala.BuscarPorId(idEscala).EscalasHorario.OrderBy(e => e.EntradaMinuto).ThenBy(e => e.EntradaHora).Where(e => e.DiaSemana.Equals(data.ToDiaDaSemana()));

            var model = new ManutencaoPontoViewModel
            {
                EscalaHorario = StringEscalaViewModel(escala),
                DiaDaSemana = data.ToDiaDaSemana(),
                Dia = data,
                colaboradorId = idColaborador

            };



            return PartialView(model);
        }

        public ActionResult TabelaManutencao(int idColaborador, DateTime data)
        {

            var valores = new FiltroPeriodoValores
            {
                Inicio = data,
                Fim = data,
                Id = idColaborador

            };
            var texto = valores.ToString();
            //Faz a Busca do filtro
            var filtro = dbRegistroPonto.Filtro("RegistroPontoEntreDatas", texto).OrderBy(e => e.DataRegistro);

            var model = new ManutencaoPontoViewModel
            {
                Registros = filtro
            };

            return PartialView(model);
        }

        public ActionResult AdicionarRegistroManualmente(int idColaborador, int idEscala, DateTime data)
        {
            var escala = dbEscala.BuscarPorId(idEscala).EscalasHorario.OrderBy(e => e.EntradaMinuto).ThenBy(e => e.EntradaHora).Where(e => e.DiaSemana.Equals(data.ToDiaDaSemana()));

            var model = new ManutencaoPontoViewModel
            {
                EscalaHorario = StringEscalaViewModel(escala),
                DiaDaSemana = data.ToDiaDaSemana(),
                Dia = data,
                colaboradorId = idColaborador

            };

            return PartialView(model);
        }

        public string DesconsiderarMarcacao(IList<string> desconsidera, IList<string> observacao, IList<int> registroId)
        {
            if (registroId != null)
            {
                foreach (var id in registroId)
                {

                    var registro = dbRegistroPonto.BuscarPorId(id);
                    if (!registro.RegistroManual)
                    {
                        registro.DesconsiderarMarcacao = false;
                        registro.Observacao = null;
                        dbRegistroPonto.Atualiza(registro);
                    }
                }
            }
                if (desconsidera != null)
                {
                    for (int i = 0; i < desconsidera.Count(); i++)
                    {
                        var registro = dbRegistroPonto.BuscarPorId(Convert.ToInt32(desconsidera[i]));
                        registro.DesconsiderarMarcacao = true;
                        registro.Observacao = observacao[i];
                        dbRegistroPonto.Atualiza(registro);
                    }
                }

            return "True";

        }

        public bool RemoverRegistroManual(int idRegistro)
        {
            var registro = dbRegistroPonto.BuscarPorId(idRegistro);
            if (dbRegistroPonto.ExcluirRegistroPonto(registro))
                return true;

            return false;
        }

        public bool AdicionarRegistroManutencao(DateTime data, DateTime hora, int colaboradorId, string motivo)
        {

            data = data.AddHours(hora.Hour).AddMinutes(hora.Minute);
            var registro = new RegistroPonto
            {
                ColaboradorId = colaboradorId,
                DataRegistro = data,
                HoraRegistro = hora.Hour,
                MinutoRegistro = hora.Minute,
                Observacao = motivo,
                DesconsiderarMarcacao = false,
                RegistroManual = true


            };
            dbRegistroPonto.Adiciona(registro);

            return false;
        }


        public List<HistoricoRegistroPontoViewModels> ListaPontoRegistroViewModel(IList<RegistroPonto> filtro)
        {

            List<HistoricoRegistroPontoViewModels> model = new List<HistoricoRegistroPontoViewModels>();

            foreach (var registro in filtro.OrderByDescending(d => d.DataRegistro).Select(x => x.DataRegistro.Date).Distinct())
            {
                model.Add(new HistoricoRegistroPontoViewModels
                {
                    Data = registro.Date.ToShortDateString(),
                    Registros = string.Join(" - ", filtro.OrderBy(x => x.DataRegistro).Where(x => x.DataRegistro.Date == registro.Date && x.DesconsiderarMarcacao == false).Select(x => x.HoraRegistro.ToString("00") + ":" + x.MinutoRegistro.ToString("00"))),
                    colaborador = filtro[0].Colaborador
                });
            }

            return model;

        }

        public string StringEscalaViewModel(IEnumerable<EscalaHorario> filtro)
        {
            string resultado = "";

            resultado = string.Join(" - ", filtro.Select(x => x.EntradaHora.ToString("00") + ":" + x.EntradaMinuto.ToString("00") + " - " + x.SaidaHora.ToString("00") + x.SaidaMinuto.ToString("00")));

            return resultado;

        }

        public string CalculoPonto(int idColaborador, DateTime dataInicial, DateTime dataFinal)
        {
            var calculo = new RegrasOcorrenciaDia();



            try
            {
                calculo.CalculoPonto(idColaborador, dataInicial, dataFinal);
            }
            catch (Exception e)
            {

                return e.Message;
            }
            return "";

        }


        
    }
}