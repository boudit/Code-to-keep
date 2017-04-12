//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableBool.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Types
{
    public class ValidatableBool : Validatable<bool>
    {
        public ValidatableBool(bool value)
            : base(value)
        {
        }

        public ValidatableBool(bool? value)
            : base(value)
        {
        }

        public ValidatableBool(string parsedText)
            : base(parsedText)
        {
        }

        public static implicit operator ValidatableBool(bool value)
        {
            return new ValidatableBool(value);
        }

        public static implicit operator ValidatableBool(bool? value)
        {
            return new ValidatableBool(value);
        }

        public static implicit operator ValidatableBool(string stringValue)
        {
            var result = new ValidatableBool(stringValue);

            bool value;

            if (bool.TryParse(stringValue, out value))
            {
                result.InternalValue = value;
            }

            return result;
        }
        
        public override string ToString()
        {
            if (this.IsValid)
            {
                return this.Value.ToString();
            }

            return base.ToString();
        }
    }
}