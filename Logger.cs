using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboard
{
    public interface ILogger
    {
        void Info(string msg);
        void Error(string msg, Exception ex = null);
    }

    // This is just a simple logging class. In a production environment 
    // something more full-featured, like log4net, should be used.
    public class Logger : ILogger
    {
        public void Info(string msg) 
        {
            Console.Out.WriteLine(msg);
        }

        public void Error(string msg, Exception ex = null)
        {
            Console.Error.WriteLine(msg);
            if (ex != null)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.Source);
                Console.Error.WriteLine(ex.StackTrace);
            }
        }
    }
}
