using Godot;
using SpaceWarsHex;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Model;
using SpaceWarsHex.Orders;
using SpaceWarsHex.Godot;
using System.ComponentModel;
using System.Linq;

namespace SpaceWarsHex
{
    public partial class ShipControls : Control
    {
        #region Controls

        private Label _shipNameText;
        private ResourceBar _hullBar;
        private ResourceBar _powerBar;
        private ResourceBar _shieldsBar;
        private CheckButton _reactorToggle;
        private CheckButton _emergencyPowerToggle;
        private Label _emergencyPowerValue;
        private EnergyWeapons _energyWeapons;
        private Label _cruiseValue;
        private Label _attackValue;

        #endregion

        private IShip _ship;
        public IShip Ship
        {
            get { return _ship; }
            set
            {
                if (_ship != value)
                {
                    if (_ship != null)
                    {
                        _ship.PropertyChanged -= OnShipPropertyChanged;
                    }
                    _ship = value;
                    if (_ship != null)
                    {
                        _ship.PropertyChanged += OnShipPropertyChanged;
                        UpdateUI();
                        Visible = true;
                    }
                    else
                    {
                        Visible = false;
                    }
                }
            }
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            SetProcessInput(false);
            _shipNameText = GetNode<Label>("%ShipNameText");
            _hullBar = GetNode<ResourceBar>("%HullBar");
            _powerBar = GetNode<ResourceBar>("%PowerBar");
            _shieldsBar = GetNode<ResourceBar>("%ShieldsBar");
            _cruiseValue = GetNode<Label>("%CruiseValue");
            _attackValue = GetNode<Label>("%AttackValue");
            _reactorToggle = GetNode<CheckButton>("%ReactorToggle");
            _emergencyPowerToggle = GetNode<CheckButton>("%EmergencyPowerToggle");
            _emergencyPowerValue = GetNode<Label>("%EmergencyPowerValue");
            _energyWeapons = GetNode<EnergyWeapons>("%EnergyWeapons");
        }

        private void UpdateUI()
        {
            if (_ship != null)
            {
                _shipNameText.Text = _ship.Name;

                _hullBar.MaxValue = _ship.Hull.MaxIntegrity;
                _hullBar.Value = _ship.Hull.CurrentIntegrity;

                _powerBar.MaxValue = _ship.Reactor.CurrentAvailablePower;
                _powerBar.Value = _ship.Reactor.PowerAllocated;

                bool hasAttackPower = _ship.Reactor.CurrentMaxPower > _ship.Reactor.CruisePower;
                bool atAttack = _ship.Reactor.CurrentState == ReactorState.Attack;
                _reactorToggle.ButtonPressed = atAttack;
                _reactorToggle.Disabled = !hasAttackPower;
                _cruiseValue.LabelSettings.OutlineSize = atAttack ? 0 : 2;
                _attackValue.LabelSettings.OutlineSize = atAttack ? 2 : 0;

                _emergencyPowerToggle.ButtonPressed = _ship.Reactor.UsingEmergencyPower;
                _emergencyPowerToggle.Disabled = _ship.Reactor.EmergencyPower == 0 || _ship.Reactor.UsedEmergencyPowerLastTurn;
                _cruiseValue.Text = $"{_ship.Reactor.CruisePower}";
                _attackValue.Text = hasAttackPower ? $"{_ship.Reactor.AttackPower}" : string.Empty;
                _emergencyPowerValue.Text = _ship.Reactor.EmergencyPower > 0 ? $"+{_ship.Reactor.EmergencyPower}" : string.Empty;

                _energyWeapons.SetShip(_ship);
            }
        }

        #region Control Callbacks

        public void OnReactorToggleToggled(bool toggled)
        {
            Ship.Reactor.CurrentState = toggled ? ReactorState.Attack : ReactorState.Cruise;
            UpdateUI();
        }

        public void OnEmergencyPowerToggled(bool toggled)
        {
            Ship.Reactor.UsingEmergencyPower = toggled;
        }

        #endregion // Control Callbacks

        private void OnShipPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateUI();
        }

        public override void _ExitTree()
        {
            if (_ship != null)
            {
                _ship.PropertyChanged -= OnShipPropertyChanged;
            }
            base._ExitTree();
        }
    }

}
