using Godot;
using SpaceWarsHex.Bridges;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Lobby;
using SpaceWarsHex.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWarsHex
{
    public partial class GodotLobby : Control
    {
        public bool _isHost = false;

        public List<ShipPrototype> ShipPrototypes = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public ILobby _lobby;
        public ItemList _blueprtintsList;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public override void _Ready()
        {
            _lobby = DependencyFactory.Create<ILobby>();
            _blueprtintsList = GetNode<ItemList>("%BlueprintList");
            LoadPrototypes();
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        //public override void _Process(double delta)
        //{
        //}

        private void LoadPrototypes()
        {
            var ser = DependencyFactory.Create<IPrototypeSerializer>();
            var cache = DependencyFactory.Create<IPrototypeCache>();
            var dir = DirAccess.Open("res://Blueprints/");
            var files = dir.GetFiles().Where(f => f.EndsWith(".swp"));
            foreach (var path in files)
            {
                var absPath = ProjectSettings.GlobalizePath($"{dir.GetCurrentDir()}/{path}");
                
                GD.Print(absPath);
                //var fa = FileAccess.Open(dir. path, FileAccess.ModeFlags.Read);
                //var absPath = fa.GetPathAbsolute();
                //var localPath = ProjectSettings.GlobalizePath(path);
                var ship = ser.Deserialize<ShipPrototype>(absPath);
                cache.AddOrUpdate(ship);
            }

            ShipPrototypes = cache.GetAllOfType<ShipPrototype>()
                .OrderBy(p => p.Name)
                .ToList();

            foreach (var ship in ShipPrototypes)
            {
                _blueprtintsList.AddItem(ship.Name);
            }
        }
    } 
}
