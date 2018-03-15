using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onScreenKeyboard
{
    class Keyboard
    {

        // Create the the layout of the keyboard
        public static string[] KeyLayOut { get; } = new string[]
            {
                "ABCDEF",
                "GHIJKL",
                "MNOPQR",
                "STUVWX",
                "YZ1234",
                "567890"
            };

        public static int coulmnCount = KeyLayOut[0].Length;
        public static int rowCount = KeyLayOut.Length;

        public string Text { get; set;}

        // Start at "A" on new search
        private Position InitialPostion = new Position(0, 0);
        private Position cursor;

        // reset the keyboard
        public Keyboard()
        {
            Reset();
        }

        public void Reset()
        {
            cursor = InitialPostion;
            Text = "";
        }

        // Handle any wrap-around cases
        public void moveCursor(Position movement)
        {
            // Origin is upper left corner "A" all Y values are <= 0 and all X values >= 0 
            // Y wrap around check subtats the rowcount to ensure value is i range
            cursor = new Position(
                (cursor.X + movement.X + coulmnCount) % coulmnCount,
                (cursor.Y + movement.Y -rowCount ) % rowCount
                );
        }

        public void addKeyAtCursor()
        {
            Text += keyFromPosition(cursor);
        }

        private char keyFromPosition( Position position)
        {
            // since the orgin point is "A" al Y values are <= 0
            // Invert Y  before accessing correct row
            return KeyLayOut[-position.Y][position.X];
        }

        public void addSpace()
        {
            Text += " ";
        }

    }
}
