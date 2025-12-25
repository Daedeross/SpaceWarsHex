using ProtoBuf;
using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Model;
using System;

namespace SpaceWarsHex.States.Orders
{
    [ProtoContract]
    public class MoveOrder : OrderBase, IMoveOrder
    {
        [ProtoMember(1)]
        public HexVector2 Acceleration { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(TurnNumber, Acceleration);
        }
    }
}
