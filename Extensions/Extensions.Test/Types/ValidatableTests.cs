//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableTests.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Extensions.Test.Types
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Extensions.Types;

    using FluentAssertions;

    using Xunit;

    public class ValidatableTests
    {
        private const string ParsedText = "ParsedText";

        private readonly Validatable<double> validatableDbl;

        private readonly Validatable<bool> validatableBool;

        public ValidatableTests()
        {
            this.validatableDbl = new Validatable<double>(ParsedText);
            this.validatableBool = new Validatable<bool>(ParsedText);
        }

        [Fact]
        public void Constructor_WhenParsedtextIsProvided_ThenParsedTextPropertyIsSetAndInternalValueIsNull()
        {
            // Actors

            // Activity

            // Asserts
            Assert.Equal(ParsedText, this.validatableDbl.ParsedText);
            Assert.False(this.validatableDbl.IsValid);
            Assert.Equal(ParsedText, this.validatableBool.ParsedText);
            Assert.False(this.validatableBool.IsValid);
        }

        [Fact]
        public void Constructor_WhenValueIsProvided_ThenInternalValueIsEquals()
        {
            // Actors

            // Activity
            var wrapper = new Validatable<int>(12);

            // Asserts
            Assert.Equal(12, wrapper.Value);
        }

        [Fact]
        public void IsValid_WhenInternalValueIsNull_ThenResultIsFalse()
        {
            // Actors

            // Activity
            this.validatableDbl.InternalValue = null;
            this.validatableBool.InternalValue = null;

            // Asserts
            Assert.False(this.validatableDbl.IsValid);
            Assert.False(this.validatableBool.IsValid);
        }

        [Fact]
        public void IsValid_WhenInternalValueIsNotNull_ThenResultIsTrue()
        {
            // Actors

            // Activity
            this.validatableDbl.InternalValue = 12.5;
            this.validatableBool.InternalValue = false;

            // Asserts
            Assert.True(this.validatableDbl.IsValid);
            Assert.True(this.validatableBool.IsValid);
        }

        [Fact]
        public void Value_WhenInternalValueIsNull_ThenThrowException()
        {
            // Actors

            // Activity
            this.validatableDbl.InternalValue = null;
            this.validatableBool.InternalValue = null;

            // Asserts
            Assert.Throws<InvalidOperationException>(() => this.validatableDbl.Value);
            Assert.Throws<InvalidOperationException>(() => this.validatableBool.Value);
        }

        [Fact]
        public void Value_WhenInternalValueIsNotNull_ThenResultIsValue()
        {
            // Actors

            // Activity
            this.validatableDbl.InternalValue = 12.5;
            this.validatableBool.InternalValue = false;

            // Asserts
            Assert.Equal(12.5, this.validatableDbl.Value);
            Assert.Equal(false, this.validatableBool.Value);
        }

        [Fact]
        public void ImplicitConversionToValue_WhenWrapperIsValid_ThenReturnWrapperValue()
        {
            // Actors
            const double InternalValueDbl = 12.5;
            const bool InternalValueBool = true;
            this.validatableDbl.InternalValue = InternalValueDbl;
            this.validatableBool.InternalValue = InternalValueBool;

            // Activity
            double resultDbl = this.validatableDbl;
            bool resultBool = this.validatableBool;

            // Asserts
            Assert.Equal(InternalValueDbl, resultDbl);
            Assert.Equal(InternalValueBool, resultBool);
        }

        [Fact]
        public void ImplicitConversionToValue_WhenWrapperIsNotValid_ThenThrowsException()
        {
            // Actors
            double resultDbl;
            bool resultBool;

            // Activity & Assert
            Assert.Throws<InvalidOperationException>(() => resultDbl = this.validatableDbl);
            Assert.Throws<InvalidOperationException>(() => resultBool = this.validatableBool);
        }

        [Fact]
        public void ImplicitConversionFromValue_AlwaysReturnValidWrapper()
        {
            // Actors
            const double InternalValueDbl = 12.5;
            const bool InternalValueBool = true;

            // Activity
            Validatable<double> resultDbl = InternalValueDbl;
            Validatable<bool> resultBool = InternalValueBool;

            // Asserts
            Assert.True(resultDbl.IsValid);
            Assert.Equal(InternalValueDbl, resultDbl.Value);
            Assert.True(resultBool.IsValid);
            Assert.Equal(InternalValueBool, resultBool.Value);
        }

        [Fact]
        public void ImplicitConversionFromNullableValueWithValue_AlwaysReturnValidWrapper()
        {
            // Actors
            double? internalValue = 125;

            // Activity
            Validatable<double> result = internalValue;

            // Asserts
            Assert.True(result.IsValid);
            Assert.Equal(internalValue.Value, result.Value);
        }

        [Fact]
        public void ImplicitConversionFromNullableValueWithNoValue_AlwaysReturnInvalidWrapper()
        {
            // Actors

            // Activity
            Validatable<double> result = null as double?;

            // Asserts
            Assert.False(result.IsValid);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsValid_ThenReturnsValidObject()
        {
            // Actors
            var value1 = (-12.5D).ToString();

            // Activity
            Validatable<double> result = value1;

            // Asserts
            result.Should().NotBeNull();
            result.Value.Should().Be(-12.5);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsNotValidInCurrentCultureButInInvariantCulture_ThenReturnsValidObject()
        {
            // Actors
            const string Value1 = "-12,512.5";

            // Activity
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            Validatable<double> result = Value1;

            // Asserts
            result.Should().NotBeNull();
            result.Value.Should().Be(-12512.5);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsValidInCurrentCultureButNotInInvariantCulture_ThenReturnsInvalidObject()
        {
            // Actors
            const string Value1 = "-12/5";

            var culture = new CultureInfo("fr-FR");
            culture.NumberFormat.NumberDecimalSeparator = "/";

            System.Threading.Thread.CurrentThread.CurrentCulture = culture;

            // Activity
            Validatable<double> result = Value1;

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
            Validatable<double> result = value;

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
            Validatable<double> result = Value1;

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

            // Activity
            this.validatableBool.InternalValue = null;

            // Asserts
            Assert.Throws<InvalidOperationException>(() => result = this.validatableBool);
        }

        [Fact]
        public void ImplicitConversionToString_WhenInternalValueIsNotNull_ThenResultIsStringValue()
        {
            // Actors
            string resultBool;
            string resultDbl;

            const double Value = 44.372;

            this.validatableBool.InternalValue = true;
            this.validatableDbl.InternalValue = Value;

            // Activity
            resultBool = this.validatableBool;
            resultDbl = this.validatableDbl;

            // Asserts
            resultBool.Should().Be(true.ToString());
            resultDbl.Should().Be(Value.ToString());
        }
        
        [Fact]
        public void OperatorEquals_WhenValidatableIsNull_ThenReturnFalse()
        {
            // Actors
            const bool InternalValueBool = true;

            // Activity
            var result = (Validatable<bool>)null == InternalValueBool;

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void OperatorEquals_WhenValidatableIsNotValid_ThenReturnFalse()
        {
            // Actors

            // Activity
            var result = this.validatableDbl == 12.5;

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void OperatorEquals_WhenValuesAreEqual_ThenReturnTrue()
        {
            // Actors
            this.validatableDbl.InternalValue = 12.5;

            // Activity
            var result = this.validatableDbl == 12.5;

            // Asserts
            Assert.True(result);
        }

        [Fact]
        public void OperatorEquals_WhenValuesAreNotEqual_ThenReturnFalse()
        {
            // Actors
            this.validatableDbl.InternalValue = 127000.568;

            // Activity
            var result = this.validatableDbl == 12.5;

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void OperatorNotEquals_WhenValidatableIsNull_ThenReturnTrue()
        {
            // Actors
            const bool InternalValueBool = true;

            // Activity
            var result = (Validatable<bool>)null != InternalValueBool;

            // Asserts
            Assert.True(result);
        }

        [Fact]
        public void OperatorNotEquals_WhenValidatableIsNotValid_ThenReturnTrue()
        {
            // Actors

            // Activity
            var result = this.validatableDbl != 12.5;
            
            // Asserts
            Assert.True(result);
        }

        [Fact]
        public void OperatorNotEquals_WhenValuesAreEqual_ThenReturnFalse()
        {
            // Actors
            this.validatableDbl.InternalValue = 12.5;

            // Activity
            var result = this.validatableDbl != 12.5;

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void OperatorNotEquals_WhenValuesAreNotEqual_ThenReturnTrue()
        {
            // Actors
            this.validatableDbl.InternalValue = 127000.568;

            // Activity
            var result = this.validatableDbl != 12.5;

            // Asserts
            Assert.True(result);
        }

        [Fact]
        public void GetHashCode_WhenValidatableIsValid_ThenReturnsValueHash()
        {
            // Actors
            this.validatableDbl.InternalValue = 12.5;

            // Activity
            var result = this.validatableDbl.GetHashCode();

            // Asserts
            Assert.Equal(12.5.GetHashCode(), result);
        }

        [Fact]
        public void GetHashCode_WhenValidatableIsNotValid_ThenReturnsParsedTextHash()
        {
            // Actors

            // Activity
            var result = this.validatableDbl.GetHashCode();

            // Asserts
            Assert.Equal(ParsedText.GetHashCode(), result);
        }

        [Fact]
        public void ToString_WhenInternalValueIsNull_ThenThrowException()
        {
            // Actors

            // Activity
            this.validatableDbl.InternalValue = null;

            // Asserts
            Assert.Throws<InvalidOperationException>(() => this.validatableDbl.ToString());
        }

        [Fact]
        public void ToString_WhenInternalValueIsNotNull_ThenResultIsStringValue()
        {
            // Actors
            const double Value = 12.5;
            
            // Activity
            this.validatableDbl.InternalValue = Value;

            // Asserts
            Assert.Equal(Value.ToString(), this.validatableDbl.ToString());
        }

        [Fact]
        public void Equals_WhenInstance2IsNull_ThenReturnFalse()
        {
            // Actors
            this.validatableDbl.InternalValue = 12.5;

            // Activity
            var result = this.validatableDbl.Equals(null);

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void Equals_WhenBothAreTheSameInstance_ThenReturnTrue()
        {
            // Actors

            // Activity
            var result = this.validatableDbl.Equals(this.validatableDbl);

            // Asserts
            Assert.True(result);
        }

        [Fact]
        public void Equals_WhenInstance1IsNotValid_ThenReturnFalse()
        {
            // Actors
            var other = new Validatable<double>(ParsedText) { InternalValue = 12.5 };

            // Activity
            var result = this.validatableDbl.Equals(other);

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void Equals_WhenInstance2IsNotSameType_ThenReturnFalse()
        {
            // Actors
            var other = new object();

            // Activity
            var result = this.validatableDbl.Equals(other);

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void Equals_WhenInstance2IsNotValid_ThenReturnFalse()
        {
            // Actors
            this.validatableDbl.InternalValue = 12.5;
            var other = new Validatable<double>(ParsedText);

            // Activity
            var result = this.validatableDbl.Equals(other);

            // Asserts
            Assert.False(result);
        }

        [Theory]
        [InlineData(12.5, 12.5, true)]
        [InlineData(12.5, 666, false)]
        public void Equals_WhenInstancesAreValidAndValuesAreProvided_ThenReturnValuesComparison(
            double value1,
            double value2,
            bool isEquals)
        {
            // Actors
            this.validatableDbl.InternalValue = value1;
            var other = new Validatable<double>(ParsedText) { InternalValue = value2 };

            // Activity
            var result = this.validatableDbl.Equals(other);

            // Asserts
            Assert.Equal(isEquals, result);
        }

        [Fact]
        public void CompareTo_WhenObjectToCompareIsNotValidatable_ThenReturnsMinusOne()
        {
            // Actors
            var source = new Validatable<int>(12);

            // Activity
            var result = source.CompareTo(new object());

            // Asserts
            result.Should().Be(-1);
        }

        [Fact]
        public void CompareTo_WhenValidatableToCompareIsNotValid_ThenReturnsMinusOne()
        {
            // Actors
            var source = new Validatable<int>(12);
            Validatable<int> comparison = "BadValue";

            // Activity
            var result = source.CompareTo(comparison);

            // Asserts
            result.Should().Be(-1);
        }

        [Fact]
        public void CompareTo_WhenValidatableToCompareIsValidButSourceIsNot_ThenReturnsOne()
        {
            // Actors
            Validatable<int> source = "BadValue";
            var comparison = new Validatable<int>(12);

            // Activity
            var result = source.CompareTo(comparison);

            // Asserts
            result.Should().Be(1);
        }

        [Theory]
        [InlineData(12, 13, -1)]
        [InlineData(13, 12, 1)]
        [InlineData(13, 13, 0)]
        public void CompareTo_WhenBothValidatableAreValid_ThenReturnsComparisonBetweenValues(int sourceValue, int comparisonValue, int expectedResult)
        {
            // Actors
            Validatable<int> source = sourceValue;
            Validatable<int> comparison = comparisonValue;

            // Activity
            var result = source.CompareTo(comparison);

            // Asserts
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void CompareTo_WhenSorting_ThenListIsSorted()
        {
            // Actors
            Validatable<int> val1 = 1;
            Validatable<int> val2 = 2;
            Validatable<int> val3 = 3;

            var sourceObject1 = new FakeObject { ComparisonProperty = val1 };
            var sourceObject2 = new FakeObject { ComparisonProperty = val2 };
            var sourceObject3 = new FakeObject { ComparisonProperty = val3 };

            var source = new[] { sourceObject3, sourceObject2, sourceObject1 };

            // Activity
            var result = source.OrderBy(so => so.ComparisonProperty).ToList();

            // Asserts
            result.Should().ContainInOrder(sourceObject1, sourceObject2, sourceObject3);
        }

        private class FakeObject
        {
            public Validatable<int> ComparisonProperty { get; set; }
        }
    }
}