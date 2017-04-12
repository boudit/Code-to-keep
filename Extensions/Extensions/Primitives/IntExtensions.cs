//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IntExtensions.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Primitives
{
    using System;
    using System.Globalization;
    using System.Linq;

    public static class IntExtensions
    {
        public static string ToInvariantCultureString(this int? source)
        {
            return ToFormattedString(source, CultureInfo.InvariantCulture);
        }

        public static string ToInvariantCultureString(this int source)
        {
            return source.ToFormattedString(CultureInfo.InvariantCulture);
        }

        public static string ToCurrentCultureString(this int? source)
        {
            return ToFormattedString(source, CultureInfo.CurrentCulture);
        }

        public static string ToCurrentCultureString(this int source)
        {
            return source.ToFormattedString(CultureInfo.CurrentCulture);
        }

        public static string ToFormattedString(this int? source, IFormatProvider formatProvider)
        {
            if (!source.HasValue)
            {
                throw new ArgumentNullException("source");
            }

            return source.Value.ToFormattedString(formatProvider);
        }

        public static string ToFormattedString(this int source, IFormatProvider formatProvider)
        {
            return source.ToString("0", formatProvider);
        }

        public static int ParseFromInvariantCultureToInt(this string source)
        {
            return ParseToInt(source, CultureInfo.InvariantCulture);
        }

        public static int ParseFromCurrentCultureToInt(this string source)
        {
            return ParseToInt(source, CultureInfo.CurrentCulture);
        }

        public static int ParseToInt(this string source, IFormatProvider formatProvider)
        {
            return int.Parse(source, NumberStyles.Integer, formatProvider);
        }

        public static bool TryParseFromInvariantCultureToInt(this string source, out int value)
        {
            return TryParseToInt(source, CultureInfo.InvariantCulture, out value);
        }

        public static bool TryParseFromCurrentCultureToInt(this string source, out int value)
        {
            return TryParseToInt(source, CultureInfo.CurrentCulture, out value);
        }

        public static bool TryParseToInt(this string source, IFormatProvider formatProvider, out int value)
        {
            return int.TryParse(source, NumberStyles.Integer, formatProvider, out value);
        }
    }
}