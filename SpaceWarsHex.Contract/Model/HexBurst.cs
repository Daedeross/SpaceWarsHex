using System;
using System.Runtime.Serialization;

namespace SpaceWars.Model
{
    /// <summary>
    /// Struct which represents a 'burst' (i.e. circle) on a hex grid.
    /// Radius 0 equates to a single hex.
    /// </summary>
    [Serializable]
    [DataContract]
    public struct HexBurst : IEquatable<HexBurst>
    {
        #region Fields
        [DataMember]
        public readonly HexVector2 Center;
        [DataMember]
        public readonly int Radius;
        #endregion

        public HexBurst(HexVector2 center, int radius)
        {
            Center = center;
            Radius = radius;
        }

        public HexBurst(int x, int y, int radius)
        {
            Center = new HexVector2(x, y);
            Radius = radius;
        }

        public bool Contains(HexVector2 v)
        {
            return (v - Center).Length() <= Radius;
        }

        public bool Intersects(HexBurst other)
        {
            return (Center - other.Center).Length() < Radius + other.Radius;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Center.X, Center.Y, Radius);
        }

        public bool Equals(HexBurst other)
        {
            return Equals(other.Center, Center)
                && Equals(other.Radius, Radius);
        }
    }
}
