using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Prototypes
{
    /// <inheritdoc />
    [DataContract]
    public class TorpedoPrototype : HexObjectPrototypeBase, ITorpedoPrototye
    {
        /// <inheritdoc />
        [DataMember]
        public TurnPhase FirePhase { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int Strength { get; set; }

        /// <inheritdoc />
        [DataMember]
        public HomingType Homing { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int HomingLoss { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int Acceleration { get; set; }
    }
}
