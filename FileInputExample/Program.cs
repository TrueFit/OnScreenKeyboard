using System;
using System.IO;

namespace Test
{
    class Program
    {
        

        static void Main(string[] args)
        {
            var keyboard = new OnScreenKeyboard.CommandParser();

            var result = new TextFileReader(Directory.GetCurrentDirectory() + @"\ExampleInput.txt");

            Console.WriteLine(keyboard.ReadInput(result));
            Console.ReadLine();
        }   

    }
}
