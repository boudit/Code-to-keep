//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="CollectionExtensions.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> elementsToAdd)
        {
            if (elementsToAdd != null)
            {
                elementsToAdd.ForEach(source.Add);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            source.ToList().ForEach(action);
        }

        public static IEnumerable<TSource> GetDuplicatedValues<TSource>(this IEnumerable<TSource> source)
        {
            return GetDuplicatedValues(source, item => item);
        }

        public static IEnumerable<TValue> GetDuplicatedValues<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> elementSelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (elementSelector == null)
            {
                throw new ArgumentNullException("elementSelector");
            }

            return source.ToLookup(elementSelector).Where(grp => grp.Count() > 1).Select(grp => grp.Key).ToList();
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, object> lambdaComparer)
        {
            return source.Distinct(new EqualityLambdaComparer<T>(lambdaComparer));
        }
    }
}