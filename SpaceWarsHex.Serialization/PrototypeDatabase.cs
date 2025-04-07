using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Prototypes;
using System;
using System.Collections.Concurrent;

namespace SpaceWarsHex.Serialization
{
    public class PrototypeDatabase
    {
        private readonly ConcurrentDictionary<Guid, IPrototype> _cache = new ConcurrentDictionary<Guid, IPrototype>();

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
            var output = _cache.GetOrAdd(prototype.Id, prototype);

            if (output is TPrototype prototype1)
            {
                return prototype1;
            }
            else throw new InvalidCastException(
                $"The existing prototype with id {prototype.Id} is not of type {typeof(TPrototype)}");
        }
    }
}
