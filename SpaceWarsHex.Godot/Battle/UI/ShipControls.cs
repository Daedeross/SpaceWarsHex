using Godot;
using SpaceWars.Interfaces;
using SpaceWars.Model;
using SpaceWars.Orders;
using System;
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
        private Label _cruiseValue;
        private Label _attackValue;
        private OptionButton _energyWeaponSelect;
        private HSlider _energyWeaponSlider;

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
                    }
                    UpdateUI();
                }
            }
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _shipNameText = GetNode<Label>("%ShipNameText");
            _hullBar = GetNode<ResourceBar>("%HullBar");
            _powerBar = GetNode<ResourceBar>("%PowerBar");
            _shieldsBar = GetNode<ResourceBar>("%ShieldsBar");
            _cruiseValue = GetNode<Label>("%CruiseValue");
            _attackValue = GetNode<Label>("%AttackValue");
            _reactorToggle = GetNode<CheckButton>("%ReactorToggle");
            _emergencyPowerToggle = GetNode<CheckButton>("%EmergencyPowerToggle");
            _emergencyPowerValue = GetNode<Label>("%EmergencyPowerValue");
            _energyWeaponSelect = GetNode<OptionButton>("%EnergyWeaponSelect");
            _energyWeaponSlider = GetNode<HSlider>("%EnergyWeaponSlider");
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
                _emergencyPowerToggle.Disabled = _ship.Reactor.EmergencyPower == 0;
                _cruiseValue.Text = $"{_ship.Reactor.CruisePower}";
                _attackValue.Text = hasAttackPower ? $"{_ship.Reactor.AttackPower}" : string.Empty;
                _emergencyPowerValue.Text = _ship.Reactor.EmergencyPower > 0 ? $"+{_ship.Reactor.EmergencyPower}" : string.Empty;

                UpdateEnergyWeapons();
            }
        }

        private void UpdateEnergyWeapons()
        {
            var weapons = _ship.EnergyWeapons.ToArray();
            _energyWeaponSelect.Clear();
            for (int i = 0; i < weapons.Length; i++)
            {
                _energyWeaponSelect.AddItem(weapons[i].Name, i);
            }

            var order = _ship.CurrentEnergyWeaponOrder;
            if (order is null)
            {
                _energyWeaponSelect.Selected = -1;
                _energyWeaponSlider.Value = 0;
                _energyWeaponSlider.MaxValue = 0;
                _energyWeaponSlider.Editable = false;
            }
            else
            {
                var weapon = _ship.EnergyWeapons[order.WeaponIndex];
                _energyWeaponSelect.Selected = order.WeaponIndex;
                _energyWeaponSlider.MaxValue = weapon.CurrentMaxDice * weapon.EnergyPerDie;
                _energyWeaponSlider.Step = weapon.EnergyPerDie;
                _energyWeaponSlider.Editable = true;
                _energyWeaponSlider.Value = order.Power;
            }
        }

        #region Control Callbacks

        public void OnReactorToggleToggled(bool toggled)
        {
            Ship.Reactor.CurrentState = toggled ? ReactorState.Attack : ReactorState.Cruise;
            UpdateUI();
        }

        public void OnEnergyWeaponSelectItemSelected(int index)
        {
            // TODO: Support other fire modes.
            var oldOrder = Ship.CurrentEnergyWeaponOrder as DirectFireEnergyWeaponOrder;
            var power = oldOrder?.Power ?? 0;
            var oldIndex = oldOrder?.WeaponIndex ?? -1;
            if (Equals(oldIndex, index))
            {
                return;
            }

            if (index >= Ship.EnergyWeapons.Count)
            {
                GD.PushWarning("Tried to select an Energy Weapon with invalid index.");
            }

            var weapon = Ship.EnergyWeapons[index];
            power = power / weapon.EnergyPerDie * weapon.EnergyPerDie;              // power must be a multiple of EnergyPerDie
            power = Mathf.Max(power, weapon.EnergyPerDie);                          // must be at least one die.
            power = Mathf.Min(power, weapon.EnergyPerDie * weapon.CurrentMaxDice);  // clamp power to max capable

            var newOrder = new DirectFireEnergyWeaponOrder
            {
                Power = power,
                TargetId = oldOrder?.TargetId ?? Ship.Id, // TODO: design targeting workflow
                WeaponIndex = index
            };

            var result = Ship.GiveOrder(newOrder);
            GD.Print(result);
        }

        public void OnEnergyWeaponSliderValueChanged(float value)
        {
            int power = (int)value;
            var oldOrder = Ship.CurrentEnergyWeaponOrder as DirectFireEnergyWeaponOrder;
            if (oldOrder is null || oldOrder.Power == power)
            {
                return;
            }

            var weapon = Ship.EnergyWeapons[oldOrder.WeaponIndex];
            power = power / weapon.EnergyPerDie * weapon.EnergyPerDie;              // power must be a multiple of EnergyPerDie
            power = Mathf.Max(power, weapon.EnergyPerDie);                          // must be at least one die.
            power = Mathf.Min(power, weapon.EnergyPerDie * weapon.CurrentMaxDice);  // clamp power to max capable


            var newOrder = new DirectFireEnergyWeaponOrder
            {
                Power = power,
                TargetId = oldOrder.TargetId,
                WeaponIndex = oldOrder.WeaponIndex
            };

            Ship.GiveOrder(newOrder);
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
