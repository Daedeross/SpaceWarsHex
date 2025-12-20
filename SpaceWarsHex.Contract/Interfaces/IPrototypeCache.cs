using SpaceWarsHex.Interfaces.Prototypes;
using System;
using System.Collections.Generic;

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
        TPrototype GetOrAdd<TPrototype>(TPrototype prototype)
            where TPrototype : IPrototype, IHaveId;

        /// <summary>
        /// Replaces the existing prototype with the same Id or addes it if none exists with the same Id.
        /// </summary>
        /// <typeparam name="TPrototype"></typeparam>
        /// <param name="prototype"></param>
        void AddOrUpdate<TPrototype>(TPrototype prototype)
            where TPrototype : IPrototype, IHaveId;

        /// <summary>
        /// Try to get a protoype from the cache and cast it the the given type.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="prototype"></param>
        /// <returns></returns>
        bool TryGetValue<TPrototype>(Guid id, out TPrototype? prototype)
            where TPrototype : IPrototype, IHaveId;

        /// <summary>
        /// Gets all cached prototypes of the given type.
        /// </summary>
        /// <typeparam name="TPrototype">The type of prototype to get.</typeparam>
        /// <returns>An <see cref="IEnumerable{T}"/> of <typeparamref name="TPrototype"/>. Can be empty.</returns>
        IEnumerable<TPrototype> GetAllOfType<TPrototype>()
            where TPrototype : IPrototype;
    }
}
