using ProtoBuf;
using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Model;
using System;

namespace SpaceWarsHex.States.Orders
{
    [ProtoContract]
    public class ReactorOrder : OrderBase, IReactorOrder
    {
        [ProtoMember(1)]
        public ReactorState DesiredState { get; set; }
        [ProtoMember(2)]
        public bool UseEmergencyPower { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(TurnNumber, DesiredState, UseEmergencyPower);
        }
    }
}
