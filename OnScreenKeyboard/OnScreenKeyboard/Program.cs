using System;
using System.IO;
using OnScreenKeyboard.Bootstrapping;
using SimulatableApi.StreamStore;

namespace OnScreenKeyboard
{
    public class Program
    {
        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                var exception = args.ExceptionObject as Exception;
                if(exception != null) Console.WriteLine(exception.Message);
                Environment.Exit(0);
            };
        }


        public static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("You gotta provide a file uri, bro.");
                Environment.Exit(0);
            }
            
            var container = Bootstrapper.BuildContainer();
            (container.GetInstance<Orchestrator>()).Do(args[0]);
            
        }

        
    }
}