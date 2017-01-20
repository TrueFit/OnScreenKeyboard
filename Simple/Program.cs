using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Library;
using Simple.Library.SearchService;
using Simple.Library.Logging;
using System.Reflection;
using System.IO;

namespace Simple
{
    /// <summary>
    /// SimpleConsoleDemo 
    /// </summary>
    class SimpleConsoleDemo
    {
        //Location of logger file from App.config file
        public static string loggerFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ConfigurationManager.AppSettings["LoggerFlatFileLocation"]);
        public static string searchTermFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ConfigurationManager.AppSettings["SearchTermFile"]);
        public static string searchTermDelimter = ConfigurationManager.AppSettings["SearchTermDelimeter"];
        static void Main(string[] args)
        {
            //initialize Logger and search term service            
            ILogger logger = new FileLogger(loggerFile);
            SearchService search = new SearchService(logger);

            CreatePaths(search);
            Console.ReadLine();
        }

        public static void CreatePaths(SearchService search)
        {
            Console.WriteLine("KEYBOARD");
            Console.WriteLine(search.ReturnKeyboardInformation());
            Console.WriteLine(search.ReturnKeyboardRepresentation());
            Console.WriteLine("--------------------------------");
            string term = "";
            try
            { 
                using (StreamReader sr = new StreamReader(searchTermFile))
                {
                    while ((term = sr.ReadLine()) != null)
                    {
                        Console.WriteLine("Search term: " + term);
                        string path = search.ReturnSearchPath(term, searchTermDelimter);
                        Console.WriteLine("Search term path: " + path);
                    }
                }
                Console.WriteLine("Search completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to read search term input.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
    }
}
