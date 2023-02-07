using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboard
{
    public interface IKeyboard
    {
        string pathToNextLetter(char nextLetter);

        char outputDelimiter();

        char selectKey();

        void resetKeyboard();

    }
}
