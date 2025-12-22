using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using SpaceWarsHex.Prototypes;
using System.Reactive;
using System.Reactive.Linq;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class ShipViewModel : DocumentViewModelBase<ShipPrototype>, IViewModel<ShipPrototype>, IDocumentViewModel
    {
        private readonly IDefaultValueProvider _defaultValueProvider;
        private readonly IViewModelFactory _viewModelFactory;
        private ShipPrototype _current;

        public ShipViewModel(IDefaultValueProvider defaultValueProvider, IViewModelFactory viewModelFactory, IPrototypeSerializer serializer)
            : this(defaultValueProvider.GetDefaultValue<ShipPrototype>(), defaultValueProvider, viewModelFactory, serializer)
        { }

        public ShipViewModel(ShipPrototype prototype, IDefaultValueProvider defaultValueProvider,
            IViewModelFactory viewModelFactory, IPrototypeSerializer serializer)
            : base(prototype, serializer)
        {
            _visual = prototype.Visual ?? new RenderDefinition
            {
                Kind = SingleRenderKind.Sprite,
                Path = "Assets/Sprites/SimpleShip.png"
            };
            _name = prototype.Name;
            _visualKey = prototype.VisualKey;

            _saved = prototype ?? throw new ArgumentNullException(nameof(prototype));
            _defaultValueProvider = defaultValueProvider;
            _viewModelFactory = viewModelFactory;
            _current = _saved;

            Reactor = _current.Reactor is ReactorPrototype rp ? new ReactorViewModel(rp) : new ReactorViewModel();
            Drive = _current.Drive is DrivePrototype dp ? new DriveViewModel(dp) : new DriveViewModel();
            Shields = _current.Shields is ShieldsPrototype sp ? new ShieldsViewModel(sp) : new ShieldsViewModel();
            Hull = _current.Hull is HullPrototype hp ? new HullViewModel(hp) : new HullViewModel();

            EnergyWeapons = _viewModelFactory.For<EnergyWeaponsViewModel, EnergyWeaponPrototype>(_current.EnergyWeapons.OfType<EnergyWeaponPrototype>());

            Ordinances = new ObservableCollectionExtended<OrdinanceViewModel>(
                (_current.Ordinances ?? Enumerable.Empty<IOrdinancePrototype>())
                .OfType<OrdinancePrototype>()
                .Select(o => new OrdinanceViewModel(o))
            );

            //SaveCommand = ReactiveCommand.Create(Save);
            //ResetCommand = ReactiveCommand.Create(Reset);
        }

        #region Base Properties

        [Reactive]
        private RenderDefinition _visual;

        [Reactive]
        private string _name;

        [Reactive]
        private string _visualKey;

        #endregion

        [Reactive]
        private ReactorViewModel _reactor;

        [Reactive]
        private DriveViewModel _drive;

        [Reactive]
        private ShieldsViewModel _shields;

        [Reactive]
        private HullViewModel _hull;

        [Reactive]
        private EnergyWeaponsViewModel _energyWeapons;

        [Reactive]
        private EnergyWeaponViewModel? _selectedEnergyWeapon;

        public IObservableCollection<OrdinanceViewModel> Ordinances { get; }

        public override void SaveTo(ShipPrototype prototype)
        {
            prototype.Name = _name;
            prototype.Visual = _visual;
            prototype.VisualKey = _visualKey;
            Reactor.SaveTo(prototype._reactor);
            Drive.SaveTo(prototype._drive);
            Shields.SaveTo(prototype._shields);
            Hull.SaveTo(prototype._hull);

            prototype._ordinances = Ordinances.Select(o => o.ToPrototype()).Cast<IOrdinancePrototype>().ToList();
            prototype._energyWeapons = [];
            foreach (var ew in EnergyWeapons.EnergyWeapons)
            {
                var proto = new EnergyWeaponPrototype();
                ew.SaveTo(proto);
                prototype._energyWeapons.Add(proto);
            }
        }

        public override void LoadFrom(ShipPrototype prototype)
        {
            Name = prototype.Name;
            Visual = prototype.Visual;
            Reactor.LoadFrom(prototype._reactor);
            Drive.LoadFrom(prototype._drive);
            Shields.LoadFrom(prototype._shields);
            Hull.LoadFrom(prototype._hull);
        }

        [ReactiveCommand]
        private void Reset()
        {
            LoadFrom(_saved);
        }

        //[ReactiveCommand]
        //private void NewEnergyWeapon()
        //{
        //    var defaultEnergyWeapon = _defaultValueProvider.GetDefaultValue<EnergyWeaponPrototype>();
        //    var newWeaponVM = new EnergyWeaponViewModel(defaultEnergyWeapon);
        //    EnergyWeapons.Add(newWeaponVM);
        //    SelectedEnergyWeapon ??= newWeaponVM;
        //}

        //[ReactiveCommand(CanExecute = nameof(DeleteEnergyWeaponCanExecute))]
        //private void DeleteEnergyWeapon()
        //{
        //    var index = EnergyWeapons.IndexOf(SelectedEnergyWeapon!);
        //    index = Math.Max(index - 1, 0);
        //    EnergyWeapons.Remove(SelectedEnergyWeapon!);
        //    SelectedEnergyWeapon = EnergyWeapons.ElementAtOrDefault(index);
        //}

        //private bool DeleteEnergyWeaponCanExecute()
        //{
        //    return EnergyWeapons.Any()
        //        && SelectedEnergyWeapon is not null
        //        && EnergyWeapons.Contains(SelectedEnergyWeapon);
        //}

        [ReactiveCommand]
        private void NewOrdinance()
        {
            var defaultOrdinance = _defaultValueProvider.GetDefaultValue<OrdinancePrototype>();
            var newOrdinanceVM = new OrdinanceViewModel(defaultOrdinance);
            Ordinances.Add(newOrdinanceVM);
        }
    }
}