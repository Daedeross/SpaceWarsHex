using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Prototypes;
using System.Reactive;
using System.Reactive.Linq;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class EnergyWeaponsViewModel : ViewModelBase, ICollectionViewModel<EnergyWeaponPrototype>
    {
        public IObservableCollection<EnergyWeaponViewModel> EnergyWeapons { get; }

        public IObservableCollection<EnergyWeaponPrototype> StockEnergyWeapons { get; }

        [Reactive]
        private EnergyWeaponViewModel? _selectedWeapon;

        [Reactive]
        private EnergyWeaponPrototype? _selectedStockWeapon;

        public EnergyWeaponsViewModel(IPrototypeCache prototypeCache)
            : this(prototypeCache, [])
        { }

        public EnergyWeaponsViewModel(IPrototypeCache prototypeCache, IEnumerable<EnergyWeaponPrototype> models)
        {
            EnergyWeapons = new ObservableCollectionExtended<EnergyWeaponViewModel>(models.Select(p => new EnergyWeaponViewModel(p)));
            var pregenWeapons = prototypeCache.GetAllOfType<IPregenWeapons>()
                .SelectMany(weps => weps.EnergyWeapons)
                .OfType<EnergyWeaponPrototype>();
            StockEnergyWeapons = new ObservableCollectionExtended<EnergyWeaponPrototype>(pregenWeapons);
        }

        public ICollection<EnergyWeaponPrototype> GetPrototypes()
        {
            return EnergyWeapons.Select(vm => vm.GetPrototype()).Cast<EnergyWeaponPrototype>().ToList();
        }

        #region Commands

        //[ReactiveCommand(CanExecute = nameof(AddEnergyWeaponCanExecute))]
        //private void AddEnergyWeapon()
        //{
        //    var proto = (EnergyWeaponPrototype)_selectedStockWeapon!.Clone();

        //    EnergyWeapons.Add(new EnergyWeaponViewModel(proto));
        //}

        //private bool AddEnergyWeaponCanExecute() => _selectedStockWeapon is not null;

        [ReactiveCommand]
        private void AddEnergyWeapon(EnergyWeaponPrototype prototype)
        {
            EnergyWeapons.Add(new EnergyWeaponViewModel(prototype));
            SelectedWeapon = EnergyWeapons.Last();
        }

        [ReactiveCommand]
        private void RemoveEnergyWeapon(EnergyWeaponViewModel viewModel)
        {
            var index = Math.Max(EnergyWeapons.IndexOf(viewModel) - 1, 0);
            EnergyWeapons.Remove(viewModel);
            SelectedWeapon = EnergyWeapons.ElementAtOrDefault(index);
        }

        //[ReactiveCommand(CanExecute = nameof(RemoveEnergyWeaponCanExecute))]
        //private void RemoveEnergyWeapon()
        //{
        //    EnergyWeapons.Remove(_selectedWeapon!);
        //}

        //private bool RemoveEnergyWeaponCanExecute() => _selectedWeapon != null && EnergyWeapons.Contains(_selectedWeapon);

        #endregion // Commands
    }
}
