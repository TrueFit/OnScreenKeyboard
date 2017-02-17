using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keys.Core;

namespace Keys.Console
{
	public class Program
	{
		public static void Main(string[] args)
		{
			System.Console.Write("\r\n-------\r\n");
			if (args.Length == 0)
			{
				System.Console.WriteLine("You must provide at least one file");
				return;
			}

			var kb = new Keyboard();
			foreach (var arg in args)
			{
				if (!File.Exists(arg))
				{
					System.Console.WriteLine("### Couldn't find file at {0}", arg);
					continue;
				}
				System.Console.WriteLine("### Processing {0}\r\n", arg);
				kb.ProcessFile(args[0]).ToList().ForEach(System.Console.WriteLine);
				System.Console.WriteLine("\r\n");
			}
						
		}
	}
}
