using System;

namespace SpaceWarsHex.Model
{
    /// <summary>
    /// Hex-coordinate based Axialy Aligned Bounding Box
    /// </summary>
    public class HexAABB
    {
        public HexVector2 Min { get; set; }

        public HexVector2 Max { get; set; }

        public HexAABB() { }
        public HexAABB(HexVector2 min, HexVector2 max)
        {
            if (min == null)
            {
                throw new ArgumentNullException(nameof (min));
            }
            if (max == null)
            {
                throw new ArgumentNullException(nameof (max));
            }

            Min = min;
            Max = max;
        }

        public bool Contains(HexVector2 hex)
            => hex.X >= Min.X && hex.Y >= Min.Y
            && hex.X <= Max.X && hex.Y <= Max.Y;

        public bool Intersects(HexAABB other)
            => Contains(other.Min) || Contains(other.Max);
    }
}
