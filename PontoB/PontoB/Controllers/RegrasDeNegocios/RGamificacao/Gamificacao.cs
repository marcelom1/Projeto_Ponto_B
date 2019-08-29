using PontoB.Business.Utils;
using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Controllers.RegrasDeNegocios.RPontuacao
{
    public class Gamificacao
    {
        private PontuacaoDAO dbPontuacao = new PontuacaoDAO();
        private EscalaHorarioDAO dbEscala = new EscalaHorarioDAO();
        private RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();


        private static int NaPrimeiraMarcacao = 1;
        private static int InclusaoRegistroManual = 2;

        
        public void VerificaSePontuouAoRegistrar(RegistroPonto registro)
        {
            var pontuacao = new Pontuacao
            {
               Colaborador = registro.Colaborador,
               ColaboradorId = registro.ColaboradorId,
               DataRegistro = registro.DataRegistro,
               Ponto = 0,
               OrigemPontuacaoId = 0
               
            };
            if (PontualidadeNaPrimeiraMarcacao(registro)){
                pontuacao.Ponto = 1;
                pontuacao.OrigemPontuacaoId = NaPrimeiraMarcacao;
                dbPontuacao.Adiciona(pontuacao);
            }
        }

        public void VerificaSePontuouAoCalcularDia(List<RegistroPonto> registro, DateTime Dia, Colaborador colaborador)
        {
            

            var periodo = new FiltroPeriodoValores
            {
                Inicio = Dia.Date,
                Fim = Dia.Date,
                Id = colaborador.Id

            };


            var pontuacao = new Pontuacao
            {
                Colaborador = colaborador,
                ColaboradorId = colaborador.Id,
                DataRegistro = Dia.Date,
                Ponto = 0,
                OrigemPontuacaoId = 0

            };

            if (SemInclusaoRegistroManual(registro, periodo))
            {
                pontuacao.Ponto = 1;
                pontuacao.OrigemPontuacaoId = InclusaoRegistroManual;
                dbPontuacao.Adiciona(pontuacao);
            }
        }

        private void ExcluirPontuacaoAnterior(FiltroPeriodoValores periodo, int codPontuacao)
        {
            
            var pontuacacao = dbPontuacao.Filtro("PontuacaoEntreDataColaborador", periodo.ToString());
            foreach (var ponto in pontuacacao.Where(x=>x.OrigemPontuacaoId.Equals(codPontuacao)))
            {
                dbPontuacao.ExcluirPontuacao(ponto);
            }
        }

        private bool PontualidadeNaPrimeiraMarcacao(RegistroPonto registro)
        {
            var filtro = dbRegistroPonto.Filtro("UltimoRegistroDia", registro.Colaborador.Id.ToString()).FirstOrDefault();
            var diaDaSemana = registro.DataRegistro.ToDiaDaSemana();
            var escala = dbEscala.Lista(registro.Colaborador.EscalaId).Where(e => e.DiaSemana.Equals(diaDaSemana)).OrderBy(x=>x.EntradaMinuto).ThenBy(x=>x.EntradaHora).FirstOrDefault() ;
            if (escala != null)
            {
                var horaEscala = new TimeSpan(escala.EntradaHora, escala.EntradaMinuto, 0);
                var horaEntrada = DateTime.MinValue + horaEscala;

                if (registro.DataRegistro.TimeOfDay >= horaEntrada.AddMinutes(-5).TimeOfDay && registro.DataRegistro.TimeOfDay <= horaEntrada.AddMinutes(5).TimeOfDay)
                    return true;
            }


            return false;
        }

        private bool SemInclusaoRegistroManual(List<RegistroPonto> RegistrosPontoDia, FiltroPeriodoValores periodo)
        {
            ExcluirPontuacaoAnterior(periodo, InclusaoRegistroManual);
            if (RegistrosPontoDia.Count > 0)
            {



                foreach (var registro in RegistrosPontoDia)
                {
                    if (registro.RegistroManual == true)
                        return false;
                }

                return true;
            }
            return false;
        }



    }
}