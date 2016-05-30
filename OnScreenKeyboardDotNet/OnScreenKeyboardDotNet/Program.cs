using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboardDotNet
{
	class Program
	{
		static int Main(string[] args)
		{
			DateTime startTime = DateTime.Now;

			var commandOptions = new CommandLineOptions(args);
			if (!commandOptions.IsValid)
			{
				ShowCommandLineOptions();
				return 1;
			}

			var gridData = new GridDataDTO(commandOptions.Traversal, commandOptions.StartChar);
			if (!string.IsNullOrWhiteSpace(commandOptions.Keys))
			{
				if (!gridData.LoadFromFile(commandOptions.Keys))
				{
					Console.WriteLine("Failed to load key mappings from file. Use default mapping instead?");
					Console.WriteLine("");
					Console.WriteLine("  ABCDEF");
					Console.WriteLine("  GHIJKL");
					Console.WriteLine("  MNOPQR");
					Console.WriteLine("  STUVWX");
					Console.WriteLine("  YZ1234");
					Console.WriteLine("  567890");
					Console.WriteLine("");

					string loadDefault = "";
					while (!loadDefault.Equals("y", StringComparison.OrdinalIgnoreCase) && !loadDefault.Equals("n", StringComparison.OrdinalIgnoreCase))
					{
						Console.Write("\n(y/n) ");
						loadDefault = Console.ReadLine();
					}
					
					if (loadDefault.Equals("n", StringComparison.OrdinalIgnoreCase))
						return 2;

					gridData.LoadDefault();
				}
			}

			PathMapper theMapper = new PathMapper(
				commandOptions.Source,
				commandOptions.Paths,
				gridData);
			int lines = theMapper.Process();

			TimeSpan runningTime = DateTime.Now - startTime;

			Console.WriteLine("");
			Console.WriteLine(
				string.Format(
					"Total seconds: {0:0.0}", runningTime.TotalSeconds));
			Console.WriteLine(
				string.Format(
					"Average rows per second: {0:#,##0}",
					Math.Floor(runningTime.TotalSeconds) > 0.0 
						? lines / runningTime.TotalSeconds 
						: lines));

			return 0;
		}

		private static void ShowCommandLineOptions()
		{
			Console.WriteLine("OnScreenKeyboardDotNet -source <Source File> -paths <Paths File>");
			Console.WriteLine("                        [-traversal <Infinite or Bounded> -keys <Keys File>");
			Console.WriteLine("                        [-startchar <Character To Start At>");
			Console.WriteLine("     -source: Specifies the source file containing the list of strings.");
			Console.WriteLine("              This must be a text file that is UTF-8 encoded.");
			Console.WriteLine("");
			Console.WriteLine("      -paths: Specifies the output file into which navigation paths will be written.");
			Console.WriteLine("              This file, if it previously exists, will not be appended. It will be overwritten.");
			Console.WriteLine("");
			Console.WriteLine(" [-traversal:] Dictates whether or not the cursor can move freely beyond the boundary of the grid");
			Console.WriteLine("               and automatically wrap-around. When in \"Infinite\" mode, a cursor on the left-most");
			Console.WriteLine("               coordinate can move left and the cursor will move to the far right. This behavior");
			Console.WriteLine("               carries over to all bounds. A \"Bounded\" grid will not permit cursor movement past the");
			Console.WriteLine("               four boundaries. (Default = Infinite)");
			Console.WriteLine("");
			Console.WriteLine("      [-keys:] Specifies the name of a file containing an alternate mapping of keys. This file");
			Console.WriteLine("               must contain both the mappings along with definitions for what \"alternate\"");
			Console.WriteLine("               characters should be mapped to.");
			Console.WriteLine("");
			Console.WriteLine(" [-startchar:] Specifies the character on the grid where the cursor will start (default = A)");
		}
	}
}
