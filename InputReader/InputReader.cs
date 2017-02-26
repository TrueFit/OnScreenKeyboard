using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnScreenKeyboard;

namespace InputReader
{
    class InputReader
    {
        static void Main(string[] args)
        {
            DVRKeyboard keyboard = new DVRKeyboard();
            InputProcessor processor = new InputProcessor(keyboard);

            System.IO.StreamReader inputFile = new System.IO.StreamReader(args[0]);
            System.IO.StreamWriter outputWriter;
            string line;

            if (args.Length > 1)
            {
                outputWriter = new System.IO.StreamWriter(args[1]);
            }
            else
            {
                outputWriter = new System.IO.StreamWriter("Output.txt");
            }
            while ((line = inputFile.ReadLine()) != null)
            {
                if (line.Length > 0)
                {
                    string output = processor.ProcessInputString(line);
                    Console.WriteLine(line + " ==> " + output);
                    Console.WriteLine();
                    outputWriter.WriteLine(line + " ==> " + output);
                    outputWriter.WriteLine();
                }
            }

            inputFile.Close();
            outputWriter.Close();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
