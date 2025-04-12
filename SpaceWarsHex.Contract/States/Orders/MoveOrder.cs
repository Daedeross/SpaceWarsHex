using ProtoBuf;
using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;

namespace SpaceWarsHex.States.Orders
{
    [ProtoContract]
    public class MoveOrder : OrderBase
    {
        [ProtoMember(1)]
        public HexVector2 Acceleration { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(TurnNumber, Acceleration);
        }
    }
}
