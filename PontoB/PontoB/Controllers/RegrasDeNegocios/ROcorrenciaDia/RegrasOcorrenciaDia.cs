using PontoB.Business.Utils;
using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Controllers.RegrasDeNegocios.ROcorrenciaDia
{

    
    public class RegrasOcorrenciaDia
    {
        private static int IdOcorrenciaPrevistas        = 1;
        private static int IdOcorrenciaTrabalhadas      = 2;
        private static int IdOcorrenciaAusenciaAbona    = 3;
        private static int IdOcorrenciaAusenciaDesconta = 4;
        private static int IdOcorrenciaHorasExedentes   = 5;
        private static int IdOcorrenciaHorasFaltas      = 6;
        private static int IdOcorrenciaDiaFalta         = 7;




        private EscalaDAO dbEscala                  = new EscalaDAO();
        private AusenciaDAO dbAusencia              = new AusenciaDAO();
        private ColaboradorDAO dbColaborador        = new ColaboradorDAO();
        private RegistroPontoDAO dbRegistroPonto    = new RegistroPontoDAO();
        private EscalaHorarioDAO dbEscalaHorario    = new EscalaHorarioDAO();
        private OcorrenciaDiaDAO dbOcorenciaDia     = new OcorrenciaDiaDAO();

        public void CalculoPonto(int colaboradorId, DateTime dataInicio, DateTime dataFim)
        {
            //Busca Dados para o Calculo
            var colaborador = dbColaborador.BuscarPorId(colaboradorId);
            var escala = dbEscala.BuscarPorId(colaborador.EscalaId);

            var valores = PeriodoCalculoValidoAdmissaoDemissao(colaborador, dataInicio, dataFim);
            var texto = valores.ToString();

            var registros = dbRegistroPonto.Filtro("RegistroPontoEntreDatas", texto);
            var ausencia = dbAusencia.Filtro("ColaboradorEntreData", texto);
            var ocorrencias = dbOcorenciaDia.Filtro("OcorrenciaEntreDatas", texto);

            
           
            //Validar Registros pares
            for (DateTime data = valores.Inicio.Value.Date; data <= valores.Fim.Value.Date; data = data.AddDays(1))
            {
                var registrosDia = registros.OrderBy(x => x.DataRegistro).Where(x => x.DataRegistro.Date.Equals(data) && x.DesconsiderarMarcacao==false).ToList();

                RegistrosPares(registrosDia);
            }

            LimparApuracaoAnterior(ocorrencias);


            //Gerar Ocorrências
            for (DateTime data = valores.Inicio.Value.Date; data<= valores.Fim.Value.Date; data = data.AddDays(1))
            {

                var registrosDia = registros.OrderBy(x => x.DataRegistro).Where(x => x.DataRegistro.Date.Equals(data) && x.DesconsiderarMarcacao == false).ToList();
                GerarOcorrenciasAusencia(data, ausencia, escala);
                GerarOcorrenciasHorasPrevistas(data, escala, colaboradorId);
                GerarOcorrenciasHorasTrabalhadas(registrosDia,data,colaborador.Id);
            }


            ocorrencias = dbOcorenciaDia.Filtro("OcorrenciaEntreDatas", texto);
            for (DateTime data = valores.Inicio.Value.Date; data <= valores.Fim.Value.Date; data = data.AddDays(1))
            {
                GerarOcorrenciasHorasFaltantesOuExcedentes(ocorrencias.Where(x => x.Date.Equals(data)).ToList());
            }


        }

        private FiltroPeriodoValores PeriodoCalculoValidoAdmissaoDemissao(Colaborador colaborador, DateTime dataInicio, DateTime dataFim)
        {
           

            var inicioConsiderar = dataInicio > colaborador.DataAdmissao ? dataInicio : colaborador.DataAdmissao;
            var fimConsiderar = colaborador.DataDemissao==null || dataFim < colaborador.DataDemissao ? dataFim : colaborador.DataDemissao;

            var periodoValido = new FiltroPeriodoValores
            {
                Inicio = inicioConsiderar,
                Fim = fimConsiderar,
                ColaboradorId = colaborador.Id
            };

            return periodoValido;
        }


        private void LimparApuracaoAnterior(IList<OcorrenciaDia> ocorrenciaDias)
        {
            foreach (var ocorencia in ocorrenciaDias)
            {
                dbOcorenciaDia.ExcluirOcorrenciaDia(ocorencia);
            }
        }

        private void GerarOcorrenciasHorasFaltantesOuExcedentes(IList<OcorrenciaDia> ocorrencias)
        {
            if (ocorrencias.Count > 0)
            {
                var Trabalhadas = 0;
                var Prevista = 0;
                var AusenciaAbona = 0;
                var AusenciaDesconta = 0;


                foreach (var ocorrenciaDia in ocorrencias)
                {
                    if (ocorrenciaDia.CodigoOcorrencia.Equals(IdOcorrenciaPrevistas))
                        Prevista = ocorrenciaDia.QtdMinutos;
                    else if (ocorrenciaDia.CodigoOcorrencia.Equals(IdOcorrenciaTrabalhadas))
                        Trabalhadas = ocorrenciaDia.QtdMinutos;
                    else if (ocorrenciaDia.CodigoOcorrencia.Equals(IdOcorrenciaAusenciaAbona))
                        AusenciaAbona = ocorrenciaDia.QtdMinutos;
                    else if (ocorrenciaDia.CodigoOcorrencia.Equals(IdOcorrenciaAusenciaDesconta))
                        AusenciaDesconta = ocorrenciaDia.QtdMinutos;
                }

                var calculo = Trabalhadas - Prevista + AusenciaAbona - AusenciaDesconta;

                var temp = new OcorrenciaDia
                {
                    Date = ocorrencias[0].Date.Date,
                    ColaboradorId = ocorrencias[0].ColaboradorId
                };

                if (calculo < 0)
                {
                    temp.CodigoOcorrencia = IdOcorrenciaHorasFaltas;
                    temp.QtdMinutos = calculo * (-1);
                    dbOcorenciaDia.Adiciona(temp);
                }
                else if (calculo > 0)
                {
                    temp.CodigoOcorrencia = IdOcorrenciaHorasExedentes;
                    temp.QtdMinutos = calculo;
                    dbOcorenciaDia.Adiciona(temp);
                }
            }

        }

        

        private bool RegistrosPares(IList<RegistroPonto> registros)
        {

            if (registros.Where(x => x.DesconsiderarMarcacao == false).Count() % 2 == 0)
            {
                return true;
            }
            throw new System.ArgumentException("Falha no cálculo - Os registros devem está em quantidade pares para efetuar o cálculo no dia: "+registros[0].DataRegistro.ToShortDateString());
           
        }


        private void GerarOcorrenciasAusencia(DateTime dataCalculo, IList<AusenciaColaboradores> ausencias, Escala escala)
        {
            IList<OcorrenciaDia> Lista = new List<OcorrenciaDia>();

            var ausenciasConsideradas = ausencias.Where(a => a.DataInicio.GetValueOrDefault().Date <= dataCalculo
                                                       && a.DataFim.GetValueOrDefault() >= dataCalculo).ToList();
            var diaSemana = dataCalculo.ToDiaDaSemana();

            var escalaHorarios = escala.EscalasHorario.Where(h => h.DiaSemana == diaSemana).ToList();
            foreach (var ausencia in ausenciasConsideradas)
            {
                foreach (var horario in escalaHorarios)
                {
                    var entrada = new DateTime(dataCalculo.Year, dataCalculo.Month, dataCalculo.Day, horario.EntradaHora, horario.EntradaMinuto, 0);
                    var saida = new DateTime(dataCalculo.Year, dataCalculo.Month, dataCalculo.Day, horario.SaidaHora, horario.SaidaMinuto, 0);

                    var inicioConsiderar = entrada > ausencia.DataInicio ? entrada : ausencia.DataInicio;
                    var fimConsiderar = saida < ausencia.DataFim ? saida : ausencia.DataFim;

                    if (inicioConsiderar < fimConsiderar)
                    {
                        var horaConsiderada = fimConsiderar - inicioConsiderar;
                        var minutosConsiderados = horaConsiderada.Value.Hours * 60 + horaConsiderada.Value.Minutes;
                        var codOcorrencia=0;
                        if (ausencia.MotivoAusencia.Abonar)
                            codOcorrencia = IdOcorrenciaAusenciaAbona;
                        else
                            codOcorrencia = IdOcorrenciaAusenciaDesconta;


                        var temp = new OcorrenciaDia
                        {
                            Date = dataCalculo,
                            ColaboradorId = ausencia.ColaboradorId,
                            CodigoOcorrencia = codOcorrencia,
                            QtdMinutos = minutosConsiderados
                        };
                        Lista.Add(temp);
                    }

                }
            }

           

            foreach (var codOcorrencia in Lista.Select(x=>x.CodigoOcorrencia).Distinct())
            {
                var ocorrencia = new OcorrenciaDia
                {
                    Date = dataCalculo,
                    ColaboradorId = Lista[0].ColaboradorId,
                    CodigoOcorrencia = codOcorrencia,
                    QtdMinutos = Lista.Where(x => x.CodigoOcorrencia.Equals(codOcorrencia)).Select(x=>x.QtdMinutos).Sum()
                };

                dbOcorenciaDia.Adiciona(ocorrencia);
            }

        }

       

        private void GerarOcorrenciasHorasPrevistas(DateTime dataCalculo, Escala escala, int colaboradorId)
        {

            IList<EscalaHorario> EscalaTotalDiaSemana = GetHorasDiaDaSemana(escala);
            var diaSemana = dataCalculo.ToDiaDaSemana();
            foreach (var horas in EscalaTotalDiaSemana)
            {
                if (diaSemana == horas.DiaSemana)
                {
                    var ocorencia = new OcorrenciaDia
                    {
                        Date = dataCalculo,
                        ColaboradorId = colaboradorId,
                        CodigoOcorrencia = IdOcorrenciaPrevistas,
                        QtdMinutos = horas.TotalEmMinutos
                    };
                    dbOcorenciaDia.Adiciona(ocorencia);

                }

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

        private void GerarOcorrenciasHorasTrabalhadas(IList<RegistroPonto> registros, DateTime data, int colaboradorId)
        {
            var ocorencia = new OcorrenciaDia();
            if (registros.Count > 0)
            {
                var totalHorasTrabalhadasEmMinuto = 0;
               
                for (int i = 0; i < registros.Count; i += 2)
                {
                    var horas = registros[i + 1].DataRegistro.TimeOfDay - registros[i].DataRegistro.TimeOfDay;
                    totalHorasTrabalhadasEmMinuto += horas.Hours * 60 + horas.Minutes;


                }
                ocorencia = new OcorrenciaDia
                {
                    Date = data.Date,
                    ColaboradorId = colaboradorId,
                    CodigoOcorrencia = IdOcorrenciaTrabalhadas,
                    QtdMinutos = totalHorasTrabalhadasEmMinuto
                };
               

            }
            else
            {
                ocorencia = new OcorrenciaDia
                {
                    Date = data.Date,
                    ColaboradorId = colaboradorId,
                    CodigoOcorrencia = IdOcorrenciaDiaFalta,
                    QtdMinutos = 0
                };
            }

            dbOcorenciaDia.Adiciona(ocorencia);
        }
        




    }
}