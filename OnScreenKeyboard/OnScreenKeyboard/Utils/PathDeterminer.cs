using System.Collections.Generic;
using OnScreenKeyboard.Enums;

namespace OnScreenKeyboard.Utils
{
    public interface IDetermineKeyboardNavigationPaths
    {
        List<Navigation> Do(string input);
    }

    public class PathDeterminer : IDetermineKeyboardNavigationPaths
    {
        readonly IAmTheConfiguration _config;
        public KeyboardPosition _current = new KeyboardPosition(0, 0);

        public PathDeterminer(IAmTheConfiguration config)
        {
            _config = config;
        }

        public List<Navigation> Do(string input)
        {
            input = input.ToUpper();

            var list = new List<Navigation>();

            foreach(var character in input)
            {
                if(character == ' ')
                {
                    list.Add(Navigation.Space);
                    continue;
                }

                var absolutePositionOfCharacter = KeyboardPosition.FromIndex(character.GetKeyboardIndex(), _config.CharactersPerLine);
                var vector = absolutePositionOfCharacter - _current;

                _current = absolutePositionOfCharacter;

                list.AddRange(vector.GetPath());
            }

            return list;
        }
    }
}
