using Godot;

public partial class TextToggle : CheckButton
{
    private string _upText = string.Empty;
    [Export]
    public string UpText
    {
        get => _upText;
        set { _upText = value; UpdateText(ButtonPressed); }
    }

    private string _downText = string.Empty;
    [Export]
    public string DownText
    {
        get { return _downText; }
        set { _downText = value; UpdateText(ButtonPressed); }
    }

    public override void _Ready()
    {
        this.Toggled += UpdateText;
    }
    public void UpdateText(bool toggled)
    {
        Text = toggled ? _downText : _upText;
    }
}
