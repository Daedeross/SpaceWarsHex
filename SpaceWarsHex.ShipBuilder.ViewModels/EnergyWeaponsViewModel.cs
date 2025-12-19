using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Prototypes;
using System.Reactive;
using System.Reactive.Linq;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class EnergyWeaponsViewModel : ViewModelBase
    {
        public IObservableCollection<EnergyWeaponViewModel> EnergyWeapons { get; }

        public Interaction<Unit, EnergyWeaponPrototype?> SelectPremadeEnergyWeapon { get; } = new();

        public EnergyWeaponsViewModel(IEnumerable<EnergyWeaponPrototype> energyWeapons)
        {
            EnergyWeapons = new ObservableCollectionExtended<EnergyWeaponViewModel>(energyWeapons.Select(p => new EnergyWeaponViewModel(p)));
        }

        [ReactiveCommand]
        private async Task NewEnergyWeapon()
        {
            var newWeapon = await SelectPremadeEnergyWeapon.Handle(Unit.Default);
            if (newWeapon is null)
            {
                return;
            }

            EnergyWeapons.Add(new EnergyWeaponViewModel(newWeapon));
        }

        [ReactiveCommand]
        private void AddEnergyWeapon(EnergyWeaponPrototype prototype)
        {
            EnergyWeapons.Add(new EnergyWeaponViewModel(prototype));
        }

        [ReactiveCommand]
        private void RemoveEnergyWeapon(EnergyWeaponViewModel viewModel)
        {
            EnergyWeapons.Remove(viewModel);
        }
    }
}
