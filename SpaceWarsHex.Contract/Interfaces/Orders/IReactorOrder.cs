using SpaceWarsHex.Model;

namespace SpaceWarsHex.Interfaces.Orders
{
    /// <summary>
    /// Order for a ship's reactor.
    /// </summary>
    public interface IReactorOrder : IOrder
    {
        /// <summary>
        /// The desired state for this turn.
        /// </summary>
        public ReactorState DesiredState { get; }

        /// <summary>
        /// True if desired to use EmergencyPower this turn.
        /// </summary>
        public bool UseEmergencyPower { get; }
    }
}
