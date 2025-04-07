using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;

namespace SpaceWarsHex.Interfaces
{
    /// <summary>
    /// The public interface for a HexMath library that can convert between rectangular vectors <typeparamref name="TVector"/> and
    /// <see cref="HexVector2"/> in various ways.
    /// </summary>
    /// <typeparam name="TVector">The type that is used for the rectangular vector, assumed to have at least 2 components.</typeparam>
    public interface IHexMath<TVector>: IHexMath
        where TVector : struct, IEquatable<TVector>
    {
        /// <summary>
        /// Gets the rectangular coordinates of the six vertices of a hexagon with the origin at the hex's center.
        /// </summary>
        IReadOnlyList<TVector> HexagonVertices { get; }

        /// <summary>
        /// Get's the rectangular coordinates of the center of a hexagon given by a <see cref="HexVector2"/>
        /// </summary>
        /// <param name="hexVector">The input hex.</param>
        /// <returns>Coordiantes of the hex's center.</returns>
        TVector HexVector2ToVector(HexVector2 hexVector);

        /// <summary>
        /// Get's the rectangular coordinates of the center of a hexagon given by a <see cref="HexVector2"/> with an additional third component offset.
        /// </summary>
        /// <param name="hexVector">The input hex.</param>
        /// <param name="z">The value for the third component.</param>
        /// <returns>Coordiantes of the hex's center with a third component equal to <paramref name="z"/>.</returns>
        TVector HexVector2ToVector(HexVector2 hexVector, float z);

        /// <summary>
        /// Finds's the hex that the rectangular vector <paramref name="v"/> is in.
        /// </summary>
        /// <param name="v">The rectangular coordinate vector.</param>
        /// <returns></returns>
        HexVector2 VectorToHexVector2(TVector v);

        /// <summary>
        /// Returns the Rectangular coordinates for the vertex of a unit hexagon. Can take any positive int.
        /// </summary>
        /// <param name="vertexIndex">The clockwise index of the desired vert. 0 = top right, 1 = right, etc.</param>
        /// <returns></returns>
        TVector HexagonVertex(int vertexIndex);

        /// <summary>
        /// Returns the Rectangular coordinates for the vertex of a hexagon of the specifed radius;
        /// </summary>
        /// <param name="vertexIndex"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        TVector HexagonVertex(int vertexIndex, float radius);

        /// <summary>
        /// Gets the rectangular coordinates of a specific vertex of a specific hex.
        /// </summary>
        /// <param name="hex">The hex to find the vertex of</param>
        /// <param name="vertexIndex">The clockwise index of the desired vert. 0 = top right, 1 = right, etc.</param>
        /// <returns>Rectangular <typeparamref name="TVector"/> of the vertex.</returns>
        TVector HexagonVertex(HexVector2 hex, int vertexIndex);

        /// <summary>
        /// Get's the angle in degrees between two <see cref="HexVector2"/>s, from first to second.
        /// </summary>
        /// <param name="hv1">The first vector</param>
        /// <param name="hv2">The second vector</param>
        /// <returns>The angle in degrees.</returns>
        float AngleAcross(HexVector2 hv1, HexVector2 hv2);
    }
}
