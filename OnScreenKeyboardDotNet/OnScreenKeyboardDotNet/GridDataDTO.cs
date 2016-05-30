using System;
using System.Collections.Generic;
using System.IO;

namespace OnScreenKeyboardDotNet
{
	public class GridDataDTO
	{
		public GridDataDTO(GridTraversal traversal, char startchar)
		{
			IsInfiniteGrid = (traversal == GridTraversal.Infinite);
			StartChar = startchar;

			LoadDefault();
		}

		public int GridWidth { get; set; }

		public int GridHeight { get; set; }

		public bool IsInfiniteGrid { get; private set; }

		public char StartChar { get; private set; }

		public Dictionary<char, Tuple<int, int>> Mappings { get; set; }

		public Dictionary<char, char> AlternateChars { get; set; }

		public bool LoadFromFile(string keysFile)
		{
			Mappings.Clear();
			AlternateChars.Clear();

			// Read the lines to process
			List<string> lines = new List<string>();
			using (StreamReader file = new System.IO.StreamReader(keysFile))
			{
				string line;
				while ((line = file.ReadLine()) != null)
				{
					if (!string.IsNullOrWhiteSpace(line))
						lines.Add(line);
				}
			}

			if (lines.Count == 0)
				return false;

			string delimiter = "";
			bool isKeyMapSection = true;
			int cntLines = lines.Count, currRow;
			for (currRow = 0; currRow < cntLines; currRow++)
			{
				string buffer = lines[currRow].Trim();
				int len = buffer.Length;

				// validation of row length
				if (currRow > 0)
				{
					if (isKeyMapSection)
					{
						if (len != GridWidth)
						{
							Console.WriteLine(
								string.Format(
									"Key data at row #{0} is different from rest of data block.",
									currRow + 1));
							return false;
						}
					}
					else if (len != 2)
					{
						Console.WriteLine(
							string.Format(
								"Alternate character data at row #{0} is invalid. (Length is {1}.)",
								currRow + 1,
								len));
						return false;
					}
				}

				if (isKeyMapSection)
				{
					if (buffer != delimiter)
					{
						for (int currCol = 0; currCol < len; currCol++)
						{
							char c = buffer[currCol];

							if (Mappings.ContainsKey(c))
							{
								Console.WriteLine(
									string.Format(
										"Key character {0} is defined multiple times.",
										c));
								return false;
							}

							Mappings[c] = new Tuple<int,int>(currRow, currCol);
						}

						if (currRow == 0)
						{
							GridWidth = len;
							delimiter = new string('-', len);
						}
					}
					else
					{
						GridHeight = currRow;
						isKeyMapSection = false;
					}
				}
				else
				{
					if (AlternateChars.ContainsKey(buffer[0]))
					{
						Console.WriteLine(
							string.Format(
								"Alternate character {0} is defined multiple times.",
								buffer[0]));
						return false;
					}

					AlternateChars[buffer[0]] = buffer[1];
				}
			}

			// In case config has NO alternate character mappings (and no delimiter)
			if (isKeyMapSection)
				GridHeight = currRow;

			return currRow > 0;
		}

		public void LoadDefault()
		{
			GridWidth = GridHeight = 6;

			Mappings = new Dictionary<char, Tuple<int, int>>
			{
				{ 'A', new Tuple<int, int>(0, 0) },
				{ 'B', new Tuple<int, int>(0, 1) },
				{ 'C', new Tuple<int, int>(0, 2) },
				{ 'D', new Tuple<int, int>(0, 3) },
				{ 'E', new Tuple<int, int>(0, 4) },
				{ 'F', new Tuple<int, int>(0, 5) },

				{ 'G', new Tuple<int, int>(1, 0) },
				{ 'H', new Tuple<int, int>(1, 1) },
				{ 'I', new Tuple<int, int>(1, 2) },
				{ 'J', new Tuple<int, int>(1, 3) },
				{ 'K', new Tuple<int, int>(1, 4) },
				{ 'L', new Tuple<int, int>(1, 5) },

				{ 'M', new Tuple<int, int>(2, 0) },
				{ 'N', new Tuple<int, int>(2, 1) },
				{ 'O', new Tuple<int, int>(2, 2) },
				{ 'P', new Tuple<int, int>(2, 3) },
				{ 'Q', new Tuple<int, int>(2, 4) },
				{ 'R', new Tuple<int, int>(2, 5) },

				{ 'S', new Tuple<int, int>(3, 0) },
				{ 'T', new Tuple<int, int>(3, 1) },
				{ 'U', new Tuple<int, int>(3, 2) },
				{ 'V', new Tuple<int, int>(3, 3) },
				{ 'W', new Tuple<int, int>(3, 4) },
				{ 'X', new Tuple<int, int>(3, 5) },

				{ 'Y', new Tuple<int, int>(4, 0) },
				{ 'Z', new Tuple<int, int>(4, 1) },
				{ '1', new Tuple<int, int>(4, 2) },
				{ '2', new Tuple<int, int>(4, 3) },
				{ '3', new Tuple<int, int>(4, 4) },
				{ '4', new Tuple<int, int>(4, 5) },

				{ '5', new Tuple<int, int>(5, 0) },
				{ '6', new Tuple<int, int>(5, 1) },
				{ '7', new Tuple<int, int>(5, 2) },
				{ '8', new Tuple<int, int>(5, 3) },
				{ '9', new Tuple<int, int>(5, 4) },
				{ '0', new Tuple<int, int>(5, 5) }
			};

			AlternateChars = new Dictionary<char, char>()
			{
				{ 'a', 'A' },
				{ 'b', 'B' },
				{ 'c', 'C' },
				{ 'd', 'D' },
				{ 'e', 'E' },
				{ 'f', 'F' },
				{ 'g', 'G' },
				{ 'h', 'H' },
				{ 'i', 'I' },
				{ 'j', 'J' },
				{ 'k', 'K' },
				{ 'l', 'L' },
				{ 'm', 'M' },
				{ 'n', 'N' },
				{ 'o', 'O' },
				{ 'p', 'P' },
				{ 'q', 'Q' },
				{ 'r', 'R' },
				{ 's', 'S' },
				{ 't', 'T' },
				{ 'u', 'U' },
				{ 'v', 'V' },
				{ 'w', 'W' },
				{ 'x', 'X' },
				{ 'y', 'Y' },
				{ 'z', 'Z' }
			};
		}
	}
}