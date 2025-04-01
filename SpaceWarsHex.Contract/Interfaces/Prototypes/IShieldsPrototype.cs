namespace SpaceWars.Interfaces.Prototypes
{
    /// <summary>
    /// Interface for the the Shields system prototype.
    /// </summary>
    public interface IShieldsPrototype : ISystemPrototype
    {
        /// <summary>
        /// The max power that can be allocated to the shield.
        /// </summary>
        public int MaxPower { get; }
    }
}
