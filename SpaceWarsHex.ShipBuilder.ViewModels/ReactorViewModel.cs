using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Prototypes;
using System;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class ReactorViewModel : SystemViewModel, IViewModel<IReactorPrototype>
    {
        private IReactorPrototype? _saved;

        private int m_CruisePower;
        public int CruisePower
        {
            get => m_CruisePower;
            set
            {
                if (value != m_CruisePower)
                {
                    this.RaiseAndSetIfChanged(ref m_CruisePower, value);
                    AttackPower = Math.Max(AttackPower, CruisePower);
                }
            }
        }

        private int m_AttackPower;
        public int AttackPower
        {
            get => m_AttackPower;
            set
            {
                if (value != m_AttackPower)
                {
                    this.RaiseAndSetIfChanged(ref m_AttackPower, value);
                    CruisePower = Math.Min(CruisePower, AttackPower);
                }
            }
        }

        [Reactive]
        private int _emergencyPower;
        [Reactive]
        private int _maxTurnsAtAttackPower;

        public ReactorViewModel()
            : this(new ReactorPrototype() { Name = "Reactor", Id = Guid.NewGuid() })
        { }

        public ReactorViewModel(ReactorPrototype prototype)
            : base(prototype)
        {
            _saved = prototype.GetOrThrow();
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
    }
}