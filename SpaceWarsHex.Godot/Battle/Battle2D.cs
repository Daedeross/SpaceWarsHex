using Godot;
using SpaceWars;
using SpaceWars.Entities;
using SpaceWars.Interfaces;
using SpaceWars.Model;
using SpaceWars.Orders;
using SpaceWars.Rules;
using SpaceWarsHex.Bridges;
using System.Linq;

namespace SpaceWarsHex
{
    public partial class Battle2D : Node2D
    {
        private Director _director;
        private ShipControls _shipControls;
        private GodotEntityFactory _godotEntityFactory;

        private IShip _ship1;
        private IShip _ship2;

        private InputContext _inputContext = InputContext.Normal;

        private ISelectable _selected;

        private ChooseEntityList _selectList;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            WireComponents();
            CreateTestStuff();
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            TestProcess(delta);
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            // Mouse in viewport coordinates.
            switch (@event)
            {
                case InputEventMouseButton mouseButton:
                    OnClick(mouseButton); break;
                default:
                    break;
            }
        }

        private void WireComponents()
        {
            _godotEntityFactory = new GodotEntityFactory(this);
            _director = new Director(new EntityFactory<Entities.GodotHexObject>(_godotEntityFactory, new BoardRules()));
            _shipControls = GetNode<ShipControls>("UI/ShipControls");
            _selectList = GetNode<ChooseEntityList>("UI/ChooseEntityList");
        }

        #region Test Stuff

        private void TestProcess(double delta)
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
            }
            else
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
        #endregion // TestStuff

        #region Input Handling

        private void OnClick(InputEventMouseButton mouseButton)
        {
            var index = mouseButton.ButtonIndex;
            var localPos = mouseButton.GetPosition();


            if (mouseButton.IsReleased() && index == MouseButton.Left)
            {
                OnSelect(localPos);
            }
        }

        private void OnSelect(Vector2 pos)
        {
            switch (_inputContext)
            {
                case InputContext.None:
                    GD.PushWarning("Unsuported InputContext.");
                    break;
                case InputContext.Normal:
                    NormalSelect(pos);
                    break;
                case InputContext.Move:
                    break;
                case InputContext.DirectFire:
                    break;
                case InputContext.Beam:
                    break;
                case InputContext.Bomb:
                    break;
                case InputContext.Torpedo:
                    break;
                case InputContext.Smoke:
                    break;
                case InputContext.Detonate:
                    break;
                default:
                    GD.PushWarning("Unsuported InputContext.");
                    break;
            }
        }

        private void NormalSelect(Vector2 pos)
        {
            var worldPos = GetViewport().CanvasTransform.AffineInverse() * pos;
            var hex = worldPos.GetHex();
            var seletables = _director.GetEntitiesInHex<ISelectable>(hex).ToList();
            if (seletables.Count > 0)
            {
                if (seletables.Count == 1)
                {
                    ChangeSelection(seletables[0]);
                }
                else
                {
                    _selectList.QueueSelect(pos, seletables, ChangeSelection);
                }
            }
            else
            {
                ChangeSelection(null);
            }
        }

        private void ChangeSelection(ISelectable selectable)
        {
            _selected = selectable;
            switch (selectable)
            {
                case null:
                    if (_shipControls.Ship != null)
                    {
                        _shipControls.Ship.Selected = false;
                    }
                    _shipControls.Ship = null;
                    break;
                case IShip ship:
                    ship.Selected = true;
                    _shipControls.Ship = ship;
                    break;
                default:
                    break;
            }
        }

        private void TargetDirectFire(ITargetable targetable)
        {

        }

        #endregion // Input Handling
    }
}
