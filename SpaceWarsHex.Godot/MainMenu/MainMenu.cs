using Godot;
using System;

public partial class MainMenu : Control
{
    private CanvasItem _lobby;
    private CanvasItem _mainPanel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _lobby = GetNode<CanvasItem>("Lobby");
        _mainPanel = GetNode<CanvasItem>("MainPanel");
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
