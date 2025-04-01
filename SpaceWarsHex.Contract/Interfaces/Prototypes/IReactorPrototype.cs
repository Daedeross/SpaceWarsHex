namespace SpaceWars.Interfaces.Prototypes
{
    /// <summary>
    /// Interface for a ship prototype's reactor. The system that provides power.
    /// </summary>
    public interface IReactorPrototype : ISystemPrototype
    {
        /// <summary>
        /// The base cruise power of the reactor
        /// </summary>
        public int CruisePower { get; }

        /// <summary>
        /// The base attack power available
        /// </summary>
        public int AttackPower { get; }

        /// <summary>
        /// The emergency power the reactor can use.
        /// Gives extra power for one turn for an equivalent cost the following turn.
        /// </summary>
        public int EmergencyPower { get; }

        /// <summary>
        /// How many total turns per encounter the reactor can be at <see cref="AttackPower"/>.
        /// </summary>
        public int MaxTurnsAtAttackPower { get; }
    }
}
