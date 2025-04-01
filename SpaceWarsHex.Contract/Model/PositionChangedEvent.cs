using SpaceWars.Interfaces;
using System;

namespace SpaceWars.Model
{
    public class PositionChangedEventArgs: EventArgs
    {
        public HexVector2 OldPosition { get; }
        public HexVector2 NewPosition { get; }

        public PositionChangedEventArgs(HexVector2 oldPosition, HexVector2 newPosition)
        {
            OldPosition = oldPosition;
            NewPosition = newPosition;
        }
    }

    public delegate void PositionChangedEventHandler(IHexObject sender, PositionChangedEventArgs e);
}
