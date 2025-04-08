using Godot;
using SpaceWarsHex.Bridges;
using SpaceWarsHex.Model;

namespace SpaceWarsHex
{
    public partial class TargetLine : Line2D
    {
        private HexVector2 _source;
        public HexVector2 Source
        {
            get { return _source; }
            set { _source = value; Redraw(); }
        }

        private HexVector2 _destination;
        public HexVector2 Destination
        {
            get { return _destination; }
            set { _destination = value; Redraw(); }
        }

        public void SetPoints(HexVector2 source, HexVector2 destination)
        {
            _source = source;
            _destination = destination;
            Redraw();
        }

        public void Redraw()
        {
            var p1 = _source.ToVector2();
            var p2 = _destination.ToVector2();

            ClearPoints();
            AddPoint(p1);
            AddPoint(p2);
        }
    }
}
