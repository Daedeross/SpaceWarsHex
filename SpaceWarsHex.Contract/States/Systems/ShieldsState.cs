using ProtoBuf;
using System;

namespace SpaceWarsHex.States.Systems
{
    [ProtoContract]
    public class ShieldsState : SystemStateBase
    {
        [ProtoMember(1)]
        public int MaxPowerAvailable { get; set; }
        [ProtoMember(2)]
        public int AllocatedPower { get; set; }
        [ProtoMember(3)]
        public int CurrentPower { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, PrototypeId, Hash, Name, MaxPowerAvailable, AllocatedPower, CurrentPower);
        }
    }
}
