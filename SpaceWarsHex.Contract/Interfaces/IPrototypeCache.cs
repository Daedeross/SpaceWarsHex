using SpaceWarsHex.Interfaces.Prototypes;
using System;

namespace SpaceWarsHex.Interfaces
{
    /// <summary>
    /// Runtime cache or registered <see cref="IPrototype"/>s.
    /// </summary>
    public interface IPrototypeCache
    {
        /// <summary>
        /// Get's the prototype with the same Id or adds it to the cache it
        /// it does not exist yet.
        /// </summary>
        /// <typeparam name="TPrototype"></typeparam>
        /// <param name="prototype"></param>
        /// <returns></returns>
        TPrototype GetOrAdd<TPrototype>(TPrototype prototype);

        /// <summary>
        /// Try to get a protoype from the cache.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="prototype"></param>
        /// <returns></returns>
        bool TryGetValue(Guid id, out IPrototype prototype);

        /// <summary>
        /// Try to get a protoype from the cache and cast it the the given type.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="prototype"></param>
        /// <returns></returns>
        bool TryGetValue<TPrototype>(Guid id, out TPrototype prototype)
            where TPrototype : IPrototype, IHaveId;
    }
}
