using Godot;
using SpaceWarHex.Bridges;
using SpaceWars;
using SpaceWars.Entities;
using SpaceWars.Interfaces;
using SpaceWars.Model;
using SpaceWars.Orders;
using SpaceWars.Rules;
using SpaceWarsHex.Bridges;

namespace SpaceWarsHex
{
    public partial class TestBattle : Node2D
    {
        private Director _director;
        private ShipControls _shipControls;
        private GodotEntityFactory _godotEntityFactory;

        private IShip _ship1;
        private IShip _ship2;

        private WrapperMap _wrapperMap = new WrapperMap();

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            WireComponents();
            CreateTestStuff();
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            HexVector2 hv = HexVector2.Zero;
            if (Input.IsActionJustReleased("ui_up"))
            {
                hv = new HexVector2(0, 1);
            }
            if (Input.IsActionJustReleased("ui_down"))
            {
                hv = new HexVector2(0, -1);
            }
            if (Input.IsActionJustReleased("ui_left"))
            {
                hv = new HexVector2(-1, 0);
            }
            if (Input.IsActionJustReleased("ui_right"))
            {
                hv = new HexVector2(1, 0);
            }

            if (Input.IsPhysicalKeyPressed(Key.Alt))
            {
                _ship1.Velocity += hv;
            } else
            {
                _ship1.Position += hv;
            }

            if (Input.IsActionJustReleased("test_damage"))
            {
                //_ship1.Ship.Hull.CurrentIntegrity -= 1;
                _ship1.GiveOrder(new DirectFireEnergyWeaponOrder { WeaponIndex = 0, Power = 9, TargetId = _ship2.Id });
            }

            if (Input.IsActionJustReleased("test_heal"))
            {
                _ship1.Hull.CurrentIntegrity += 1;
            }
        }

        private void WireComponents()
        {
            _godotEntityFactory = new GodotEntityFactory(this);
            _director = new Director(new EntityFactory<Entities.GodotHexObject>(_godotEntityFactory, new BoardRules()));
            _shipControls = GetNode<ShipControls>("UI/ShipControls");
        }

        private void CreateTestStuff()
        {
            _director.CreateEntity(SpaceWars.Mock.Prototypes.Ship1(), null, new HexVector2(0, 0));
            _director.CreateEntity(SpaceWars.Mock.Prototypes.Ship2(), null, new HexVector2(3, -2));

            var ships = _director.GetEntities<IShip>();
            _ship1 = ships[0];
            _ship2 = ships[1];

            _ship1.Velocity = new HexVector2(0, 2);
            _ship2.Velocity = new HexVector2(1, 2);

            _shipControls.Ship = _ship1;
        }
    }
}
