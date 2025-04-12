using ProtoBuf;
using System;

namespace SpaceWarsHex.States.Systems
{
    [ProtoContract]
    public class ReactorState : SystemStateBase
    {
        [ProtoMember(1)]
        public Model.ReactorState CurrentState { get; set; }
        [ProtoMember(2)]
        public Model.ReactorState PreviousState { get; set; }
        [ProtoMember(3)]
        public int CurrentMaxPower { get; set; }
        [ProtoMember(4)]
        public bool UsingEmergencyPower { get; set; }
        [ProtoMember(5)]
        public bool UsedEmergencyPowerLastTurn { get; set; }
        [ProtoMember(6)]
        public int CurrentTurnPenalty { get; set; }
        [ProtoMember(7)]
        public int NextTurnPenalty { get; set; }
        [ProtoMember(8)]
        public int TurnsSpentAtAttackPower { get; set; }
        [ProtoMember(9)]
        public int PowerAllocated { get; set; }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(PrototypeId);
            hash.Add(Hash);
            hash.Add(Name);
            hash.Add(CurrentState);
            hash.Add(PreviousState);
            hash.Add(UsingEmergencyPower);
            hash.Add(UsedEmergencyPowerLastTurn);
            hash.Add(CurrentTurnPenalty);
            hash.Add(NextTurnPenalty);
            hash.Add(TurnsSpentAtAttackPower);
            return hash.ToHashCode();
        }
    }
}
