using ProtoBuf;
using System;

namespace SpaceWarsHex.States.Systems
{
    [ProtoContract]
    [ProtoInclude(2, typeof(ReactorState))]
    [ProtoInclude(3, typeof(HullState))]
    [ProtoInclude(4, typeof(ShieldsState))]
    [ProtoInclude(5, typeof(EnergyWeaponState))]
    [ProtoInclude(6, typeof(OrdinanceState))]
    public class SystemStateBase: StateBase
    {
        [ProtoMember(1)]
        public string Name { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, PrototypeId, Hash, Name);
        }
    }
}
