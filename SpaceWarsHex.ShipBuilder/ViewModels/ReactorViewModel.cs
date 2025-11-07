using ReactiveUI;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;
using System;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public class ReactorViewModel : SystemViewModel, IViewModel<IReactorPrototype>
    {
        private IReactorPrototype? _saved;
        private int _cruisePower;
        private int _attackPower;
        private int _emergencyPower;
        private int _maxTurnsAtAttackPower;

        public ReactorViewModel()
            : this(new ReactorPrototype())
        { }

        public ReactorViewModel(ReactorPrototype prototype)
        {
            _saved = prototype ?? throw new ArgumentNullException(nameof(prototype));
            LoadFrom(_saved);
        }

        public override void LoadFrom(ISystemPrototype prototype)
        {
            base.LoadFrom(prototype);

            if (prototype is IReactorPrototype rp)
            {
                _saved = rp;
                CruisePower = rp.CruisePower;
                AttackPower = rp.AttackPower;
                EmergencyPower = rp.EmergencyPower;
                MaxTurnsAtAttackPower = rp.MaxTurnsAtAttackPower;
            }
        }

        public override void SaveTo(ISystemPrototype prototype)
        {
            base.SaveTo(prototype);

            if (prototype is ReactorPrototype rp)
            {
                rp.CruisePower = CruisePower;
                rp.AttackPower = AttackPower;
                rp.EmergencyPower = EmergencyPower;
                rp.MaxTurnsAtAttackPower = MaxTurnsAtAttackPower;
            }
        }

        public int CruisePower
        {
            get => _cruisePower;
            set => this.RaiseAndSetIfChanged(ref _cruisePower, value);
        }

        public int AttackPower
        {
            get => _attackPower;
            set => this.RaiseAndSetIfChanged(ref _attackPower, value);
        }

        public int EmergencyPower
        {
            get => _emergencyPower;
            set => this.RaiseAndSetIfChanged(ref _emergencyPower, value);
        }

        public int MaxTurnsAtAttackPower
        {
            get => _maxTurnsAtAttackPower;
            set => this.RaiseAndSetIfChanged(ref _maxTurnsAtAttackPower, value);
        }
    }
}