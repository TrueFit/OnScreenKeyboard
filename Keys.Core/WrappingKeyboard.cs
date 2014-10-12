using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keys.Core
{
	/// <summary>
	/// A keyboard implementation that wraps on columns and rows, so you can go from last column to first in a single move
	/// </summary>
	public class WrappingKeyboard : Keyboard
	{
		protected override Tuple<short, short> FindShortestPath(Position start, Position end)
		{
			// get the shortest wrapped distance, and compare it to the internal distance
			var rw = (start.Row < this.Height / 2)
				? (short)(end.Row - this.Height - start.Row)
				: (short)(this.Height - start.Row - end.Row);
			var row = (Math.Abs(rw) < Math.Abs((short)(end.Row - start.Row))) ? rw : (short)(end.Row - start.Row);

			var cw = (start.Column < this.Width/2) 
				? (short)(end.Column - this.Width - start.Column )
				: (short)(this.Width - start.Column + end.Column );
			var col = (Math.Abs(cw) < Math.Abs((short)(end.Column - start.Column))) ? cw : (short)(end.Column - start.Column);

			return new Tuple<short, short>(row, col);
		}
	}
}
