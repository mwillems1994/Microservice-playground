﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace Playground.Nl.OrderManagementAPI.Nl.Attributes
{
    [AttributeUsage(
        AttributeTargets.Class,
        AllowMultiple = false,
        Inherited = true)]
    public sealed class DiClassAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; internal set; }

        public DiClassAttribute(
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            Lifetime = lifetime;
        }
    }
}
