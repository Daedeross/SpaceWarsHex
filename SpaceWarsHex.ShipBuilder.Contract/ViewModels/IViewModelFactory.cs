using SpaceWarsHex.Interfaces.Prototypes;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public interface IViewModelFactory
    {
        T Create<T>() where T : class, IViewModel;

        TViewModel For<TViewModel, TModel>(TModel model) where TViewModel : class, IViewModel<TModel> where TModel : class, IPrototype;

        IViewContainer CreateContainer(string title, IViewModel content, bool ownsContent);

        void Release(object viewModel);
    }
}
