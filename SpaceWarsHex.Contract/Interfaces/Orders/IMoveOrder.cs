using SpaceWars.Model;

namespace SpaceWars.Interfaces.Orders
{
    /// <summary>
    /// Interface for orders that change a <see cref="IHexObject"/>'s velocity.
    /// Used on ships and (maybe) homig torpedos.
    /// </summary>
    public interface IMoveOrder : IOrder
    {
        /// <summary>
        /// The change to the entity's velocity vector.
        /// </summary>
        public HexVector2 Acceleration { get; }
    }
}
