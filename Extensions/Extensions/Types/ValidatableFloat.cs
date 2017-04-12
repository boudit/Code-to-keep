//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableFloat.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Types
{
    using Extensions.Primitives;

    using global::Extensions.Types;

    public class ValidatableFloat : Validatable<float>
    {
        #region Constructors and Destructors

        public ValidatableFloat(float value)
            : base(value)
        {
        }

        public ValidatableFloat(float? value)
            : base(value)
        {
        }

        public ValidatableFloat(string parsedText)
            : base(parsedText)
        {
        }

        #endregion

        public static implicit operator ValidatableFloat(float value)
        {
            return new ValidatableFloat(value);
        }

        public static implicit operator ValidatableFloat(float? value)
        {
            return new ValidatableFloat(value);
        }

        public static implicit operator ValidatableFloat(string stringValue)
        {
            var result = new ValidatableFloat(stringValue);

            float value;

            if (stringValue.TryParseFromInvariantCultureToFloat(out value))
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