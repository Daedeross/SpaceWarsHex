using SpaceWars.Model;

namespace SpaceWars.Interfaces.Orders
{
    /// <summary>
    /// Interface for ordering an entity to fire a persistent multi-hex weapon or countermeasure.
    /// </summary>
    public interface IWallOrder : IWeaponOrder, IHaveDirection6
    {
        /// <summary>
        /// The desired velocity of the wall.
        /// </summary>
        HexVector2 Velocity { get; }
    }
}
