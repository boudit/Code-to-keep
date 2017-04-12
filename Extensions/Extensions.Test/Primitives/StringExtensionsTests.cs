//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="StringExtensionsTests.cs" company="Eurofins">
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

    public class StringExtensionsTests
    {
        public static IEnumerable<object[]> AllStringVariants
        {
            get
            {
                return new[]
                    {
                        new[] { string.Empty },
                        new[] { (string)null },
                        new[] { " " },
                        new[] { "  " },
                        new[] { "a" },
                        new[] { "aalmzrfgbomajbzermgvjklbazmefvbamezo" },
                        new[] { "      &" },
                        new[] { "      &    " },
                        new[] { "&     " },
                    };
            }
        }

        public static IEnumerable<object[]> StringTrimCombinations
        {
            get
            {
                yield return new object[] { null, null };
                yield return new object[] { string.Empty, string.Empty };
                yield return new object[] { " ", string.Empty };
                yield return new object[] { "     ", string.Empty };
                yield return new object[] { "a", "a" };
                yield return new object[] { " a ", "a" };
                yield return new object[] { " a", "a" };
                yield return new object[] { "     a", "a" };
                yield return new object[] { "a ", "a" };
                yield return new object[] { "a  ", "a" };
                yield return new object[] { "a     ", "a" };
                yield return new object[] { "a b", "a b" };
                yield return new object[] { " a b", "a b" };
                yield return new object[] { "a b ", "a b" };
                yield return new object[] { "   a       b    ", "a b" };
                yield return new object[] { "a       b", "a b" };
            }
        }

        [Theory]
        [MemberData("AllStringVariants")]
        public void IsNullOrWhiteSpace_WhenCalled_ShouldCallStringClassMethod(
            string inputString)
        {
            // Activities
            var result = inputString.IsNullOrWhiteSpace();

            // Assertions
            result.Should().Be(string.IsNullOrWhiteSpace(inputString));
        }

        [Theory]
        [MemberData("AllStringVariants")]
        public void IsNullOrEmpty_WhenCalled_ShouldCallStringClassMethod(
            string inputString)
        {
            // Activities
            var result = inputString.IsNullOrEmpty();

            // Assertions
            result.Should().Be(string.IsNullOrEmpty(inputString));
        }

        [Theory]
        [InlineData("Toto")]
        [InlineData(42)]
        [InlineData(true)]
        public void Format_WhenCalledWithOneParameter_ShouldCallStringClassMethod(
            object inputArg)
        {
            // Actors
            const string ToOperate = @"{0} mange des dauphins";

            // Activities
            var result = ToOperate.Format(inputArg);

            // Assertions
            result.Should().Be(string.Format(ToOperate, inputArg));
        }

        [Theory]
        [InlineData("Toto", 42)]
        [InlineData(42, true)]
        [InlineData(true, "Toto")]
        public void Format_WhenCalledWithTwoParameter_ShouldCallStringClassMethod(
            object inputArg1, object inputArg2)
        {
            // Actors
            const string ToOperate = @"{0} mange {1} dauphins";

            // Activities
            var result = ToOperate.Format(inputArg1, inputArg2);

            // Assertions
            result.Should().Be(string.Format(ToOperate, inputArg1, inputArg2));
        }

        [Theory]
        [InlineData("Toto", 42, true)]
        [InlineData(42, true, "toto")]
        [InlineData(true, "Toto", 42)]
        public void Format_WhenCalledWithThreeParameter_ShouldCallStringClassMethod(
            object inputArg1, object inputArg2, object inputArg3)
        {
            // Actors
            const string ToOperate = @"{0} mange {1} dauphins mais sans sel, parce que {2}";

            // Activities
            var result = ToOperate.Format(inputArg1, inputArg2, inputArg3);

            // Assertions
            result.Should().Be(string.Format(ToOperate, inputArg1, inputArg2, inputArg3));
        }

        [Theory]
        [InlineData("Toto", 42, true)]
        [InlineData(42, true, "toto")]
        [InlineData(true, "Toto", 42)]
        public void Format_WhenCalledWithObjectEnumerableParameter_ShouldCallStringClassMethod(
            object inputArg1, object inputArg2, object inputArg3)
        {
            // Actors
            const string ToOperate = @"{0} mange {1} dauphins mais sans sel, parce que {2}";
            var parameters = new object[] { inputArg1, inputArg2, inputArg3 };

            // Activities
            var result = ToOperate.Format(parameters);

            // Assertions
            result.Should().Be(string.Format(ToOperate, parameters));
        }

        [Theory]
        [MemberData("StringTrimCombinations")]
        public void TrimAndSuppressRedundantSpaces_WhenCalled_ShouldBahaveAsTrimStringMethodAndSuppressRedundantSpacesInsideTheString(
            string givenString, string expectedString)
        {
            // Activities
            var result = givenString.TrimAndSuppressRedundantSpaces();

            // Assertions
            result.Should().Be(expectedString);
        }

        [Fact]
        public void RemoveDiacritics_WhenStringHasAccents_ThenRemoveThem()
        {
            // Actors
            const string Value = "Vàlùé";
            const string ExpectedValue = "Value";

            // Activity
            var returnedValue = Value.RemoveDiacritics();

            // Asserts
            returnedValue.Should().BeEquivalentTo(ExpectedValue);
        }

        [Fact]
        public void RemoveDiacritics_WhenStringHasNoAccent_ThenValueDoesNotChange()
        {
            // Actors
            const string Value = "Value";

            // Activity
            var returnedValue = Value.RemoveDiacritics();

            // Asserts
            returnedValue.Should().BeEquivalentTo(Value);
        }

        [Fact]
        public void ContainsInsensitive_WhenFirstAndSecondStringsAreDifferents_ThenReturnsFalse()
        {
            // Actors
            const string Value1 = "Value1";
            const string Value2 = "Other";

            // Activity
            var contains = Value1.ContainsInsensitive(Value2);

            // Asserts
            contains.Should().BeFalse();
        }

        [Fact]
        public void ContainsInsensitive_WhenFirstAndSecondStringsAreEquals_ThenReturnsTrue()
        {
            // Actors
            const string Value1 = "Value";
            const string Value2 = "Value";

            // Activity
            var contains = Value1.ContainsInsensitive(Value2);

            // Asserts
            contains.Should().BeTrue();
        }

        [Fact]
        public void ContainsInsensitive_WhenFirstHasAccentsAndContainsSecondWithAccents_ThenReturnsTrue()
        {
            // Actors
            const string Value1 = "Vàlue";
            const string Value2 = "Và";

            // Activity
            var contains = Value1.ContainsInsensitive(Value2);

            // Asserts
            contains.Should().BeTrue();
        }

        [Fact]
        public void ContainsInsensitive_WhenFirstHasAccentsAndContainsSecondWithNoAccent_ThenReturnsTrue()
        {
            // Actors
            const string Value1 = "Vàlue";
            const string Value2 = "Va";

            // Activity
            var contains = Value1.ContainsInsensitive(Value2);

            // Asserts
            contains.Should().BeTrue();
        }

        [Fact]
        public void
            ContainsInsensitive_WhenFirstIsUpperCaseWithAccentsAndContainsSecondLowerCaseWithNoAccent_ThenReturnsTrue()
        {
            // Actors
            const string Value1 = "VÀLÛË";
            const string Value2 = "alu";

            // Activity
            var contains = Value1.ContainsInsensitive(Value2);

            // Asserts
            contains.Should().BeTrue();
        }

        [Theory]
        [InlineData("Value", new[] { "some", "thing" })]
        [InlineData("Value", new[] { "val", "thing" })]
        [InlineData("Value", new[] { "val", "lue", "something" })]
        [InlineData("Value", new[] { "some", "lue" })]
        [InlineData("Value", new[] { "some", "" })]
        [InlineData("Value", new[] { "", "thing" })]
        [InlineData("Value", new[] { "val", "lue " })]
        [InlineData("Value", new[] { "val", "lue", " " })]
        [InlineData("Value", new[] { " v ", " a ", "l", "u", "e", "" })]
        public void ContainsInsensitive_WhenFirstStringDoesNotContainAllWordsToSeek_ThenReturnsFalse(string source, IEnumerable<string> textsToSeek)
        {
            // Actors

            // Activity
            var contains = source.ContainsInsensitive(textsToSeek);

            // Asserts
            contains.Should().BeFalse();
        }

        [Theory]
        [InlineData("Value", new[] { "val" })]
        [InlineData("Value", new[] { "val", "lue" })]
        [InlineData("Value", new[] { "val", "lue", "" })]
        [InlineData("Value", new[] { "v", "a", "l", "u", "e", "" })]
        public void ContainsInsensitive_WhenFirstStringContainsAllWordsToSeek_ThenReturnsTrue(string source, IEnumerable<string> textsToSeek)
        {
            // Actors

            // Activity
            var contains = source.ContainsInsensitive(textsToSeek);

            // Asserts
            contains.Should().BeTrue();
        }

        [Theory]
        [InlineData("Vàlue", new[] { "val" })]
        [InlineData("Vàlue", new[] { "val", "lue" })]
        [InlineData("Vàlue", new[] { "val", "lue", "" })]
        [InlineData("Vàlue", new[] { "v", "a", "l", "u", "e", "" })]
        public void ContainsInsensitive_WhenFirstStringHasAccentAndContainsAllWordsToSeek_ThenReturnsTrue(string source, IEnumerable<string> textsToSeek)
        {
            // Actors

            // Activity
            var contains = source.ContainsInsensitive(textsToSeek);

            // Asserts
            contains.Should().BeTrue();
        }

        [Theory]
        [InlineData("Vàlue", new[] { "väl" })]
        [InlineData("Vàlue", new[] { "väl", "lué" })]
        [InlineData("Vàlue", new[] { "väl", "lué", "" })]
        [InlineData("Vàlue", new[] { "v", "ä", "l", "u", "é", "" })]
        public void ContainsInsensitive_WhenFirstStringHasNotAccentAndContainsAllWordsWithAccents_ThenReturnsTrue(string source, IEnumerable<string> textsToSeek)
        {
            // Actors

            // Activity
            var contains = source.ContainsInsensitive(textsToSeek);

            // Asserts
            contains.Should().BeTrue();
        }

        [Theory]
        [InlineData("Value", "something", " ", true)]
        [InlineData("Value", "some thing", " ", true)]
        [InlineData("Value", "val thing", " ", true)]
        [InlineData("Value", "some lue", " ", true)]
        [InlineData("Value", "value something", " ", true)]
        [InlineData("Value", "    value     something     ", " ", true)]
        [InlineData("Value", "something", " ", false)]
        [InlineData("Value", "some thing", " ", false)]
        [InlineData("Value", "val thing", " ", false)]
        [InlineData("Value", "some lue", " ", false)]
        [InlineData("Value", "value something", " ", false)]
        [InlineData("Value", "    value     something     ", " ", false)]
        [InlineData("Value", "m val m lue", "m", false)]
        public void ContainsInsensitive_WhenFirstStringDoesNotContainAllWordsToSeekOrWordsAreNotProperlyTrimmed_ThenReturnsFalse(string source, string wordsToSeek, string separator, bool trimWords)
        {
            // Actors

            // Activity
            var contains = source.ContainsInsensitive(wordsToSeek, separator, trimWords);

            // Asserts
            contains.Should().BeFalse();
        }

        [Theory]
        [InlineData("Value", "value", " ", true)]
        [InlineData("Value", " val  lue ", " ", true)]
        [InlineData("Value", "      val  lue      ", " ", true)]
        [InlineData("Value", " v          a l u e ", " ", true)]
        [InlineData("Value", "m val m lue ", "m", true)]
        [InlineData("Value", "mvalmlue", "m", false)]
        public void ContainsInsensitive_WhenFirstStringContainsAllWordsSeekOrWordsAreProperlyTrimmed_ThenReturnsTrue(string source, string wordsToSeek, string separator, bool trimWords)
        {
            // Actors

            // Activity
            var contains = source.ContainsInsensitive(wordsToSeek, separator, trimWords);

            // Asserts
            contains.Should().BeTrue();
        }

        [Theory]
        [InlineData("Vàlué", "value", " ", true)]
        [InlineData("Vàlué", " val  lue ", " ", true)]
        [InlineData("Vàlué", "      val  lue      ", " ", true)]
        [InlineData("Vàlué", " v          a l u e ", " ", true)]
        [InlineData("Vàlué", "m val m lue ", "m", true)]
        [InlineData("Vàlué", "mvalmlue", "m", false)]
        public void ContainsInsensitive_WhenFirstStringHasAccentAndContainsAllWordsToSeekProperlyTrimmed_ThenReturnsTrue(string source, string wordsToSeek, string separator, bool trimWords)
        {
            // Actors

            // Activity
            var contains = source.ContainsInsensitive(wordsToSeek, separator, trimWords);

            // Asserts
            contains.Should().BeTrue();
        }

        [Theory]
        [InlineData("Vàlué", "välûé", " ", true)]
        [InlineData("Vàlué", " väl  lué ", " ", true)]
        [InlineData("Vàlué", "      väl  lûe      ", " ", true)]
        [InlineData("Vàlué", " v          ä l û è ", " ", true)]
        [InlineData("Vàlué", "m väl m lûe ", "m", true)]
        [InlineData("Vàlué", "mvälmlûe", "m", false)]
        public void ContainsInsensitive_WhenFirstStringHasNotAccentAndContainsAllWordsWithAccentsProperlyTrimmed_ThenReturnsTrue(string source, string wordsToSeek, string separator, bool trimWords)
        {
            // Actors

            // Activity
            var contains = source.ContainsInsensitive(wordsToSeek, separator, trimWords);

            // Asserts
            contains.Should().BeTrue();
        }
        
        [Fact]
        public void TryConvert_WhenConversionIsAvailable_ThenReturnsTrueAndValueIsConverted()
        {
            // Actors
            const string Value1 = "123";
            int resultValue;

            // Activity
            var result = Value1.TryConvert(out resultValue);

            // Asserts
            result.Should().BeTrue();
            resultValue.Should().Be(123);
        }

        [Fact]
        public void TryConvert_WhenConversionIsNotAvailable_ThenReturnsFalseAndValueIsNotConverted()
        {
            // Actors
            const string Value1 = "Toto";
            int resultValue = 0;

            // Activity
            var result = Value1.TryConvert(out resultValue);

            // Asserts
            result.Should().BeFalse();
            resultValue.Should().Be(0);
        }

        [Theory]
        [InlineData("False", false)]
        [InlineData("false", false)]
        [InlineData("FALSE", false)]
        [InlineData("True", true)]
        [InlineData("true", true)]
        [InlineData("TRUE", true)]
        public void TryConvert_WhenConversionIsAvailable_ThenConversionIsNotCaseSensitive(string stringValue, bool expectedValue)
        {
            // Actors
            bool resultValue;

            // Activity
            var result = stringValue.TryConvert(out resultValue);

            // Asserts
            result.Should().BeTrue();
            resultValue.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("12.5", 12.5)]
        [InlineData("12", 12)]
        [InlineData("-12", -12)]
        [InlineData("-12.5", -12.5)]
        public void TryConvert_WhenConversionIsAvailableWithFloatingPointAsDot_ThenConversionIsDone(string stringValue, double expectedValue)
        {
            // Actors
            double resultValue = 0;

            // Activity
            var result = stringValue.TryConvert(out resultValue);

            // Asserts
            result.Should().BeTrue();
            resultValue.Should().Be(expectedValue);
        }
        
        [Theory]
        [InlineData("12,5", 12.5)]
        [InlineData("-12,5", -12.5)]
        public void TryConvert_WhenConversionIsAvailableWithFloatingPointAsCommaAndFormatProviderDefinesItAsSeparator_ThenConversionIsDone(string stringValue, double expectedValue)
        {
            // Actors
            double resultValue = 0;
            IFormatProvider formatProvider = new NumberFormatInfo { NumberDecimalSeparator = "," };

            // Activity
            var result = stringValue.TryConvert(formatProvider, out resultValue);

            // Asserts
            result.Should().BeTrue();
            resultValue.Should().Be(expectedValue);
        }
    }
}