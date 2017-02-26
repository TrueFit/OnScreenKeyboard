using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboard
{
    public class KeyboardButton
    {
        public int xValue { get; set; }
        public int yValue { get; set; }
        public char buttonValue { get; set; }

        public KeyboardButton(int x, int y, char val)
        {
            xValue = x;
            yValue = y;
            buttonValue = val;
        }
    }
}
