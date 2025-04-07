using SpaceWarsHex.Interfaces.Orders;

namespace SpaceWarsHex.Interfaces
{
    /// <summary>
    /// Public interface for Torpedos as on-board entities.
    /// </summary>
    public interface ITorpedo : IMovingHexObject, IOrderable<ITorpedoExplodeOrder>, IOrderable<IMoveOrder>
    {
        /// <summary>
        /// The ship that fired the torpedo.
        /// </summary>
        IShip Owner { get; }

        /// <summary>
        /// The strength of the torpedo when it explodes.
        /// </summary>
        int Power { get; }

        /// <summary>
        /// True if the torpedo is homing Capable.
        /// </summary>
        bool Homing { get; }
    }
}
