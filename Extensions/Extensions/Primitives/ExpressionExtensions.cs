//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ExpressionExtensions.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Primitives
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Extensions.Primitives.ExpressionHelper;

    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return ApplyBinaryOperator(first, second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return ApplyBinaryOperator(first, second, Expression.Or);
        }

        public static Expression<T> ApplyBinaryOperator<T>(
            this Expression<T> first,
            Expression<T> second,
            Func<Expression, Expression, Expression> binaryOperator)
        {
            if (second == null)
            {
                return first;
            }

            if (first == null)
            {
                return second;
            }

            return MergeExpressions(first, second, binaryOperator);
        }

        public static Expression<Func<T, bool>> Invert<T>(this Expression<Func<T, bool>> expressionToModify)
        {
            return ApplyUnaryOperator(expressionToModify, Expression.Not);
        }

        public static Expression<T> ApplyUnaryOperator<T>(
            this Expression<T> expressionToModify,
            Func<Expression, Expression> unaryOperator)
        {
            if (expressionToModify == null)
            {
                return null;
            }

            return MergeExpressions(expressionToModify, unaryOperator);
        }

        public static string GetPropertyName<T>(this Expression<Func<T>> property)
        {
            return GetPropertyName(property as LambdaExpression);
        }

        public static string GetPropertyName(this LambdaExpression lambdaExpression)
        {
            MemberExpression memberExpression;

            if (lambdaExpression.Body is UnaryExpression)
            {
                var unaryExpression = lambdaExpression.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambdaExpression.Body as MemberExpression;
            }

            return memberExpression.Member.Name;
        }

        public static Expression<Func<V, bool>> NotEqualTo<V, W>(this Expression<Func<V, W>> getExpression, W value)
        {
            return Expression.Lambda<Func<V, bool>>(
                Expression.NotEqual(getExpression.Body, Expression.Constant(value)),
                getExpression.Parameters);
        }

        public static Expression<Func<V, bool>> EqualTo<V, W>(this Expression<Func<V, W>> getExpression, W value)
        {
            return Expression.Lambda<Func<V, bool>>(
                Expression.Equal(getExpression.Body, Expression.Constant(value)),
                getExpression.Parameters);
        }

        private static Expression<T> MergeExpressions<T>(Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> mergeFunction)
        {
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            return Expression.Lambda<T>(mergeFunction(first.Body, secondBody), first.Parameters);
        }

        private static Expression<T> MergeExpressions<T>(Expression<T> first, Func<Expression, Expression> mergeFunction)
        {
            return Expression.Lambda<T>(mergeFunction(first.Body), first.Parameters);
        }
    }
}