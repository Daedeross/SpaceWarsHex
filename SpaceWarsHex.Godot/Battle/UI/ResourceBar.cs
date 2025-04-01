using Godot;

public partial class ResourceBar : ReferenceRect
{
    private ProgressBar _bar;
    private Label _label;

    [Export]
    public double Value
    {
        get => _bar?.Value ?? 0d;
        set
        {
            if (_bar != null)
            {
                _bar.Value = value;
                SetText();
            }
        }
    }

    [Export]
    public double MinValue
    {
        get => _bar?.MinValue ?? 0d;
        set
        {
            if (_bar != null)
            {
                _bar.MinValue = value;
                SetText();
            }
        }
    }

    [Export]
    public double MaxValue
    {
        get => _bar?.MaxValue ?? 0d;
        set
        {
            if (_bar != null)
            {
                _bar.MaxValue = value;
                SetText();
            }
        }
    }

    [Export(PropertyHint.Enum, "BeginToEnd,EndToBegin,TopToBottom,BottomToTop")]
    public int FillMode { get => _bar.FillMode; set => _bar.FillMode = value; }

    private string _separator = " / ";
    [Export]
    public string Separator
    {
        get { return _separator; }
        set { _separator = value; SetText(); }
    }

    // Called when the node enters the scene tree for the first time.
    private double _value;
    public override void _Ready()
    {
        _bar = GetNode<ProgressBar>("Bar");
        _label = GetNode<Label>("Text");
    }

    private void SetText()
    {
        _label.Text = $"{_bar?.Value}{_separator}{_bar?.MaxValue}";
    }
}
