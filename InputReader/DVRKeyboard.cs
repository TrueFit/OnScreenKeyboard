using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnScreenKeyboard;

namespace InputReader
{
    public class DVRKeyboard : IKeyboard
    {
        private Dictionary<char,KeyboardButton> _keys;
        private char _outputDelimiter;
        private KeyboardButton _curButton;

        public DVRKeyboard() {
            //Initialize the keyboard
            _outputDelimiter = ',';
            _keys = new Dictionary<char, KeyboardButton>();
            _keys.Add('a',new KeyboardButton(0, 0, 'a'));
            _keys.Add('b', new KeyboardButton(1, 0, 'b'));
            _keys.Add('c', new KeyboardButton(2, 0, 'c'));
            _keys.Add('d', new KeyboardButton(3, 0, 'd'));
            _keys.Add('e', new KeyboardButton(4, 0, 'e'));
            _keys.Add('f', new KeyboardButton(5, 0, 'f'));
            _keys.Add('g', new KeyboardButton(0, 1, 'g'));
            _keys.Add('h', new KeyboardButton(1, 1, 'h'));
            _keys.Add('i', new KeyboardButton(2, 1, 'i'));
            _keys.Add('j', new KeyboardButton(3, 1, 'j'));
            _keys.Add('k', new KeyboardButton(4, 1, 'k'));
            _keys.Add('l', new KeyboardButton(5, 1, 'l'));
            _keys.Add('m', new KeyboardButton(0, 2, 'm'));
            _keys.Add('n', new KeyboardButton(1, 2, 'n'));
            _keys.Add('o', new KeyboardButton(2, 2, 'o'));
            _keys.Add('p', new KeyboardButton(3, 2, 'p'));
            _keys.Add('q', new KeyboardButton(4, 2, 'q'));
            _keys.Add('r', new KeyboardButton(5, 2, 'r'));
            _keys.Add('s', new KeyboardButton(0, 3, 's'));
            _keys.Add('t', new KeyboardButton(1, 3, 't'));
            _keys.Add('u', new KeyboardButton(2, 3, 'u'));
            _keys.Add('v', new KeyboardButton(3, 3, 'v'));
            _keys.Add('w', new KeyboardButton(4, 3, 'w'));
            _keys.Add('x', new KeyboardButton(5, 3, 'x'));
            _keys.Add('y', new KeyboardButton(0, 4, 'y'));
            _keys.Add('z', new KeyboardButton(1, 4, 'z'));
            _keys.Add('1', new KeyboardButton(2, 4, '1'));
            _keys.Add('2', new KeyboardButton(3, 4, '2'));
            _keys.Add('3', new KeyboardButton(4, 4, '3'));
            _keys.Add('4', new KeyboardButton(5, 4, '4'));
            _keys.Add('5', new KeyboardButton(0, 5, '5'));
            _keys.Add('6', new KeyboardButton(1, 5, '6'));
            _keys.Add('7', new KeyboardButton(2, 5, '7'));
            _keys.Add('8', new KeyboardButton(3, 5, '8'));
            _keys.Add('9', new KeyboardButton(4, 5, '9'));
            _keys.Add('0', new KeyboardButton(5, 5, '0'));
            _keys.TryGetValue('a', out _curButton);
        }

        public string PathToNextChar(char nextChar)
        {
            if (nextChar == ' ')
            {
                return "S";
            }
            KeyboardButton nextButton;
            //Get the buttons that correspond to the characters we are getting the path between
            var nextButtonExists = _keys.TryGetValue(Char.ToLower(nextChar), out nextButton);
            if (nextButtonExists)
            {
                StringBuilder path = new StringBuilder();
                int numVerticalMoves = _curButton.yValue - nextButton.yValue;
                //Determine if we need to move up or down or do nothing
                if (numVerticalMoves > 0)
                {
                    for (int i = 0; i < numVerticalMoves; i++)
                    {
                        path.Append("U");
                        path.Append(_outputDelimiter);
                    }
                }
                else if (numVerticalMoves < 0)
                {
                    for (int i = 0; i > numVerticalMoves; i--)
                    {
                        path.Append("D");
                        path.Append(_outputDelimiter);
                    }
                }

                int numHorizontalMoves = _curButton.xValue - nextButton.xValue;
                //Determine if we need to move left or right or do nothing
                if (numHorizontalMoves > 0)
                {
                    for (int i = 0; i < numHorizontalMoves; i++)
                    {
                        path.Append("L");
                        path.Append(_outputDelimiter);
                    }
                }
                else if (numHorizontalMoves < 0)
                {
                    for (int i = 0; i > numHorizontalMoves; i--)
                    {
                        path.Append("R");
                        path.Append(_outputDelimiter);
                    }
                }

                path.Append("#");
                _curButton = nextButton;
                return path.ToString();
            }
            else
            {
                throw new InvalidInputException("Invalid Input");
            }

        }

        public char GetOutputDelimiter()
        {
            return _outputDelimiter;
        }

        public void ResetCursor()
        {
            _keys.TryGetValue('a', out _curButton);
        }
    }
}
