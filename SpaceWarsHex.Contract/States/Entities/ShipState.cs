using ProtoBuf;
using SpaceWarsHex.States.Orders;
using SpaceWarsHex.States.Systems;
using System;
using System.Collections.Generic;

namespace SpaceWarsHex.States.Entities
{
    [ProtoContract]
    public class ShipState: MovingHexObjectState
    {
        [ProtoMember(1)]
        public ReactorState Reactor { get; set; }
        [ProtoMember(2)]
        public SystemStateBase Drive { get; set; }
        [ProtoMember(3)]
        public ShieldsState Shields { get; set; }
        [ProtoMember(4)]
        public HullState Hull { get; set; }
        [ProtoMember(5)]
        public List<EnergyWeaponState> EnergyWeapons { get; set; }
        [ProtoMember(6)]
        public List<OrdinanceState> Ordinances { get; set; }
        [ProtoMember(7)]
        public List<OrderBase> CurrentOrders { get; set; }
        [ProtoMember(8)]
        public List<OrderBase> PastOrders { get; set; }

        public override int GetHashCode()
        {
            HashCode hash = new();
            hash.Add(base.GetHashCode());
            hash.Add(Id);
            hash.Add(PrototypeId);
            hash.Add(Hash);
            hash.Add(Name);
            hash.Add(TeamNumber);
            hash.Add(Position);
            hash.Add(Velocity);
            hash.Add(Reactor);
            hash.Add(Drive);
            hash.Add(Shields);
            hash.Add(Hull);
            foreach (var ewp in EnergyWeapons)
            {
                hash.Add(ewp);
            }
            foreach (var ord in Ordinances)
            {
                hash.Add(ord);
            }
            foreach (var order in CurrentOrders)
            {
                hash.Add(order);
            }
            foreach (var order in PastOrders)
            {
                hash.Add(order);
            }
            return hash.ToHashCode();
        }
    }
}
