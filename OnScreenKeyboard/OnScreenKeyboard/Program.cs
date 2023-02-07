using System;

namespace OnScreenKeyboard
{
    class Program
    {
        static void Main(string[] args)
        {
            DefaultKeyboard keyboard = new DefaultKeyboard();
            KeyboardProcessor keyBoardProcessor = new KeyboardProcessor(keyboard);

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
                    string output = keyBoardProcessor.ProcessInput(line);
                    Console.WriteLine(line + ": " + output);
                    Console.WriteLine();
                    outputWriter.WriteLine(line + ": " + output);
                    outputWriter.WriteLine();
                }
            }

            inputFile.Close();
            outputWriter.Close();
        }
    }
}
