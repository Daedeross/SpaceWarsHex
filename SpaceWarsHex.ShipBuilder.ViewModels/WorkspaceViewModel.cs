using DynamicData.Binding;
using ReactiveUI.SourceGenerators;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class WorkspaceViewModel : ViewModelBase
    {
        public WorkspaceViewModel()
        {
        }

        public IObservableCollection<ShipViewModel> Ships { get; } = new ObservableCollectionExtended<ShipViewModel>();

        [Reactive]
        private ShipViewModel? _currentShip;

        #region Commands

        [ReactiveCommand]
        private void NewShip()
        {
            Ships.Add(new ShipViewModel() {  Name = "New Ship" });
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
        }

        private bool SaveShipCanExecute() => _currentShip is not null;

        [ReactiveCommand]
        private void SaveShipAs()
        {
            // TODO
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
