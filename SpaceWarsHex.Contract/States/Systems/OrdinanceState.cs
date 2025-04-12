using ProtoBuf;
using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;

namespace SpaceWarsHex.States.Systems
{
    [ProtoContract]
    public class OrdinanceState: SystemStateBase
    {
        [ProtoMember(1)]
        public int UsesRemaining { get; set; }
        [ProtoMember(2)]
        public bool Loaded { get; set; }
        [ProtoMember(3)]
        public bool Loading { get; set; }
        [ProtoMember(4)]
        public HexVector2 LaunchVelocity { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, PrototypeId, Hash, Name, UsesRemaining, Loaded, Loading, LaunchVelocity);
        }
    }
}
