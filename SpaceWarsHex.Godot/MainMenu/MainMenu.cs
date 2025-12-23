using Godot;

namespace SpaceWarsHex
{
    public partial class MainMenu : Control
    {
#pragma warning disable CS8618 // These will be assigned to in _Ready(), if not then something went wrong and any resulting exceptions should be thrown.
        private Networking _lobby;
        private Control _mainPanel;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _lobby = GetNode<Networking>("Networking");
            _mainPanel = GetNode<Control>("MainPanel");
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }

        public void OnStartBtnPressed()
        {
            _lobby.Visible = true;
            _mainPanel.Visible = false;
        }
    }

}