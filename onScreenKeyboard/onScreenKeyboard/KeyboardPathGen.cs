using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace onScreenKeyboard
{
    class KeyboardPathGen
    {
        // regular expression
        private static Regex vaildInputCharacter = new Regex("[a-zA-Z0-9]", RegexOptions.Compiled);
        private static Dictionary<char, Position> keyPositions = generateLookUpTable();
        private Position InitialPosition = new Position(0, 0);
        private Position cursor;

        public KeyboardPathGen()
        {
            Reset();
        }

        public void Reset()
        {
            cursor = InitialPosition;
        }

        public string GenarateShortHandForm(string testCase)
        {
            Reset();
            if (testCase == "")
            {
                return "";
            }

            List<string> shortHand = new List<string>();
            for (int i = 0; i < testCase.Length; i++)
            {
                var character = toUpperCase(testCase[i]);
                if (character == ' ')
                {
                    shortHand.Add("S");
                }
                else if (!vaildInputCharacter.IsMatch(character.ToString()))
                {
                    return "Invalid input: " + testCase;
                }
                else
                {
                    shortHand.Add(shortHandForMoveTo(character));
                }

            }
            return String.Join(",", shortHand);
        }


        // this will assume ther is valid input
        private  string shortHandForMoveTo(char character)
        {
            var characterPosition = keyPositions[character];
            var distance = characterPosition - cursor;

            cursor = characterPosition;

            var shorthand = "";

            if (distance.Y > 0)
            {
                shorthand += optimizedDirection('D', 'U', distance.Y, Keyboard.rowCount);
            }
            else if (distance.Y < 0)
            {
                shorthand += optimizedDirection('U', 'D', -distance.Y, Keyboard.rowCount);
            }
            if (distance.X > 0)
            {
                shorthand += optimizedDirection('R', 'L', distance.X, Keyboard.coulmnCount);
            }
            else if (distance.X < 0)
            {
                shorthand += optimizedDirection('L', 'R', -distance.X, Keyboard.coulmnCount);
            }

            if (shorthand == "")
            {
                return "#";
            }
            else
            {
                return String.Join(",", shorthand.ToCharArray()) + ",#"; 
            }


        }

        private string optimizedDirection(char forward, char backward, int distance, int dimensionSize)
        {
            if (distance == 0) return "";

            if (distance <= dimensionSize / 2.00)
            {
                return new string(forward, distance);
            }
            else
            {
                return new string(backward, dimensionSize - distance);
            }
        }




        // aummes something is on the keyborad
        private static Dictionary<char, Position> generateLookUpTable()
        {
            var table = new Dictionary<char, Position>();
            for (int i = 0; i < Keyboard.rowCount; i++)
            {
                for (int j = 0; j < Keyboard.coulmnCount; j++)
                {
                    // posititons are x,y so we use i, j as an index
                    table.Add(Keyboard.KeyLayOut[i][j], new Position(j, i));
                }
            }
            return table;
        }

        private static char toUpperCase(char character)
        {
            return character.ToString().ToUpper()[0];
        }

    }

    

}
