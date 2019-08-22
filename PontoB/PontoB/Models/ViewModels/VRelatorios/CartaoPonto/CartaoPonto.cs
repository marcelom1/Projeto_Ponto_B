using PontoB.Controllers.RegrasDeNegocios.RCalculo;
using PontoB.Models.RegistroPontoModels;
using PontoB.Models.ViewModels.VEscala;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models.ViewModels.VRelatorios.CartaoPonto
{
    public class CartaoPonto
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public Empresa Empresa { get; set; }
        public Colaborador Colaborador { get; set; }
        public IList<ModalEscalaColaboradorViewModel> RelatorioEscala { get; set; }
        public IList<RegistroPontoCalculo> RegistroPontoCalculo { get; set; }

        public CalculoPonto CalculoPonto { get; set; }

       
        public string SaldoPeriodo { get; set; }
        

    }
}