using ProtoBuf;
using SpaceWarsHex.Model;
using System;

namespace SpaceWarsHex.States.Entities
{
    [ProtoContract]
    [ProtoInclude(4, typeof(MovingHexObjectState))]
    public class HexObjectState : StateBase
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public int TeamNumber { get; set; }
        [ProtoMember(3)]
        public HexVector2 Position {  get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, PrototypeId, Hash, Name, TeamNumber, Position);
        }
    }
}
