using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWarsHex.Systems
{
    /// <summary>
    /// WIP implementation of <see cref="IReactor"/>
    /// </summary>
    public class Reactor : SystemBase, IReactor
    {
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
        private int m_EmergencyPower;
        public int EmergencyPower
        {
            get => m_EmergencyPower;
            protected set
            {
                if (RaiseAndSetIfChanged(ref m_EmergencyPower, value))
                {
                    RaisePropertyChanged(nameof(CurrentAvailablePower));
                }
            }
        }

        /// <inheritdoc />
        public int MaxTurnsAtAttackPower { get; protected set; }

        /// <inheritdoc />
        private ReactorState m_CurrentState;
        public ReactorState CurrentState
        {
            get => m_CurrentState;
            set
            {
                if (RaiseAndSetIfChanged(ref m_CurrentState, value))
                {
                    RaisePropertyChanged();
                }
            }
        }

        /// <inheritdoc />
        private bool m_UsingEmergencyPower;
        public bool UsingEmergencyPower
        {
            get => m_UsingEmergencyPower;
            set
            {
                if(RaiseAndSetIfChanged(ref m_UsingEmergencyPower, value))
                {
                    RaisePropertyChanged(nameof(CurrentAvailablePower));
                }
            }
        }

        /// <inheritdoc />
        public ReactorState PreviousState { get; set; }

        /// <inheritdoc />
        public bool UsedEmergencyPowerLastTurn { get; set; }

        /// <inheritdoc />
        private int m_CurrentTurnPenalty;
        public int CurrentTurnPenalty
        {
            get => m_CurrentTurnPenalty;
            set
            {
                if (RaiseAndSetIfChanged(ref m_CurrentTurnPenalty, value))
                {
                    RaisePropertyChanged(nameof(CurrentAvailablePower));
                }
            }
        }

        /// <inheritdoc />
        public int NextTurnPenalty { get; set; }

        /// <inheritdoc />
        private int m_CurrentMaxPower;
        public int CurrentMaxPower
        {
            get => m_CurrentMaxPower;
            protected set
            {
                if (RaiseAndSetIfChanged(ref m_CurrentMaxPower, value))
                {
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
        private int m_PowerAllocated;
        public int PowerAllocated
        {
            get => m_PowerAllocated;
            set => this.RaiseAndSetIfChanged(ref m_PowerAllocated, value);
        }

        /// <inheritdoc />
        public int TurnsSpentAtAttackPower { get; set; }

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

            CurrentState = ReactorState.Cruise;
            UsingEmergencyPower = false;
            PreviousState = ReactorState.Cruise;
            UsedEmergencyPowerLastTurn = false;
            CurrentMaxPower = Math.Max(AttackPower, CruisePower);
            CurrentTurnPenalty = 0;
            NextTurnPenalty = 0;
            PowerAllocated = 0;
            TurnsSpentAtAttackPower = 0;
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
    }
}
