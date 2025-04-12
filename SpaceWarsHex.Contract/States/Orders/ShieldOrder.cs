using ProtoBuf;
using System;

namespace SpaceWarsHex.States.Orders
{
    [ProtoContract]
    public class ShieldOrder : OrderBase
    {
        [ProtoMember(1)]
        public int Power { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(TurnNumber, Power);
        }
    }
}
