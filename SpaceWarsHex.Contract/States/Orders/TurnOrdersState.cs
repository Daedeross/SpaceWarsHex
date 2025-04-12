using ProtoBuf;
using System.Collections.Generic;

namespace SpaceWarsHex.States.Orders
{
    [ProtoContract]
    public class TurnOrdersState
    {
        [ProtoMember(1)]
        public int TurnNumber { get; set; }
        [ProtoMember(2)]
        public List<OrderBase> Orders { get; set; }
    }
}
