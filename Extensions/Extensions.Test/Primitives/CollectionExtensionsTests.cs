//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="CollectionExtensionsTests.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Extensions.Test.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Extensions.Primitives;

    using FluentAssertions;

    using Xunit;

    public class CollectionExtensionsTests
    {
        [Fact]
        public void AddRange_WhenElementsListIsNull_ThenDoNotThrowException()
        {
            // Actors
            List<string> elements = null;
            ICollection<string> source = new List<string>();

            // Activity & Asserts
            Action action = () => source.AddRange(elements);
            action.ShouldNotThrow();
            source.Should().BeEmpty();
        }

        [Fact]
        public void AddRange_WhenElementsListIsDefined_ThenAllItemsAreAdded()
        {
            // Actors
            var elements = new[] { "A", "B" };
            ICollection<string> source = new[] { "C", "D" }.ToList();

            // Activity
            source.AddRange(elements);

            // Asserts
            source.ShouldBeEquivalentTo(new[] { "C", "D", "A", "B" });
        }

        [Fact]
        public void ForEach_WhenActionIsNull_ThenThrowsException()
        {
            // Actors
            var source = Enumerable.Empty<string>();

            // Activity
            Action action = () => source.ForEach(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ForEach_WhenActionIsNotNull_ThenActionIsExecutedOnEachElement()
        {
            // Actors
            var source = new[] { new FakeObject(), new FakeObject() };
            
            // Activity
            source.ForEach(fo => fo.Value = 42);

            // Asserts
            source.Should().OnlyContain(fo => fo.Value == 42);
        }

        [Fact]
        public void GetDuplicatedValues_WhenNoArgsAndSourceIsNull_ThenThrowException()
        {
            // Actors
            IEnumerable<string> source = null;

            // Activity
            Action action = () => source.GetDuplicatedValues();
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GetDuplicatedValues_WhenArgsAndSourceIsNull_ThenThrowException()
        {
            // Actors
            IEnumerable<string> source = null;

            // Activity
            Action action = () => source.GetDuplicatedValues(str => str.Length);
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GetDuplicatedValues_WhenArgsAndElementSelectorIsNull_ThenThrowException()
        {
            // Actors
            IEnumerable<string> source = new[] { "A" };

            // Activity
            Action action = () => source.GetDuplicatedValues<string, int>(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GetDuplicatedValues_WhenNoArgsAndEmptySourceIsProvided_ThenResultIsEmpty()
        {
            // Actors
            IEnumerable<string> source = Enumerable.Empty<string>();

            // Activity
            var result = source.GetDuplicatedValues();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetDuplicatedValues_WhenArgsAndEmptySourceIsProvided_ThenResultIsEmpty()
        {
            // Actors
            IEnumerable<string> source = Enumerable.Empty<string>();

            // Activity
            var result = source.GetDuplicatedValues(str => str.Length);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetDuplicatedValues_WhenNoArgsAndNoDuplicatedValueIsProvided_ThenResultIsEmpty()
        {
            // Actors
            IEnumerable<string> source = new[] { "A", "B", "C" };

            // Activity
            var result = source.GetDuplicatedValues();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetDuplicatedValues_WhenArgsAndNoDuplicatedValueIsProvided_ThenResultIsEmpty()
        {
            // Actors
            IEnumerable<string> source = new[] { "ABC", "AB", "A" };

            // Activity
            var result = source.GetDuplicatedValues(str => str.Length);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetDuplicatedValues_WhenNoArgsAndDuplicatedValueIsProvided_ThenResultContainsOneEntryPerDuplicatedValue()
        {
            // Actors
            IEnumerable<string> source = new[] { "A", "B", "C", "A", "B", "A" };

            // Activity
            var result = source.GetDuplicatedValues().ToList();

            // Assert
            result.Should().BeEquivalentTo("A", "B");
            result.Should().OnlyHaveUniqueItems();
        }

        [Fact]
        public void GetDuplicatedValues_WhenArgsAndDuplicatedValueIsProvided_ThenResultContainsOneEntryPerDuplicatedValue()
        {
            // Actors
            IEnumerable<string> source = new[] { "ABC", "AB", "A", "ABC", "AB", "ABC" };

            // Activity
            var result = source.GetDuplicatedValues(str => str.Length).ToList();

            // Assert
            result.Should().BeEquivalentTo(3, 2);
            result.Should().OnlyHaveUniqueItems();
        }
        
        [Fact]
        public void Distinct_WhenCondition_ThenDistinctIsDoneOnLambda()
        {
            // Actors
            var collection = new[] { "AAA", "AAA", "BBB", "AA", "BB", "A", "ABCD" };
            var expectedCollection = new[] { "AAA", "AA", "A", "ABCD" };

            // Activity
            var result = collection.Distinct(str => str.Length);

            // Asserts
            result.ShouldAllBeEquivalentTo(expectedCollection);
        }

        private class FakeObject
        {
            public int Value { get; set; }
        }
    }
}