using SpaceWars.Interfaces.Prototypes;
using System.Runtime.Serialization;

namespace SpaceWars.Prototypes
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
