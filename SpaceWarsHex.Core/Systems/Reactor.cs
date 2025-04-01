using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Interfaces.Systems;
using SpaceWars.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWars.Systems
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
        public int CruisePower { get; protected set; }

        /// <inheritdoc />
        public int AttackPower { get; protected set; }

        /// <inheritdoc />
        public int EmergencyPower { get; protected set; }

        /// <inheritdoc />
        public int MaxTurnsAtAttackPower { get; protected set; }

        /// <inheritdoc />
        public ReactorState CurrentState { get; set; }

        /// <inheritdoc />
        public bool UsingEmergencyPower { get; set; }

        /// <inheritdoc />
        public ReactorState PreviousState { get; set; }

        /// <inheritdoc />
        public bool UsedEmergencyPowerLastTurn { get; set; }

        /// <inheritdoc />
        public int CurrentTurnPenalty { get; set; }

        /// <inheritdoc />
        public int NextTurnPenalty { get; set; }

        /// <inheritdoc />
        public int CurrentMaxPower { get; protected set; }

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
        public int PowerAllocated { get; set; }

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
