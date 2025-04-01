using SpaceWars.Model;

namespace SpaceWars.Interfaces
{
    /// <summary>
    /// Interface for helper classes for <see cref="HexVector2"/> to implement.
    /// </summary>
    public interface IHexMath
    {
        /// <summary>
        /// The radius (from center to any vertex) of a unit hexagon.
        /// </summary>
        float R { get; }

        /// <summary>
        /// Half a unit hexagon's height. (from center to middle of any edge)
        /// </summary>
        float H1 { get; }

        /// <summary>
        /// A unit hexagon's height. (2 * H1)
        /// </summary>
        float H2 { get; }

        /// <summary>
        /// Half a unit hexagon's radius. (R / 2)
        /// </summary>
        float R2 { get; }

        /// <summary>
        /// One and one-half a unit hexagon's radius. (1.5 * R)
        /// </summary>
        float R3 { get; }

        /// <summary>
        /// Takes in a square (Euclidian) vector and returns the oblique
        /// hexagonal coordinates containing the vector (x, y).
        /// </summary>
        /// <param name="x">The x component of the rectangular vector being tested.</param>
        /// <param name="y">The y component of the rectangular vector being tested.</param>
        /// <returns>The oblique hexagonal x,y coordinates of the hex conaining v.</returns>
        HexVector2 RectangularToHexVector2(float x, float y);
    }
}
