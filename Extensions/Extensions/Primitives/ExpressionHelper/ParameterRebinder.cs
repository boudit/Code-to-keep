// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterRebinder.cs" company="Eurofins">
//   Copyright (c) Eurofins. All rights reserved.
// </copyright>
// <summary>
//   Defines the ParameterRebinder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Extensions.Primitives.ExpressionHelper
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (this.map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }

            return base.VisitParameter(p);
        }
    }
}
