using SpaceWarsHex.Interfaces.Prototypes;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Prototypes
{
    /// <inheritdoc />
    [DataContract]
    public class ShieldsPrototype : SystemPrototypeBase, IShieldsPrototype
    {
        /// <inheritdoc />
        [DataMember]
        public int MaxPower { get; set; }

        override public object Clone()
        {
            var clone = new ShieldsPrototype()
            {
                MaxPower = MaxPower
            };
            CopyTo(clone);
            return clone;
        }
    }
}
