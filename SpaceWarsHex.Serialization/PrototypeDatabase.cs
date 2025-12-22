using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Prototypes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWarsHex.Serialization
{
    public class PrototypeDatabase : IPrototypeCache
    {
        //private readonly ConcurrentDictionary<Guid, IPrototype> _cache = new();
        private readonly ConcurrentDictionary<Type, ConcurrentDictionary<Guid, IPrototype>> _cache = new();

        /// <summary>
        /// Adds a prototype to the DB if it does not exist yet,
        /// otherwise gets the prototype based on the ID
        /// </summary>
        /// <typeparam name="TPrototype"></typeparam>
        /// <param name="prototype"></param>
        /// <returns>The prototype</returns>
        public TPrototype GetOrAdd<TPrototype>(TPrototype prototype)
            where TPrototype : IPrototype, IHaveId
        {
            var dict = _cache.AddOrUpdate(
                typeof(TPrototype),
                _ => new ConcurrentDictionary<Guid, IPrototype>
                {
                    [prototype.Id] = prototype,
                },
                (_, dict) => {
                    dict.AddOrUpdate(prototype.Id, prototype, (id, p) => prototype);
                    return dict;
                });

            var output = dict[prototype.Id];

            if (output is TPrototype prototype1)
            {
                return prototype1;
            }
            else throw new InvalidCastException(
                $"The existing prototype with id {prototype.Id} is not of type {typeof(TPrototype)}");
        }

        public void AddOrUpdate<TPrototype>(TPrototype prototype)
            where TPrototype : IPrototype, IHaveId
        {
            _cache.AddOrUpdate(
                typeof(TPrototype),
                _ => new ConcurrentDictionary<Guid, IPrototype>
                {
                    [prototype.Id] = prototype,
                },
                (_, dict) => {
                    dict.AddOrUpdate(prototype.Id, prototype, (id, p) => prototype);
                    return dict;
                });
        }

        public bool TryGetValue<TPrototype>(Guid id, out TPrototype? prototype)
            where TPrototype : IPrototype, IHaveId
        {
            if (_cache.TryGetValue(typeof(TPrototype), out var dict))
            {
                if (dict.TryGetValue(id, out var obj))
                {
                    if (obj is TPrototype proto)
                    {
                        prototype = proto;
                        return true;
                    }
                }
            }
            // fallthough
            prototype = default;

            return false;
        }

        public IEnumerable<TPrototype> GetAllOfType<TPrototype>()
            where TPrototype : IPrototype
        {
            if (_cache.TryGetValue(typeof(TPrototype), out var dict))
            {
                return dict.Values
                    .Cast<TPrototype>()
                    .ToList();
            }

            return [];
        }
    }
}
