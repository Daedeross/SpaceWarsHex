using SpaceWars.Interfaces.Prototypes;
using System.Runtime.Serialization;

namespace SpaceWars.Prototypes
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
