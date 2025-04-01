using SpaceWars.Interfaces.Prototypes;
using System.Runtime.Serialization;

namespace SpaceWars.Prototypes
{
    /// <inheritdoc />
    [DataContract]
    public class ShieldsPrototype : SystemPrototypeBase, IShieldsPrototype
    {
        /// <inheritdoc />
        [DataMember]
        public int MaxPower { get; set; }
    }
}
