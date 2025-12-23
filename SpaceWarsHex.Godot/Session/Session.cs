using Godot;
using System;

namespace SpaceWarsHex
{
    /// <summary>
    /// Class that manages a single multiplayer session.
    /// </summary>
    public partial class Session : Node
    {
        public const int ServerPort = 6500;
        public const int MaxPlayers = 64;

#pragma warning disable CS8618 // These will be assigned to in _Ready(), if not then something went wrong and any resulting exceptions should be thrown.
        [Export]
        public PackedScene LobbyScene { get; set; }

        [Export]
        public PackedScene BattleScene { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [Signal]
        public delegate void PlayerListChangedEventHandler();
        [Signal]
        public delegate void ConnectionFailedEventHandler();
        [Signal]
        public delegate void ConnectionSucceededEventHandler();
        [Signal]
        public delegate void SessionEndedEventHandler();
        [Signal]
        public delegate void SessionErrorEventHandler(string errorMessage);

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Multiplayer.PeerConnected += PlayerConnected;
            Multiplayer.PeerDisconnected += PlayerDisconnected;
            Multiplayer.ConnectedToServer += () => EmitSignal(SignalName.ConnectionSucceeded);
            Multiplayer.ConnectionFailed += () => EmitSignal(SignalName.ConnectionFailed);
            Multiplayer.ServerDisconnected += ServerDisconected;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }

        public void StartServer()
        {
            // TODO: following is prototype
            var peer = new ENetMultiplayerPeer();
            peer.CreateServer(ServerPort, MaxPlayers);
            GetTree().GetMultiplayer().MultiplayerPeer = peer;
        }

        public void ConnectToServer(string address, int port)
        {
            var peer = new ENetMultiplayerPeer();
            peer.CreateClient(address, port);
            GetTree().GetMultiplayer().MultiplayerPeer = peer;
        }

        //public void LoadCampaign(Campaign campaign)
        //{
        //    Campaign = campaign;
        //    AddChild(campaign);
        //    campaign.Owner = this;
        //}

        #region Signal Handling

        private void PlayerConnected(long id)
        {
            GD.Print($"Player Connected id:{id}");
        }

        private void PlayerDisconnected(long id)
        {
            GD.Print($"Player Disconnected id:{id}");
        }

        private void ServerDisconected()
        {
            GD.Print("Disconected from server.");
        }

        #endregion
    }
}
