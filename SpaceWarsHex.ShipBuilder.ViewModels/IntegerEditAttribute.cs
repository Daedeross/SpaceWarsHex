namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IntegerEditAttribute : PropertyEditAttribute
    {
        public int? Min { get; set; }
        public int? Max { get; set; }
        public IntegerEditAttribute() { }
    }
}
