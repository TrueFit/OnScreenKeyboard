using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboard
{
    class DVRChar
    {
        private int row;
        private int column;
        private char character;

        public DVRChar(int r, int c, char ch)
        {
            this.row = r;
            this.column = c;
            this.character = ch;
        }

        public int Row
        {
            get
            {
                return row;
            }
        }

        public int Column
        {
            get
            {
                return column;
            }
        }

        public char Character
        {
            get
            {
                return character;
            }
        }
    }
}
