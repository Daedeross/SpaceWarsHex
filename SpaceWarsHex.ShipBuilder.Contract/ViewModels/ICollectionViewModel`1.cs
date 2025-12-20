using SpaceWarsHex.Interfaces.Prototypes;
using System.Collections.Generic;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public interface ICollectionViewModel<T>
        where T : class, IPrototype
    {
        ICollection<T> GetPrototypes();
    }
}
