using SpaceWarsHex.Interfaces.Prototypes;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Prototypes
{
    /// <inheritdoc />
    [DataContract]
    public class DrivePrototype : SystemPrototypeBase, IDrivePrototype
    {
        /// <inheritdoc />
        [DataMember]
        public int MaxWarp { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int AccelerationClass { get; set; }

    }
}
