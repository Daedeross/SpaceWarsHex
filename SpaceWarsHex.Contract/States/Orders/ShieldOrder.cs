using ProtoBuf;
using SpaceWarsHex.Interfaces.Orders;
using System;

namespace SpaceWarsHex.States.Orders
{
    [ProtoContract]
    public class ShieldOrder : OrderBase, IShieldOrder
    {
        [ProtoMember(1)]
        public int Power { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(TurnNumber, Power);
        }
    }
}
