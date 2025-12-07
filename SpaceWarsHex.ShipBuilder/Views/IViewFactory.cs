using ReactiveUI;

namespace SpaceWarsHex.ShipBuilder.Views
{
    public interface IViewFactory
    {
        IViewFor<T> For<T>()
            where T : class;
    }
}
