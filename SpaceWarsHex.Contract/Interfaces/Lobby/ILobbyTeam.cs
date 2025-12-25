using System.Collections.Generic;
using System.ComponentModel;

namespace SpaceWarsHex.Interfaces.Lobby
{
    public interface ILobbyTeam : INotifyPropertyChanged
    {
        /// <summary>
        /// Index of the team in the battle.
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// Name of the team/faction
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// List of players
        /// </summary>
        IList<ILobbyPlayer> Players { get; }
    }
}
