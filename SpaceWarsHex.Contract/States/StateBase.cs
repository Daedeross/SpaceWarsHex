using ProtoBuf;
using SpaceWarsHex.States.Entities;
using SpaceWarsHex.States.Systems;
using System;

namespace SpaceWarsHex.States
{
    [ProtoContract]
    [ProtoInclude(4, typeof(HexObjectState))]
    [ProtoInclude(5, typeof(SystemStateBase))]
    public class StateBase
    {
        [ProtoMember(1)]
        public Guid Id { get; set; }
        [ProtoMember(2)]
        public Guid PrototypeId { get; set; }
        [ProtoMember(3)]
        public int Hash { get; set; }
    }
}
