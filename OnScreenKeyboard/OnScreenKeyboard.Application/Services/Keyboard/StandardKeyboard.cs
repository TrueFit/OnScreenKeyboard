using System;

namespace OnScreenKeyboard.Application.Services.Keyboard
{
    public class StandardKeyboard : IKeyboard
    {
        Dictionary<int, Dictionary<int, char>> _keyboard;

        //Would probably pull this from somewhere in a prod environment.
        //Database, json file, xml file etc...
        //If we were pulling this information and loading the dictionary from somewhere else, ->
        //The implementation would actually reside in services and be injected there.

        //UPDATE: After thinking about this a bit more I would break it up a bit differently than originally thought.
        //I would leave this implementation as a 1 dimensional, graph based on screen keyboard.
        //If the on screen keyboard has a more than one option (special characters button, gif button etc...)
        //I would then create a different service and implement the logic locally in that service.
        public StandardKeyboard()
        {
            _keyboard = new Dictionary<int, Dictionary<int, char>>();

            for (int i = 0; i < 6; i++)
            {
                _keyboard[i] = new Dictionary<int, char>();
            }
            _keyboard[0].Add(0, 'A');
            _keyboard[0].Add(1, 'B');
            _keyboard[0].Add(2, 'C');
            _keyboard[0].Add(3, 'D');
            _keyboard[0].Add(4, 'E');
            _keyboard[0].Add(5, 'F');

            _keyboard[1].Add(0, 'G');
            _keyboard[1].Add(1, 'H');
            _keyboard[1].Add(2, 'I');
            _keyboard[1].Add(3, 'J');
            _keyboard[1].Add(4, 'K');
            _keyboard[1].Add(5, 'L');

            _keyboard[2].Add(0, 'M');
            _keyboard[2].Add(1, 'N');
            _keyboard[2].Add(2, 'O');
            _keyboard[2].Add(3, 'P');
            _keyboard[2].Add(4, 'Q');
            _keyboard[2].Add(5, 'R');

            _keyboard[3].Add(0, 'S');
            _keyboard[3].Add(1, 'T');
            _keyboard[3].Add(2, 'U');
            _keyboard[3].Add(3, 'V');
            _keyboard[3].Add(4, 'W');
            _keyboard[3].Add(5, 'X');

            _keyboard[4].Add(0, 'Y');
            _keyboard[4].Add(1, 'Z');
            _keyboard[4].Add(2, '1');
            _keyboard[4].Add(3, '2');
            _keyboard[4].Add(4, '3');
            _keyboard[4].Add(5, '4');

            _keyboard[5].Add(0, '5');
            _keyboard[5].Add(1, '6');
            _keyboard[5].Add(2, '7');
            _keyboard[5].Add(3, '8');
            _keyboard[5].Add(4, '9');
            _keyboard[5].Add(5, '0');
        }

        public string GetDirectionFromInput(string line)
        {
            int currentY = 0;
            int currentX = 0;
            List<string> currentLineOutput = new List<string>();

            foreach (var character in line.ToUpper().ToCharArray())
            {
                if (character == ' ')
                {
                    currentLineOutput.Add("S");
                    continue;
                }

                var row = GetLetterRow(character);

                var column = GetLetterColumn(row, character);

                var horizontalNum = row - currentX;

                string horizontalDirection = horizontalNum > 0 ? "D" : "U";

                currentLineOutput.AddRange(Enumerable.Repeat(horizontalDirection, Math.Abs(horizontalNum)));

                currentX += horizontalNum;

                var verticalNum = column - currentY;

                string verticalDirection = verticalNum > 0 ? "R" : "L";

                currentLineOutput.AddRange(Enumerable.Repeat(verticalDirection, Math.Abs(verticalNum)));

                currentY += verticalNum;

                currentLineOutput.Add("#");

            }
            return string.Join(",", currentLineOutput);
        }

        private int GetLetterRow(char character)
        {
            foreach (var key in _keyboard.Keys)
            {
                if (_keyboard[key].Values.Any(x => x == character))
                {
                    return key;
                }
            }

            throw new ArgumentException($"Character {character} not found in keyboard");
        }

        private int GetLetterColumn(int row, char character)
        {
            foreach (var key in _keyboard[row].Keys)
            {
                if (_keyboard[row][key] == character)
                {
                    return key;
                }
            }

            throw new ArgumentException($"Character {character} not found in keyboard");
        }



    }
}

