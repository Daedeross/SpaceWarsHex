namespace SpaceWarsHex.Interfaces.Systems
{
    /// <summary>
    /// Interface for the the Shields system
    /// </summary>
    public interface IShields : ISystem
    {
        /// <summary>
        /// The max power that can be allocated to the shield.
        /// </summary>
        int MaxPower { get; }

        /// <summary>
        /// The max power that can be allocated to the shield after damage is taken into account.
        /// </summary>
        int MaxPowerAvailable { get; }

        /// <summary>
        /// How much power is allocated this turn.
        /// </summary>
        int AllocatedPower { get; set; }

        /// <summary>
        /// The shield's current strength.
        /// </summary>
        int CurrentPower { get; set; }
    }
}
