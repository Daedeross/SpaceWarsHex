using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SpaceWarsHex.Interfaces.Lobby
{
    public interface ILobbyPlayer: IHaveId, INotifyPropertyChanged
    {
        /// <summary>
        /// Display name for the player.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// True if user is ready to go.
        /// </summary>
        bool Ready { get; set; }

        /// <summary>
        /// True if user is ready AND has at least one ship assigned.
        /// </summary>
        bool Valid { get; }

        /// <summary>
        /// List of IDs for prototypes the player has selected.
        /// </summary>
        IList<Guid> ShipPrototypes { get; }
    }
}
