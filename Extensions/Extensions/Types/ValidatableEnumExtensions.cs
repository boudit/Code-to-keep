//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableEnumExtensions.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Types
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public static class ValidatableEnumExtensions
    {
        public static List<TEnum> ToListOfEnum<TEnum>(this IEnumerable<ValidatableEnum<TEnum>> listOfValidatableEnum)
            where TEnum : struct, IConvertible, IComparable
        {
            return listOfValidatableEnum.ToEnumerableOfValidEnum().ToList();
        }

        public static bool ContainsEnum<TEnum>(this IEnumerable<ValidatableEnum<TEnum>> listOfValidatableEnum, TEnum enumValue)
            where TEnum : struct, IConvertible, IComparable
        {
            return listOfValidatableEnum.ToEnumerableOfValidEnum().Contains(enumValue);
        }

        private static IEnumerable<TEnum> ToEnumerableOfValidEnum<TEnum>(this IEnumerable<ValidatableEnum<TEnum>> listOfValidatableEnum) where TEnum : struct, IConvertible, IComparable
        {
            return listOfValidatableEnum.Where(bo => bo.IsValid).Select<ValidatableEnum<TEnum>, TEnum>(val => val);
        }
    }
}