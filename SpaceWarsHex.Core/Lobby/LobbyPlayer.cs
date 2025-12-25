using SpaceWarsHex.Entities;
using SpaceWarsHex.Interfaces.Lobby;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWarsHex.Lobby
{
    public class LobbyPlayer : NotificationObject, ILobbyPlayer
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        private bool m_Ready;
        public bool Ready
        {
            get => m_Ready;
            set
            {
                if (RaiseAndSetIfChanged(ref m_Ready, value))
                {
                    RaisePropertyChanged(nameof(Valid));
                }
            }
        }

        public bool Valid => Ready && ShipPrototypes.Count > 0;

        /// <inheritdoc />
        public IList<Guid> ShipPrototypes { get; }

        public LobbyPlayer(Guid id, string name)
        {
            Id = id;
            Name = name;
            var obs = new ObservableCollection<Guid>();
            obs.CollectionChanged += this.OnCollectionChanged;
            ShipPrototypes = obs;
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (sender == ShipPrototypes)
            {
                RaisePropertyChanged(nameof(Valid));
            }
        }
    }
}
