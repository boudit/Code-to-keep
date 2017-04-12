//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="StringExtensions.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string toOperate)
        {
            return string.IsNullOrWhiteSpace(toOperate);
        }

        public static bool IsNullOrEmpty(this string toOperate)
        {
            return string.IsNullOrEmpty(toOperate);
        }

        public static string Format(this string toOperate, params object[] formatArgs)
        {
            return string.Format(toOperate, formatArgs);
        }

        public static string Format(this string toOperate, object arg1)
        {
            return string.Format(toOperate, arg1);
        }

        public static string Format(this string toOperate, object arg1, object arg2)
        {
            return string.Format(toOperate, arg1, arg2);
        }

        public static string Format(this string toOperate, object arg1, object arg2, object arg3)
        {
            return string.Format(toOperate, arg1, arg2, arg3);
        }

        public static string TrimAndSuppressRedundantSpaces(this string toOperate)
        {
            if (toOperate == null)
            {
                return null;
            }

            var multipleSpacesRegEx = new Regex(@"\s\s+");
            var trimmedString = toOperate.Trim();

            return multipleSpacesRegEx.Replace(trimmedString, m => " ");
        }

        public static bool ContainsInsensitive(this string source, IEnumerable<string> textsToSeek)
        {
            textsToSeek = textsToSeek.Where(str => !string.IsNullOrEmpty(str)).ToList();

            return textsToSeek.All(source.ContainsInsensitive);
        }

        public static bool ContainsInsensitive(this string source, string textToSeek, string wordSeparator, bool trimWords = true)
        {
            var words = textToSeek.Split(new[] { wordSeparator }, StringSplitOptions.RemoveEmptyEntries);

            if (trimWords)
            {
                words = words.Select(word => word.Trim()).ToArray();
            }

            return source.ContainsInsensitive(words);
        }

        public static bool ContainsInsensitive(this string source, string textToSeek)
        {
            return RemoveDiacritics(source).ToUpper().Contains(RemoveDiacritics(textToSeek).ToUpper());
        }

        public static string RemoveDiacritics(this string text)
        {
            return
                string.Concat(
                    text.Normalize(NormalizationForm.FormD)
                        .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark))
                    .Normalize(NormalizationForm.FormC);
        }

        public static bool TryConvert<T>(this string value, out T val)
        {
            return value.TryConvert(CultureInfo.InvariantCulture, out val);
        }

        public static bool TryConvert<T>(this string value, IFormatProvider formatProvider, out T val)
        {
            try
            {
                val = (T)Convert.ChangeType(value, typeof(T), formatProvider);
                return true;
            }
            catch (Exception)
            {
                val = default(T);
                return false;
            }
        }
    }
}