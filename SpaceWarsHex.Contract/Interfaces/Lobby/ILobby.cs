using SpaceWarsHex.Interfaces.Prototypes;
using System.Collections.Generic;
using System.ComponentModel;

namespace SpaceWarsHex.Interfaces.Lobby
{
    public interface ILobby: INotifyPropertyChanged
    {
        /// <summary>
        /// True if the battle can be started.
        /// </summary>
        /// <remarks>
        /// Should be true if all players have been assigned a team,
        /// all teams have at least one player,
        /// and each player has at least one ship.
        /// </remarks>
        bool CanStart { get; }

        /// <summary>
        /// List of players that have not been assigned to a team.
        /// </summary>
        public IList<ILobbyPlayer> UnassignedPlayers { get; }

        /// <summary>
        /// The current battle teams
        /// </summary>
        IList<ILobbyTeam> Teams { get; }

        /// <summary>
        /// The loaded ship prototypes.
        /// </summary>
        IList<IShipPrototype> AvailableShips {  get; }

        /// <summary>
        /// Unassign or move player to another team.
        /// </summary>
        /// <param name="team">The team to assign the player to. Unassignes player if null.</param>
        void MovePlayer(ILobbyTeam? team);

        /// <summary>
        /// Startes the battle. Transitions to next scene.
        /// </summary>
        void StartBattle();

    }
}
