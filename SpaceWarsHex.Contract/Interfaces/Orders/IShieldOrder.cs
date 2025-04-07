namespace SpaceWarsHex.Interfaces.Orders
{
    /// <summary>
    /// Interface for ordering an entity to set it's shield strength.
    /// </summary>
    public interface IShieldOrder : IOrder
    {
        /// <summary>
        /// The desired power to allocate the the shields.
        /// </summary>
        public int Power { get; set; }
    }
}
