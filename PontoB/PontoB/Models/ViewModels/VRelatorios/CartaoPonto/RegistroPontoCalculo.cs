using PontoB.Models.RegistroPontoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models.ViewModels.VRelatorios.CartaoPonto
{
    public class RegistroPontoCalculo
    {
        public DateTime Data { get; set; }
        public string MarcacaoRep { get; set; }
        public string RegistroConsiderado { get; set; }
        public IList<AusenciaColaboradores> AusenciaColaboradores { get; set; }
        public IList<RegistroPonto> RegistroPontosModificados { get; set; }
        public int Saldo { get; set; }
        public OcorrenciaDia OcorrenciaDiaFalta { get; set; }
    }
}