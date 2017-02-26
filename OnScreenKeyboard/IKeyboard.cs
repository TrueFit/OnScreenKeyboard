using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboard
{
    public interface IKeyboard
    {
        string PathToNextChar(char nextChar);

        char GetOutputDelimiter();

        void ResetCursor();
    }
}
