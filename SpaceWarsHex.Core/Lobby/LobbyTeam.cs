using SpaceWarsHex.Entities;
using SpaceWarsHex.Interfaces.Lobby;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace SpaceWarsHex.Lobby
{
    public class LobbyTeam : NotificationObject, ILobbyTeam
    {
        public int Index { get; set; }
        public string Name { get; set; }

        public IList<ILobbyPlayer> Players {  get; set; }

        public bool Valid => Players.Count > 0 && Players.All(p => p.Valid);

        public LobbyTeam(int index)
        {
            Index = index;
            Name = $"Team {index}";
            var obs = new ObservableCollection<ILobbyPlayer>();
            obs.CollectionChanged += OnCollectionChanged;
            Players = obs;
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (sender == Players)
            {
                bool changed = false;
                if (e.NewItems != null)
                {
                    changed = true;
                    foreach (ILobbyPlayer player in e.NewItems)
                    {
                        player.PropertyChanged += OnPlayerChanged;
                    }
                }
                if (e.OldItems != null)
                {
                    changed = true;
                    foreach (ILobbyPlayer player in e.OldItems)
                    {
                        player.PropertyChanged -= OnPlayerChanged;
                    }
                }
                if (changed)
                {
                    RaisePropertyChanged(nameof(Valid));
                }
            }
        }

        private void OnPlayerChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is ILobbyPlayer && e.PropertyName == nameof(ILobbyPlayer.Valid))
            {
                RaisePropertyChanged(nameof(Valid));
            }
        }
    }
}
