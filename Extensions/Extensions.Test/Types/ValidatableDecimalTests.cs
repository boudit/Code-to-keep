//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableDecimalTests.cs" company="Eurofins">
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

    public class ValidatableDecimalTests
    {
        [Fact]
        public void ImplicitConversionFromValue_AlwaysReturnValidWrapper()
        {
            // Actors
            const decimal InternalValue = 12.5M;

            // Activity
            ValidatableDecimal result = InternalValue;

            // Asserts
            Assert.True(result.IsValid);
            Assert.Equal(InternalValue, result.Value);
        }

        [Fact]
        public void ImplicitConversionFromNullableValueWithValue_AlwaysReturnValidWrapper()
        {
            // Actors
            decimal? internalValue = 12.5M;

            // Activity
            ValidatableDecimal result = internalValue;

            // Asserts
            Assert.True(result.IsValid);
            Assert.Equal(internalValue.Value, result.Value);
        }

        [Fact]
        public void ImplicitConversionFromNullableValueWithNoValue_AlwaysReturnInvalidWrapper()
        {
            // Actors

            // Activity
            ValidatableDecimal result = null as decimal?;

            // Asserts
            Assert.False(result.IsValid);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsValid_ThenReturnsValidObject()
        {
            // Actors
            const string Value1 = "-12.5";

            // Activity
            ValidatableDecimal result = Value1;

            // Asserts
            result.Should().NotBeNull();
            result.Value.Should().Be(-12.5m);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsNotValidInCurrentCultureButInInvariantCulture_ThenReturnsValidObject()
        {
            // Actors
            const string Value1 = "-000012512.5000000";

            // Activity
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            ValidatableDecimal result = Value1;

            // Asserts
            result.Should().NotBeNull();
            result.Value.Should().Be(-12512.5m);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsValidInCurrentCultureButNotInInvariantCulture_ThenReturnsInvalidObject()
        {
            // Actors
            const string Value1 = "-12,5";

            // Activity
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            ValidatableDecimal result = Value1;

            // Asserts
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueContainsThousandSeparator_ThenReturnsInvalidObject()
        {
            // Actors
            const string Value1 = "-12,555.2";

            // Activity
            ValidatableDecimal result = Value1;

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
            ValidatableDecimal result = value;

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
            ValidatableDecimal result = Value1;

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

            var validatable = new ValidatableDecimal(12.5m);

            // Activity
            validatable.InternalValue = null;

            // Asserts
            Assert.Throws<InvalidOperationException>(() => result = validatable);
        }

        [Fact]
        public void ImplicitConversionToString_WhenInternalValueIsNotNull_ThenResultIsStringValue()
        {
            // Actors
            var validatable = new ValidatableDecimal(12.5m);

            validatable.InternalValue = 44.372m;

            // Activity
            string result = validatable;

            // Asserts
            result.Should().Be("44.372");
        }

        [Fact]
        public void ImplicitConversionToString_WhenInternalValueHasManyTrailingZeros_ThenResultIsStringValueWithoutZero()
        {
            // Actors
            var validatable = new ValidatableDecimal(00000000.000000010000000m);
            
            // Activity
            string result = validatable;

            // Asserts
            result.Should().Be("0.00000001");
        }
    }
}