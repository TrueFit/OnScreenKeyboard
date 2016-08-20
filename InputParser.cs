using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboard
{
    public interface IInputParser
    {
        List<string> Parse(string file);
    }

    public class InputParser : IInputParser
    {
        // Typically the logger and other dependencies would be injected.
        // This has been omitted for expediency.
        static ILogger _logger = new Logger();

        public List<string> Parse(string file)
        {
            var list = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            _logger.Info("Blank line found, ignoring it.");
                            continue;
                        }

                        list.Add(line.Trim());
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Error while parsing {0}", file), ex);
                throw ex;
            }
        }
    }
}
