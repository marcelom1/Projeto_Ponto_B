using PontoB.Controllers.RegrasDeNegocios.RCalculo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models.ViewModels.VResumoCalculo
{
    public class ResumoCalculoViewModel
    {
        public Colaborador Colaborador { get; set; }
       
        public CalculoPonto CalculoPonto { get; set; }
    }
}