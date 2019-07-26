using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PontoB.Business.Utils;
using PontoB.Models;

namespace PontoB.Business.Utils
{
    public class FiltroPeriodoValores
    {
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public int ColaboradorId { get; set; }

        public override string ToString()
        {
            return Inicio.ToFilterString() + "|" + Fim.ToFilterString() + "|" + ColaboradorId;
        }

        public static FiltroPeriodoValores FromString(string s)
        {
            var paramentros = s.Split('|');
            int.TryParse(paramentros[2], out int id);
            var resultado = new FiltroPeriodoValores
            {
                Inicio = paramentros[0].ToDateTimeNullable(),
                Fim = paramentros[1].ToDateTimeNullable(),
                ColaboradorId = id
        };
            return resultado;
        }

    }
}