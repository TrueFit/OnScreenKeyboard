using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace onScreenKeyboard
{
    class Program
    {
        static void Main(string[] args)
        {
            // this block clode reads in the file
            // testing was not required 
            if (args.Length == 0)
            {
                System.Console.WriteLine("C:\\test.txt");
            }

            var keyboardPathGen = new KeyboardPathGen();


            // Tyr catch to reead in the file or send a message there is error reading the file
            try
            {
                var testCases = File.ReadAllLines("test.txt");
                foreach (var testCase in testCases)
                {
                    var shorthand = keyboardPathGen.GenarateShortHandForm(testCase);
                    System.Console.WriteLine(shorthand);
                }
            }
            catch (IOException e)
            {

                System.Console.Error.WriteLine("Error reading from the path: " + "test.txt");
                System.Console.Error.WriteLine(e.Message);
            }


           
            
        }
    }
}
