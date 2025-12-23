using Godot;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWarsHex.Godot
{
    public partial class EnergyWeapons : GridContainer
    {
        #region Child Nodes

#pragma warning disable CS8618 // These will be assigned to in the editor or in _Ready(), if not then something went wrong and any resulting exceptions should be thrown.
        private Label _energyWeaponPower;
        private Button _energyWeaponFire;
        private OptionButton _energyWeaponSelect;
        private HSlider _energyWeaponSlider;

        #endregion

        private PendingEnergyWeaponOrder? _currentOrder;

        private readonly Dictionary<IShip, PendingEnergyWeaponOrder> _orders = [];

        private bool isTargeting = false;

        private Battle2D _battle;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.


        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _energyWeaponSelect = GetNode<OptionButton>("%EnergyWeaponSelect");
            _energyWeaponSlider = GetNode<HSlider>("%EnergyWeaponSlider");
            _energyWeaponPower = GetNode<Label>("%EnergyWeaponPower");
            _energyWeaponFire = GetNode<Button>("%EnergyWeaponFire");
            _energyWeaponFire.SetPressedNoSignal(false);
            _battle = (Battle2D)FindParent("Battle2D");
        }

        public void SetShip(IShip ship)
        {
            if (ship is null)
            {
                _currentOrder = null;
            }
            else if (_currentOrder is null || _currentOrder?.Ship.Id != ship.Id)
            {
                _currentOrder = _orders.GetOrAdd(ship, () => new PendingEnergyWeaponOrder(ship, ship.CurrentEnergyWeaponOrder!));
            }

            UpdateEnergyWeapons();
        }

        public void Clear()
        {
            _currentOrder = null;
            _orders.Clear();
        }

        private void UpdateEnergyWeapons()
        {
            var ship = _currentOrder?.Ship;
            if (ship is null)
            {
                return;
            }

            var weapons = ship.EnergyWeapons.ToArray();
            _energyWeaponSelect.Clear();
            //_energyWeaponSelect.AddItem($"Clear Order", -1);
            for (int i = 0; i < weapons.Length; i++)
            {
                _energyWeaponSelect.AddItem($"{weapons[i].Name} ({weapons[i].MaxEnergy()})", i);
            }
            _energyWeaponSelect.AddItem("None");

            if (_currentOrder is null || _currentOrder.WeaponIndex < 0)
            {
                _energyWeaponPower.Text = "";
                _energyWeaponSelect.Selected = -1;
                _energyWeaponSlider.Value = 0;
                _energyWeaponSlider.MaxValue = 0;
                _energyWeaponSlider.Editable = false;
                _energyWeaponFire.Disabled = true;
            }
            else
            {
                var weapon = ship.EnergyWeapons[_currentOrder.WeaponIndex];
                _energyWeaponPower.Text = $"{_currentOrder.Power} / {weapon.MaxEnergy()}";
                _energyWeaponSelect.Selected = _currentOrder.WeaponIndex;
                _energyWeaponSlider.MaxValue = weapon.CurrentMaxDice * weapon.EnergyPerDie;
                _energyWeaponSlider.Step = weapon.EnergyPerDie;
                _energyWeaponSlider.Editable = true;
                _energyWeaponSlider.Value = Convert.ToDouble(_currentOrder.Power);
                _energyWeaponFire.Disabled = false;
                _energyWeaponFire.ButtonPressed = false;
            }
        }

        public void OnClearEnergyWeapons()
        {
            if (_currentOrder is null)
            {
                return;
            }

            _currentOrder.ChangeWeapon(-1);
            _currentOrder.GiveOrder();

            UpdateEnergyWeapons();
        }

        public void OnEnergyWeaponSelectItemSelected(int index)
        {
            if (_currentOrder is null) { throw new InvalidOperationException($"{nameof(OnEnergyWeaponSelectItemSelected)} should not be called when _currentOrder is null.") ; }
            // TODO: Support other fire modes.
            var oldOrder = _currentOrder;
            var oldIndex = oldOrder?.WeaponIndex ?? -1;
            if (index >= _energyWeaponSelect.ItemCount)
            {
                index = -1;
            }

            if (Equals(oldIndex, index))
            {
                return;
            }

            if (index >= _currentOrder.Ship.EnergyWeapons.Count)
            {
                GD.PushWarning("Tried to select an Energy Weapon with invalid index.");
            }

            _currentOrder.ChangeWeapon(index);

            var result = _currentOrder.GiveOrder();
            GD.Print(result);

            UpdateEnergyWeapons();
        }

        public void OnEnergyWeaponSliderValueChanged(float value)
        {
            int power = (int)value;
            if (_currentOrder is null || _currentOrder.Power == power)
            {
                return;
            }

            _currentOrder.Power = power;

            var result = _currentOrder.GiveOrder();
            GD.Print(result);

            UpdateEnergyWeapons();
        }

        public void OnFireToggled(bool toggled_on)
        {
            if (toggled_on && _currentOrder?.Ship != null)
            {
                _energyWeaponFire.Text = "Cancel Target";
                _battle.SelectTarget(_currentOrder.Ship, SetDirectFireTarget);
            }
            else
            {
                _energyWeaponFire.Text = "Set Target";
                _battle.CancelContext();
            }
        }

        private void SetDirectFireTarget(ITargetable target)
        {
            _currentOrder!.TargetId = target.Id;
            _currentOrder.GiveOrder();
            _energyWeaponFire.ButtonPressed = false;
        }
    }
}
