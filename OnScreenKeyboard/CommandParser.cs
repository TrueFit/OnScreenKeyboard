using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OnScreenKeyboard
{
    public class CommandParser
    {
        public CommandParser()
        {;
            _parsedResult = new StringBuilder();
        }

        private readonly StringBuilder _parsedResult;

        private readonly MasterKey _masterKey = new MasterKey();

        public static string ExceptionMessage = "An unexpected character was found in the input : '{0}'";

        private const string DirectionUp = "U";
        private const string DirectionDown = "D";
        private const string DirectionLeft = "L";
        private const string DirectionRight = "R";
        private const string DirectionSpace = "S";
        private const string DirectionSelect = "#";

        public string ReadInput(ITextReader reader)
        {
            var actualText = reader.GetText().ToUpper(); //convert to uppercase so our text comparisons are case insensitive
            var currentKey = ResetKeyToDefault();

            string[] searches = actualText.Split(new string[] { "\r\n" }, StringSplitOptions.None); //split string on carriage return into multiple searches

            foreach (var search in searches)
            {
                foreach (char c in search)
                {
                    
                    if (c == ' ')
                    {
                        WriteDirection(DirectionSpace);
                    }
                    else
                    {
                        if (_masterKey.Keyboard.Count(x => x.Character == c.ToString()) == 0)
                        {
                            throw new InvalidCharacterException(String.Format(ExceptionMessage, c));
                        }
                   
                        var requestedKey = _masterKey.Keyboard.First(x => x.Character == c.ToString());
                        //need to move down
                        if (requestedKey.Row > currentKey.Row)
                        {
                            for (var i = 0; i <= (requestedKey.Row - currentKey.Row) - 1; i++)
                            {
                                WriteDirection(DirectionDown);
                            }

                        }
                            //need to move up
                        else if (requestedKey.Row < currentKey.Row)
                        {
                            for (var i = 0; i <= (currentKey.Row - requestedKey.Row) - 1; i++)
                            {
                                WriteDirection(DirectionUp);
                            }
                        }

                        if (requestedKey.Value == currentKey.Value)
                        {
                            WriteDirection(DirectionSelect);
                        }
                        else
                        {
                            var currentPosition = _masterKey.GetColumnPosition(currentKey);
                            var requestedPosition = _masterKey.GetColumnPosition(requestedKey);

                            if (currentPosition > requestedPosition)
                            {
                                for (var i = 0; i <= (currentPosition - requestedPosition) - 1; i++)
                                {
                                    WriteDirection(DirectionLeft);
                                }
                            }
                            else
                            {
                                for (var i = 0; i <= (requestedPosition - currentPosition) - 1; i++)
                                {
                                    WriteDirection(DirectionRight);
                                }
                            }
                            WriteDirection(DirectionSelect);
                        }
                        currentKey = requestedKey;
                    }
                }
            }
            return _parsedResult.ToString();
        }

        private Key ResetKeyToDefault()
        {
            return _masterKey.Keyboard.First(x => x.Character == "A");
        }


        private void WriteDirection(string direction)
        {
            if (_parsedResult.ToString() != String.Empty)
            {
                _parsedResult.Append(",");
            }

            _parsedResult.Append(direction);
        }

    }
}