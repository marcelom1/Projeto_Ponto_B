using System;
using System.Collections.Generic;
using System.Globalization;

namespace PontoB.Business.Utils
{
    public static class DateTimeExtensions
    {
        private static Dictionary<DayOfWeek, string> _diasDaSemana = new Dictionary<DayOfWeek, string> {
            { DayOfWeek.Monday , "Segunda" },
            { DayOfWeek.Tuesday , "Terça" },
            { DayOfWeek.Wednesday , "Quarta" },
            { DayOfWeek.Thursday , "Quinta" },
            { DayOfWeek.Friday , "Sexta" },
            { DayOfWeek.Saturday , "Sábado" },
            { DayOfWeek.Sunday , "Domingo" },
        };

        public static string ToDiaDaSemana(this DateTime data)
        {
            return _diasDaSemana[data.DayOfWeek];
        }

    }
}