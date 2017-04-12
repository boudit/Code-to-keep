//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DecimalExtensions.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Primitives
{
    using System;
    using System.Globalization;
    using System.Linq;

    public static class DecimalExtensions
    {
        public static string ToInvariantCultureString(this decimal? source, int precision = 29)
        {
            return ToFormattedString(source, CultureInfo.InvariantCulture, precision);
        }

        public static string ToInvariantCultureString(this decimal source, int precision = 29)
        {
            return source.ToFormattedString(CultureInfo.InvariantCulture, precision);
        }

        public static string ToCurrentCultureString(this decimal? source, int precision = 29)
        {
            return ToFormattedString(source, CultureInfo.CurrentCulture, precision);
        }

        public static string ToCurrentCultureString(this decimal source, int precision = 29)
        {
            return source.ToFormattedString(CultureInfo.CurrentCulture, precision);
        }

        public static string ToFormattedString(this decimal? source, IFormatProvider formatProvider, int precision = 29)
        {
            if (!source.HasValue)
            {
                throw new ArgumentNullException("source");
            }

            return source.Value.ToFormattedString(formatProvider, precision);
        }

        public static string ToFormattedString(this decimal source, IFormatProvider formatProvider, int precision = 29)
        {
            return source.ToString(GetNumberFormat(precision), formatProvider);
        }

        public static decimal ParseFromInvariantCultureToDecimal(this string source)
        {
            return ParseToDecimal(source, CultureInfo.InvariantCulture);
        }

        public static decimal ParseFromCurrentCultureToDecimal(this string source)
        {
            return ParseToDecimal(source, CultureInfo.CurrentCulture);
        }

        public static decimal ParseToDecimal(this string source, IFormatProvider formatProvider)
        {
            return decimal.Parse(source, NumberStyles.Float, formatProvider);
        }

        public static bool TryParseFromInvariantCultureToDecimal(this string source, out decimal value)
        {
            return TryParseToDecimal(source, CultureInfo.InvariantCulture, out value);
        }

        public static bool TryParseFromCurrentCultureToDecimal(this string source, out decimal value)
        {
            return TryParseToDecimal(source, CultureInfo.CurrentCulture, out value);
        }

        public static bool TryParseToDecimal(this string source, IFormatProvider formatProvider, out decimal value)
        {
            return decimal.TryParse(source, NumberStyles.Float, formatProvider, out value);
        }

        private static string GetNumberFormat(int precision)
        {
            return @"0." + string.Concat(Enumerable.Repeat("#", precision));
        }
    }
}