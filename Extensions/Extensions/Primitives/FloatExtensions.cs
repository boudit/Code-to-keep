//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FloatExtensions.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Primitives
{
    using System;
    using System.Globalization;
    using System.Linq;

    public static class FloatExtensions
    {
        public static string ToInvariantCultureString(this float? source, int precision = 15)
        {
            return ToFormattedString(source, CultureInfo.InvariantCulture, precision);
        }

        public static string ToInvariantCultureString(this float source, int precision = 15)
        {
            return source.ToFormattedString(CultureInfo.InvariantCulture, precision);
        }

        public static string ToCurrentCultureString(this float? source, int precision = 15)
        {
            return ToFormattedString(source, CultureInfo.CurrentCulture, precision);
        }

        public static string ToCurrentCultureString(this float source, int precision = 15)
        {
            return source.ToFormattedString(CultureInfo.CurrentCulture, precision);
        }

        public static string ToFormattedString(this float? source, IFormatProvider formatProvider, int precision = 15)
        {
            if (!source.HasValue)
            {
                throw new ArgumentNullException("source");
            }

            return source.Value.ToFormattedString(formatProvider, precision);
        }

        public static string ToFormattedString(this float source, IFormatProvider formatProvider, int precision = 15)
        {
            return source.ToString(GetNumberFormat(precision), formatProvider);
        }

        public static float ParseFromInvariantCultureToFloat(this string source)
        {
            return ParseToFloat(source, CultureInfo.InvariantCulture);
        }

        public static float ParseFromCurrentCultureToFloat(this string source)
        {
            return ParseToFloat(source, CultureInfo.CurrentCulture);
        }

        public static float ParseToFloat(this string source, IFormatProvider formatProvider)
        {
            return float.Parse(source, NumberStyles.Float, formatProvider);
        }

        public static bool TryParseFromInvariantCultureToFloat(this string source, out float value)
        {
            return TryParseToFloat(source, CultureInfo.InvariantCulture, out value);
        }

        public static bool TryParseFromCurrentCultureToFloat(this string source, out float value)
        {
            return TryParseToFloat(source, CultureInfo.CurrentCulture, out value);
        }

        public static bool TryParseToFloat(this string source, IFormatProvider formatProvider, out float value)
        {
            return float.TryParse(source, NumberStyles.Float, formatProvider, out value);
        }

        private static string GetNumberFormat(int precision)
        {
            return @"0." + string.Concat(Enumerable.Repeat("#", precision));
        }
    }
}