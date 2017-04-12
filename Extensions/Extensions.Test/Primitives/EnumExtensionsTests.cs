//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EnumExtensionsTests.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Extensions.Test.Primitives
{
    using System;
    using System.ComponentModel;

    using Extensions.Primitives;

    using FluentAssertions;

    using Xunit;

    public class EnumExtensionsTests
    {
        [Fact]
        public void GetEnumValues_WhenProvidedTypeIsValidEnum_ThenAllValuesOfEnumAreReturned()
        {
            // Actors

            // Activity
            var values = EnumExtensions.GetEnumValues<FakeEnum>();

            // Asserts
            values.Should().Contain(FakeEnum.Value1).And.Contain(FakeEnum.Value2);
        }

        [Fact]
        public void GetEnumValues_WhenProvidedTypeIsNotValidEnum_ThenExceptionIsThrown()
        {
            // Actors

            // Activity & Asserts
            Action action = () => EnumExtensions.GetEnumValues<NotAnEnum>();
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void ToEnum_WhenValueIsPartOfAnEnum_ThenReturnsEnumValue()
        {
            // Actors
            const string Value1 = "Value1";

            // Activity
            var result = Value1.ToEnum<FakeEnum>();

            // Asserts
            result.Should().Be(FakeEnum.Value1);
        }

        [Fact]
        public void ToEnum_WhenValueIsDescriptionOfAnEnum_ThenReturnsEnumValue()
        {
            // Actors
            const string Value2Description = "Value 2";

            // Activity
            var result = Value2Description.ToEnum<FakeEnum>();

            // Asserts
            result.Should().Be(FakeEnum.Value2);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ToEnum_WhenValueIsNullOrEmpty_ThenExceptionIsThrown(string value)
        {
            // Actors

            // Activity & Asserts
            Action action = () => value.ToEnum<FakeEnum>();
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void ToEnum_WhenValueIsCastIntoNotEnumType_ThenExceptionIsThrown()
        {
            // Actors
            const string Value1 = "Value1";

            // Activity
            Action action = () => Value1.ToEnum<NotAnEnum>();
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void ToEnum_WhenValueIsNotPartOfEnum_ThenExceptionIsThrown()
        {
            // Actors
            const string Value1 = "BadValue";

            // Activity
            Action action = () => Value1.ToEnum<FakeEnum>();
            action.ShouldThrow<ArgumentException>();
        }
        
        [Fact]
        public void TryMapToEnum_WhenValueIsPartOfAnEnum_ThenReturnsEnumValue()
        {
            // Actors
            const string Value1 = "Value1";
            FakeEnum? resultValue;

            // Activity
            var result = Value1.TryMapToEnum(out resultValue);

            // Asserts
            result.Should().BeTrue();
            resultValue.Should().Be(FakeEnum.Value1);
        }

        [Fact]
        public void TryMapToEnum_WhenValueIsDescriptionOfAnEnum_ThenReturnsEnumValue()
        {
            // Actors
            const string Value2Description = "Value 2";
            FakeEnum? resultValue;

            // Activity
            var result = Value2Description.TryMapToEnum(out resultValue);

            // Asserts
            result.Should().BeTrue();
            resultValue.Should().Be(FakeEnum.Value2);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void TryMapToEnum_WhenValueIsNullOrEmpty_ThenReturnsFalse(string value)
        {
            // Actors
            FakeEnum? resultValue;

            // Activity
            var result = value.TryMapToEnum(out resultValue);

            // Asserts
            result.Should().BeFalse();
            resultValue.Should().BeNull();
        }

        [Fact]
        public void TryMapToEnum_WhenValueIsCastIntoNotEnumType_ThenExceptionIsThrown()
        {
            // Actors
            const string Value1 = "Value1";
            NotAnEnum? resultValue;

            // Activity
            Action action = () => Value1.TryMapToEnum(out resultValue);
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void TryMapToEnum_WhenValueIsNotPartOfEnum_ThenReturnsFalse()
        {
            // Actors
            const string Value1 = "BadValue";
            FakeEnum? resultValue;

            // Activity
            var result = Value1.TryMapToEnum(out resultValue);

            // Asserts
            result.Should().BeFalse();
            resultValue.Should().BeNull();
        }

        [Theory]
        [InlineData("Value1", FakeEnum.Value1)]
        [InlineData("VALUE1", FakeEnum.Value1)]
        [InlineData("value2", FakeEnum.Value2)]
        [InlineData("    value2     ", FakeEnum.Value2)]
        public void TryMapToEnum_WhenValueIsPartOfAnEnumWithBadCase_ThenReturnsEnumValue(string text, FakeEnum expectedEnum)
        {
            // Actors
            FakeEnum? resultValue;

            // Activity
            var result = text.TryMapToEnum(out resultValue);

            // Asserts
            result.Should().BeTrue();
            resultValue.Should().Be(expectedEnum);
        }

        [Fact]
        public void GetDescriptionAttribute_WhenProvidedTypeIsNotValidEnum_ThenExceptionIsThrown()
        {
            // Actors
            var badValue = new NotAnEnum();

            // Activity & Asserts
            Action action = () => badValue.GetDescriptionAttribute();
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void GetDescriptionAttribute_WhenProvidedTypeIsValidEnumWithoutDescriptionAttribute_ThenReturnsNull()
        {
            // Actors
            const FakeEnum Value = FakeEnum.Value1;

            // Activity
            var result = Value.GetDescriptionAttribute();

            // Asserts
            result.Should().BeNull();
        }

        [Fact]
        public void GetDescriptionAttribute_WhenProvidedTypeIsValidEnumWithDescriptionAttribute_ThenReturnsAttribute()
        {
            // Actors
            const FakeEnum Value = FakeEnum.Value2;

            // Activity
            var result = Value.GetDescriptionAttribute();

            // Asserts
            result.Should().NotBeNull();
            result.Description.Should().Be("Value 2");
        }

        [Fact]
        public void GetDescriptionAttribute_WhenProvidedTypeIsValidEnumButValueIsNot_ThenReturnsNull()
        {
            // Actors
            const FakeEnum Value = (FakeEnum)42;

            // Activity
            var result = Value.GetDescriptionAttribute();

            // Asserts
            result.Should().BeNull();
        }

        [Fact]
        public void GetStringValue_WhenProvidedTypeIsNotValidEnum_ThenExceptionIsThrown()
        {
            // Actors
            var badValue = new NotAnEnum();

            // Activity & Asserts
            Action action = () => badValue.GetStringValue();
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void GetStringValue_WhenProvidedTypeIsValidEnumWithoutDescriptionAttribute_ThenReturnsStringValueOfEnum()
        {
            // Actors
            const FakeEnum Value = FakeEnum.Value1;

            // Activity
            var result = Value.GetStringValue();

            // Asserts
            result.Should().Be("Value1");
        }

        [Fact]
        public void GetStringValue_WhenProvidedTypeIsValidEnumWithDescriptionAttribute_ThenReturnsDescriptionAttributeValue()
        {
            // Actors
            const FakeEnum Value = FakeEnum.Value2;

            // Activity
            var result = Value.GetStringValue();

            // Asserts
            result.Should().Be("Value 2");
        }

        private struct NotAnEnum
        {
            [Description("Value 1")]
            public string Value1 { get; set; }
        }
    }
}