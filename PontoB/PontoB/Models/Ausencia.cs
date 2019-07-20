using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PontoB.Models
{
    public class Ausencia
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual IList<AusenciaColaboradores> AusenciaColaboradores { get; set; }

    }
}