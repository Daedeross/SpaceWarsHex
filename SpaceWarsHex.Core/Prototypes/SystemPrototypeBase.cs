using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Prototypes
{
    /// <inheritdoc/>
    [Serializable]
    [DataContract]
    [KnownType(nameof(KnownTypes))]
    public abstract class SystemPrototypeBase : ISystemPrototype
    {
        private static readonly IEnumerable<Type> _knownTypes;
        internal static IEnumerable<Type> KnownTypes() => _knownTypes;

        static SystemPrototypeBase()
        {
            _knownTypes = Assembly.GetAssembly(typeof(SystemPrototypeBase))
                ?.GetTypes()
                ?.Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(SystemPrototypeBase)))
                ?? [];
        }

        /// <inheritdoc/>
        public Guid Id { get; set; }

        /// <inheritdoc/>
        [DataMember]
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc/>
        [IgnoreDataMember]
        public IEnumerable<DamageThreshold> DamageThresholds => _damageThresholds;

        [DataMember(Name = "DamageThresholds")]
        internal List<DamageThreshold> _damageThresholds = [];
    }
}
