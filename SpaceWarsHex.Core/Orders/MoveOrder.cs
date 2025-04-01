using SpaceWars.Interfaces.Orders;
using SpaceWars.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWars.Orders
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
