using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboard.Test
{
    public class StringLiteralReader : ITextReader
    {
        private readonly string _stringValue = "";
        public StringLiteralReader(string stringValue)
        {
            _stringValue = stringValue;
        }

        public string GetText()
        {
            return _stringValue;
        }
    }
}
