namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyEditAttribute : Attribute
    {
        public string? DisplayName { get; }
        public PropertyEditAttribute() { }
    }
}
