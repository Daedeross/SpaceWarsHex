using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Prototypes
{
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    public class TransporterBombPrototype : OrdinancePrototype, ITransporterBombPrototype
    {
        /// <inheritdoc />
        [DataMember]
        public int DetonationDelay { get; set; }

        /// <inheritdoc />
        [DataMember]
        public TurnPhase DetonationPhase { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int RevealDelay { get; set; }

        /// <inheritdoc />
        [DataMember]
        public TurnPhase RevealPhase { get; set; }
    }
}
