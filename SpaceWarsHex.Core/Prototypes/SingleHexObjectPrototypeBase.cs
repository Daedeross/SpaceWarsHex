using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWars.Prototypes
{
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    public abstract class SingleHexObjectPrototypeBase : HexObjectPrototypeBase, ISingleHexObjectPrototype
    {
#pragma warning disable CS8618 // Null-checking will be done uing comprehensive Validators
        /// <inheritdoc />
        [DataMember]
        public RenderDefinition Visual { get; set; }
#pragma warning restore CS8618
    }
}
