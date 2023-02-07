using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboard
{
    class Button
    {
        public int x { get; set; }
        public int y { get; set; }
        public char letter { get; set; }

        public Button(int _x,int _y, char val)
        {
            x = _x;
            y = _y;
            letter = val;
        }
    }
}
