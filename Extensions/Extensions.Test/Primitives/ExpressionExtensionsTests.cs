//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ExpressionExtensionsTests.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Extensions.Test.Primitives
{
    using System;
    using System.Linq.Expressions;

    using Extensions.Primitives;

    using FluentAssertions;

    using Xunit;

    public class ExpressionExtensionsTests
    {
        [Fact]
        public void And_WhenSecondExpressionIsNull_ThenFirstExpressionIsReturned()
        {
            // Actors
            Expression<Func<int, bool>> firstExpr = i => i > 0;

            // Activity
            var result = firstExpr.And(null);

            // Asserts
            result.Should().NotBeNull();
            result.Compile().Invoke(1).Should().BeTrue();
            result.Compile().Invoke(0).Should().BeFalse();
        }

        [Fact]
        public void And_WhenFirstExpressionIsNull_ThenSecondExpressionIsReturned()
        {
            // Actors
            Expression<Func<int, bool>> firstExpr = null;
            Expression<Func<int, bool>> secondExpr = i => i > 0;

            // Activity
            var result = firstExpr.And(secondExpr);

            // Asserts
            result.Should().NotBeNull();
            result.Compile().Invoke(1).Should().BeTrue();
            result.Compile().Invoke(0).Should().BeFalse();
        }

        [Fact]
        public void And_WhenBothExpressionsAreNotNull_ThenNewExpressionIsTheAnd()
        {
            // Actors
            Expression<Func<int, bool>> firstExpr = i => i < 2;
            Expression<Func<int, bool>> secondExpr = i => i > 0;

            // Activity
            var result = firstExpr.And(secondExpr);

            // Asserts
            result.Should().NotBeNull();
            result.Compile().Invoke(1).Should().BeTrue();
            result.Compile().Invoke(0).Should().BeFalse();
            result.Compile().Invoke(2).Should().BeFalse();
        }

        [Fact]
        public void Or_WhenSecondExpressionIsNull_ThenFirstExpressionIsReturned()
        {
            // Actors
            Expression<Func<int, bool>> firstExpr = i => i > 0;

            // Activity
            var result = firstExpr.Or(null);

            // Asserts
            result.Should().NotBeNull();
            result.Compile().Invoke(1).Should().BeTrue();
            result.Compile().Invoke(0).Should().BeFalse();
        }

        [Fact]
        public void Or_WhenFirstExpressionIsNull_ThenSecondExpressionIsReturned()
        {
            // Actors
            Expression<Func<int, bool>> firstExpr = null;
            Expression<Func<int, bool>> secondExpr = i => i > 0;

            // Activity
            var result = firstExpr.Or(secondExpr);

            // Asserts
            result.Should().NotBeNull();
            result.Compile().Invoke(1).Should().BeTrue();
            result.Compile().Invoke(0).Should().BeFalse();
        }

        [Fact]
        public void Or_WhenBothExpressionsAreNotNull_ThenNewExpressionIsTheOr()
        {
            // Actors
            Expression<Func<int, bool>> firstExpr = i => i < 0;
            Expression<Func<int, bool>> secondExpr = i => i > 0;

            // Activity
            var result = firstExpr.Or(secondExpr);

            // Asserts
            result.Should().NotBeNull();
            result.Compile().Invoke(1).Should().BeTrue();
            result.Compile().Invoke(-1).Should().BeTrue();
            result.Compile().Invoke(0).Should().BeFalse();
        }

        [Fact]
        public void ApplyBinaryOperator_WhenSecondExpressionIsNull_ThenFirstExpressionIsReturned()
        {
            // Actors
            Expression<Func<int, bool>> firstExpr = i => i > 0;

            // Activity
            var result = firstExpr.ApplyBinaryOperator(null, Expression.OrElse);

            // Asserts
            result.Should().NotBeNull();
            result.Compile().Invoke(1).Should().BeTrue();
            result.Compile().Invoke(0).Should().BeFalse();
        }

        [Fact]
        public void ApplyBinaryOperator_WhenFirstExpressionIsNull_ThenSecondExpressionIsReturned()
        {
            // Actors
            Expression<Func<int, bool>> firstExpr = null;
            Expression<Func<int, bool>> secondExpr = i => i > 0;

            // Activity
            var result = firstExpr.ApplyBinaryOperator(secondExpr, Expression.OrElse);

            // Asserts
            result.Should().NotBeNull();
            result.Compile().Invoke(1).Should().BeTrue();
            result.Compile().Invoke(0).Should().BeFalse();
        }

        [Fact]
        public void ApplyBinaryOperator_WhenBothExpressionsAreNotNull_ThenNewExpressionIsTheMergeOfBoth()
        {
            // Actors
            Expression<Func<int, bool>> firstExpr = i => i < 0;
            Expression<Func<int, bool>> secondExpr = i => i > 0;

            // Activity
            var result = firstExpr.ApplyBinaryOperator(secondExpr, Expression.OrElse);

            // Asserts
            result.Should().NotBeNull();
            result.Compile().Invoke(-1).Should().BeTrue();
            result.Compile().Invoke(0).Should().BeFalse();
            result.Compile().Invoke(1).Should().BeTrue();
        }

        [Fact]
        public void GetPropertyName_WhenPropertyIsProvided_ThenReturnsNameOfProperty()
        {
            // Actors
            var fakeObj = new FakeObject();
            Expression<Func<string>> expression = () => fakeObj.FakeProperty;

            // Activity
            var result = expression.GetPropertyName();

            // Asserts
            result.Should().Be("FakeProperty");
        }

        [Fact]
        public void Invert_WhenExpressionIsNull_ThenReturnsNull()
        {
            // Actors
            Expression<Func<int, bool>> expression = null;

            // Activity
            var result = expression.Invert();

            // Asserts
            result.Should().BeNull();
        }

        [Fact]
        public void Invert_WhenExpressionIsDefined_ThenReturnsExpressionInverted()
        {
            // Actors
            Expression<Func<int, bool>> expression = i => i == 0;

            // Activity
            var result = expression.Invert();

            // Asserts
            result.Should().NotBeNull();
            result.Compile().Invoke(0).Should().BeFalse();
            result.Compile().Invoke(1).Should().BeTrue();
        }

        [Fact]
        public void ApplyUnaryOperator_WhenExpressionIsNull_ThenReturnsNull()
        {
            // Actors
            Expression<Func<int, bool>> expression = null;

            // Activity
            var result = expression.ApplyUnaryOperator(Expression.IsTrue);

            // Asserts
            result.Should().BeNull();
        }

        [Fact]
        public void ApplyUnaryOperator_WhenExpressionIsDefined_ThenApplyUnaryOperator()
        {
            // Actors
            Expression<Func<bool>> expression = () => true;

            // Activity
            var result = expression.ApplyUnaryOperator(Expression.IsFalse);

            // Asserts
            result.Should().NotBeNull();
            result.Compile().Invoke().Should().BeFalse();
        }

        [Fact]
        public void EqualTo_WhenCall_ThenApplyEqualOperator()
        {
            // Actors
            Expression<Func<FakeObject, string>> expression = f => f.FakeProperty;

            // Activity
            var equalExpression = expression.EqualTo("42");

            // Asserts
            equalExpression.Compile().Invoke(new FakeObject { FakeProperty = "42" }).Should().Be(true);
            equalExpression.Compile().Invoke(new FakeObject { FakeProperty = "43" }).Should().Be(false);
        }

        [Fact]
        public void NotEqualTo_WhenCall_ThenApplyNotEqualOperator()
        {
            // Actors
            Expression<Func<FakeObject, string>> expression = f => f.FakeProperty;

            // Activity
            var equalExpression = expression.NotEqualTo("42");

            // Asserts
            equalExpression.Compile().Invoke(new FakeObject { FakeProperty = "42" }).Should().Be(false);
            equalExpression.Compile().Invoke(new FakeObject { FakeProperty = "43" }).Should().Be(true);
        }

        private class FakeObject
        {
            public string FakeProperty { get; set; }
        }
    }
}