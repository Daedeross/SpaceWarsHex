using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWars.Prototypes
{
    /// <inheritdoc/>
    [Serializable]
    [DataContract]
    public class MultiHexObjectPrototypeBase : HexObjectPrototypeBase, IMultiHexObjectPrototype
    {
    }
}
