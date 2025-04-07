using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Model;

namespace SpaceWarsHex.Interfaces.Systems
{
    /// <summary>
    /// Public interface defining a torpedo tube
    /// </summary>
    public interface ITorpedoLauncher : IWeapon
    {
        /// <summary>
        /// The minimum warp the torpedo must be launched at.
        /// </summary>
        int MinWarp { get; }

        /// <summary>
        /// The maximum warp the torpedo may be launched at.
        /// </summary>
        int MaxWarp { get; }

        /// <summary>
        /// True if the torpedos are homing-capable
        /// </summary>
        bool Homing { get; }

        /// <summary>
        /// True if the tube can load and fire in the same turn.
        /// Effectively, it can fire without loading first.
        /// </summary>
        bool LoadFire { get; }

        /// <summary>
        /// True if the tube is currently loaded.
        /// </summary>
        bool Loaded { get; }

        #region Order State

        /// <summary>
        /// True if the tube is spending this turn loading or unloading.
        /// </summary>
        bool Loading { get; set; }

        /// <summary>
        /// The velocity vector the torpedo is ordered to fire on. Null if not ordered.
        /// </summary>
        HexVector2? LaunchVelocity { get; set; }

        #endregion
    }
}
