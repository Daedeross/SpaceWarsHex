using SpaceWars.Model;

namespace SpaceWars.Interfaces.Orders
{
    /// <summary>
    /// Interface for ordering a ship to transport a bomb to a target hex.
    /// </summary>
    public interface IBombOrder : IWeaponOrder
    {
        /// <summary>
        /// The hex the bomb will explode in.
        /// </summary>
        HexVector2 TargetHex { get; set; }
    }
}
