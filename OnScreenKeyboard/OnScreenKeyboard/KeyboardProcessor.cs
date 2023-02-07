using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboard
{
    public class KeyboardProcessor
    {
        private IKeyboard _keyboard;

        public KeyboardProcessor(IKeyboard keyboard)
        {
            _keyboard = keyboard;
        }

        public string ProcessInput(string inputString)
        {
            try
            {
                //for each input, we want to move the cursor back to 0,0
                _keyboard.resetKeyboard();

                var path = new StringBuilder();

                //for each char in the inputString, call the keyboard provided to calculate the distance
                foreach (char nextLetter in inputString)
                {
                    path.Append(_keyboard.pathToNextLetter(nextLetter));
                }


                return path.ToString().TrimEnd(_keyboard.outputDelimiter());
            }
            catch (Exception ex)
            {
                return "There was an error processing the input";
            }
        }
    }
}
