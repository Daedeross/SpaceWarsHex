using SpaceWars.Interfaces.Orders;
using SpaceWars.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWars.Orders
{
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    public class TorpedoTubeFireOrder : IOrdinanceOrder, ITorpedoFireOrder
    {
        /// <inheritdoc />
        [DataMember]
        public int WeaponIndex { get; set; }

        /// <inheritdoc />
        [DataMember]
        public bool Clear { get; set; }

        /// <inheritdoc />
        [DataMember]
        public HexVector2 Velocity { get; set; }
    }
}
