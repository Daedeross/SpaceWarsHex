using ReactiveUI;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;
using SpaceWarsHex.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public class EnergyWeaponViewModel : SystemViewModel, IViewModel<IEnergyWeaponPrototype>
    {
        private IEnergyWeaponPrototype? _saved;

        private int _maxDice;
        private FireMode _fireMode;
        private TurnPhase _firePhase;
        private bool _visual;
        private int _energyPerDie;
        private int _maxRange;

        public EnergyWeaponViewModel()
            : this(new EnergyWeaponPrototype())
        { }

        public EnergyWeaponViewModel(EnergyWeaponPrototype prototype)
        {
            _saved = prototype ?? throw new ArgumentNullException(nameof(prototype));
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

                Effects = new ObservableCollection<WeaponEffect>((ep.Effects ?? Enumerable.Empty<WeaponEffect>()).ToList());
            }
        }

        public override void SaveTo(ISystemPrototype prototype)
        {
            base.SaveTo(prototype);

            if (prototype is IEnergyWeaponPrototype ep)
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
                    var field = typeof(EnergyWeaponPrototype).GetField("_effects", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (field != null)
                    {
                        var list = Effects?.ToList() ?? new System.Collections.Generic.List<WeaponEffect>();
                        field.SetValue(concrete, list);
                    }
                }
                else
                {
                    // If we only have the interface (unlikely for concrete save), try best-effort via reflection on runtime type
                    var field = prototype.GetType().GetField("_effects", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (field != null)
                    {
                        var list = Effects?.ToList() ?? new System.Collections.Generic.List<WeaponEffect>();
                        field.SetValue(prototype, list);
                    }
                }
            }
        }

        public int MaxDice
        {
            get => _maxDice;
            set => this.RaiseAndSetIfChanged(ref _maxDice, value);
        }

        public FireMode FireMode
        {
            get => _fireMode;
            set => this.RaiseAndSetIfChanged(ref _fireMode, value);
        }

        public TurnPhase FirePhase
        {
            get => _firePhase;
            set => this.RaiseAndSetIfChanged(ref _firePhase, value);
        }

        public bool Visual
        {
            get => _visual;
            set => this.RaiseAndSetIfChanged(ref _visual, value);
        }

        public int EnergyPerDie
        {
            get => _energyPerDie;
            set => this.RaiseAndSetIfChanged(ref _energyPerDie, value);
        }

        public int MaxRange
        {
            get => _maxRange;
            set => this.RaiseAndSetIfChanged(ref _maxRange, value);
        }

        /// <summary>
        /// The list of effects for the weapon. This is a simple collection exposed for binding.
        /// Saving will copy this collection back into the prototype (via reflection when necessary).
        /// </summary>
        public ObservableCollection<WeaponEffect> Effects { get; private set; } = new();
    }
}