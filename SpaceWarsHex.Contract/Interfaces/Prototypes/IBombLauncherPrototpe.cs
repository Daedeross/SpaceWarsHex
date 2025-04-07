using SpaceWarsHex.Model;

namespace SpaceWarsHex.Interfaces.Prototypes
{
    /// <summary>
    /// Public interface for all bomb launcher systems' prototypes
    /// </summary>
    public interface IBombLauncherPrototpe : ISystemPrototype
    {
        /// <summary>
        /// The maximum range of the bomb, in hexes. Non-positive for infinite range.
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
    }
}
