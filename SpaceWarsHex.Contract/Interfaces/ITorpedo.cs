using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Model;

namespace SpaceWarsHex.Interfaces
{
    /// <summary>
    /// Public interface for Torpedos as on-board entities.
    /// </summary>
    public interface ITorpedo : IMovingHexObject, IOrderable<ITorpedoExplodeOrder>, IOrderable<IMoveOrder>
    {
        /// <summary>
        /// The strength of the torpedo when it explodes.
        /// </summary>
        int Power { get; }

        /// <summary>
        /// True if the torpedo is homing Capable.
        /// </summary>
        HomingType Homing { get; }

        /// <summary>
        /// The Max-Warp lost each time the torpedo manoeuvres.
        /// Only maningful if torpedo is homing.
        /// </summary>
        int HomingLoss { get; }

        /// <summary>
        /// The maximum distance, in hexes, the entity can change velocity in one turn.
        /// </summary>
        int AccelerationClass { get; }
    }
}
