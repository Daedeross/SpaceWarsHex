using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using System;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Prototypes
{
    /// <inheritdoc/>
    [Serializable]
    [DataContract]
    public class MultiHexObjectPrototypeBase : HexObjectPrototypeBase, IMultiHexObjectPrototype
    {
    }
}
