using PontoB.Business.Utils;
using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PontoB.Test.Models
{
    public class AusenciaTeste
    {
        private static AusenciaDAO dbAusencia = new AusenciaDAO();
        private static AusenciaColaboradoresDAO dbAusenciaColaboradores = new AusenciaColaboradoresDAO();

        public AusenciaColaboradores CriarAusencia(FiltroPeriodoValores dataAusencia, int MotivoAusenciaId)
        {
            
            var ausenciaColaborador = new AusenciaColaboradores
            {
                AusenciaId = 0,
                MotivoAusenciaId = MotivoAusenciaId,
                DataInicio = dataAusencia.Inicio,
                DataFim = dataAusencia.Fim,
                HoraInicio = dataAusencia.Inicio.Value.Hour,
                HoraFim = dataAusencia.Fim.Value.Hour,
                MinutoInicio = dataAusencia.Inicio.Value.Minute,
                MinutoFim = dataAusencia.Fim.Value.Minute,
                Observacao = "Ausencia Teste"
            };
            

            return ausenciaColaborador;
        }
    }
}
