using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Prototypes;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class ReactorViewModel : SystemViewModel<ReactorPrototype>
    {
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
        { }

        public override void LoadFrom(ReactorPrototype prototype)
        {
            base.LoadFrom(prototype);

            CruisePower = prototype.CruisePower;
            AttackPower = prototype.AttackPower;
            EmergencyPower = prototype.EmergencyPower;
            MaxTurnsAtAttackPower = prototype.MaxTurnsAtAttackPower;
        }

        public override void SaveTo(ReactorPrototype prototype)
        {
            base.SaveTo(prototype);

            prototype.CruisePower = CruisePower;
            prototype.AttackPower = AttackPower;
            prototype.EmergencyPower = EmergencyPower;
            prototype.MaxTurnsAtAttackPower = MaxTurnsAtAttackPower;
        }
    }
}
