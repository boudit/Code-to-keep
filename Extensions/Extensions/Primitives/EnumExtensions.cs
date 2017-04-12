//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EnumExtensions.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;

    public static class EnumExtensions
    {
        public static IEnumerable<TEnum> GetEnumValues<TEnum>() where TEnum : struct
        {
            var type = GetEnumType<TEnum>();

            return GetEnumValues<TEnum>(type);
        }

        public static TEnum ToEnum<TEnum>(this string enumRepresentation) where TEnum : struct
        {
            if (enumRepresentation.IsNullOrEmpty())
            {
                throw new ArgumentNullException("enumRepresentation");
            }

            TEnum result;

            if (Enum.TryParse(enumRepresentation, out result))
            {
                return result;
            }
            
            var type = GetEnumType<TEnum>();

            var allValues = GetEnumValues<TEnum>(type);

            foreach (var enumValue in allValues)
            {
                var attribute = GetDescriptionAttribute(enumValue, type);
                if (attribute != null && attribute.Description == enumRepresentation)
                {
                    return enumValue;
                }
            }

            throw new ArgumentException("Provided value is not a valid enum value.", "enumRepresentation");
        }

        public static bool TryMapToEnum<TEnum>(this string enumRepresentation, out TEnum? result) where TEnum : struct
        {
            TEnum tmpResult;
            if (Enum.TryParse(enumRepresentation, out tmpResult))
            {
                result = tmpResult;
                return true;
            }

            var lowerEnumRepresentation = enumRepresentation == null ? string.Empty : enumRepresentation.ToLower().TrimAndSuppressRedundantSpaces();
            
            var type = GetEnumType<TEnum>();

            var allValues = GetEnumValues<TEnum>(type);

            foreach (var enumValue in allValues)
            {
                var lowerStringValue = enumValue.ToString().ToLower();
                if (lowerEnumRepresentation == lowerStringValue)
                {
                    result = enumValue;
                    return true;
                }

                var attribute = GetDescriptionAttribute(enumValue, type);
                if (attribute != null && attribute.Description == enumRepresentation)
                {
                    result = enumValue;
                    return true;
                }
            }

            result = null;
            return false;
        }

        public static string GetStringValue<TEnum>(this TEnum enumValue) where TEnum : struct
        {
            var type = GetEnumType<TEnum>();

            var attribute = GetDescriptionAttribute(enumValue, type);

            return attribute != null ? attribute.Description : enumValue.ToString();
        }

        public static DescriptionAttribute GetDescriptionAttribute<TEnum>(this TEnum enumValue) where TEnum : struct
        {
            var type = GetEnumType<TEnum>();

            return GetDescriptionAttribute(enumValue, type);
        }

        private static IEnumerable<TEnum> GetEnumValues<TEnum>(Type type) where TEnum : struct
        {
            return type.GetEnumValues().OfType<TEnum>().ToList();
        }

        private static Type GetEnumType<TEnum>() where TEnum : struct
        {
            var type = typeof(TEnum);

            if (!type.IsEnum)
            {
                throw new ArgumentException("Type provided must be an Enum.", "TEnum");
            }

            return type;
        }

        private static DescriptionAttribute GetDescriptionAttribute<TEnum>(TEnum enumValue, Type typeOfEnum) where TEnum : struct
        {
            var memberOfEnum = typeOfEnum.GetMember(enumValue.ToString());

            if (!memberOfEnum.Any())
            {
                return null;
            }

            return memberOfEnum[0].GetCustomAttributes(typeof(DescriptionAttribute), false).OfType<DescriptionAttribute>().FirstOrDefault();
        }
    }
}