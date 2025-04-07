using Godot;
using SpaceWarsHex.Helpers;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;

namespace SpaceWarsHex.Bridges
{
    public interface IGodotHexMath : IHexMath<Vector2> { }

    public class HexMath : HexMathCore, IGodotHexMath
    {
        public static readonly IHexMath<Vector2> Default = new HexMath(64f);

        public HexMath()
            : base()
        {
            HexagonVertices = InitializeVertices();
        }

        public HexMath(float radius)
            : base(radius)
        {
            HexagonVertices = InitializeVertices();
        }

        private List<Vector2> InitializeVertices()
        {
            return new List<Vector2>
            {
                new (r2, h1),    // top right
                new (r, 0),      // right
                new (r2, -h1),   // bottom right
                new (-r2, -h1),  // bottom left
                new (-r, 0),     // left
                new (-r2, h1),   // top left
            };
        }

        /// <inheritdoc />
        public IReadOnlyList<Vector2> HexagonVertices { get; }

        /// <inheritdoc />
        public float AngleAcross(HexVector2 hv1, HexVector2 hv2)
        {
            Vector2 v1 = HexVector2ToVector(hv1).Normalized();
            Vector2 v2 = HexVector2ToVector(hv2).Normalized();

            return (float)Mathf.Acos((float)v1.Dot(v2));
        }

        /// <inheritdoc />
        public Vector2 HexagonVertex(int vertexIndex)
        {
            vertexIndex %= 6; // converts index if > 5

            return HexagonVertices[vertexIndex];
        }

        /// <inheritdoc />
        public Vector2 HexagonVertex(int vertexIndex, float radius)
        {
            vertexIndex %= 6; // converts index if > 5
            float radius2 = radius / 2;
            float height = radius2 * (float)Mathf.Sqrt(3);

            return vertexIndex switch
            {
                0 => new Vector2(radius2, height),   // top right
                1 => new Vector2(radius, 0),         // right
                2 => new Vector2(radius2, -height),  // bottom right
                3 => new Vector2(-radius2, -height), // bottom left
                4 => new Vector2(-radius, 0),        // left
                5 => new Vector2(-radius2, height),  // top left

                _ => Vector2.Zero,                    // should never happen
            };
        }

        /// <inheritdoc />
        public Vector2 HexagonVertex(HexVector2 hex, int vertexIndex)
        {
            vertexIndex %= 6; // converts index if > 5

            return HexagonVertices[vertexIndex] + HexVector2ToVector(hex);
        }

        /// <inheritdoc />
        public Vector2 HexVector2ToVector(HexVector2 hexVector)
        {
            return new (
                hexVector.X * r3,
                hexVector.X * h1 + hexVector.Y * h2
                );
        }

        /// <inheritdoc />
        public Vector2 HexVector2ToVector(HexVector2 hexVector, float z)
        {
            return new(
                hexVector.X * r3,
                hexVector.X * h1 + hexVector.Y * h2
                );
        }

        /// <inheritdoc />
        public HexVector2 VectorToHexVector2(Vector2 v)
        {
            return RectangularToHexVector2(v.X, v.Y);
        }
    }
}
