using SpaceWars.Model;

namespace SpaceWars.Interfaces.Orders
{
    /// <summary>
    /// Order for an entity to fire one of its Torpedos
    /// </summary>
    public interface ITorpedoFireOrder : IWeaponOrder
    {
        /// <summary>
        /// The desired velocity of the torpedo.
        /// </summary>
        HexVector2 Velocity { get; }
    }
}
