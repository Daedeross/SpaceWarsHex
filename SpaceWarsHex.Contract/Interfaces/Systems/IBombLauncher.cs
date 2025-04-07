using SpaceWarsHex.Model;

namespace SpaceWarsHex.Interfaces.Systems
{
    /// <summary>
    /// Public interface for all bomb systems
    /// </summary>
    public interface IBombLauncher : IWeapon
    {
        /// <summary>
        /// The maximum range of the bomb, in hexes.
        /// </summary>
        int MaxRange { get; }

        /// <summary>
        /// The number of turns before the bomb explodes.
        /// </summary>
        int DetonationDelay { get; }

        /// <summary>
        /// The <see cref="TurnPhase"/> the bomb explodes.
        /// </summary>
        TurnPhase DetonationPhase { get; }

        /// <summary>
        /// The number of turns before the bomb's location is revealed to enemies.
        /// </summary>
        int RevealDelay { get; }

        /// <summary>
        /// The <see cref="TurnPhase"/> in which the bomb's location is revealed to enemies.
        /// </summary>
        TurnPhase RevealPhase { get; }

        /// <summary>
        /// The target hex for the bomb. Null for no order.
        /// </summary>
        HexVector2? TargetHex { get; set; }
    }
}
