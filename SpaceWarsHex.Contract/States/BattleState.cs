using ProtoBuf;
using SpaceWarsHex.Model;
using SpaceWarsHex.States.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWarsHex.States
{
    [ProtoContract]
    public class BattleState
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public int TurnNumber { get; set; }
        [ProtoMember(4)]
        public TurnPhase CurrentPhase { get; set; }
    }
}
