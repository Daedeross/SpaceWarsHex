using SpaceWarsHex.Model;
using System.Collections.Generic;

namespace SpaceWarsHex.Interfaces
{
    /// <summary>
    /// Interface for a spatial data structure to calculate entity collisions.
    /// </summary>
    public interface IHexQuadtree : ICollection<IHexObject>
    {
        /// <summary>
        /// Gets the bounding box for the tree.
        /// </summary>
        /// <returns><see cref="HexAABB"/></returns>
        public HexAABB GetExtents();

        /// <summary>
        /// Gets all entities that collide with the given hex;
        /// </summary>
        /// <param name="hex">The <see cref="HexVector2"/> to check.</param>
        /// <returns>A Collection of all entities that intersect the input hex.</returns>
        ICollection<IHexObject> GetCollisions(HexVector2 hex);

        /// <summary>
        /// Gets all entities that collide with the given <see cref="IHexObject"/>.
        /// This can be a <see cref="IMultiHexObject"/>.
        /// </summary>
        /// <param name="hex">The <see cref="IHexObject"/> to check.</param>
        /// <returns>A Collection of all entities that intersect the input entity.</returns>
        ICollection<IHexObject> GetCollisions(IHexObject hex);
    }
}
