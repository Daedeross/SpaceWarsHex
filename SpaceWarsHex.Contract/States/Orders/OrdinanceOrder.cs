﻿using ProtoBuf;
using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;

namespace SpaceWarsHex.States.Orders
{
    [ProtoContract]
    public class OrdinanceOrder: WeaponOrder
    {
        [ProtoMember(3)]
        public bool Clear { get; set; }
        [ProtoMember(2)]
        public OridinanceLoad Load { get; set; }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(TurnNumber);
            hash.Add(WeaponIndex);
            hash.Add(TargetId);
            hash.Add(TargetHex);
            hash.Add(Velocity);
            hash.Add(Orientation6);
            hash.Add(Orientation12);
            hash.Add(Clear);
            hash.Add(Load);
            return hash.ToHashCode();
        }
    }
}
