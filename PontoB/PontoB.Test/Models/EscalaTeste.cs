using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PontoB.Test.Cadastro
{
    public class EscalaTeste
    {
        private static EscalaDAO dbEscala = new EscalaDAO();

        public IList<EscalaHorario> EscalaPadraoSegundaSextaGeral()
        {

            var escalas = new List<EscalaHorario>();
            var diaSemana = new string[] { "Segunda", "Terça", "Quarta", "Quinta", "Sexta" };

            for (int i = 0; i < diaSemana.Count(); i++)
            {
                var escalaHorario = new EscalaHorario
                {
                    DiaSemana = diaSemana[i],
                    EntradaHora = 8,
                    EntradaMinuto = 0,
                    EscalaId = 0,
                    SaidaHora = 12,
                    SaidaMinuto = 0,
                    TotalEmMinutos = (12 - 8) * 60 + 0 + 0
                };
                escalas.Add(escalaHorario);

                var escalaHorario2 = new EscalaHorario
                {
                    DiaSemana = diaSemana[i],
                    EntradaHora = 13,
                    EntradaMinuto = 0,
                    EscalaId = 0,
                    SaidaHora = 17,
                    SaidaMinuto = 30,
                    TotalEmMinutos = (17 - 13) * 60 + 0 + 30
                };


                escalas.Add(escalaHorario2);

            }

            return escalas;
        }

    }
}
