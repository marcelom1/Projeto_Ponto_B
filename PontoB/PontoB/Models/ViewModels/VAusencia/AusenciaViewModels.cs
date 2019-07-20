using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models.ViewModels.VAusencia
{
    public class AusenciaViewModels
    {
       
        public AusenciaViewModels()
        {
            AusenciaColaboradores = new AusenciaColaboradores
            {
                Ausencia = new Ausencia(),
                Colaborador = new Colaborador(),
                DataInicio = null,
                DataFim = null,
                MotivoAusencia = new MotivoAusencia(),
                Observacao = ""
        };
           
            TodosColaboradores = false;
            TodasEmpresas = false;
            Empresa = new Empresa();
            AusenciaColaboradoresLista = null;
        }

        public AusenciaColaboradores AusenciaColaboradores { get; set; }
        public bool TodosColaboradores { get; set; }
        public bool TodasEmpresas { get; set; }
        public Empresa Empresa { get; set; }
        public virtual IList<AusenciaColaboradores> AusenciaColaboradoresLista { get; set; }

    }
}