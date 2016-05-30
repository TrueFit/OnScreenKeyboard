using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OnScreenKeyboardDotNet
{
	public class PathMapper
	{
		public PathMapper(string inputFile, string pathsFile, GridDataDTO gridData)
		{
			this.inputFile = inputFile;
			this.pathsFile = pathsFile;
			this.gridData = gridData;

			char startChar = gridData.StartChar;
			Tuple<int, int> startCoords;
			if (!gridData.Mappings.TryGetValue(startChar, out startCoords))
			{
				char altChar;
				if (gridData.AlternateChars.TryGetValue(startChar, out altChar))
					startChar = altChar;
			}

			if (gridData.Mappings.TryGetValue(startChar, out startCoords))
			{
				startRow = startCoords.Item1;
				startCol = startCoords.Item2;
			}
			else
				startRow = startCol = 0;
		}

		public int Process()
		{
			// Read the lines to process
			List<string> lines = new List<string>();
			using (StreamReader file = new System.IO.StreamReader(inputFile))
			{
				string line;
				while ((line = file.ReadLine()) != null)
				{
					if (!string.IsNullOrWhiteSpace(line))
						lines.Add(line);
				}
			}

			// Write the string to a file.
			using (StreamWriter file = new System.IO.StreamWriter(pathsFile))
			{
				int maxTravelCols = (gridData.IsInfiniteGrid
					? gridData.GridWidth / 2 + gridData.GridWidth % 2
					: gridData.GridWidth);
				int maxTravelRows = (gridData.IsInfiniteGrid
					? gridData.GridHeight / 2 + gridData.GridHeight % 2
					: gridData.GridHeight);
				int lineCount = 0;

				foreach (string line in lines)
				{
					if ((++lineCount % 100000) == 0 || (lineCount % 99954 == 0))
						Console.WriteLine(
							string.Format(
								"Now processing {0} at row {1}",
								line, lineCount));

					StringBuilder sbPath = new StringBuilder();
					int row = startRow;
					int col = startCol;

					foreach (char d in line) 
					{
						if (d == ' ') 
						{
							sbPath.Append((sbPath.Length > 0 ? "," : "") + "S");
							continue;
						}

						char c = d, a;
						if (gridData.AlternateChars.TryGetValue(c, out a))
							c = a;

						Tuple<int, int> mapEntry;
						if (!gridData.Mappings.TryGetValue(c, out mapEntry))
						{
							sbPath.Append((sbPath.Length > 0 ? "," : "") + "X");
							continue;
						}

						int newRow = mapEntry.Item1;
						int newCol = mapEntry.Item2;

						WriteLateral(sbPath, row, newRow, gridData.GridHeight, maxTravelRows, 'D', 'U');
						WriteLateral(sbPath, col, newCol, gridData.GridWidth, maxTravelCols, 'R', 'L');
						sbPath.Append((sbPath.Length > 0 ? "," : "") + "#");

						row = newRow;
						col = newCol;
					}

					file.WriteLine(sbPath.ToString());
				}

				return lineCount;
			}
		}

		private void WriteLateral(StringBuilder sb, int currPos, int newPos, int extent, int maxTravel, char posChar, char negChar) {
			int diff = newPos - currPos;
			int absDiff = Math.Abs(diff);

			if (absDiff > maxTravel) {
				diff = (extent - absDiff) * (diff > 0 ? -1 : 1);
				absDiff = Math.Abs(diff);
			}

			for (int y = 0; y < absDiff; y++)
				sb.Append((sb.Length > 0 ? "," : "") + (diff > 0 ? posChar : negChar));
		}

		private string inputFile, pathsFile;
		private GridDataDTO gridData;
		private int startRow, startCol;
	}
}