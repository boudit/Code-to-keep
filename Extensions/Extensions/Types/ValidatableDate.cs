//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableDate.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Types
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Perfect type for a BO property to be validated as a valid date (no time taken into account)
    /// </summary>
    public class ValidatableDate : Validatable<DateTime>
    {
        public const string DateFormat = @"yyyy-MM-dd";

        public ValidatableDate(DateTime value)
            : base(value)
        {
        }

        public ValidatableDate(DateTime? value)
            : base(value)
        {
        }

        public ValidatableDate(string parsedText)
            : base(parsedText)
        {
        }

        public static implicit operator ValidatableDate(DateTime value)
        {
            return new ValidatableDate(value);
        }

        public static implicit operator ValidatableDate(DateTime? value)
        {
            return new ValidatableDate(value);
        }

        public static implicit operator ValidatableDate(string stringValue)
        {
            var result = new ValidatableDate(stringValue);

            DateTime value;

            if (DateTime.TryParseExact(
                stringValue, 
                DateFormat, 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None, 
                out value))
            {
                result.InternalValue = value;
            }

            return result;
        }
        
        public override string ToString()
        {
            if (this.IsValid)
            {
                return this.Value.ToString(DateFormat, CultureInfo.InvariantCulture);
            }

            return base.ToString();
        }
    }
}