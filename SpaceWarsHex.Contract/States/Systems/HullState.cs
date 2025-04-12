using ProtoBuf;
using System;

namespace SpaceWarsHex.States.Systems
{
    [ProtoContract]
    public class HullState : SystemStateBase
    {
        [ProtoMember(1)]
        public int PendingDamage { get; set; }
        [ProtoMember(2)]
        public int CurrentIntegrity { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, PrototypeId, Hash, Name, PendingDamage, CurrentIntegrity);
        }
    }
}
