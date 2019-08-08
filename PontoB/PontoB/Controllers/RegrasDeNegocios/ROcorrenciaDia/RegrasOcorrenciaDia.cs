using PontoB.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Controllers.RegrasDeNegocios.ROcorrenciaDia
{
    public class RegrasOcorrenciaDia
    {
        
        private AusenciaColaboradoresDAO dbAusenciaColaborador = new AusenciaColaboradoresDAO();
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();
        private EscalaHorarioDAO dbEscalaHorario = new EscalaHorarioDAO();

        public bool CalculoPonto()
        {


            return false;
        }

    }
}