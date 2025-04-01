using SpaceWars.Interfaces.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace SpaceWars.Prototypes
{
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    [KnownType(nameof(KnownTypes))]
    public class OrdinancePrototype : SystemPrototypeBase, IOrdinancePrototype
    {
        private static readonly IEnumerable<Type> _knownTypes;

        /// <summary>
        /// Collection of Known derrived types, used for Serialization/Deserialization.
        /// </summary>
        public static new IEnumerable<Type> KnownTypes() => _knownTypes;

        static OrdinancePrototype()
        {
            _knownTypes = Assembly.GetAssembly(typeof(OrdinancePrototype))
                .GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(OrdinancePrototype)));
        }

        /// <inheritdoc />
        [DataMember]
        public int Strength { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int MaxUses { get; set; }
    }
}
