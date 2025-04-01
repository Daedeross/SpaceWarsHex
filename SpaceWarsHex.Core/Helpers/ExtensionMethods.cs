using SpaceWars.Helpers;
using SpaceWars.Interfaces;
using SpaceWars.Interfaces.Systems;
using SpaceWars.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SpaceWars
{
    /// <summary>
    /// Helper extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Checks if an <see cref="OrderResult"/> is for a successful state.
        /// </summary>
        /// <param name="orderResult"><see cref="OrderResult"/></param>
        /// <returns>True if the order was accepted, false if not.</returns>
        public static bool Success(this OrderResult orderResult)
        {
            return (int)(orderResult.Status) / 100 == 2;
        }

        /// <summary>
        /// Get the system power multiplier using the ratio of current/max hull.
        /// </summary>
        /// <param name="thresholds">Enumeration of <see cref="DamageThreshold"/> for the system.</param>
        /// <param name="currentRatio"></param>
        /// <returns></returns>
        public static float GetThresholdMultiplier(this IEnumerable<DamageThreshold> thresholds, float currentRatio)
        {
            if (!thresholds.Any(dt => dt.HullStrength >= currentRatio))
            {
                return 1f;
            }

            var threshold = thresholds.First(dt => dt.HullStrength >= currentRatio);
            return threshold.SystemMultiplier;
        }

        /// <summary>
        /// Get the system power multiplier using the current and max hull values.
        /// </summary>
        /// <param name="thresholds">Enumeration of <see cref="DamageThreshold"/> for the system.</param>
        /// <param name="currentHull">The ship's current hull value.</param>
        /// <param name="maxHull">The ship's maximum hull value.</param>
        /// <returns></returns>
        public static float GetThresholdMultiplier(this IEnumerable<DamageThreshold> thresholds, int currentHull, int maxHull)
        {
            float ratio = (float)currentHull / (float)maxHull;

            return GetThresholdMultiplier(thresholds, ratio);
        }

        /// <summary>
        /// Get the system's power multiplier using the ship's <see cref="IHull"/> instance.
        /// </summary>
        /// <param name="thresholds">Enumeration of <see cref="DamageThreshold"/> for the system.</param>
        /// <param name="hull"><see cref="IHull"/></param>
        /// <returns></returns>
        public static float GetThresholdMultiplier(this IEnumerable<DamageThreshold> thresholds, IHull hull)
        {
            return GetThresholdMultiplier(thresholds, hull.CurrentIntegrity, hull.MaxIntegrity);
            
        }

        /// <summary>
        /// Get the system's power multiplier using the ship's <see cref="IHull"/> instance.
        /// </summary>
        /// <param name="system">The <see cref="ISystem"/> to query.</param>
        /// <param name="hull">The system owner's <see cref="IHull"/>.</param>
        /// <returns></returns>
        public static float GetThresholdMultiplier(this ISystem system, IHull hull)
        {
            return GetThresholdMultiplier(system.DamageThresholds, hull);
        }

        /// <summary>
        /// Helper to simplify ApplyDamage calls.
        /// </summary>
        /// <param name="system">The <see cref="ISystem"/> to apply damage to.</param>
        /// <param name="hull">The <see cref="IHull"/> of the system's parent ship.</param>
        public static void ApplyDamage(this ISystem system, IHull hull)
        {
            system.ApplyDamage(hull.CurrentIntegrity, hull.MaxIntegrity);
        }

        /// <summary>
        /// Checks if a <see cref="IHexObject"/> collides with a given hex.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="hex"></param>
        /// <returns>True if the given <paramref name="obj"/> occupies <paramref name="hex"/>,
        /// otherwise false.</returns>
        /// <remarks>
        /// Tests if the object is single-hex or multi-hex, then does the appropriate checks.
        /// </remarks>
        public static bool CollidesWith(this IHexObject obj, HexVector2 hex)
        {
            if (obj is IMultiHexObject multi)
            {
                return multi.CheckHex(hex);
            }
            else
            {
                return obj.Position == hex;
            }
        }

        /// <summary>
        /// Gets a value from a dictionary. If the key does not exist,
        /// creates the value using the given factory
        /// and adds it to the dictionary at the given key.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"><see cref="IDictionary{TKey, TValue}"/></param>
        /// <param name="key">The <typeparamref name="TKey"/> to lookup.</param>
        /// <param name="addValueFactory">Callback to create the value if <paramref name="key"/> does not exist.</param>
        /// <returns>The current value at the given key, whether preexisting or created by <paramref name="addValueFactory"/></returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> addValueFactory)
        {
            if (!dictionary.TryGetValue(key, out var value))
            {
                value = addValueFactory();
                dictionary[key] = value;
            }

            return value;
        }

        /// <summary>
        /// Adds a value to a dicitonary or applies an update lambda to the existing value if the key already exists.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary's key.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary's value.</typeparam>
        /// <param name="dictionary">The dictionary to act upon.</param>
        /// <param name="key">The key to seek.</param>
        /// <param name="newValue">The value to add if the key does not already exist.</param>
        /// <param name="updateFactory">The update factory to apply uppon the existing value if the key already exists.</param>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue newValue, Func<TValue, TValue> updateFactory)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                dictionary[key] = updateFactory(value);
            }
            else
            {
                dictionary[key] = newValue;
            }
        }

        /// <summary>
        /// Adds a value to a dicitonary via a factory callback or applies an update lambda to the existing value if the key already exists.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary's key.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary's value.</typeparam>
        /// <param name="dictionary">The dictionary to act upon.</param>
        /// <param name="key">The key to seek.</param>
        /// <param name="newValueFactory">The lambda to generate the value to add if the key does not yet exist.</param>
        /// <param name="updateFactory">The update factory to apply uppon the existing value if the key already exists.</param>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> newValueFactory, Func<TValue, TValue> updateFactory)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                dictionary[key] = updateFactory(value);
            }
            else
            {
                dictionary[key] = newValueFactory();
            }
        }

        /// <summary>
        /// Adds an item to an inner collection within a dictionary.
        /// Creates a new collection if none exists at the given key..
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddOrAppend<TKey, TCollection, TValue>(this IDictionary<TKey, TCollection> dictionary, TKey key, TValue value)
            where TCollection : ICollection<TValue>, new()
        {
            dictionary.AddOrUpdate(
                key,
                () => new TCollection() { value },
                collection => { collection.Add(value); return collection; });
        }

        /// <summary>
        /// Removes a single value from a collection which is itself a value of a dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary's key.</typeparam>
        /// <typeparam name="TCollection">The type of the inner collection.</typeparam>
        /// <typeparam name="TValue">The type of the value to remove.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TCollection}"/></param>
        /// <param name="key">The key to lookup the inner collection.</param>
        /// <param name="value">The value to find in the inner collection.</param>
        /// <remarks>If the inner collection is empty after removing <paramref name="value"/>
        /// then the collection will also be removed.
        /// Not thread safe.
        /// </remarks>
        public static void RemoveAndClean<TKey, TCollection, TValue>(this IDictionary<TKey, TCollection> dictionary, TKey key, TValue value)
            where TCollection : ICollection<TValue>, new()
        {
            var collection = dictionary[key];
            collection.Remove(value);
            if (collection.Count == 0)
            {
                dictionary.Remove(key);
            }
            else
            {
                dictionary[key] = collection;
            }
        }

        /// <summary>
        /// Recalulates the indices of a <see cref="HexMatrix{TEntity}"/>
        /// by re-adding all entities.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="matrix"></param>
        /// <remarks>Intended for testing only.</remarks>
        public static void Recalculate<TEntity>(this HexMatrix<TEntity> matrix)
            where TEntity : IHexObject
        {
            var entities = matrix.ToList();
            matrix.Clear();
            foreach (var entity in entities)
            {
                matrix.Add(entity);
            }
        }

        /// <summary>
        /// Mapping of a <see cref="IWeapon"/>'s <see cref="FireMode"/> to the corresponding <see cref="OrderKind"/>.
        /// </summary>
        /// <param name="fireMode">The <see cref="FireMode"/> to map.</param>
        /// <returns><see cref="OrderKind"/></returns>
        /// <exception cref="NotImplementedException">For an un-implemnted <see cref="FireMode"/></exception>
        public static OrderKind ToOrderKind(this FireMode fireMode)
        {
            return fireMode switch
            {
                FireMode.DirectFire => OrderKind.DirectFireOder,
                FireMode.Beam => OrderKind.BeamOrder,
                FireMode.Bomb => OrderKind.BombOrder,
                FireMode.Torpedo => OrderKind.TorpedoOrder,
                _ => throw new NotImplementedException("Additional fire modes not yet implemented.")
            };
        }

        /// <summary>
        /// Enumerates all the enum names as a string list.
        /// </summary>
        /// <param name="type">They enum type to enumerate.</param>
        /// <param name="separator">The list delimiter.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If <paramref name="type"/> is not an enum type.</exception>
        public static string ToStringList(this Type type, string separator = ",")
        {
            if (!typeof(Enum).IsAssignableFrom(type))
            {
                throw new ArgumentException($"'{nameof(type)}' input is not an enum type.");
            }

            return string.Join(separator, Enum.GetNames(type));
        }

        /// <summary>
        /// Get all entities of a type (i.e. IsAssignableTo).
        /// </summary>
        /// <typeparam name="T">The type of entities to return.</typeparam>
        /// <returns><see cref="IReadOnlyCollection{T}"/> of filtered entities</returns>
        public static IReadOnlyList<T> GetEntities<T>(this IDirector director)
            where T : IHexObject
        {
            return [.. director.AllEntities.OfType<T>()];
        }
    }
}