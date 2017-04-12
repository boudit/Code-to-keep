//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableEnum.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Types
{
    using System;
    using System.Globalization;

    using Extensions.Primitives;

    using global::Extensions.Types;

    public class ValidatableEnum<TEnum> : Validatable<TEnum>
        where TEnum : struct, IConvertible, IComparable
    {
        #region Constructors and Destructors

        public ValidatableEnum(TEnum enumValue)
            : base(enumValue)
        {
        }

        public ValidatableEnum(TEnum? enumValue)
            : base(enumValue)
        {
        }

        internal ValidatableEnum(string parsedText)
            : base(parsedText)
        {
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// Get the string value representing the <see cref="TEnum"/> value.
        /// <para><see cref="InvalidOperationException" /> : throws when IsValid is False.</para>
        /// </summary>
        public string StringValue
        {
            get
            {
                return this.Value.ToString(CultureInfo.InvariantCulture);
            }
        }

        #endregion

        #region Implicit Operators

        public static implicit operator TEnum(ValidatableEnum<TEnum> validatableEnum)
        {
            return validatableEnum.Value;
        }

        public static implicit operator ValidatableEnum<TEnum>(TEnum enumValue)
        {
            return new ValidatableEnum<TEnum>(enumValue);
        }

        public static implicit operator ValidatableEnum<TEnum>(TEnum? value)
        {
            return new ValidatableEnum<TEnum>(value);
        }

        public static implicit operator ValidatableEnum<TEnum>(string stringValue)
        {
            var result = new ValidatableEnum<TEnum>(stringValue);

            TEnum? enumValue;

            if (stringValue.TryMapToEnum(out enumValue))
            {
                result.InternalValue = enumValue;
            }

            return result;
        }
        
        #endregion
        
        public override string ToString()
        {
            if (this.IsValid)
            {
                return this.StringValue;
            }

            return base.ToString();
        }
    }
}