using System;
using System.Collections.Generic;

namespace SpaceWarsHex.ShipBuilder
{
    public interface IDefaultValueProvider
    {
        bool HasDefaultValue<T>()
            where T : ICloneable;
        T GetDefaultValue<T>()
            where T : ICloneable;
        List<T> GetDefaultValues<T>()
            where T : ICloneable;
        void SetDefaultValue<T>(T value)
            where T : ICloneable;
        void SetDefaultValues<T>(ICollection<T> values)
            where T : ICloneable;
    }
}
