using SpaceWars.Model;

namespace SpaceWars.Interfaces.Systems
{
    /// <summary>
    /// Interface for Drive systems.
    /// </summary>
    public interface IDrive : ISystem
    {
        /// <summary>
        /// The maximum speed available, in hexes per turn.
        /// </summary>
        int MaxWarp { get; }

        /// <summary>
        /// The maximum distance, in hexes, the entity can change velocity in one turn.
        /// </summary>
        int AccelerationClass { get; }

        /// <summary>
        /// The current velocity of the entity.
        /// </summary>
        HexVector2 Velocity { get; }

        /// <summary>
        /// The current move order (i.e. acceleration) for this turn.
        /// </summary>
        HexVector2 Acceleration { get; }
    }
}
