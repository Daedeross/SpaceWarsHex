using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Orders
{
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    public class ReactorOrder : IReactorOrder
    {
        /// <inheritdoc />
        [DataMember]
        public ReactorState State { get; set; }

        /// <inheritdoc />
        [DataMember]
        public bool UseEmergencyPower { get; set; }
    }
}
