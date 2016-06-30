using System;
using System.Collections.Generic;
using System.Linq;

namespace OnScreenKeyboard.Enums
{
    public class KeyboardPosition
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public KeyboardPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static KeyboardPosition FromIndex(int index, int charactersPerLine)
        {
            var row = index / charactersPerLine;
            var col = index % charactersPerLine;

            return new KeyboardPosition(col,row);
        }

        public static KeyboardPositionVector operator -(KeyboardPosition end, KeyboardPosition start)
        {
            return KeyboardPositionVector.From(end.X - start.X, end.Y - start.Y);
        }
    }

    public class KeyboardPositionVector
    {
        public int X { get; private set; }
        public int Y{ get; private set; }

        private KeyboardPositionVector(){}

        public static KeyboardPositionVector From(int x, int y)
        {
            return new KeyboardPositionVector { X = x, Y = y };
        }

        public List<Navigation> GetPath()
        {
            var list = new List<Navigation>();

            var columnManipulation = (X < 0) ? Navigation.Left : Navigation.Right;
            var rowManipulation = (Y < 0) ? Navigation.Up : Navigation.Down;

            list.AddRange(Enumerable.Repeat(rowManipulation, Math.Abs(Y)).ToList());
            list.AddRange(Enumerable.Repeat(columnManipulation, Math.Abs(X)).ToList());

            list.Add(Navigation.Select);

            return list;
        }
    }
}