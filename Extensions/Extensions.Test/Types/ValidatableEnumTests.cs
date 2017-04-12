//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableEnumTests.cs" company="Eurofins">
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

    public class ValidatableEnumTests
    {
        private const string ParsedText = "ParsedText";

        private readonly ValidatableEnum<FakeEnum> validatableEnum;

        public ValidatableEnumTests()
        {
            this.validatableEnum = new ValidatableEnum<FakeEnum>(ParsedText);
        }

        [Fact]
        public void Constructor_WhenParsedtextIsProvided_ThenParsedTextPropertyIsSetAndInternalValueIsNull()
        {
            // Actors

            // Activity

            // Asserts
            Assert.Equal(ParsedText, this.validatableEnum.ParsedText);
            Assert.False(this.validatableEnum.IsValid);
        }

        [Fact]
        public void Constructor_WhenEnumValueIsProvided_ThenInternalValueIsEquals()
        {
            // Actors

            // Activity
            var bo = new ValidatableEnum<FakeEnum>(FakeEnum.Value1);

            // Asserts
            Assert.Equal(FakeEnum.Value1, bo.Value);
        }

        [Fact]
        public void IsValid_WhenInternalValueIsNull_ThenResultIsFalse()
        {
            // Actors

            // Activity
            this.validatableEnum.InternalValue = null;

            // Asserts
            Assert.False(this.validatableEnum.IsValid);
        }

        [Fact]
        public void IsValid_WhenInternalValueIsNotNull_ThenResultIsTrue()
        {
            // Actors

            // Activity
            this.validatableEnum.InternalValue = FakeEnum.Value1;

            // Asserts
            Assert.True(this.validatableEnum.IsValid);
        }

        [Fact]
        public void Value_WhenInternalValueIsNull_ThenThrowException()
        {
            // Actors

            // Activity
            this.validatableEnum.InternalValue = null;

            // Asserts
            Assert.Throws<InvalidOperationException>(() => this.validatableEnum.Value);
        }

        [Fact]
        public void Value_WhenInternalValueIsNotNull_ThenResultIsValueOfEnum()
        {
            // Actors

            // Activity
            this.validatableEnum.InternalValue = FakeEnum.Value1;

            // Asserts
            Assert.Equal(FakeEnum.Value1, this.validatableEnum.Value);
        }

        [Fact]
        public void StringValue_WhenInternalValueIsNull_ThenThrowException()
        {
            // Actors

            // Activity
            this.validatableEnum.InternalValue = null;

            // Asserts
            Assert.Throws<InvalidOperationException>(() => this.validatableEnum.StringValue);
        }

        [Fact]
        public void StringValue_WhenInternalValueIsNotNull_ThenResultIsStringValue()
        {
            // Actors
            var expectedValue = FakeEnum.Value1.ToString(CultureInfo.InvariantCulture);

            // Activity
            this.validatableEnum.InternalValue = FakeEnum.Value1;

            // Asserts
            Assert.Equal(expectedValue, this.validatableEnum.StringValue);
        }

        [Fact]
        public void ImplicitConversionToEnum_WhenBoIsValid_ThenReturnBoValue()
        {
            // Actors
            const FakeEnum EnumValue = FakeEnum.Value2;
            this.validatableEnum.InternalValue = EnumValue;

            // Activity
            FakeEnum result = this.validatableEnum;

            // Asserts
            Assert.Equal(EnumValue, result);
        }

        [Fact]
        public void ImplicitConversionToEnum_WhenBoIsNotValid_ThenThrowsException()
        {
            // Actors
            FakeEnum? result = null;

            // Activity & Assert
            Assert.Throws<InvalidOperationException>(() => result = this.validatableEnum);
        }

        [Fact]
        public void ImplicitConversionFromEnum_AlwaysReturnValidBo()
        {
            // Actors
            const FakeEnum EnumValue = FakeEnum.Value2;

            // Activity
            ValidatableEnum<FakeEnum> result = EnumValue;

            // Asserts
            Assert.True(result.IsValid);
            Assert.Equal(EnumValue, result.Value);
        }

        [Fact]
        public void ImplicitConversionFromNullableValueWithValue_AlwaysReturnValidWrapper()
        {
            // Actors
            FakeEnum? internalValue = FakeEnum.Value2;

            // Activity
            ValidatableEnum<FakeEnum> result = internalValue;

            // Asserts
            Assert.True(result.IsValid);
            Assert.Equal(internalValue.Value, result.Value);
        }

        [Fact]
        public void ImplicitConversionFromNullableValueWithNoValue_AlwaysReturnInvalidWrapper()
        {
            // Actors

            // Activity
            ValidatableEnum<FakeEnum> result = null as FakeEnum?;

            // Asserts
            Assert.False(result.IsValid);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsPartOfAnEnum_ThenReturnsValidEnumValue()
        {
            // Actors
            const string Value1 = "Value1";

            // Activity
            ValidatableEnum<FakeEnum> result = Value1;

            // Asserts
            result.Should().NotBeNull();
            result.Value.Should().Be(FakeEnum.Value1);
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsDescriptionOfAnEnum_ThenReturnsValidEnumValue()
        {
            // Actors
            const string Value2Description = "Value 2";

            // Activity
            ValidatableEnum<FakeEnum> result = Value2Description;

            // Asserts
            result.Should().NotBeNull();
            result.Value.Should().Be(FakeEnum.Value2);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ImplicitConversionFromString_WhenValueIsNullOrEmpty_ThenReturnsInvalidEnumValue(string value)
        {
            // Actors

            // Activity
            ValidatableEnum<FakeEnum> result = value;

            // Asserts
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ImplicitConversionFromString_WhenValueIsNotPartOfEnum_ThenReturnsInvalidEnumValue()
        {
            // Actors
            const string Value1 = "BadValue";

            // Activity
            ValidatableEnum<FakeEnum> result = Value1;

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
            this.validatableEnum.InternalValue = null;

            // Asserts
            Assert.Throws<InvalidOperationException>(() => result = this.validatableEnum);
        }

        [Fact]
        public void ImplicitConversionToString_WhenInternalValueIsNotNull_ThenResultIsStringValue()
        {
            // Actors
            string result;
            var expectedValue = FakeEnum.Value1.ToString(CultureInfo.InvariantCulture);
            this.validatableEnum.InternalValue = FakeEnum.Value1;

            // Activity
            result = this.validatableEnum;

            // Asserts
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void OperatorEquals_WhenBoIsNull_ThenReturnFalse()
        {
            // Actors
            const FakeEnum EnumValue = FakeEnum.Value2;

            // Activity
            var result = (ValidatableEnum<FakeEnum>)null == EnumValue;

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void OperatorEquals_WhenBoIsNotValid_ThenReturnFalse()
        {
            // Actors
            const FakeEnum EnumValue = FakeEnum.Value2;

            // Activity
            var result = this.validatableEnum == EnumValue;

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void OperatorEquals_WhenValuesAreEqual_ThenReturnTrue()
        {
            // Actors
            const FakeEnum EnumValue = FakeEnum.Value2;
            this.validatableEnum.InternalValue = EnumValue;

            // Activity
            var result = this.validatableEnum == EnumValue;

            // Asserts
            Assert.True(result);
        }

        [Fact]
        public void OperatorEquals_WhenValuesAreNotEqual_ThenReturnFalse()
        {
            // Actors
            const FakeEnum EnumValue = FakeEnum.Value2;
            this.validatableEnum.InternalValue = FakeEnum.Value1;

            // Activity
            var result = this.validatableEnum == EnumValue;

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void OperatorNotEquals_WhenBoIsNull_ThenReturnTrue()
        {
            // Actors
            const FakeEnum EnumValue = FakeEnum.Value2;

            // Activity
            var result = (ValidatableEnum<FakeEnum>)null != EnumValue;

            // Asserts
            Assert.True(result);
        }

        [Fact]
        public void OperatorNotEquals_WhenBoIsNotValid_ThenReturnTrue()
        {
            // Actors
            const FakeEnum EnumValue = FakeEnum.Value2;

            // Activity
            var result = this.validatableEnum != EnumValue;

            // Asserts
            Assert.True(result);
        }

        [Fact]
        public void OperatorNotEquals_WhenValuesAreEqual_ThenReturnFalse()
        {
            // Actors
            const FakeEnum EnumValue = FakeEnum.Value2;
            this.validatableEnum.InternalValue = EnumValue;

            // Activity
            var result = this.validatableEnum != EnumValue;

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void OperatorNotEquals_WhenValuesAreNotEqual_ThenReturnTrue()
        {
            // Actors
            const FakeEnum EnumValue = FakeEnum.Value2;
            this.validatableEnum.InternalValue = FakeEnum.Value1;

            // Activity
            var result = this.validatableEnum != EnumValue;

            // Asserts
            Assert.True(result);
        }

        [Fact]
        public void GetHashCode_WhenBoIsValid_ThenReturnsEnumValueHash()
        {
            // Actors
            const FakeEnum EnumValue = FakeEnum.Value2;
            this.validatableEnum.InternalValue = EnumValue;

            // Activity
            var result = this.validatableEnum.GetHashCode();

            // Asserts
            Assert.Equal(EnumValue.GetHashCode(), result);
        }

        [Fact]
        public void GetHashCode_WhenBoIsNotValid_ThenReturnsParsedTextHash()
        {
            // Actors

            // Activity
            var result = this.validatableEnum.GetHashCode();

            // Asserts
            Assert.Equal(ParsedText.GetHashCode(), result);
        }

        [Fact]
        public void ToString_WhenInternalValueIsNull_ThenThrowException()
        {
            // Actors

            // Activity
            this.validatableEnum.InternalValue = null;

            // Asserts
            Assert.Throws<InvalidOperationException>(() => this.validatableEnum.ToString());
        }

        [Fact]
        public void ToString_WhenInternalValueIsNotNull_ThenResultIsStringValue()
        {
            // Actors
            var expectedValue = FakeEnum.Value1.ToString(CultureInfo.InvariantCulture);

            // Activity
            this.validatableEnum.InternalValue = FakeEnum.Value1;

            // Asserts
            Assert.Equal(expectedValue, this.validatableEnum.ToString());
        }

        [Fact]
        public void Equals_WhenInstance2IsNull_ThenReturnFalse()
        {
            // Actors
            this.validatableEnum.InternalValue = FakeEnum.Value1;

            // Activity
            var result = this.validatableEnum.Equals(null);

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void Equals_WhenBothAreTheSameInstance_ThenReturnTrue()
        {
            // Actors

            // Activity
            var result = this.validatableEnum.Equals(this.validatableEnum);

            // Asserts
            Assert.True(result);
        }

        [Fact]
        public void Equals_WhenInstance1IsNotValid_ThenReturnFalse()
        {
            // Actors
            var other = new ValidatableEnum<FakeEnum>(ParsedText) { InternalValue = FakeEnum.Value1 };

            // Activity
            var result = this.validatableEnum.Equals(other);

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void Equals_WhenInstance2IsNotSameType_ThenReturnFalse()
        {
            // Actors
            var other = new object();

            // Activity
            var result = this.validatableEnum.Equals(other);

            // Asserts
            Assert.False(result);
        }

        [Fact]
        public void Equals_WhenInstance2IsNotValid_ThenReturnFalse()
        {
            // Actors
            this.validatableEnum.InternalValue = FakeEnum.Value1;
            var other = new ValidatableEnum<FakeEnum>(ParsedText);

            // Activity
            var result = this.validatableEnum.Equals(other);

            // Asserts
            Assert.False(result);
        }

        [Theory]
        [InlineData(FakeEnum.Value1, FakeEnum.Value1, true)]
        [InlineData(FakeEnum.Value1, FakeEnum.Value2, false)]
        [InlineData(FakeEnum.Value2, FakeEnum.Value2, true)]
        [InlineData(FakeEnum.Value2, FakeEnum.Value1, false)]
        public void Equals_WhenInstancesAreValidAndValuesAreProvided_ThenReturnValuesComparison(
            FakeEnum value1,
            FakeEnum value2,
            bool isEquals)
        {
            // Actors
            this.validatableEnum.InternalValue = value1;
            var other = new ValidatableEnum<FakeEnum>(ParsedText) { InternalValue = value2 };

            // Activity
            var result = this.validatableEnum.Equals(other);

            // Asserts
            Assert.Equal(isEquals, result);
        }
    }
}