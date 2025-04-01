using SpaceWars.Interfaces.Orders;
using SpaceWars.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWars.Orders
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
