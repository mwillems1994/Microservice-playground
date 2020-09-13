using System;

namespace Playground.Nl.OrderManagementAPI.Nl.Attributes
{
    [AttributeUsage(
        AttributeTargets.Class,
        AllowMultiple = false,
        Inherited = true)]
    public class DiSettingsAttribute : Attribute
    {
        public string? Name { get; internal set; }

        public DiSettingsAttribute(string? name = null)
        {
            Name = name;
        }
    }
}
