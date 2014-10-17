using System.IO;
using System.Text;
using OnScreenKeyboard;

namespace Test
{
    public class TextFileReader : ITextReader
    {
        private string _sourceFile = "";
        public TextFileReader(string sourceFile)
        {
            _sourceFile = sourceFile;
        }

        public string GetText()
        {
            var returnValue = new StringBuilder();

            using (var sr = new StreamReader(_sourceFile))
            {
                var line = sr.ReadToEnd();
                returnValue.Append(line);
            }

            return returnValue.ToString();
        }
    }
}
