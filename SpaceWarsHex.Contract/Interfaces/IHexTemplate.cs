using SpaceWars.Model;
using System.Collections.Generic;

namespace SpaceWars.Interfaces
{
    /// <summary>
    /// Interface for a collection of hexes relative to some origin.
    /// </summary>
    /// <remarks>
    /// This is for the abstract template that multi-hex entities use. For example Smoke+Spray have a simeple 2-hex template, while beams are more complex.
    /// The "origin" of the tempalte's local coordinate system is always the same as the position of the instantiated <see cref="IHexObject"/>
    /// Some objects will have changing templates. eg. Beams will have a different template each turn to represent the advancment and dispersion of the beam through space.
    /// </remarks>
    public interface IHexTemplate
    {
        /// <summary>
        /// Gets the collection of template hexes in local-coordinates
        /// </summary>
        /// <returns>A collection of vectors to each hex relative to the entity's origin and orientation</returns>
        IReadOnlyCollection<HexVector2> GetHexes();

        /// <summary>
        /// Gets the collection of template hexes transformed into another coordinates.
        /// </summary>
        /// <param name="origin">The position of the template's origin.</param>
        /// <param name="rotation">The number of hex-faces the template should be rotated about it's origin.</param>
        /// <returns>A collection of vectors to each hex transformed into the desired coordinate system.</returns>
        /// <remarks>
        /// If the supplied <paramref name="origin"/> is the controling entity's <see cref="IHexObject.Position"/> and the <paramref name="rotation"/> is the same as entity's
        /// then the results will be the world-space coordinates of the templat's hexes.
        /// </remarks>
        IReadOnlyCollection<HexVector2> GetHexes(HexVector2 origin, int rotation);
    }
}
