using ReactiveUI;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using SpaceWarsHex.Prototypes;
using System;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public class OrdinanceViewModel : SystemViewModel, IViewModel<IOrdinancePrototype>
    {
        private IOrdinancePrototype? _saved;

        private FireMode _fireMode;
        private TurnPhase _firePhase;
        private int _maxRange;
        private int _strength;
        private int _maxUses;

        public OrdinanceViewModel()
            : this(new OrdinancePrototype())
        { }

        public OrdinanceViewModel(OrdinancePrototype prototype)
        {
            _saved = prototype ?? throw new ArgumentNullException(nameof(prototype));
            LoadFrom(_saved);
        }

        public override void LoadFrom(ISystemPrototype prototype)
        {
            base.LoadFrom(prototype);

            if (prototype is IOrdinancePrototype op)
            {
                _saved = op;
                FireMode = op.FireMode;
                FirePhase = op.FirePhase;
                MaxRange = op.MaxRange;
                Strength = op.Strength;
                MaxUses = op.MaxUses;
            }
        }

        public override void SaveTo(ISystemPrototype prototype)
        {
            base.SaveTo(prototype);

            if (prototype is OrdinancePrototype op)
            {
                op.FireMode = FireMode;
                op.FirePhase = FirePhase;
                op.MaxRange = MaxRange;
                op.Strength = Strength;
                op.MaxUses = MaxUses;
            }
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

        public int MaxRange
        {
            get => _maxRange;
            set => this.RaiseAndSetIfChanged(ref _maxRange, value);
        }

        public int Strength
        {
            get => _strength;
            set => this.RaiseAndSetIfChanged(ref _strength, value);
        }

        public int MaxUses
        {
            get => _maxUses;
            set => this.RaiseAndSetIfChanged(ref _maxUses, value);
        }
    }
}