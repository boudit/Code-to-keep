//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventExtensionsTests.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Extensions.Test.Primitives
{
    using System;

    using Extensions.Primitives;

    using FluentAssertions;

    using Xunit;

    public class EventExtensionsTests
    {
        private event EventHandler FakeEmptyEvent;

        private event EventHandler<string> FakeGenericEvent;

        [Fact]
        public void SafeExecute_WhenNoHandlerIsDefinedOnEmptyEvent_ThenNoExceptionIsThrown()
        {
            // Actors

            // Activity & Asserts
            Action action = () => this.FakeEmptyEvent.SafeExecute(this);
            action.ShouldNotThrow();
        }

        [Fact]
        public void SafeExecute_WhenArgsAreNullOnEmptyEvent_ThenEmptyArgsAreSent()
        {
            // Actors
            object senderReceived = null;
            EventArgs argsReceived = null;
            this.FakeEmptyEvent += (sender, args) => { senderReceived = sender; argsReceived = args; };

            // Activity
            this.FakeEmptyEvent.SafeExecute(this);

            // Asserts
            senderReceived.Should().NotBeNull().And.BeSameAs(this);
            argsReceived.Should().NotBeNull().And.Be(EventArgs.Empty);
        }

        [Fact]
        public void SafeExecute_WhenArgsAreDefinedOnEmptyEvent_ThenTheyAreSent()
        {
            // Actors
            object senderReceived = null;
            EventArgs argsReceived = null;
            var argsSent = new FakeEventArgs();
            this.FakeEmptyEvent += (sender, args) => { senderReceived = sender; argsReceived = args; };

            // Activity
            this.FakeEmptyEvent.SafeExecute(this, argsSent);

            // Asserts
            senderReceived.Should().NotBeNull().And.BeSameAs(this);
            argsReceived.Should().NotBeNull().And.BeSameAs(argsSent);
        }

        [Fact]
        public void SafeExecute_WhenNoHandlerIsDefinedOnGenericEvent_ThenNoExceptionIsThrown()
        {
            // Actors

            // Activity & Asserts
            Action action = () => this.FakeGenericEvent.SafeExecute(this, "Test");
            action.ShouldNotThrow();
        }

        [Fact]
        public void SafeExecute_WhenArgsAreDefinedOnGenericEvent_ThenTheyAreSent()
        {
            // Actors
            const string ArgsSent = "Args";
            object senderReceived = null;
            string argsReceived = null;

            this.FakeGenericEvent += (sender, args) => { senderReceived = sender; argsReceived = args; };

            // Activity
            this.FakeGenericEvent.SafeExecute(this, ArgsSent);

            // Asserts
            senderReceived.Should().NotBeNull().And.BeSameAs(this);
            argsReceived.Should().NotBeNull().And.BeSameAs(ArgsSent);
        }

        private class FakeEventArgs : EventArgs 
        {
        }
    }
}