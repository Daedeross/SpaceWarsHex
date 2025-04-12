using ProtoBuf;
using System;

namespace SpaceWarsHex.States.Systems
{
    [ProtoContract]
    public class EnergyWeaponState : SystemStateBase
    {
        [ProtoMember(1)]
        public int CurrentMaxDice { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, PrototypeId, Hash, Name, CurrentMaxDice);
        }
    }
}
