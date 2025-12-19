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

        override public object Clone()
        {
            var clone = new HullPrototype()
            {
                MaxIntegrity = MaxIntegrity
            };
            CopyTo(clone);
            return clone;
        }
    }
}
