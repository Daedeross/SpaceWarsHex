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
    public partial class EnergyWeaponViewModel : SystemViewModel<EnergyWeaponPrototype>
    {
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
            _maxEnergyHelper = this
                .WhenAnyValue(x => x.MaxDice, x => x.EnergyPerDie,
                    (dice, energy) => dice * energy)
                .ToProperty(this, x => x.MaxEnergy)
                .DisposeWith(_disposables);
        }

        public override void LoadFrom(EnergyWeaponPrototype prototype)
        {
            base.LoadFrom(prototype);
            MaxDice = prototype.MaxDice;
            FireMode = prototype.FireMode;
            FirePhase = prototype.FirePhase;
            Visual = prototype.Visual;
            EnergyPerDie = prototype.EnergyPerDie;
            MaxRange = prototype.MaxRange;

            Effects = new WeaponEffectsViewModel(prototype.Effects ?? []);
        }

        public override void SaveTo(EnergyWeaponPrototype prototype)
        {
            base.SaveTo(prototype);

            prototype.MaxDice = MaxDice;
            prototype.FireMode = FireMode;
            prototype.FirePhase = FirePhase;
            prototype.Visual = Visual;
            prototype.EnergyPerDie = EnergyPerDie;
            prototype.MaxRange = MaxRange;
            prototype._effects = Effects.ToList();
        }
    }
}