//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EqualityLambdaComparerTests.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Extensions.Test.Primitives
{
    using System;

    using Extensions.Primitives;

    using FluentAssertions;

    using Xunit;

    public class EqualityLambdaComparerTests
    {
        [Fact]
        public void Constructor_WhenLambdaExpressionIsNull_ThenThrowsException()
        {
            // Actors

            // Activity
            Action action = () => new EqualityLambdaComparer<string>(null);

            // Asserts
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_WhenLambdaExpressionIsDefined_ThenDoNotThrowException()
        {
            // Actors

            // Activity
            Action action = () => new EqualityLambdaComparer<string>(str => str.Length);

            // Asserts
            action.ShouldNotThrow();
        }

        [Fact]
        public void Equals_WhenObjectsAreNull_ThenReturnsFalse()
        {
            // Actors
            var comparer = new EqualityLambdaComparer<string>(str => str.Length);

            // Activity
            var result = comparer.Equals(null, null);

            // Asserts
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("A", null)]
        [InlineData(null, "A")]
        public void Equals_WhenOnlyOneObjectIsNull_ThenReturnsFalse(string obj1, string obj2)
        {
            // Actors
            var comparer = new EqualityLambdaComparer<string>(str => str.Length);

            // Activity
            var result = comparer.Equals(obj1, obj2);

            // Asserts
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("AA", "A", false)]
        [InlineData("A", "A", true)]
        public void Equals_WhenBothObjectsAreDefined_ThenReturnsComparison(string obj1, string obj2, bool expectedResult)
        {
            // Actors
            var comparer = new EqualityLambdaComparer<string>(str => str.Length);

            // Activity
            var result = comparer.Equals(obj1, obj2);

            // Asserts
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void Equals_WhenBothObjectsPropertyReturnsNull_ThenReturnsTrue()
        {
            // Actors
            var comparer = new EqualityLambdaComparer<string>(str => null);
            
            // Activity
            var result = comparer.Equals("A", "A");

            // Asserts
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("A", "B")]
        [InlineData("B", "A")]
        public void Equals_WhenOnlyOneObjectPropertyReturnsNull_ThenReturnsFalse(string obj1, string obj2)
        {
            // Actors
            var comparer = new EqualityLambdaComparer<string>(str => str == "A" ? str : null);

            // Activity
            var result = comparer.Equals(obj1, obj2);

            // Asserts
            result.Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_WhenObjectIsDefined_ThenReturnsHashOfLambdaEvaluation()
        {
            // Actors
            var comparer = new EqualityLambdaComparer<string>(str => str.Length);

            // Activity
            var result = comparer.GetHashCode("AAA");

            // Asserts
            result.Should().Be(3.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WhenObjectIsUndefined_ThenReturnsZero()
        {
            // Actors
            var comparer = new EqualityLambdaComparer<string>(str => null);

            // Activity
            var result = comparer.GetHashCode("AAA");

            // Asserts
            result.Should().Be(0);
        }
    }
}