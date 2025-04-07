using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Orders
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
