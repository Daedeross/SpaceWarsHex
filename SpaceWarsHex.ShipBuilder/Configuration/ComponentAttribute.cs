using System;

namespace SpaceWarsHex.ShipBuilder.Configuration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public string? Name { get; set; }
    }
}
