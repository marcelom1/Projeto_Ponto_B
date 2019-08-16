using PontoB.Business.Utils;
using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Controllers.RegrasDeNegocios.RCalculo
{
    public class CalculoPonto
    {
        public int HorasPrevista { get; } = 0;
        public int HorasTrabalhadas { get; } = 0;
        public int AusenciaAbona { get; } = 0;
        public int AusenciaDesconta { get; } = 0;
        public int HorasExedentes { get; } = 0;
        public int HorasFaltas { get; } = 0;
        public int Saldo { get; }   = 0;

        
        private OcorrenciaDiaDAO dbOcorrenciaDia = new OcorrenciaDiaDAO();
        private static int OcorrenciaPrevistasId = 1;
        private static int OcorrenciaTrabalhadasId = 2;
        private static int OcorrenciaAusenciaAbonaId = 3;
        private static int OcorrenciaAusenciaDescontaId = 4;
        private static int OcorrenciaHorasExedentesId = 5;
        private static int OcorrenciaHorasFaltasId = 6;
        private static int OcorrenciaDiaFaltaId = 7;

        public CalculoPonto(IList<OcorrenciaDia> ocorrencias)
        {

            foreach (var ocorrenciaDia in ocorrencias)
            {
                if (ocorrenciaDia.CodigoOcorrencia.Equals(OcorrenciaPrevistasId))
                    HorasPrevista += ocorrenciaDia.QtdMinutos;
                else if (ocorrenciaDia.CodigoOcorrencia.Equals(OcorrenciaTrabalhadasId))
                    HorasTrabalhadas += ocorrenciaDia.QtdMinutos;
                else if (ocorrenciaDia.CodigoOcorrencia.Equals(OcorrenciaAusenciaAbonaId))
                    AusenciaAbona += ocorrenciaDia.QtdMinutos;
                else if (ocorrenciaDia.CodigoOcorrencia.Equals(OcorrenciaAusenciaDescontaId))
                    AusenciaDesconta += ocorrenciaDia.QtdMinutos;
                else if (ocorrenciaDia.CodigoOcorrencia.Equals(OcorrenciaHorasExedentesId))
                    HorasExedentes += ocorrenciaDia.QtdMinutos;
                else if (ocorrenciaDia.CodigoOcorrencia.Equals(OcorrenciaHorasFaltasId))
                    HorasFaltas += ocorrenciaDia.QtdMinutos;
            }

            Saldo = HorasExedentes - HorasFaltas;


        }

        
    }

    
}