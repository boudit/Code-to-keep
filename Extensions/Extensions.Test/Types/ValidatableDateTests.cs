//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableDateTests.cs" company="Eurofins">
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

    public class ValidatableDateTests
    {
        [Fact]
        public void ImplicitConversionFromValue_AlwaysReturnValidWrapper()
        {
            // Actors
            var internalValue = DateTime.Now;

            // Activity
            ValidatableDate result = internalValue;

            // Asserts
            Assert.True(result.IsValid);
            Assert.Equal(internalValue, result.Value);
        }

        [Fact]
        public void ImplicitConversionFromNullableValueWithValue_AlwaysReturnValidWrapper()
        {
            // Actors
            DateTime? internalValue = DateTime.Now;

            // Activity
            ValidatableDate result = internalValue;

            // Asserts
            Assert.True(result.IsValid);
            Assert.Equal(internalValue.Value, result.Value);
        }

        [Fact]
        public void ImplicitConversionFromNullableValueWithNoValue_AlwaysReturnInvalidWrapper()
        {
            // Actors

            // Activity
            ValidatableDate result = null as DateTime?;

            // Asserts
            Assert.False(result.IsValid);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsValid_ThenReturnsValidObject()
        {
            // Actors
            const string Value1 = "2000-01-01";

            // Activity
            ValidatableDate result = Value1;

            // Asserts
            result.Should().NotBeNull();
            result.Value.Should().Be(new DateTime(2000, 1, 1));
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsNotConvertible_ThenReturnsInvalidObject()
        {
            // Actors
            const string Value1 = "yolo";

            // Activity
            ValidatableDate result = Value1;

            // Asserts
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ParsedText.Should().Be(Value1);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ImplicitConversionFromString_WhenValueIsNullOrEmpty_ThenReturnsInvalidObject(string value)
        {
            // Actors

            // Activity
            ValidatableDate result = value;

            // Asserts
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void ImplicitConversionToString_WhenInternalValueIsNull_ThenThrowException()
        {
            // Actors
            string result;

            var validatable = new ValidatableDate("2000-01-01");

            // Activity
            validatable.InternalValue = null;

            // Asserts
            Assert.Throws<InvalidOperationException>(() => result = validatable);
        }

        [Fact]
        public void ImplicitConversionToString_WhenInternalValueIsNotNull_ThenResultIsStringValue()
        {
            // Actors
            var validatable = new ValidatableDate("2000-01-01");

            validatable.InternalValue = new DateTime(2000, 1, 1);

            // Activity
            string result = validatable;

            // Asserts
            result.Should().Be("2000-01-01");
        }
    }
}