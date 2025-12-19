using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWarsHex.ShipBuilder.Configuration
{
    public class DefaultValueProvider : IDefaultValueProvider
    {
        private ConcurrentDictionary<Type, ICloneable> _defaultValues = new();
        private ConcurrentDictionary<Type, List<ICloneable>> _defaultLists = new();

        public T GetDefaultValue<T>()
            where T : ICloneable
        {
            if (_defaultValues.TryGetValue(typeof(T), out var value))
            {
                return (T)value.Clone();
            }
            throw new InvalidOperationException($"No default value set for type {typeof(T).FullName}");
        }

        public List<T> GetDefaultValues<T>() where T : ICloneable
        {
            if (_defaultLists.TryGetValue(typeof(T), out var list))
            {
                return [.. list.Select(item => (T)item.Clone())];
            }
            throw new InvalidOperationException($"No default values set for type {typeof(T).FullName}");
        }

        public bool HasDefaultValue<T>()
            where T : ICloneable
        {
            return _defaultValues.ContainsKey(typeof(T));
        }

        public void SetDefaultValue<T>(T value)
            where T : ICloneable
        {
            _defaultValues[typeof(T)] = value!;
        }

        public void SetDefaultValues<T>(ICollection<T> values) where T : ICloneable
        {
            _defaultLists[typeof(T)] = [.. values.Cast<ICloneable>()];
        }
    }
}
