using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Orders
{
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    public class MoveOrder : IMoveOrder
    {
        /// <inheritdoc />
        [DataMember]
        public HexVector2 Acceleration { get; set; }
    }
}
