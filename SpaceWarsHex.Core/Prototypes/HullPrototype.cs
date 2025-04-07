using SpaceWarsHex.Interfaces.Prototypes;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Prototypes
{
    /// <inheritdoc />
    [DataContract]
    public class HullPrototype : SystemPrototypeBase, IHullPrototype
    {
        /// <inheritdoc />
        [DataMember]
        public int MaxIntegrity { get; set; }
    }
}
