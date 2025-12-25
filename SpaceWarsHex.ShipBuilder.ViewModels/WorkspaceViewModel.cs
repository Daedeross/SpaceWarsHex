using DynamicData.Binding;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class WorkspaceViewModel : ViewModelBase
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IDefaultValueProvider _defaultValueProvider;

        public WorkspaceViewModel(IViewModelFactory viewModelFactory, IDefaultValueProvider defaultValueProvider)
        {
            _viewModelFactory = viewModelFactory;
            _defaultValueProvider = defaultValueProvider;
        }

        public IObservableCollection<ShipViewModel> Ships { get; } = new ObservableCollectionExtended<ShipViewModel>();

        [Reactive]
        private ShipViewModel? _currentShip;

        #region Commands

        [ReactiveCommand]
        private void NewShip()
        {
            var proto = _defaultValueProvider.GetDefaultValue<ShipPrototype>();
            proto.Id = Guid.NewGuid();
            var vm = _viewModelFactory.For<ShipViewModel, ShipPrototype>(proto);
            Ships.Add(vm);
            CurrentShip = Ships[^1];
        }

        [ReactiveCommand]
        private void OpenShip()
        {
            // TODO
        }

        [ReactiveCommand(CanExecute = nameof(SaveShipCanExecute))]
        private void SaveShip()
        {
            CurrentShip?.SaveAsCommand?.Execute().Subscribe();
        }

        private bool SaveShipCanExecute() => _currentShip is not null;

        [ReactiveCommand]
        private void SaveShipAs()
        {
            CurrentShip?.SaveAsCommand?.Execute().Subscribe();
        }

        [ReactiveCommand]
        private void CloseShip(ShipViewModel ship)
        {
            Ships.Remove(ship);
        }

        [ReactiveCommand]
        private void CloseAll()
        {
            Ships.Clear();
        }

        [ReactiveCommand]
        private void Exit()
        {
            // TODO
        }

        #endregion
    }
}
