//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FloatExtensionsTests.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Extensions.Test.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Extensions.Primitives;

    using FluentAssertions;

    using Xunit;

    public class FloatExtensionsTests
    {
        private static readonly CultureInfo CultureWithComma;

        private static readonly CultureInfo CultureWithDash;
        
        static FloatExtensionsTests()
        {
            CultureWithComma = new CultureInfo("fr-FR") { NumberFormat = { NumberDecimalSeparator = "," } };
            CultureWithDash = new CultureInfo("fr-FR") { NumberFormat = { NumberDecimalSeparator = "-" } };
        }
        
        [Fact]
        public void ToInvariantCultureString_WhenNullableFloatIsNull_ThenThrowException()
        {
            // Actors
            float? value = null;

            // Activity
            Action action = () => value.ToInvariantCultureString();

            // Asserts
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ToCurrentCultureString_WhenNullableFloatIsNull_ThenThrowException()
        {
            // Actors
            float? value = null;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithDash;

            // Activity
            Action action = () => value.ToCurrentCultureString();

            // Asserts
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ToFormattedString_WhenNullableFloatIsNull_ThenThrowException()
        {
            // Actors
            float? value = null;

            // Activity
            Action action = () => value.ToFormattedString(CultureWithComma);

            // Asserts
            action.ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData(-123.456000000F, "-123.456")]
        [InlineData(0006.000002F, "6.000002")]
        [InlineData(0.000000000001000000000F, "0.000000000001")]
        [InlineData(123000F, "123000")]
        public void ToInvariantCultureString_WhenNullableFloatIsNotNull_ThenResultIsFormatted(float? value, string expected)
        {
            // Actors

            // Activity
            var result = value.ToInvariantCultureString();

            // Asserts
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-123.456000000F, "-123-456")]
        [InlineData(0006.000002F, "6-000002")]
        [InlineData(0.000000000001000000000F, "0-000000000001")]
        [InlineData(123000F, "123000")]
        public void ToCurrentCultureString_WhenNullableFloatIsNotNull_ThenResultIsFormattedWithCurrentCulture(float? value, string expected)
        {
            // Actors
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithDash;

            // Activity
            var result = value.ToCurrentCultureString();

            // Asserts
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-123.456000000F, "-123,456")]
        [InlineData(0006.000002F, "6,000002")]
        [InlineData(0.000000000001000000000F, "0,000000000001")]
        [InlineData(123000F, "123000")]
        public void ToFormattedString_WhenNullableFloatIsNotNull_ThenResultIsFormattedWithCulture(float? value, string expected)
        {
            // Actors

            // Activity
            var result = value.ToFormattedString(CultureWithComma);

            // Asserts
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-123.456000000F, "-123.456")]
        [InlineData(0006.000002F, "6.000002")]
        [InlineData(0.000000000001000000000F, "0.000000000001")]
        [InlineData(123000F, "123000")]
        public void ToInvariantCultureString_WhenFloatIsNotNullable_ThenResultIsFormatted(float value, string expected)
        {
            // Actors

            // Activity
            var result = value.ToInvariantCultureString();

            // Asserts
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-123.456000000F, "-123-456")]
        [InlineData(0006.000002F, "6-000002")]
        [InlineData(0.000000000001000000000F, "0-000000000001")]
        [InlineData(123000F, "123000")]
        public void ToCurrentCultureString_WhenFloatIsNotNullable_ThenResultIsFormattedWithCurrentCulture(float value, string expected)
        {
            // Actors
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithDash;

            // Activity
            var result = value.ToCurrentCultureString();

            // Asserts
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-123.456000000F, "-123,456")]
        [InlineData(0006.000002F, "6,000002")]
        [InlineData(0.000000000001000000000F, "0,000000000001")]
        [InlineData(123000F, "123000")]
        public void ToFormattedString_WhenFloatIsNotNullable_ThenResultIsFormattedWithCulture(float value, string expected)
        {
            // Actors

            // Activity
            var result = value.ToFormattedString(CultureWithComma);

            // Asserts
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-123456.789000000F, "-123456.789")]
        [InlineData(0006.0000000002F, "6.0000000002")]
        [InlineData(0.0000000000000001000000000F, "0.0000000000000001")]
        [InlineData(123000F, "123000")]
        public void ParseFromInvariantCultureToFloat_WhenConversionIsAvailableWithFloatingPointAsDot_ThenConversionIsDone(float floatValue, string stringValue)
        {
            // Actors

            // Activity
            var result = stringValue.ParseFromInvariantCultureToFloat();

            // Asserts
            result.Should().Be(floatValue);
        }

        [Theory]
        [InlineData(-123456.789000000F, "-123456-789")]
        [InlineData(0006.0000000002F, "6-0000000002")]
        [InlineData(0.0000000000000001000000000F, "0-0000000000000001")]
        [InlineData(123000F, "123000")]
        public void ParseFromCurrentCultureToFloat_WhenConversionIsAvailableWithFloatingPointAsCurrentCultureSeparator_ThenConversionIsDone(float floatValue, string stringValue)
        {
            // Actors
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithDash;

            // Activity
            var result = stringValue.ParseFromCurrentCultureToFloat();

            // Asserts
            result.Should().Be(floatValue);
        }

        [Theory]
        [InlineData(-123456.789000000F, "-123456,789")]
        [InlineData(0006.0000000002F, "6,0000000002")]
        [InlineData(0.0000000000000001000000000F, "0,0000000000000001")]
        [InlineData(123000F, "123000")]
        public void
            ParseToFloat_WhenConversionIsAvailableWithFloatingPointAsProvidedCultureSeparator_ThenConversionIsDone(float floatValue, string stringValue)
        {
            // Actors

            // Activity
            var result = stringValue.ParseToFloat(CultureWithComma);

            // Asserts
            result.Should().Be(floatValue);
        }
        
        [Theory]
        [InlineData(-123456.789000000F, "-123456.789")]
        [InlineData(0006.0000000002F, "6.0000000002")]
        [InlineData(0.0000000000000001000000000F, "0.0000000000000001")]
        [InlineData(123000F, "123000")]
        public void TryParseFromInvariantCultureToFloat_WhenConversionIsAvailableWithFloatingPointAsDot_ThenConversionIsDone(float floatValue, string stringValue)
        {
            // Actors
            float parsed;

            // Activity
            var result = stringValue.TryParseFromInvariantCultureToFloat(out parsed);

            // Asserts
            result.Should().BeTrue();
            parsed.Should().Be(floatValue);
        }

        [Fact]
        public void TryParseFromInvariantCultureToFloat_WhenConversionIsNotAvailable_ThenReturnsFalse()
        {
            // Actors
            float parsed;

            // Activity
            var result = "12,5".TryParseFromInvariantCultureToFloat(out parsed);

            // Asserts
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(-123456.789000000F, "-123456-789")]
        [InlineData(0006.0000000002F, "6-0000000002")]
        [InlineData(0.0000000000000001000000000F, "0-0000000000000001")]
        [InlineData(123000F, "123000")]
        public void TryParseFromCurrentCultureToFloat_WhenConversionIsAvailable_ThenConversionIsDone(float floatValue, string stringValue)
        {
            // Actors
            float parsed;

            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithDash;

            // Activity
            var result = stringValue.TryParseFromCurrentCultureToFloat(out parsed);

            // Asserts
            result.Should().BeTrue();
            parsed.Should().Be(floatValue);
        }

        [Fact]
        public void TryParseFromCurrentCultureToFloat_WhenConversionIsNotAvailable_ThenReturnsFalse()
        {
            // Actors
            float parsed;

            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithDash;

            // Activity
            var result = "12.5".TryParseFromCurrentCultureToFloat(out parsed);

            // Asserts
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(-123456.789000000F, "-123456,789")]
        [InlineData(0006.0000000002F, "6,0000000002")]
        [InlineData(0.0000000000000001000000000F, "0,0000000000000001")]
        [InlineData(123000F, "123000")]
        public void TryParseToFloat_WhenConversionIsAvailable_ThenConversionIsDone(float floatValue, string stringValue)
        {
            // Actors
            float parsed;

            // Activity
            var result = stringValue.TryParseToFloat(CultureWithComma, out parsed);

            // Asserts
            result.Should().BeTrue();
            parsed.Should().Be(floatValue);
        }

        [Fact]
        public void TryParseToFloat_WhenConversionIsNotAvailable_ThenReturnsFalse()
        {
            // Actors
            float parsed;

            // Activity
            var result = "12.5".TryParseToFloat(CultureWithComma, out parsed);

            // Asserts
            result.Should().BeFalse();
        }
    }
}