using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Models
{
    public class RecuperarSenha
    {
        public int Id { get; set; }
        public string Chave { get; set; }
        public int ColaboradorId { get; set; }
        public Colaborador Colaborador { get; set; }
        public DateTime Validade { get; set; }


    }
}