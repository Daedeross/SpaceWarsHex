using SpaceWars.Interfaces.Orders;
using SpaceWars.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWars.Orders
{
    [Serializable]
    [DataContract]
    public class OrdinanceOrder : WeaponOrderBase, IOrdinanceOrder
    {
        [DataMember]
        public bool Clear { get; set; }
        [DataMember]
        public OridinanceLoad Load { get; set; }
    }
}
