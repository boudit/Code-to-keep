//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidatableEnumExtensionsTests.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Test.Types
{
    using System.Collections.Generic;
    using System.Linq;

    using Extensions.Types;

    using Xunit;

    public class ValidatableEnumExtensionsTests
    {
        [Fact]
        public void ToListOfEnum_WhenSourceIsEmpty_ThenReturnsEmptyList()
        {
            // Actors
            var source = Enumerable.Empty<ValidatableEnum<FakeEnum>>();

            // Activity
            var result = source.ToListOfEnum();

            // Asserts
            Assert.Empty(result);
        }

        [Fact]
        public void ToListOfEnum_WhenSourceIsNotEmpty_ThenReturnsEnumValuesOfValidBos()
        {
            // Actors
            var expected = new[] { FakeEnum.Value1, FakeEnum.Value2 };
            IEnumerable<ValidatableEnum<FakeEnum>> source = new[] { new ValidatableEnum<FakeEnum>(FakeEnum.Value1), new ValidatableEnum<FakeEnum>(FakeEnum.Value2), new ValidatableEnum<FakeEnum>("FakeOne"), };

            // Activity
            var result = source.ToListOfEnum();

            // Asserts
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ContainsEnum_WhenListContainsprovidedValue_ThenReturnsTrue()
        {
            // Actors
            const FakeEnum EnumValue = FakeEnum.Value1;
            IEnumerable<ValidatableEnum<FakeEnum>> listOfValues = new[] { new ValidatableEnum<FakeEnum>(FakeEnum.Value1), new ValidatableEnum<FakeEnum>(FakeEnum.Value2), new ValidatableEnum<FakeEnum>("FakeOne"), };

            // Activity
            var result = listOfValues.ContainsEnum(EnumValue);

            // Asserts
            Assert.True(result);
        }

        [Fact]
        public void ContainsEnum_WhenListDoesNotContainprovidedValue_ThenReturnsFalse()
        {
            // Actors
            const FakeEnum EnumValue = FakeEnum.Value1;
            IEnumerable<ValidatableEnum<FakeEnum>> listOfValues = new[] { new ValidatableEnum<FakeEnum>(FakeEnum.Value2), new ValidatableEnum<FakeEnum>("FakeOne"), };

            // Activity
            var result = listOfValues.ContainsEnum(EnumValue);

            // Asserts
            Assert.False(result);
        }
    }
}