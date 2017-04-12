//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableIntTests.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Extensions.Test.Types
{
    using System;
    using System.Globalization;

    using Extensions.Types;

    using FluentAssertions;

    using Xunit;

    public class ValidatableIntTests
    {
        [Fact]
        public void ImplicitConversionFromValue_AlwaysReturnValidWrapper()
        {
            // Actors
            const int InternalValue = 125;

            // Activity
            ValidatableInt result = InternalValue;

            // Asserts
            Assert.True(result.IsValid);
            Assert.Equal(InternalValue, result.Value);
        }

        [Fact]
        public void ImplicitConversionFromNullableValueWithValue_AlwaysReturnValidWrapper()
        {
            // Actors
            int? internalValue = 125;

            // Activity
            ValidatableInt result = internalValue;

            // Asserts
            Assert.True(result.IsValid);
            Assert.Equal(internalValue.Value, result.Value);
        }

        [Fact]
        public void ImplicitConversionFromNullableValueWithNoValue_AlwaysReturnInvalidWrapper()
        {
            // Actors

            // Activity
            ValidatableInt result = null as int?;

            // Asserts
            Assert.False(result.IsValid);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsValid_ThenReturnsValidObject()
        {
            // Actors
            const string Value1 = "-12";

            // Activity
            ValidatableInt result = Value1;

            // Asserts
            result.Should().NotBeNull();
            result.Value.Should().Be(-12);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsValidInInvariantCulture_ThenReturnsValidObject()
        {
            // Actors
            const string Value1 = "-000012512";

            // Activity
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            ValidatableInt result = Value1;

            // Asserts
            result.Should().NotBeNull();
            result.Value.Should().Be(-12512);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueContainsThousandSeparator_ThenReturnsInvalidObject()
        {
            // Actors
            const string Value1 = "-12,555";

            // Activity
            ValidatableFloat result = Value1;

            // Asserts
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ImplicitConversionFromString_WhenValueIsNullOrEmpty_ThenReturnsInvalidObject(string value)
        {
            // Actors

            // Activity
            ValidatableInt result = value;

            // Asserts
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsNotConvertible_ThenReturnsInvalidObject()
        {
            // Actors
            const string Value1 = "BadValue";

            // Activity
            ValidatableInt result = Value1;

            // Asserts
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ParsedText.Should().Be(Value1);
        }

        [Fact]
        public void ImplicitConversionToString_WhenInternalValueIsNull_ThenThrowException()
        {
            // Actors
            string result;

            var validatable = new ValidatableInt(12);

            // Activity
            validatable.InternalValue = null;

            // Asserts
            Assert.Throws<InvalidOperationException>(() => result = validatable);
        }

        [Fact]
        public void ImplicitConversionToString_WhenInternalValueIsNotNull_ThenResultIsStringValue()
        {
            // Actors
            var validatable = new ValidatableInt(12);

            validatable.InternalValue = 44;

            // Activity
            string result = validatable;

            // Asserts
            result.Should().Be("44");
        }
    }
}