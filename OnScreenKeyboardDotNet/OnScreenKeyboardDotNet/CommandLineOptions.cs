using System;
using System.Collections.Generic;
using System.IO;

namespace OnScreenKeyboardDotNet
{
	public class CommandLineOptions
	{
		public CommandLineOptions(string[] args)
		{
			options = new Dictionary<string, Predicate<string>>()
			{
				{
					"-source",
					arg =>
					{
						if (!File.Exists(arg))
							return false;

						Source = arg;
						return true;
					}
				},
				{
					"-paths",
					arg =>
					{
						string folder = Path.GetDirectoryName(arg);
						try
						{
							if (!Directory.Exists(folder))
								Directory.CreateDirectory(folder);
						}
						catch (DirectoryNotFoundException)
						{
							return false;
						}

						Paths = arg;
						return true;
					}
				},
				{
					"-traversal",
					arg =>
					{
						GridTraversal? traversal = (arg.Equals("infinite", StringComparison.OrdinalIgnoreCase)
							? (GridTraversal?)GridTraversal.Infinite
							: arg.Equals("bounded", StringComparison.OrdinalIgnoreCase)
								? (GridTraversal?)GridTraversal.Bounded
								: null);

						if (traversal == null)
							return false;

						Traversal = traversal.Value;
						return true;
					}
				},
				{
					"-keys",
					arg => 
					{
						if (!File.Exists(arg))
							return false;

						Keys = arg;
						return true;
					}
				},
				{
					"-startchar",
					arg => 
					{
						arg = arg.Trim();
						if (string.IsNullOrWhiteSpace(arg) || arg.Length != 1)
							return false;

						StartChar = arg[0];
						return true;
					}
				}	
			};

			Traversal = GridTraversal.Infinite;  // <-- default if unspecified
			StartChar = 'A';                     // <-- default if unspecified

			IsValid = ProcessArgs(args);
		}

		public string Source { get; private set; }

		public string Paths { get; private set; }

		public GridTraversal Traversal { get; private set; }

		public string Keys { get; private set; }

		public char StartChar { get; private set; }

		public bool IsValid { get; private set; }

		private bool ProcessArgs(string[] args)
		{
			int cnt = args.Length;
			for (int x = 0; x < (args.Length - 1); x += 2)
			{
				string entry = args[x].Trim().ToLower();

				Predicate<string> func;
				if (!options.TryGetValue(entry, out func)
					|| !func.Invoke(args[x + 1].Trim()))
					return false;
			}

			return !string.IsNullOrWhiteSpace(Source) && !string.IsNullOrWhiteSpace(Paths);
		}

		private Dictionary<string, Predicate<string>> options;
	}
}