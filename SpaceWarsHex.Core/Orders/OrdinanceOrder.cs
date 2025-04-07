using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Orders
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
