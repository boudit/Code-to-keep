//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EqualityLambdaComparer.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Primitives
{
    using System;
    using System.Collections.Generic;

    public class EqualityLambdaComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, object> lambdaComparer;

        public EqualityLambdaComparer(Func<T, object> lambdaComparer)
        {
            if (lambdaComparer == null)
            {
                throw new ArgumentNullException("lambdaComparer");
            }

            this.lambdaComparer = lambdaComparer;
        }
        
        public bool Equals(T object1, T object2)
        {
            if (object1 == null || object2 == null)
            {
                return false;
            }

            var object1Value = this.lambdaComparer(object1);
            var object2Value = this.lambdaComparer(object2);

            if (object1Value == null)
            {
                return object2Value == null;
            }

            return object1Value.Equals(object2Value);
        }

        public int GetHashCode(T obj)
        {
            var objValue = this.lambdaComparer(obj);

            return objValue == null ? 0 : objValue.GetHashCode();
        }
    }
}