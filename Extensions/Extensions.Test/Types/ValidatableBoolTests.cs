//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableBoolTests.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Extensions.Test.Types
{
    using System;

    using Extensions.Types;

    using FluentAssertions;

    using Xunit;

    public class ValidatableBoolTests
    {
        [Fact]
        public void ImplicitConversionFromValue_AlwaysReturnValidWrapper()
        {
            // Actors
            const bool InternalValue = true;

            // Activity
            ValidatableBool result = InternalValue;

            // Asserts
            Assert.True(result.IsValid);
            Assert.Equal(InternalValue, result.Value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ImplicitConversionFromNullableValueWithValue_AlwaysReturnValidWrapper(bool? nullableValue)
        {
            // Actors

            // Activity
            ValidatableBool result = nullableValue;

            // Asserts
            Assert.True(result.IsValid);
            Assert.Equal(nullableValue, result.Value);
        }

        [Fact]
        public void ImplicitConversionFromNullableValueWithNoValue_AlwaysReturnInvalidWrapper()
        {
            // Actors

            // Activity
            ValidatableBool result = null as bool?;

            // Asserts
            Assert.False(result.IsValid);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsValid_ThenReturnsValidObject()
        {
            // Actors
            const string Value1 = "True";

            // Activity
            ValidatableBool result = Value1;

            // Asserts
            result.Should().NotBeNull();
            result.Value.Should().Be(true);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsNotConvertible_ThenReturnsInvalidObject()
        {
            // Actors
            const string Value1 = "Trou";

            // Activity
            ValidatableBool result = Value1;

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
            ValidatableBool result = value;

            // Asserts
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
        }
        
        [Fact]
        public void ImplicitConversionToString_WhenInternalValueIsNull_ThenThrowException()
        {
            // Actors
            string result;

            var validatable = new ValidatableBool(false);

            // Activity
            validatable.InternalValue = null;

            // Asserts
            Assert.Throws<InvalidOperationException>(() => result = validatable);
        }

        [Fact]
        public void ImplicitConversionToString_WhenInternalValueIsNotNull_ThenResultIsStringValue()
        {
            // Actors
            var validatable = new ValidatableBool(true);

            validatable.InternalValue = false;

            // Activity
            string result = validatable;

            // Asserts
            result.Should().Be("False");
        }
    }
}