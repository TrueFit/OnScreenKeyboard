using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onScreenKeyboard
{
    class Position
    {
        public int X { get; set; }
        public int Y { get; set;}

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }


        public static Position operator +(Position a, Position b)
        {
             // get the new  position
            return new Position(a.X + b.X, a.Y + b.Y);
        }

        public static Position operator -(Position a, Position b)
        {
            // get the new  position
            return new Position(a.X - b.X, a.Y - b.Y);
        }
    }
}
