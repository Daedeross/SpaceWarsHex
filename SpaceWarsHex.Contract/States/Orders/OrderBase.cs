using ProtoBuf;
using SpaceWarsHex.Interfaces.Orders;
using System;

namespace SpaceWarsHex.States.Orders
{
    [ProtoContract]
    [ProtoInclude(2, typeof(MoveOrder))]
    [ProtoInclude(2, typeof(ReactorOrder))]
    [ProtoInclude(2, typeof(ShieldOrder))]
    [ProtoInclude(2, typeof(WeaponOrder))]
    public class OrderBase: IOrder
    {
        [ProtoMember(1)]
        public int TurnNumber { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(TurnNumber);
        }
    }
}
