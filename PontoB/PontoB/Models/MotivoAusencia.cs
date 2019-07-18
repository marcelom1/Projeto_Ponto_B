using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PontoB.Models
{
    public class MotivoAusencia
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Abonar { get; set; }

        public virtual ICollection<AusenciaColaboradores> AusenciaColaboradores { get; set; }
    }
}