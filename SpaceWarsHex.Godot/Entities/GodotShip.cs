using Godot;
using SpaceWarsHex.Entities;
using SpaceWarsHex.Interfaces;
using System;
using System.ComponentModel;

namespace SpaceWarsHex
{
    public partial class GodotShip : GodotMovingHexObject
    {
        protected const int ShieldSavePower = 4;
        protected const string Accepted = "Accepted";
        protected const string OldVelocityName = "VelArrow";
        protected const string AccelerationName = "AccArrow";
        protected const string NewVelocityName = "VelArrowNext";
        protected const string ValidStateMessage = "";
        protected const string TooMuchPower = "Too much power allocated.";

        protected static Color OldVelocityColor = new(0f, 1f, 0f, 0.5f);
        protected static Color AccelerationColor = new(1f, 1f, 0f, 0.5f);
        protected static Color NewVelocityColor = new(0, 1f, 1f, 0.5f);


        public new IShip Entity
        {
            get => (IShip)base.Entity;
            set => base.Entity = value;
        }

        #region Child References


        #endregion // Child References

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {

        }

        #region TODO
        private void HideArrows()
        {
            //_accArrow.gameObject.SetActive(false);
            //_oldVelArrow.gameObject.SetActive(false);
            //_newVelArrow.gameObject.SetActive(false);
        }
        private void ShowArrows()
        {
            //_accArrow.gameObject.SetActive(true);
            //_oldVelArrow.gameObject.SetActive(true);
            //_newVelArrow.gameObject.SetActive(true);
        }

        private void SetArrows()
        {
            //var newVel = Velocity + _drive.Acceleration;
            //_accArrow.Set(Velocity, newVel);
            //_oldVelArrow.TargetHex = Velocity;
            //_newVelArrow.TargetHex = newVel;
        }
        #endregion
    }
}
