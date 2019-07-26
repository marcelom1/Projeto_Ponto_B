using System;
using System.Globalization;

namespace PontoB.Business.Utils
{
    public static class NullableDateTimeExtensions
    {
        private const string NullDateString = "NULL";

        public static string ToFilterString(this DateTime? data)
        {
            if (data.HasValue)
                return data.Value.ToString(CultureInfo.InvariantCulture);
            return NullDateString;

        }

        public static DateTime? ToDateTimeNullable(this string s)
        {
            if (NullDateString.Equals(s))
                return null;
            var data = DateTime.Parse(s, CultureInfo.InvariantCulture);
            return data;
        }
    }
}