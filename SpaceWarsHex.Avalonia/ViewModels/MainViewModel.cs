using Avalonia.Controls.ApplicationLifetimes;
using DynamicData.Binding;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.ShipBuilder.ViewModels;

namespace SpaceWarsHex.Avalonia.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public ObservableCollectionExtended<ShipViewModel> Ships { get; set; } = new();

        [Reactive]
        private ShieldsViewModel? _currentShip;

        public MainViewModel()
        {
        }

        #region Commands

        [ReactiveCommand]
        private void NewShip()
        {
            Ships.Add(new ShipViewModel());
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
            if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
            {
                lifetime.Shutdown();
            }
        } 

        #endregion
    }
}