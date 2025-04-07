using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Orders
{
    [Serializable]
    [DataContract]
    public abstract class WeaponOrderBase : IWeaponOrder
    {
        [DataMember]
        public int WeaponIndex { get; set; }
        [DataMember]
        public Guid? TargetId { get; set; }
        [DataMember]
        public HexVector2? TargetHex { get; set; }

        [DataMember]
        public HexVector2? Velocity { get; set; }

        [DataMember]
        public Direction6? Orientation6 { get; set; }

        [DataMember]
        public Direction12? Orientation12 { get; set; }
    }
}
