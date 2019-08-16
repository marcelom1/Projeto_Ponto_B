using PontoB.Business.Utils;
using PontoB.Controllers.RegrasDeNegocios.RCalculo;
using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using PontoB.Models.ViewModels.VEscala;
using PontoB.Models.ViewModels.VRegistroPonto;
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

        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();
        private EscalaHorarioDAO dbEscalaHorario = new EscalaHorarioDAO();
        private OcorrenciaDiaDAO dbOcrrenciaDia = new OcorrenciaDiaDAO();
        private AusenciaDAO dbAusencia = new AusenciaDAO();
        private EmpresaDAO dbEmpresa = new EmpresaDAO();

        // GET: Relatorio
        public ActionResult CartaoPonto(DateTime dataInicio, DateTime dataFim, int colaboradorId)
        {
            var filtro = new FiltroPeriodoValores
            {
                Inicio = dataInicio,
                Fim = dataFim,
                ColaboradorId = colaboradorId
            }.ToString();
            var colaborador = dbColaborador.BuscarPorId(colaboradorId);
            var empresa = dbEmpresa.BuscarPorId(colaborador.EmpresaId);
            var ocorrencias = dbOcrrenciaDia.Filtro("OcorrenciaEntreDatas", filtro);
            var registros = dbRegistroPonto.Filtro("RegistroPontoEntreDatas", filtro);
            var ausencia = dbAusencia.Filtro("ColaboradorEntreData", filtro);
            var modelRegistroPontoCalculo = GridCalculoDiario(dataInicio, dataFim, ocorrencias, registros, ausencia);
            var calculo = new CalculoPonto(ocorrencias);
            


            var model = new CartaoPonto
            {
                DataInicio = dataInicio,
                DataFim = dataFim,
                Colaborador = colaborador,
                Empresa = empresa,
                RegistroPontoCalculo = modelRegistroPontoCalculo,
                RelatorioEscala = EscalaColaborador(colaborador.EscalaId),
                HorasExedentes = (calculo.HorasExedentes / 60).ToString("D2") + ":" + (calculo.HorasExedentes % 60).ToString("D2"),
                HorasFaltas = (calculo.HorasFaltas / 60).ToString("D2") + ":" + (calculo.HorasFaltas % 60).ToString("D2"),
                SaldoPeriodo = (calculo.Saldo / 60).ToString("D2") + ":" + ((calculo.Saldo % 60) < 0 ? ((calculo.Saldo % 60) * -1).ToString("D2") : (calculo.Saldo % 60).ToString())
            };




            return View(model);
        }


        public IList<ModalEscalaColaboradorViewModel> EscalaColaborador(int escalaId)
        {

            IList<ModalEscalaColaboradorViewModel> model = new List<ModalEscalaColaboradorViewModel>();
            var filtro = dbEscalaHorario.Lista(escalaId);


            foreach (var escala in filtro.Select(x => x.DiaSemana).Distinct())
            {
                model.Add(new ModalEscalaColaboradorViewModel
                {
                    DiaDaSemana = escala,
                    Horario = string.Join(" - ", filtro.Where(x => x.DiaSemana == escala).Select(x => x.EntradaHora.ToString("00") + ":" + x.EntradaMinuto.ToString("00") + " - " + x.SaidaHora.ToString("00") + ":" + x.SaidaMinuto.ToString("00"))),

                });
            }

            return model;

        }


        private IList<RegistroPontoCalculo> GridCalculoDiario(DateTime dataInicio, DateTime dataFim, IList<OcorrenciaDia> ocorrencias, IList<RegistroPonto> registros, IList<AusenciaColaboradores> ausencia)
        {
            var modelRegistro = new List<RegistroPontoCalculo>();
            while (dataInicio <= dataFim)
            {

                modelRegistro.Add(new RegistroPontoCalculo
                {
                    Data = dataInicio,
                    MarcacaoRep = StringPontoRegistroViewModel(registros.Where(e => e.RegistroManual == false && e.DataRegistro.Date.Equals(dataInicio.Date)).ToList()),
                    RegistroConsiderado = StringPontoRegistroViewModel(registros.Where(e => e.DesconsiderarMarcacao == false && e.DataRegistro.Date.Equals(dataInicio.Date)).ToList()),
                    AusenciaColaboradores = ausencia.Where(e => e.DataFim >= dataInicio && e.DataInicio < dataInicio.AddDays(1)).ToList(),
                    RegistroPontosModificados = registros.Where(x=>x.DataRegistro.Date.Equals(dataInicio)&&(x.RegistroManual==true || x.DesconsiderarMarcacao==true)).ToList(),
                    Saldo = new CalculoPonto(ocorrencias.Where(e=>e.Date.Date.Equals(dataInicio)).ToList()).Saldo

                });
                dataInicio = dataInicio.AddDays(1);
            }

            return modelRegistro;
        }
        public string StringPontoRegistroViewModel(IList<RegistroPonto> filtro)
        {
            return string.Join(" - ", filtro.OrderBy(x => x.DataRegistro).Select(x => x.HoraRegistro.ToString("00") + ":" + x.MinutoRegistro.ToString("00")));
            
        }


    }
}