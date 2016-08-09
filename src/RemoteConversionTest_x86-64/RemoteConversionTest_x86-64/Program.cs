using StringExtensions;
using System;
using System.IO;
using System.Reflection;

namespace RemoteConversionTest_x86_64
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] lines;

            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // attempt to load the file specified in args[0]
            try
            {
                lines = File.ReadAllLines(Path.Combine(executableLocation, args[0]));
            }
            catch (FieldAccessException)
            {
                Console.WriteLine("File not found.");
                System.Console.ReadKey();
                return;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Please specify a file to translate.");
                System.Console.ReadKey();
                return;
            }

            foreach (string line in lines)
            {
                // attempt to translate the current line to a string of directions and output the result to the console.
                try
                {
                    Console.WriteLine(string.Format("{0} - {1}", line, line.ToDirections())); // output the original line, then the translated line.
                }
                catch (Exception)
                {
                    Console.WriteLine(string.Format("{0} - Contains characters that cannot be translated.", line));
                    continue;
                }
            }

            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
}
