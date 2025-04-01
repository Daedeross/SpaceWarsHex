using SpaceWars.Interfaces.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace SpaceWars.Prototypes
{
#pragma warning disable CS8618
    /// <inheritdoc/>
    [Serializable]
    [DataContract]
    [KnownType(nameof(KnownTypes))]
    public abstract class HexObjectPrototypeBase : IHexObjectPrototype
    {
        /// <inheritdoc/>
        [DataMember]
        public string Name { get; set; }

        /// <inheritdoc/>
        [DataMember]
        public string VisualKey { get; set; }

        internal static readonly IEnumerable<Type> KnownTypes;

        static HexObjectPrototypeBase()
        {
            KnownTypes = Assembly.GetAssembly(typeof(HexObjectPrototypeBase))
                .GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(HexObjectPrototypeBase)));
        }
#pragma warning restore CS8618
    }
}
