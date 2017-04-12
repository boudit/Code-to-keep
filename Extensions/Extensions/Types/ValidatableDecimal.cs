//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableDecimal.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Types
{
    using Extensions.Primitives;

    public class ValidatableDecimal : Validatable<decimal>
    {
        public ValidatableDecimal(decimal value)
            : base(value)
        {
        }

        public ValidatableDecimal(decimal? value)
            : base(value)
        {
        }

        public ValidatableDecimal(string parsedText)
            : base(parsedText)
        {
        }

        public static implicit operator ValidatableDecimal(decimal value)
        {
            return new ValidatableDecimal(value);
        }

        public static implicit operator ValidatableDecimal(decimal? value)
        {
            return new ValidatableDecimal(value);
        }

        public static implicit operator ValidatableDecimal(string stringValue)
        {
            var result = new ValidatableDecimal(stringValue);

            decimal value;

            if (stringValue.TryParseFromInvariantCultureToDecimal(out value))
            {
                result.InternalValue = value;
            }

            return result;
        }
        
        public override string ToString()
        {
            if (this.IsValid)
            {
                return this.Value.ToInvariantCultureString();
            }

            return base.ToString();
        }
    }
}