using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using SpaceWarsHex.Prototypes;
using System.Reactive.Linq;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class ShipViewModel : ViewModelBase, IViewModel<IShipPrototype>, IDocumentViewModel
    {
        private ShipPrototype? _saved = null;
        private ShipPrototype _current;

        public ShipViewModel()
            : this(new ShipPrototype())
        { }

        public ShipViewModel(ShipPrototype prototype)
        {
            _visual = prototype.Visual ?? new RenderDefinition
            {
                Kind = SingleRenderKind.Sprite,
                Path = "Assets/Sprites/SimpleShip.png"
            };
            _name = prototype.Name;
            _visualKey = prototype.VisualKey;

            _saved = prototype ?? throw new ArgumentNullException(nameof(prototype));
            _current = _saved;

            Reactor = _current.Reactor is ReactorPrototype rp ? new ReactorViewModel(rp) : new ReactorViewModel();
            Drive = _current.Drive is DrivePrototype dp ? new DriveViewModel(dp) : new DriveViewModel();
            Shields = _current.Shields is ShieldsPrototype sp ? new ShieldsViewModel(sp) : new ShieldsViewModel();
            Hull = _current.Hull is HullPrototype hp ? new HullViewModel(hp) : new HullViewModel();

            EnergyWeapons = new ObservableCollectionExtended<OrdinanceViewModel>(
                (_current.Ordinances ?? Enumerable.Empty<IOrdinancePrototype>())
                .OfType<OrdinancePrototype>()
                .Select(o => new OrdinanceViewModel(o))
            );

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

        public IObservableCollection<OrdinanceViewModel> EnergyWeapons { get; }
        public IObservableCollection<OrdinanceViewModel> Ordinances { get; }

        //public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        //public ReactiveCommand<Unit, Unit> ResetCommand { get; }

        [ReactiveCommand]
        private void Save()
        {
            if (_saved == null) throw new InvalidOperationException("No saved prototype available to save into.");

            // Save nested systems (each will write into their saved prototype)
            Reactor.SaveCommand.Execute().Subscribe();
            Drive.SaveCommand.Execute().Subscribe();
            Shields.SaveCommand.Execute().Subscribe();
            Hull.SaveCommand.Execute().Subscribe();

            // Save collection items if needed. Currently the OrdinanceViewModel/derived SaveCommand
            // will mutate the underlying prototype instance if constructed with it.
            foreach (var ev in EnergyWeapons) ev.SaveCommand.Execute().Subscribe();
            foreach (var o in Ordinances) o.SaveCommand.Execute().Subscribe();
        }

        [ReactiveCommand]
        private void Reset()
        {
            if (_saved == null) throw new InvalidOperationException("No saved prototype available to reset from.");

            Reactor.ResetCommand.Execute().Subscribe();
            Drive.ResetCommand.Execute().Subscribe();
            Shields.ResetCommand.Execute().Subscribe();
            Hull.ResetCommand.Execute().Subscribe();

            foreach (var ev in EnergyWeapons) ev.ResetCommand.Execute().Subscribe();
            foreach (var o in Ordinances) o.ResetCommand.Execute().Subscribe();
        }
    }
}