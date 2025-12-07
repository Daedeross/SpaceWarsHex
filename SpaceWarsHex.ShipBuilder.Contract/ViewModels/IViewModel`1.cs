using SpaceWarsHex.Interfaces.Prototypes;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public interface IViewModel<T> : IViewModel
        where T : class, IPrototype
    {
    }
}
