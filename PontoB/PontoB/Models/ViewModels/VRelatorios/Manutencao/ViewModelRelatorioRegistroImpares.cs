using PontoB.Models.RegistroPontoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models.ViewModels.VRelatorios.Manutencao
{
    public class ViewModelRelatorioRegistroImpares
    {
        public DateTime Data { get; set; }
        public Colaborador Colaborador { get; set; }
        public List<RegistroPonto> Registro { get; set; }
    }
}