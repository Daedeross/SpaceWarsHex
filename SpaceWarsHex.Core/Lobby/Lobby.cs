using SpaceWarsHex.Entities;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Lobby;
using SpaceWarsHex.Interfaces.Prototypes;

namespace SpaceWarsHex.Lobby
{
    public class Lobby : NotificationObject, ILobby
    {
        private readonly IPrototypeSerializer _serializer;
        private readonly IPrototypeCache _cache;

        public Lobby(IPrototypeSerializer serializer, IPrototypeCache cache)
        {
            _serializer = serializer;
            _cache = cache;
        }

        public bool CanStart => UnassignedPlayers.Count == 0 
            && Teams.All(t => t.Players.All(p => p.ShipPrototypes.Count > 0));

        public IList<ILobbyPlayer> UnassignedPlayers { get; } = [];

        public IList<ILobbyTeam> Teams { get; } = [];

        public IList<IShipPrototype> AvailableShips { get; } = [];

        public void MovePlayer(ILobbyTeam? team)
        {
            throw new NotImplementedException();
        }

        public void StartBattle()
        {
            throw new NotImplementedException();
        }
    }
}
