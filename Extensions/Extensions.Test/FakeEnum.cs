// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeEnum.cs" company="Eurofins">
//   Copyright (c) Eurofins. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Extensions.Test
{
    using System.ComponentModel;

    public enum FakeEnum
    {
        Value1,

        [Description("Value 2")]
        Value2
    }
}