using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboard
{
    public class InputProcessor
    {
        private readonly IKeyboard _keyboard;

        public InputProcessor(IKeyboard keyboard)
        {
            _keyboard = keyboard;
        }

        public string ProcessInputString(string inputString)
        {
            _keyboard.ResetCursor();
            char[] inputArray = inputString.ToCharArray();
            StringBuilder output = new StringBuilder();
            try
            {
                for (int i = 0; i < inputString.Length; i++)
                {
                    output.Append(_keyboard.PathToNextChar(inputArray[i]));
                    output.Append(_keyboard.GetOutputDelimiter());
                }

                return output.ToString().Trim(_keyboard.GetOutputDelimiter());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
