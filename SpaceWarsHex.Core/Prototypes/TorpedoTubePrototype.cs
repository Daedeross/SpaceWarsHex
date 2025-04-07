using SpaceWarsHex.Interfaces.Prototypes;
using System;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Prototypes
{
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    public class TorpedoTubePrototype : OrdinancePrototype, ITorpedoTubePrototype
    {
        /// <inheritdoc />
        [DataMember]
        public int MinWarp { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int MaxWarp { get; set; }

        /// <inheritdoc />
        [DataMember]
        public bool Homing { get; set; }

        /// <inheritdoc />
        [DataMember]
        public bool LoadFire { get; set; }
    }
}
