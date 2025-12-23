using Godot;
using SpaceWarsHex.Bridges;
using SpaceWarsHex.Entities;
using SpaceWarsHex.Godot;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Model;
using SpaceWarsHex.Rules;
using SpaceWarsHex.States.Orders;
using System;
using System.Linq;

namespace SpaceWarsHex
{
    public partial class Battle2D : Node2D
    {
#pragma warning disable CS8618 // These will be assigned to in the editor or in _Ready(), if not then something went wrong and any resulting exceptions should be thrown.
        private Director _director;
        private ShipControls _shipControls;
        private GodotEntityFactory _godotEntityFactory;

        private IShip _ship1;
        private IShip _ship2;

        private InputContext _inputContext = InputContext.Normal;

        private ISelectable? _selected;

        private ChooseEntityList _selectList;
        private TargetLine _targetLine;
        private SelectionReticle _selectReticle;
        private Action<ITargetable> _onTarget;
        private Func<ITargetable, bool> _targetFilter;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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
            _targetLine = GetNode<TargetLine>("TargetLine");
            _selectReticle = GetNode<SelectionReticle>("%SelectionReticle");
        }

        #region Test Stuff

#pragma warning disable IDE0060 // Remove unused parameter
        private void TestProcess(double delta)
#pragma warning restore IDE0060 // Remove unused parameter
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
                _ship1.GiveOrder(new EnergyWeaponOrder { WeaponIndex = 0, Power = 9, TargetId = _ship2.Id });
            }

            if (Input.IsActionJustReleased("test_heal"))
            {
                _ship1.Hull.CurrentIntegrity += 1;
            }
        }

        private void CreateTestStuff()
        {
            _director.CreateEntity(SpaceWarsHex.Mock.Prototypes.Ship1(), null, new HexVector2(0, 0));
            _director.CreateEntity(SpaceWarsHex.Mock.Prototypes.Ship2(), null, new HexVector2(3, -2));

            var ships = _director.GetEntities<IShip>();
            _ship1 = ships[0];
            _ship2 = ships[1];

            _ship1.Velocity = new HexVector2(0, 2);
            _ship1.TeamNumber = 0;
            _ship2.Velocity = new HexVector2(1, 2);
            _ship2.TeamNumber = 1;

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
                    SelectTarget(pos);
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
            var hex = GetHex(pos);
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

        private void ChangeSelection(ISelectable? selectable)
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

            if (_selected is null)
            {
                _selectReticle.Visible = false;
            }
            else
            {
                _selectReticle.HexPosition = _selected.Position;
                _selectReticle.Visible = true;
            }

                TargetLine(selectable as IShip);
        }

        private void SelectTarget(Vector2 pos)
        {
            var hex = GetHex(pos);
            var targetables = _director.GetEntitiesInHex<ITargetable>(hex)
                .Where(_targetFilter)
                .ToList();

            if (targetables.Count > 0)
            {
                if (targetables.Count == 1)
                {
                    TargetDirectFire(targetables[0]);
                }
                else
                {
                    _selectList.QueueSelect(pos, targetables, TargetDirectFire);
                }
            }
            else
            {
                //CancelContext();
            }
        }

        private void TargetDirectFire(ITargetable targetable)
        {
            _onTarget(targetable);
            TargetLine(_selected as IShip);
            CancelContext();
        }

        #region InputContext Change

        public void SelectTarget(IHexObject source, Action<ITargetable> onTarget, Func<ITargetable, bool>? filter = null)
        {
            _targetFilter = filter is null
                ? obj => obj.TeamNumber != source.TeamNumber
                : filter;

            _onTarget = onTarget;
            _inputContext = InputContext.DirectFire;
        }

        public void CancelContext()
        {
            _onTarget = x => { };
            _inputContext = InputContext.Normal;
        }

        #endregion

        #endregion // Input Handling

        public void OnEndTurnPressed()
        {
            ChangeSelection(null);
            //_director.PlayerEndPhase(null);
        }

        #region Helpers

        private HexVector2 GetHex(Vector2 viewPos, Viewport? viewport = null)
        {
            viewport = viewport ?? GetViewport();
            var worldPos = viewport.CanvasTransform.AffineInverse() * viewPos;
            return worldPos.GetHex();
        }

        private void TargetLine(IShip? ship)
        {
            var order = ship?.CurrentEnergyWeaponOrder;
            if (ship is not null && order?.TargetId != null)
            {
                if (_director.TryGetEntity(order.TargetId.Value, out var target))
                {
                    _targetLine.SetPoints(ship.Position, target!.Position);
                    _targetLine.Visible = true;
                    return;
                }
            }
            _targetLine.Visible = false;
        }

        #endregion
    }
}
