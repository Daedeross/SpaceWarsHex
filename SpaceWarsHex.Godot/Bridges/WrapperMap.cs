using SpaceWarHex.Entities;
using SpaceWarsHex.Interfaces;
using System.Collections.Generic;

namespace SpaceWarHex.Bridges
{
    public class WrapperMap
    {
        private readonly Dictionary<IHexObject, WrapperNode2D<IHexObject>> _map = new();

        public void AddItem(WrapperNode2D<IHexObject> item)
        {
            _map.Add(item.Entity, item);
        }

        public bool TryGetItem(IHexObject key, out WrapperNode2D<IHexObject> item)
        {
            return _map.TryGetValue(key, out item);
        }

        public TOut CastItem<TOut>(IHexObject key)
            where TOut : WrapperNode2D<IHexObject>
        {
            return (TOut)_map[key];
        }
    }
}
