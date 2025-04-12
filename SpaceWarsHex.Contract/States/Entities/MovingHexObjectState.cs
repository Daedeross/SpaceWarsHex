using ProtoBuf;
using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;

namespace SpaceWarsHex.States.Entities
{
    [ProtoContract]
    [ProtoInclude(5, typeof(ShipState))]
    public class MovingHexObjectState : HexObjectState
    {
        [ProtoMember(1)]
        public HexVector2 Velocity {  get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, PrototypeId, Hash, Name, TeamNumber, Position, Velocity);
        }
    }
}
