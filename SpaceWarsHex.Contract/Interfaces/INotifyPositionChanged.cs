using SpaceWarsHex.Model;

namespace SpaceWarsHex.Interfaces
{
    /// <summary>
    /// Object with can change position on the board and can notify subscribers of such.
    /// <seealso cref="PositionChangedEventHandler"/>
    /// <seealso cref="PositionChangedEventArgs"/>
    /// </summary>
    public interface INotifyPositionChanged
    {
        /// <summary>
        /// The PositionChanged event.
        /// </summary>
        event PositionChangedEventHandler PositionChanged;
    }
}
