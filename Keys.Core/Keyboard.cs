using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Keys.Core
{
	/// <summary>
	/// A default keyboard implementation, defaulting to a 6x6 grid
	/// </summary>

	public class Keyboard
	{
		[DebuggerDisplay("{Row},{Column}")]
		public class Position
		{
			public Position(short row = 0, short col = 0)
			{
				this.Row = row;
				this.Column = col;
			}
			public short Row { get; set; }
			public short Column { get; set; }
		}

		public class Commands
		{
			public const char Up = 'U';
			public const char Down = 'D';
			public const char Left = 'L';
			public const char Right = 'R';
			public const char Space = 'S';
			public const char Select = '#';
			public const string Separator = ",";
		}

		/// <remarks>
		/// instead of assuming that they are in alphabetical order, this allows us to build different keyboard layouts
		/// </remarks>
		protected const string DEFAULT_KEYS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
		public int GetCharPosition(char c)
		{
			return this.Keys.IndexOf(Char.ToUpper(c));
		} 
		protected const short DEFAULT_WIDTH = 6;

		public Keyboard(short width, string keys, short height, Position cursorPosition)
		{
			Width = width;
			Keys = keys;
			Height = height;
			CursorPosition = cursorPosition;
		}



		public short Width { get; private set; }
		public string Keys { get; private set; }
		public short Height { get; private set; }
		public Position CursorPosition { get; private set; }

		public Keyboard(short? width = null, string keys = null, Position start = null)
		{
			this.Width = width ?? DEFAULT_WIDTH;
			this.Keys = !String.IsNullOrEmpty(keys)? keys.ToUpper() : DEFAULT_KEYS;
			this.Height = (short)(this.Keys.Length / this.Width);
			this.CursorPosition = start ?? new Position();
		}

		/// <summary>
		/// Resets the position back to 0,0. 
		/// </summary>
		public void ResetCursor()
		{
			this.CursorPosition = new Position();
		}
		
		/// <summary> 
		/// Maps the character to a current position in the grid. Marked virtual as it's likely to be overriden by different keyboard implementations
		/// </summary>
		public virtual Position GetPosition(char c)
		{
			return new Position() { Column = (short)(GetCharPosition(c) % this.Width), Row = (short)(GetCharPosition(c) / this.Width) };
		}

		/// <summary> 
		/// Finds the movement necessary to get from one position to another. The default implementation doesn't wrap
		/// </summary>
		/// <remarks>Don't you just love saying Tuple?</remarks>
		protected virtual Tuple<short, short> FindShortestPath(Position start, Position end)
		{
			return new Tuple<short, short>((short)(end.Row - start.Row), (short)(end.Column - start.Column));
		}

		public string[] ProcessFile(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException("Could not find file at " + path);
			var lines = File.ReadAllLines(path);
			var cmds = new List<string>();
			foreach (var line in lines)
			{
				this.ResetCursor(); // for these purposes, we're assuming we always start from 0; but we could maintain position
				cmds.Add(this.GenerateCommands(line));
			}
			return cmds.ToArray();

		}

		/// <summary> 
		/// Takes an input string and generates the commands necessary to enter on the keyboard.
		/// </summary>
		public string GenerateCommands(string input)
		{
			var cmds = new List<char>();
			foreach (var c in input)
			{
				if (c == ' ')
				{
					cmds.Add(Commands.Space);
					continue;
				}
				var target = this.GetPosition(c);
				var path = this.FindShortestPath(CursorPosition, target);
				for (var i = 0; i < Math.Abs(path.Item1); i++)
					cmds.Add((path.Item1 > 0) ? Commands.Down : Commands.Up);
				for (var i = 0; i < Math.Abs(path.Item2); i++)
					cmds.Add((path.Item2 > 0) ? Commands.Right : Commands.Left);
				cmds.Add(Commands.Select);
				this.CursorPosition = target;
			}
			return String.Join(Commands.Separator, cmds.ToArray());
		}
	}
}
