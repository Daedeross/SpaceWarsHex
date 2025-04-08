using SpaceWarsHex.Model;
using System.ComponentModel;

namespace SpaceWarsHex.Interfaces
{
    /// <summary>
    /// Base interface for all entities that exist on the game board.
    /// </summary>
    public interface IHexObject: IHaveId, INotifyPropertyChanged
    {
        /// <summary>
        /// Display name of the entity.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The owning player, if any
        /// </summary>
        IPlayer? Player { get; set; }

        /// <summary>
        /// The entity that 'owns' this entity. e.g. a Torpedo is owned by the ship that fires it.
        /// Null for independant objects (like ships).
        /// </summary>
        IHexObject? Owner { get; }

        /// <summary>
        /// The team number this entity belongs to. Negative numbers are for non-player/neutral teams.
        /// </summary>
        int TeamNumber { get; set; }

        /// <summary>
        /// The position of the entity.
        /// </summary>
        /// <remarks>
        /// There are some entities that 
        /// </remarks>
        HexVector2 Position { get; set; }

        /// <summary>
        /// Flags for the state of the object for the engine/director to perform cleanup and hiding.
        /// </summary>
        HexObjectFlags Flags { get; }

        /// <summary>
        /// Called by the main director at the end of each phase.
        /// </summary>
        /// <param name="turnPhase">The phase the entity is to handle the end of.</param>
        /// <returns>True if the entity is destroyed. (TODO: May want to rethink this)</returns>
        bool HandleEndOfPhase(TurnPhase turnPhase);

        /// <summary>
        /// Called by the main director at the end of each turn.
        /// </summary>
        /// <param name="turnNumber">The turn number that just ended.</param>
        /// <returns>True if the entity is destroyed. (TODO: May want to rethink this)</returns>
        bool HandleEndOfTurn(int turnNumber);
    }
}
