using System;
using System.Collections.Generic;
using System.Linq;

namespace Truefit.OnScreenKeyboard.Models
{
    public struct GridPosition
    {
        public int Row;
        public int Column;

        public GridPosition(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
    }
}
