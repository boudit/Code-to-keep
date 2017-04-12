//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableInt.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Types
{
    using Extensions.Primitives;

    using global::Extensions.Types;

    public class ValidatableInt : Validatable<int>
    {
        public ValidatableInt(int value)
            : base(value)
        {
        }

        public ValidatableInt(int? value)
            : base(value)
        {
        }

        public ValidatableInt(string parsedText)
            : base(parsedText)
        {
        }

        public static implicit operator ValidatableInt(int value)
        {
            return new ValidatableInt(value);
        }

        public static implicit operator ValidatableInt(int? value)
        {
            return new ValidatableInt(value);
        }

        public static implicit operator ValidatableInt(string stringValue)
        {
            var result = new ValidatableInt(stringValue);

            int value;

            if (stringValue.TryParseFromInvariantCultureToInt(out value))
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