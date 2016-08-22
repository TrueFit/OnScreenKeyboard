using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OnScreenKeyboard
{
    public interface IKeyboardModel
    {
        bool TryFind(char c, out Point pt);
    }

    // Keyboards are locale-specific so keep it modular.
    public class KeyboardModel : IKeyboardModel
    {
        // Keyboards need not have the same number of keys per line so a
        // list of arrays is a better representation than a 2-dimensional array
        private List<char[]> _keyboard = new List<char[]>() 
        {
            new char[] { 'A', 'B', 'C', 'D', 'E', 'F'},
            new char[] { 'G', 'H', 'I', 'J', 'K', 'L'},
            new char[] { 'M', 'N', 'O', 'P', 'Q', 'R'},
            new char[] { 'S', 'T', 'U', 'V', 'W', 'X'},
            new char[] { 'Y', 'Z', '1', '2', '3', '4'},
            new char[] { '5', '6', '7', '8', '9', '0'},
        };

        public bool TryFind(char c, out Point pt)
        {
            pt = new Point();
            c = Char.ToUpper(c);
            for (int y = 0; y < _keyboard.Count; y++)
            {
                for (int x = 0; x < _keyboard[y].Length; x++)
                {
                    if (_keyboard[y][x] == c)
                    {
                        pt.X = x;
                        pt.Y = y;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
