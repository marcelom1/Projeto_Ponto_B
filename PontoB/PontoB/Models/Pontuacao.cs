using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models
{
    public class Pontuacao
    {
        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public Colaborador Colaborador { get; set; }
        public int Ponto { get; set; }
        public DateTime DataRegistro { get; set; }
        public int OrigemPontuacaoId { get; set; }
    }
}