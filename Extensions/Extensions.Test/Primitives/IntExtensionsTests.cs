//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IntExtensionsTests.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Extensions.Test.Primitives
{
    using System;
    using System.Globalization;

    using Extensions.Primitives;

    using FluentAssertions;

    using Xunit;

    public class IntExtensionsTests
    {
        private static readonly CultureInfo CultureWithChangedNegativeSign;
        
        static IntExtensionsTests()
        {
            CultureWithChangedNegativeSign = new CultureInfo("fr-FR") { NumberFormat = { NegativeSign = "a" } };
        }
        
        [Fact]
        public void ToInvariantCultureString_WhenNullableIntIsNull_ThenThrowException()
        {
            // Actors
            int? value = null;

            // Activity
            Action action = () => value.ToInvariantCultureString();

            // Asserts
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ToCurrentCultureString_WhenNullableIntIsNull_ThenThrowException()
        {
            // Actors
            int? value = null;

            // Activity
            Action action = () => value.ToCurrentCultureString();

            // Asserts
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ToFormattedString_WhenNullableIntIsNull_ThenThrowException()
        {
            // Actors
            int? value = null;

            // Activity
            Action action = () => value.ToFormattedString(CultureWithChangedNegativeSign);

            // Asserts
            action.ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData(-123, "-123")]
        [InlineData(0006, "6")]
        [InlineData(000000000000000000000, "0")]
        [InlineData(123000, "123000")]
        public void ToInvariantCultureString_WhenNullableIntIsNotNull_ThenResultIsFormatted(int? value, string expected)
        {
            // Actors

            // Activity
            var result = value.ToInvariantCultureString();

            // Asserts
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-123, "a123")]
        [InlineData(0006, "6")]
        [InlineData(000000000000000000000, "0")]
        [InlineData(123000, "123000")]
        public void ToCurrentCultureString_WhenNullableIntIsNotNull_ThenResultIsFormattedWithCurrentCulture(int? value, string expected)
        {
            // Actors
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithChangedNegativeSign;

            // Activity
            var result = value.ToCurrentCultureString();

            // Asserts
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-123, "a123")]
        [InlineData(0006, "6")]
        [InlineData(000000000000000000000, "0")]
        [InlineData(123000, "123000")]
        public void ToFormattedString_WhenNullableIntIsNotNull_ThenResultIsFormattedWithCulture(int? value, string expected)
        {
            // Actors

            // Activity
            var result = value.ToFormattedString(CultureWithChangedNegativeSign);

            // Asserts
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-123, "-123")]
        [InlineData(0006, "6")]
        [InlineData(000000000000000000000, "0")]
        [InlineData(123000, "123000")]
        public void ToInvariantCultureString_WhenIntIsNotNullable_ThenResultIsFormatted(int value, string expected)
        {
            // Actors

            // Activity
            var result = value.ToInvariantCultureString();

            // Asserts
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-123, "a123")]
        [InlineData(0006, "6")]
        [InlineData(000000000000000000000, "0")]
        [InlineData(123000, "123000")]
        public void ToCurrentCultureString_WhenIntIsNotNullable_ThenResultIsFormattedWithCurrentCulture(int value, string expected)
        {
            // Actors
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithChangedNegativeSign;

            // Activity
            var result = value.ToCurrentCultureString();

            // Asserts
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-123, "a123")]
        [InlineData(0006, "6")]
        [InlineData(000000000000000000000, "0")]
        [InlineData(123000, "123000")]
        public void ToFormattedString_WhenIntIsNotNullable_ThenResultIsFormattedWithCulture(int value, string expected)
        {
            // Actors

            // Activity
            var result = value.ToFormattedString(CultureWithChangedNegativeSign);

            // Asserts
            result.Should().Be(expected);
        }
        
        [Theory]
        [InlineData(-123, "-123")]
        [InlineData(0006, "6")]
        [InlineData(000000000000000000000, "0")]
        [InlineData(123000, "123000")]
        public void ParseFromInvariantCultureToInt_WhenConversionIsAvailable_ThenConversionIsDone(int intValue, string stringValue)
        {
            // Actors

            // Activity
            var result = stringValue.ParseFromInvariantCultureToInt();

            // Asserts
            result.Should().Be(intValue);
        }

        [Theory]
        [InlineData(-123, "a123")]
        [InlineData(0006, "6")]
        [InlineData(000000000000000000000, "0")]
        [InlineData(123000, "123000")]
        public void ParseFromCurrentCultureToInt_WhenConversionIsAvailable_ThenConversionIsDone(int intValue, string stringValue)
        {
            // Actors
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithChangedNegativeSign;

            // Activity
            var result = stringValue.ParseFromCurrentCultureToInt();

            // Asserts
            result.Should().Be(intValue);
        }

        [Theory]
        [InlineData(-123, "a123")]
        [InlineData(0006, "6")]
        [InlineData(000000000000000000000, "0")]
        [InlineData(123000, "123000")]
        public void ParseToInt_WhenConversionIsAvailable_ThenConversionIsDone(int intValue, string stringValue)
        {
            // Actors

            // Activity
            var result = stringValue.ParseToInt(CultureWithChangedNegativeSign);

            // Asserts
            result.Should().Be(intValue);
        }

        [Theory]
        [InlineData(-123, "-123")]
        [InlineData(0006, "6")]
        [InlineData(000000000000000000000, "0")]
        [InlineData(123000, "123000")]
        public void TryParseFromInvariantCultureToInt_WhenConversionIsAvailable_ThenConversionIsDone(int intValue, string stringValue)
        {
            // Actors
            int parsed;

            // Activity
            var result = stringValue.TryParseFromInvariantCultureToInt(out parsed);

            // Asserts
            result.Should().BeTrue();
            parsed.Should().Be(intValue);
        }

        [Fact]
        public void TryParseFromInvariantCultureToInt_WhenConversionIsNotAvailable_ThenReturnsFalse()
        {
            // Actors
            int parsed;

            // Activity
            var result = "a125000".TryParseFromInvariantCultureToInt(out parsed);

            // Asserts
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(-123, "a123")]
        [InlineData(0006, "6")]
        [InlineData(000000000000000000000, "0")]
        [InlineData(-123000, "a123000")]
        public void TryParseFromCurrentCultureToInt_WhenConversionIsAvailable_ThenConversionIsDone(int intValue, string stringValue)
        {
            // Actors
            int parsed;

            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithChangedNegativeSign;

            // Activity
            var result = stringValue.TryParseFromCurrentCultureToInt(out parsed);

            // Asserts
            result.Should().BeTrue();
            parsed.Should().Be(intValue);
        }

        [Fact]
        public void TryParseFromCurrentCultureToInt_WhenConversionIsNotAvailable_ThenReturnsFalse()
        {
            // Actors
            int parsed;

            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithChangedNegativeSign;

            // Activity
            var result = "-125000".TryParseFromCurrentCultureToInt(out parsed);

            // Asserts
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(-123, "a123")]
        [InlineData(0006, "6")]
        [InlineData(000000000000000000000, "0")]
        [InlineData(-123000, "a123000")]
        public void TryParseToInt_WhenConversionIsAvailable_ThenConversionIsDone(int intValue, string stringValue)
        {
            // Actors
            int parsed;

            // Activity
            var result = stringValue.TryParseToInt(CultureWithChangedNegativeSign, out parsed);

            // Asserts
            result.Should().BeTrue();
            parsed.Should().Be(intValue);
        }

        [Fact]
        public void TryParseToInt_WhenConversionIsNotAvailable_ThenReturnsFalse()
        {
            // Actors
            int parsed;

            // Activity
            var result = "-125000".TryParseToInt(CultureWithChangedNegativeSign, out parsed);

            // Asserts
            result.Should().BeFalse();
        }
    }
}