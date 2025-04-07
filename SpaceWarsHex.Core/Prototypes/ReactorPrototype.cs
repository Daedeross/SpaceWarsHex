﻿using SpaceWarsHex.Interfaces.Prototypes;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Prototypes
{
    /// <inheritdoc />
    [DataContract]
    public class ReactorPrototype : SystemPrototypeBase, IReactorPrototype
    {
        /// <inheritdoc />
        [DataMember]
        public int CruisePower { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int AttackPower { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int EmergencyPower { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int MaxTurnsAtAttackPower { get; set; }
    }
}
