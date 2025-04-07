using Godot;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWarsHex.Bridges
{
    public static class GodotHexExtensions
    {
        private const float Epsilon = 1.0e-3F;

        public static Vector2 ToVector2(this HexVector2 hv)
        {
            return HexMath.Default.HexVector2ToVector(hv);
        }

        public static Vector2 ToVector2(this HexVector2 hv, float z)
        {
            return HexMath.Default.HexVector2ToVector(hv, z);
        }

        public static Vector3 ToVector3(this HexVector2 hv, float z)
        {
            var v2 = hv.ToVector2();
            return new Vector3(v2.X, v2.Y, z);
        }
        public static Vector2I ToVector2I(this HexVector2 hv)
        {
            return new Vector2I(hv.X, hv.Y);
        }

        public static HexVector2 ToHexVector(this Vector2I v)
        {
            return new HexVector2(v.X, v.Y);
        }

        public static Vector2 GetCorner(this HexVector2 hex, int vertexIndex)
        {
            return HexMath.Default.HexagonVertex(hex, vertexIndex);
        }

        public static Vector2[] GetAllCorners(this HexVector2 hex)
        {
            var v = hex.ToVector2();
            return HexMath.Default.HexagonVertices.Select(vert => vert + v).ToArray();
        }

        /// <summary>
        /// Checks if a line (defined as a point and a vector) passes through the hex.
        /// </summary>
        /// <param name="hex">The hex.</param>
        /// <param name="p">Any point on the line.</param>
        /// <param name="v">The basis vector for the line.</param>
        /// <returns>True if the line passes through the hex, false if not.</returns>
        /// <remarks>
        /// If a vertex or edge of the hex is on line then the line is considerd to intersect the hex.
        /// </remarks>
        public static bool IntersectsLine(this HexVector2 hex, Vector2 p, Vector2 v)
        {
            var corners = hex.GetAllCorners();
            var v3 = v.ToVector3();

            // first check cross-product of p to each corner
            // if any two have a different sign, then the line intersects the hex
            bool negative = false;
            bool positive = false;
            for (int i = 0; i < 6; i++)
            {
                Vector3 a = (corners[i] - p).ToVector3();
                float cross_z = (a.Cross(v3)).Z;
                if (Mathf.Abs(cross_z) < Epsilon)
                {
                    return true; // corner is on the line, considerd to intersect
                }
                else if (cross_z < 0)
                {
                    if (positive)
                    {
                        return true;
                    }

                    negative = true;
                }
                else if (cross_z > 0)
                {
                    if (negative)
                    {
                        return true;
                    }

                    positive = true;
                }
            }

            return false;
        }

        public static IReadOnlyCollection<HexVector2> GetHexesTo(this HexVector2 origin, HexVector2 target)
        {
            HashSet<HexVector2> checkedHexes = new() { origin };
            HashSet<HexVector2> validHexes = new() { origin, target };

            HexVector2 hv = target - origin;
            int len = hv.Length();
            int dir_x = Math.Sign(hv.X);
            int dir_y = Math.Sign(hv.Y);

            // define line from origin to target
            Vector2 p = origin.ToVector2();
            Vector2 v = hv.ToVector2();

            HexVector2 cur = origin;
            HexVector2 delta;
            HexVector2 temp;
            Stack<HexVector2> next = new();
            next.Push(cur);

            // walk out of origin, checking surrounding hexes of hexes that are determined intersect the line.
            int r = 0;
            while (++r <= len)
            {
                if (!next.TryPop(out cur))
                {
                    break;
                }
                // check six sides arround cur
                delta = HexVector2.UnitY;
                for (int i = 0; i < 6; i++)
                {
                    delta = delta.RotateCW();
                    temp = cur + delta;
                    if (checkedHexes.Add(temp)) // don't check hex if we already have.
                    {
                        // skip hexes if they are in the wrong direction.
                        if (delta.X == -dir_x)
                        {
                            continue;
                        }
                        else if (delta.Y == -dir_y)
                        {
                            continue;
                        }
                        if (temp.IntersectsLine(p, v))
                        {
                            next.Push(temp);
                            validHexes.Add(temp);
                        }
                    }
                }
            }

            return validHexes;
        }

        public static HexVector2 GetHex(this Vector2 v, IHexMath<Vector2> math = null)
        {
            return (math ?? HexMath.Default).VectorToHexVector2(v);
        }
    }
}
