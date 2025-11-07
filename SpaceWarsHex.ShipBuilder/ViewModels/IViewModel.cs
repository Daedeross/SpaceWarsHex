using SpaceWarsHex.Interfaces.Prototypes;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public interface IViewModel<T> where T : class, IPrototype
    {
    }
}
