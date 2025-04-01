using SpaceWars.Interfaces.Orders;
using SpaceWars.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWars.Orders
{
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    public class BombEnergyWeaponOrder : IEnergyWeaponOrder, IBombOrder
    {
        /// <inheritdoc />
        [DataMember]
        public int WeaponIndex { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int Power { get; set; }

        /// <inheritdoc />
        [DataMember]
        public HexVector2 TargetHex { get; set; }
    }
}
