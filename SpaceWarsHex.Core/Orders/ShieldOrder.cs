using SpaceWars.Interfaces.Orders;
using SpaceWars.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWars.Orders
{
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    public class ShieldOrder : IShieldOrder
    {
        /// <inheritdoc />
        [DataMember]
        public int Power { get; set; }
    }
}
