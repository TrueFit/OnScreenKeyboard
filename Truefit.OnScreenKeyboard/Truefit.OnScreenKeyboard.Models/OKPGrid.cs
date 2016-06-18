using System;
using System.Collections.Generic;
using System.Linq;

namespace Truefit.OnScreenKeyboard.Models
{
    /// <summary>
    /// The overlay Keyboard physical grid allows for defining the full physical grid of the 
    /// keybaoard, even if all keys aren't mapped
    /// </summary>
    public struct OKPGrid
    {
        public readonly int Rows;
        public readonly int Columns;
        public readonly int CurStartRow;
        public readonly int CurStartCol;

        public OKPGrid(int rows, int columns)
        {
            if(rows < 1 || columns < 1) throw new FormatException("OverlayKeyboard PGrid must  have one or more rows and columns");

            this.Rows = rows;
            this.Columns = columns;
            this.CurStartRow = 1;
            this.CurStartCol = 1;
        }
    }
}
