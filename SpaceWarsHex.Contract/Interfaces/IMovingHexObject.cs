using SpaceWars.Model;

namespace SpaceWars.Interfaces
{
    /// <summary>
    /// Interface for any entity that has a velocity (i.e. moves)
    /// </summary>
    public interface IMovingHexObject : IHexObject, INotifyPositionChanged
    {
        /// <summary>
        /// The current velocity of the entity.
        /// </summary>
        HexVector2 Velocity { get; set; }

        /// <summary>
        /// What phase does the entity move in? Used by the Director.
        /// </summary>
        TurnPhase MovementPhase { get; }
    }
}
