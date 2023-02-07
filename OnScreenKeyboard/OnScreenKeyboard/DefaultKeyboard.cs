using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboard
{
    public class DefaultKeyboard : IKeyboard
    {
        private Dictionary<char, Button> _defaultKeys;
        private Button _currentKey;
        private Button _nextKey;
        private char _delimiter;
        private char _selectKey;

        public DefaultKeyboard()
        {
            _delimiter = outputDelimiter();
            _selectKey = selectKey();
            _defaultKeys = new Dictionary<char, Button>();
            _defaultKeys.Add('A', new Button(0, 0, 'A'));
            _defaultKeys.Add('B', new Button(1, 0, 'B'));
            _defaultKeys.Add('C', new Button(2, 0, 'C'));
            _defaultKeys.Add('D', new Button(3, 0, 'D'));
            _defaultKeys.Add('E', new Button(4, 0, 'E'));
            _defaultKeys.Add('F', new Button(5, 0, 'F'));
            _defaultKeys.Add('G', new Button(0, 1, 'G'));
            _defaultKeys.Add('H', new Button(1, 1, 'H'));
            _defaultKeys.Add('I', new Button(2, 1, 'I'));
            _defaultKeys.Add('J', new Button(3, 1, 'J'));
            _defaultKeys.Add('K', new Button(4, 1, 'K'));
            _defaultKeys.Add('L', new Button(5, 1, 'L'));
            _defaultKeys.Add('M', new Button(0, 2, 'M'));
            _defaultKeys.Add('N', new Button(1, 2, 'N'));
            _defaultKeys.Add('O', new Button(2, 2, 'O'));
            _defaultKeys.Add('P', new Button(3, 2, 'P'));
            _defaultKeys.Add('Q', new Button(4, 2, 'Q'));
            _defaultKeys.Add('R', new Button(5, 2, 'R'));
            _defaultKeys.Add('S', new Button(0, 3, 'S'));
            _defaultKeys.Add('T', new Button(1, 3, 'T'));
            _defaultKeys.Add('U', new Button(2, 3, 'U'));
            _defaultKeys.Add('V', new Button(3, 3, 'V'));
            _defaultKeys.Add('W', new Button(4, 3, 'W'));
            _defaultKeys.Add('X', new Button(5, 3, 'X'));
            _defaultKeys.Add('Y', new Button(0, 4, 'Y'));
            _defaultKeys.Add('Z', new Button(1, 4, 'Z'));
            _defaultKeys.Add('1', new Button(2, 4, '1'));
            _defaultKeys.Add('2', new Button(3, 4, '2'));
            _defaultKeys.Add('3', new Button(4, 4, '3'));
            _defaultKeys.Add('4', new Button(5, 4, '4'));
            _defaultKeys.Add('5', new Button(0, 5, '5'));
            _defaultKeys.Add('6', new Button(1, 5, '6'));
            _defaultKeys.Add('7', new Button(2, 5, '7'));
            _defaultKeys.Add('8', new Button(3, 5, '8'));
            _defaultKeys.Add('9', new Button(4, 5, '9'));
            _defaultKeys.Add('0', new Button(5, 5, '0'));

            _defaultKeys.TryGetValue('A', out _currentKey);
        }
        public string pathToNextLetter(char nextLetter)
        {
            var path = new StringBuilder();
            if(nextLetter == ' ')
            {
                return "S,";
            }
            if(_defaultKeys.TryGetValue(char.ToUpper(nextLetter), out _nextKey))
            {
                if(_nextKey.letter == _currentKey.letter)
                {
                    return "#,";
                }
                //figure out the distance we need to move
                var xDistance = _nextKey.x - _currentKey.x;
                var yDistance = _nextKey.y - _currentKey.y;

                //record the vertical moves
                for (int i = 0; i < Math.Abs(yDistance); i++)
                {
                    if (yDistance < 0)
                    {
                        path.Append("U");
                    }
                    else
                    {
                        path.Append("D");
                    }
                    path.Append(_delimiter);
                }
                //record the horizontal moves
                for (int i = 0; i < Math.Abs(xDistance); i++)
                {
                    if (xDistance < 0)
                    {
                        path.Append("L");
                    }
                    else
                    {
                        path.Append("R");
                    }
                    path.Append(_delimiter);

                }
                //after moving, select and return
                path.Append(_selectKey);
                path.Append(_delimiter);

                _currentKey = _nextKey;

                return path.ToString();

            }
            else
            {
                throw new Exception();
            }
        }

        public char outputDelimiter()
        {
            return ',';
        }

        public char selectKey()
        {
            return '#';
        }

        public void resetKeyboard()
        {
            _defaultKeys.TryGetValue('A', out _currentKey);
        }

    }
}
