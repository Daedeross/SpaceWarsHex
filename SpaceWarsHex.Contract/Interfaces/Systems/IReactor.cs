using SpaceWars.Interfaces.Orders;
using SpaceWars.Model;

namespace SpaceWars.Interfaces.Systems
{
    /// <summary>
    /// Interface for a ship's reactor. The system that provides power.
    /// </summary>
    public interface IReactor : ISystem
    {
        /// <summary>
        /// The base cruise power of the reactor
        /// </summary>
        int CruisePower { get; }

        /// <summary>
        /// The base attack power available
        /// </summary>
        int AttackPower { get; }

        /// <summary>
        /// The emergency power the reactor can use.
        /// Gives extra power for one turn for an equivalent cost the following turn.
        /// </summary>
        int EmergencyPower { get; }

        /// <summary>
        /// How many total turns per encounter the reactor can be at <see cref="AttackPower"/>.
        /// </summary>
        int MaxTurnsAtAttackPower { get; }

        /// <summary>
        /// The current state of the reactor.
        /// </summary>
        ReactorState CurrentState { get; set; }

        /// <summary>
        /// True if the reactor is using <see cref="EmergencyPower"/> this turn.
        /// </summary>
        bool UsingEmergencyPower { get; set; }

        /// <summary>
        /// What the state of the reactor was last turn.
        /// </summary>
        ReactorState PreviousState { get; set; }

        /// <summary>
        /// True if the reactor is using <see cref="EmergencyPower"/> last turn.
        /// </summary>
        bool UsedEmergencyPowerLastTurn { get; set; }

        /// <summary>
        /// The energy penalty this turn.
        /// </summary>
        int CurrentTurnPenalty { get; set; }

        /// <summary>
        /// The cumualative energy penalty for next turn.
        /// </summary>
        int NextTurnPenalty { get; set; }

        /// <summary>
        /// The current max power the reactor can output, reduced by damage.
        /// </summary>
        int CurrentMaxPower { get; }

        /// <summary>
        /// The current power available this turn.
        /// Calculated based on Power values, state, and penalties
        /// </summary>
        int CurrentAvailablePower { get; }

        /// <summary>
        /// How much power is allocated this turn/
        /// </summary>
        int PowerAllocated { get; set; }

        /// <summary>
        /// How many turns have been spen at <see cref="AttackPower"/> this encounter.
        /// </summary>
        int TurnsSpentAtAttackPower { get; set; }
    }
}
