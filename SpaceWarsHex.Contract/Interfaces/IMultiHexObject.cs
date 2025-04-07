using SpaceWarsHex.Model;
using System.Collections.Generic;

namespace SpaceWarsHex.Interfaces
{
    /// <summary>
    /// Interface for entities that take up or affect multiple hexes on the board.
    /// </summary>
    /// <remarks>
    /// TODO: This and all AoE stuff is very much a WIP.
    /// </remarks>
    public interface IMultiHexObject : IHexObject
    {
        /// <summary>
        /// Checks if the entity is on/affects a given hex.
        /// </summary>
        /// <param name="hex">Coordinates of the hex to check.</param>
        /// <returns>True if the entity exists or otherwise affects the given hex. Otherwise false.</returns>
        bool CheckHex(HexVector2 hex);

        /// <summary>
        /// Gets all the current hexes of the object.
        /// </summary>
        /// <returns>Collection of <see cref="HexVector2"/> representing the absolute coordinates of the entity's hexes.</returns>
        IReadOnlyCollection<HexVector2> GetHexes();
    }
}
