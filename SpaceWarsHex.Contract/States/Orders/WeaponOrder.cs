using ProtoBuf;
using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Model;
using System;

namespace SpaceWarsHex.States.Orders
{
    [ProtoContract]
    [ProtoInclude(10, typeof(EnergyWeaponOrder))]
    [ProtoInclude(11, typeof(OrdinanceOrder))]
    public class WeaponOrder : OrderBase, IWeaponOrder
    {
        [ProtoMember(1)]
        public int WeaponIndex { get; set; }
        [ProtoMember(2)]
        public Guid? TargetId { get; set; }
        [ProtoMember(3)]
        public HexVector2? TargetHex { get; set; }
        [ProtoMember(4)]
        public HexVector2? Velocity { get; set; }
        [ProtoMember(5)]
        public Direction6? Orientation6 { get; set; }
        [ProtoMember(6)]
        public Direction12? Orientation12 { get; set; }
    }
}
