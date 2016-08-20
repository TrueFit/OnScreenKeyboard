using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboard
{
    class Program
    {
        // Typically the logger and other dependencies would be injected.
        // This has been omitted for expediency.
        static ILogger _logger = new Logger();
        static IInputParser _inputParser = new InputParser();

        static void Main(string[] args)
        {
            // validate the input parameters
            string inputFile, outputFile;
            try
            {
                if (!ValidInputs(args, out inputFile, out outputFile))
                {
                    Usage();
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error validating input parameters", ex);
                return;
            }

            // parse the input file
            List<string> inputData = null;
            try
            {
                inputData = _inputParser.Parse(inputFile);
                //foreach (var data in inputData)
                //    Console.Out.WriteLine(data);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Error processing file {0}", inputFile), ex);
                return;
            }



        }
        static bool ValidInputs(string[] args, out string inputFile, out string outputFile)
        {
            inputFile = outputFile = string.Empty;

            if (args.Length != 2 || !File.Exists(args[0]))
                return false;

            inputFile = args[0];
            outputFile = args[1];
            return true;
        }

        static void Usage()
        {
            Console.Out.WriteLine(System.AppDomain.CurrentDomain.FriendlyName);
            Console.Out.WriteLine("Usage:");
            Console.Out.WriteLine(string.Format("    {0} <input file> <output file>", 
                System.AppDomain.CurrentDomain.FriendlyName));
        }
    }
}
