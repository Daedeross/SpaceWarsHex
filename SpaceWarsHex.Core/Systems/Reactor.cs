using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.Model;

namespace SpaceWarsHex.Systems
{
    /// <summary>
    /// WIP implementation of <see cref="IReactor"/>
    /// </summary>
    public class Reactor : SystemBase, IReactor, IHaveState<States.Systems.ReactorState>
    {
        private States.Systems.ReactorState _state;

        /// <summary>
        /// TODO: replace with localized strings later.
        /// </summary>
        public override string Name => "Reactor";

        /// <inheritdoc />
        private int m_CruisePower;
        public int CruisePower
        {
            get => m_CruisePower;
            protected set
            {
                if (RaiseAndSetIfChanged(ref m_CruisePower, value))
                {
                    RaisePropertyChanged(nameof(CurrentAvailablePower));
                }
            }
        }

        /// <inheritdoc />
        private int m_AttackPower;
        public int AttackPower
        {
            get => m_AttackPower;
            protected set
            {
                if (RaiseAndSetIfChanged(ref m_AttackPower, value))
                {
                    RaisePropertyChanged(nameof(CurrentAvailablePower));
                }
            }
        }

        /// <inheritdoc />
        public int EmergencyPower { get; }

        /// <inheritdoc />
        public int MaxTurnsAtAttackPower { get; protected set; }

        /// <inheritdoc />
        public ReactorState CurrentState
        {
            get => _state.CurrentState;
            set
            {
                if (value != _state.CurrentState)
                {
                    _state.CurrentState = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(CurrentAvailablePower));
                }
            }
        }

        /// <inheritdoc />
        public bool UsingEmergencyPower
        {
            get => _state.UsingEmergencyPower;
            set
            {
                if (value != _state.UsingEmergencyPower)
                {
                    _state.UsingEmergencyPower = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(CurrentAvailablePower));
                }
            }
        }

        /// <inheritdoc />
        public ReactorState PreviousState
        {
            get => _state.PreviousState;
            set => _state.PreviousState = value;
        }

        /// <inheritdoc />
        public bool UsedEmergencyPowerLastTurn
        {
            get => _state.UsedEmergencyPowerLastTurn;
            set => _state.UsedEmergencyPowerLastTurn = value;
        }

        /// <inheritdoc />
        public int CurrentTurnPenalty
        {
            get => _state.CurrentTurnPenalty;
            set
            {
                if (_state.CurrentTurnPenalty != value)
                {
                    _state.CurrentTurnPenalty = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(CurrentAvailablePower));
                }
            }
        }

        /// <inheritdoc />
        public int NextTurnPenalty { get; set; }

        /// <inheritdoc />
        public int CurrentMaxPower
        {
            get => _state.CurrentMaxPower;
            protected set
            {
                if (_state.CurrentMaxPower != value)
                {
                    _state.CurrentMaxPower = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(CurrentAvailablePower));
                }
            }
        }

        /// <inheritdoc />
        public int CurrentAvailablePower
        {
            get
            {
                int power = CurrentState switch
                {
                    ReactorState.Cruise => CruisePower,
                    ReactorState.Attack => AttackPower,
                    _ => throw new NotSupportedException($"Unrecognized {nameof(ReactorState)}: {CurrentState}"),
                };
                return Math.Min(power, CurrentMaxPower)
                     - CurrentTurnPenalty + (UsingEmergencyPower ? EmergencyPower : 0);
            }
        }

        /// <inheritdoc />
        public int PowerAllocated
        {
            get => _state.PowerAllocated;
            set
            {
                if (value != _state.PowerAllocated)
                {
                    _state.PowerAllocated = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <inheritdoc />
        public int TurnsSpentAtAttackPower
        {
            get => _state.TurnsSpentAtAttackPower;
            set
            {
                if (value != _state.TurnsSpentAtAttackPower)
                {
                    _state.TurnsSpentAtAttackPower = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="prototype">The prototype to create the reactor from.</param>
        public Reactor(IReactorPrototype prototype)
            : base(prototype)
        {
            CruisePower = prototype.CruisePower;
            AttackPower = prototype.AttackPower;
            EmergencyPower = prototype.EmergencyPower;
            MaxTurnsAtAttackPower = prototype.MaxTurnsAtAttackPower;

            _state = new()
            {
                Id                         = Guid.NewGuid(),
                PrototypeId                = prototype.Id,
                Name                       = prototype.Name,
                CurrentState               = ReactorState.Cruise,
                PreviousState              = ReactorState.Cruise,
                UsingEmergencyPower        = false,
                UsedEmergencyPowerLastTurn = false,
                CurrentTurnPenalty         = 0,
                NextTurnPenalty            = 0,
                TurnsSpentAtAttackPower    = 0,
                PowerAllocated             = 0,
                CurrentMaxPower            = Math.Max(AttackPower, CruisePower)
            };
        }

        /// <inheritdoc />
        public override void ApplyDamage(int currentHull, int maxHull)
        {
            var multiplier = _damageThresholds.GetThresholdMultiplier(currentHull, maxHull);
            CurrentMaxPower = Convert.ToInt32(Math.Max(AttackPower, CruisePower) * multiplier);
        }

        /// <inheritdoc />
        public override void HandleEndOfTurn(int turnNumber)
        {
            CurrentTurnPenalty = NextTurnPenalty;
            UsedEmergencyPowerLastTurn = UsingEmergencyPower;
            UsingEmergencyPower = false;
            if (UsedEmergencyPowerLastTurn)
            {
                CurrentTurnPenalty += EmergencyPower;
            }
            NextTurnPenalty = 0;
            PreviousState = CurrentState;
            if (PreviousState == ReactorState.Attack)
            {
                TurnsSpentAtAttackPower++;
            }
            if (TurnsSpentAtAttackPower >= MaxTurnsAtAttackPower)
            {
                CurrentState = ReactorState.Cruise;
            }
        }

        #region IHaveState
        public States.Systems.ReactorState GetState()
        {
            _state.Hash = _state.GetHashCode();
            return _state;
        }

        public void SetState(States.Systems.ReactorState state)
        {
            _state = state;
            RaisePropertyChanged(nameof(CurrentState));
            RaisePropertyChanged(nameof(PreviousState));
            RaisePropertyChanged(nameof(UsingEmergencyPower));
            RaisePropertyChanged(nameof(UsedEmergencyPowerLastTurn));
            RaisePropertyChanged(nameof(CurrentTurnPenalty));
            RaisePropertyChanged(nameof(NextTurnPenalty));
            RaisePropertyChanged(nameof(TurnsSpentAtAttackPower));
            RaisePropertyChanged(nameof(PowerAllocated));
            RaisePropertyChanged(nameof(CurrentMaxPower));
        }

        public int GetStateHash()
        {
            return _state.GetHashCode();
        }
        #endregion // IHaveState
    }
}
