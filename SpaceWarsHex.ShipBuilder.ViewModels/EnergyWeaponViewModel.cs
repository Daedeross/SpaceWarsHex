using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using SpaceWarsHex.Prototypes;
using System.Reactive.Disposables.Fluent;
using System.Reflection;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class EnergyWeaponViewModel : SystemViewModel, IViewModel<IEnergyWeaponPrototype>
    {
        private static readonly FieldInfo _effectsField = typeof(EnergyWeaponPrototype).GetField("_effects", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetOrThrow();

        private IEnergyWeaponPrototype? _saved;

        [Reactive]
        private int _maxDice;
        [Reactive]
        private FireMode _fireMode;
        [Reactive]
        private TurnPhase _firePhase;
        [Reactive]
        private bool _visual;
        [Reactive]
        private int _energyPerDie;
        [Reactive]
        private int _maxRange;
        [Reactive]
        private WeaponEffectsViewModel _effects = new();

        [ObservableAsProperty]
        private int _maxEnergy;

        public EnergyWeaponViewModel()
            : this(new EnergyWeaponPrototype())
        { }

        public EnergyWeaponViewModel(EnergyWeaponPrototype prototype)
            : base(prototype)
        {
            _saved = prototype ?? throw new ArgumentNullException(nameof(prototype));

            _maxEnergyHelper = this
                .WhenAnyValue(x => x.MaxDice, x => x.EnergyPerDie,
                    (dice, energy) => dice * energy)
                .ToProperty(this, x => x.MaxEnergy)
                .DisposeWith(_disposables);

            LoadFrom(_saved);
        }

        public override void LoadFrom(ISystemPrototype prototype)
        {
            base.LoadFrom(prototype);

            if (prototype is IEnergyWeaponPrototype ep)
            {
                _saved = ep;
                MaxDice = ep.MaxDice;
                FireMode = ep.FireMode;
                FirePhase = ep.FirePhase;
                Visual = ep.Visual;
                EnergyPerDie = ep.EnergyPerDie;
                MaxRange = ep.MaxRange;

                Effects = new WeaponEffectsViewModel(ep.Effects ?? []);
            }
        }

        public override void SaveTo(ISystemPrototype prototype)
        {
            base.SaveTo(prototype);

            if (prototype is IEnergyWeaponPrototype)
            {
                // Save basic properties via the concrete type when possible
                if (prototype is EnergyWeaponPrototype concrete)
                {
                    concrete.MaxDice = MaxDice;
                    concrete.FireMode = FireMode;
                    concrete.FirePhase = FirePhase;
                    concrete.Visual = Visual;
                    concrete.EnergyPerDie = EnergyPerDie;
                    concrete.MaxRange = MaxRange;

                    // _effects is internal in the prototype. Set it via reflection so the prototype's Effects reflect VM changes.
                    var list = Effects.ToList() ?? [];
                    _effectsField.SetValue(concrete, list);
                }
                else
                {
                    // If we only have the interface (unlikely for concrete save), try best-effort via reflection on runtime type
                    var field = prototype.GetType().GetField("_effects", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (field != null)
                    {
                        var list = Effects.ToList() ?? [];
                        field.SetValue(prototype, list);
                    }
                }
            }
        }
    }
}