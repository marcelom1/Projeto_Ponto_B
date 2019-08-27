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



    }
}