using Godot;

namespace SpaceWarsHex
{
    public partial class CameraControls : Camera2D
    {
        private Vector2 last_cursor;

        [Export]
        public float PanSpeed { get; set; } = 0.01f;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            var cursor = GetViewport().GetMousePosition();

            if (Input.IsActionPressed(InputActions.PanMap))
            {
                var vel = cursor - last_cursor;
                Position -= vel;
            }

            last_cursor = cursor;
        }
    }

}
