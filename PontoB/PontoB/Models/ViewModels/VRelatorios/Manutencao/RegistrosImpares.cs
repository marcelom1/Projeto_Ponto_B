using PontoB.Models.RegistroPontoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models.ViewModels.VRelatorios.Manutencao
{
    public class RegistrosImpares
    {
       
        public Empresa Empresa { get; set; }
        public string Periodo { get; set; }
        public List<ViewModelRelatorioRegistroImpares> Registros { get; set; }
    }
}