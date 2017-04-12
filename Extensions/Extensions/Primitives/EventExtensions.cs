//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventExtensions.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Primitives
{
    using System;

    public static class EventExtensions
    {
        public static void SafeExecute(this EventHandler eventHandlerSource, object eventRaiser, EventArgs args = null)
        {
            var eventHandler = eventHandlerSource;
            if (eventHandler != null)
            {
                eventHandler(eventRaiser, args ?? EventArgs.Empty);
            }
        }

        public static void SafeExecute<TEventArgs>(this EventHandler<TEventArgs> eventHandlerSource, object eventRaiser, TEventArgs args)
        {
            var eventHandler = eventHandlerSource;
            if (eventHandler != null)
            {
                eventHandler(eventRaiser, args);
            }
        }
    }
}