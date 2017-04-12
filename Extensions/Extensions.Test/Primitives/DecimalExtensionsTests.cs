//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DecimalExtensionsTests.cs" company="Eurofins">
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

    public class DecimalExtensionsTests
    {
        private static readonly CultureInfo CultureWithComma;

        private static readonly CultureInfo CultureWithDash;
        
        static DecimalExtensionsTests()
        {
            CultureWithComma = new CultureInfo("fr-FR") { NumberFormat = { NumberDecimalSeparator = "," } };
            CultureWithDash = new CultureInfo("fr-FR") { NumberFormat = { NumberDecimalSeparator = "-" } };
        }

        public static IEnumerable<object[]> GetDecimalInvariant()
        {
            yield return new object[] { -123456.789000000M, "-123456.789" };
            yield return new object[] { 0006.0000000002M, "6.0000000002" };
            yield return new object[] { 0.00000001000M, "0.00000001" };
            yield return new object[] { 123000M, "123000" };
        }

        public static IEnumerable<object[]> GetDecimalDash()
        {
            yield return new object[] { -123456.789000000M, "-123456-789" };
            yield return new object[] { 0006.0000000002M, "6-0000000002" };
            yield return new object[] { 0.00000001000M, "0-00000001" };
            yield return new object[] { 123000M, "123000" };
        }

        public static IEnumerable<object[]> GetDecimalComma()
        {
            yield return new object[] { -123456.789000000M, "-123456,789" };
            yield return new object[] { 0006.0000000002M, "6,0000000002" };
            yield return new object[] { 0.00000001000M, "0,00000001" };
            yield return new object[] { 123000M, "123000" };
        }
        
        [Fact]
        public void ToInvariantCultureString_WhenNullableDecimalIsNull_ThenThrowException()
        {
            // Actors
            decimal? value = null;

            // Activity
            Action action = () => value.ToInvariantCultureString();

            // Asserts
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ToCurrentCultureString_WhenNullableDecimalIsNull_ThenThrowException()
        {
            // Actors
            decimal? value = null;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithDash;

            // Activity
            Action action = () => value.ToCurrentCultureString();

            // Asserts
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ToFormattedString_WhenNullableDecimalIsNull_ThenThrowException()
        {
            // Actors
            decimal? value = null;

            // Activity
            Action action = () => value.ToFormattedString(CultureWithComma);

            // Asserts
            action.ShouldThrow<ArgumentNullException>();
        }

        [Theory, MemberData("GetDecimalInvariant")]
        public void ToInvariantCultureString_WhenNullableDecimalIsNotNull_ThenResultIsFormatted(decimal value, string expected)
        {
            // Actors

            // Activity
            var result = value.ToInvariantCultureString();

            // Asserts
            result.Should().Be(expected);
        }

        [Theory, MemberData("GetDecimalDash")]
        public void ToCurrentCultureString_WhenNullableDecimalIsNotNull_ThenResultIsFormattedWithCurrentCulture(decimal valueNotNullable, string expected)
        {
            // Actors
            decimal? value = valueNotNullable;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithDash;

            // Activity
            var result = value.ToCurrentCultureString();

            // Asserts
            result.Should().Be(expected);
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        [Theory, MemberData("GetDecimalComma")]
        public void ToFormattedString_WhenNullableDecimalIsNotNull_ThenResultIsFormattedWithCulture(decimal valueNotNullable, string expected)
        {
            // Actors
            decimal? value = valueNotNullable;

            // Activity
            var result = value.ToFormattedString(CultureWithComma);

            // Asserts
            result.Should().Be(expected);
        }

        [Theory, MemberData("GetDecimalInvariant")]
        public void ToInvariantCultureString_WhenDecimalIsNotNullable_ThenResultIsFormatted(decimal value, string expected)
        {
            // Actors

            // Activity
            var result = value.ToInvariantCultureString();

            // Asserts
            result.Should().Be(expected);
        }

        [Theory, MemberData("GetDecimalDash")]
        public void ToCurrentCultureString_WhenDecimalIsNotNullable_ThenResultIsFormattedWithCurrentCulture(decimal value, string expected)
        {
            // Actors
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithDash;

            // Activity
            var result = value.ToCurrentCultureString();

            // Asserts
            result.Should().Be(expected);
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        [Theory, MemberData("GetDecimalComma")]
        public void ToFormattedString_WhenDecimalIsNotNullable_ThenResultIsFormattedWithCulture(decimal value, string expected)
        {
            // Actors

            // Activity
            var result = value.ToFormattedString(CultureWithComma);

            // Asserts
            result.Should().Be(expected);
        }

        [Theory, MemberData("GetDecimalInvariant")]
        public void ParseFromInvariantCultureToDecimal_WhenConversionIsAvailableWithFloatingPointAsDot_ThenConversionIsDone(decimal decimalValue, string stringValue)
        {
            // Actors

            // Activity
            var result = stringValue.ParseFromInvariantCultureToDecimal();

            // Asserts
            result.Should().Be(decimalValue);
        }

        [Theory, MemberData("GetDecimalDash")]
        public void ParseFromCurrentCultureToDecimal_WhenConversionIsAvailableWithFloatingPointAsCurrentCultureSeparator_ThenConversionIsDone(decimal decimalValue, string stringValue)
        {
            // Actors
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithDash;

            // Activity
            var result = stringValue.ParseFromCurrentCultureToDecimal();

            // Asserts
            result.Should().Be(decimalValue);
        }

        [Theory, MemberData("GetDecimalComma")]
        public void ParseToDecimal_WhenConversionIsAvailableWithFloatingPointAsProvidedCultureSeparator_ThenConversionIsDone(decimal decimalValue, string stringValue)
        {
            // Actors

            // Activity
            var result = stringValue.ParseToDecimal(CultureWithComma);

            // Asserts
            result.Should().Be(decimalValue);
        }

        [Theory, MemberData("GetDecimalInvariant")]
        public void TryParseFromInvariantCultureToDecimal_WhenConversionIsAvailableWithFloatingPointAsDot_ThenConversionIsDone(decimal decimalValue, string stringValue)
        {
            // Actors
            decimal parsed;

            // Activity
            var result = stringValue.TryParseFromInvariantCultureToDecimal(out parsed);

            // Asserts
            result.Should().BeTrue();
            parsed.Should().Be(decimalValue);
        }
        
        [Fact]
        public void TryParseFromInvariantCultureToDecimal_WhenConversionIsNotAvailable_ThenReturnsFalse()
        {
            // Actors
            decimal parsed;

            // Activity
            var result = "12,5".TryParseFromInvariantCultureToDecimal(out parsed);

            // Asserts
            result.Should().BeFalse();
        }

        [Theory, MemberData("GetDecimalDash")]
        public void TryParseFromCurrentCultureToDecimal_WhenConversionIsAvailable_ThenConversionIsDone(decimal decimalValue, string stringValue)
        {
            // Actors
            decimal parsed;

            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithDash;

            // Activity
            var result = stringValue.TryParseFromCurrentCultureToDecimal(out parsed);

            // Asserts
            result.Should().BeTrue();
            parsed.Should().Be(decimalValue);
        }

        [Fact]
        public void TryParseFromCurrentCultureToDecimal_WhenConversionIsNotAvailable_ThenReturnsFalse()
        {
            // Actors
            decimal parsed;

            System.Threading.Thread.CurrentThread.CurrentCulture = CultureWithDash;

            // Activity
            var result = "12.5".TryParseFromCurrentCultureToDecimal(out parsed);

            // Asserts
            result.Should().BeFalse();
        }

        [Theory, MemberData("GetDecimalComma")]
        public void TryParseToDecimal_WhenConversionIsAvailable_ThenConversionIsDone(decimal decimalValue, string stringValue)
        {
            // Actors
            decimal parsed;
            
            // Activity
            var result = stringValue.TryParseToDecimal(CultureWithComma, out parsed);

            // Asserts
            result.Should().BeTrue();
            parsed.Should().Be(decimalValue);
        }

        [Fact]
        public void TryParseToDecimal_WhenConversionIsNotAvailable_ThenReturnsFalse()
        {
            // Actors
            decimal parsed;
            
            // Activity
            var result = "12.5".TryParseToDecimal(CultureWithComma, out parsed);

            // Asserts
            result.Should().BeFalse();
        }
    }
}