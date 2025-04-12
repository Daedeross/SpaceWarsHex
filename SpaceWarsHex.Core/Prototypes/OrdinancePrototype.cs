using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using System.Reflection;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Prototypes
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
                ?.GetTypes()
                ?.Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(OrdinancePrototype)))
                ?? [];
        }

        /// <inheritdoc />
        [DataMember]
        public FireMode FireMode { get; set; }

        /// <inheritdoc />
        [DataMember]
        public TurnPhase FirePhase { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int MaxRange { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int Strength { get; set; }

        /// <inheritdoc />
        [DataMember]
        public int MaxUses { get; set; }
    }
}
