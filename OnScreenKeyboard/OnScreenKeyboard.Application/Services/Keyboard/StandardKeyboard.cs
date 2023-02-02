using System;

namespace OnScreenKeyboard.Application.Services.Keyboard
{
    public class StandardKeyboard : IKeyboardService
    {
        private Dictionary<int, Dictionary<int, char>> _keyboard;

        public Dictionary<int, Dictionary<int, char>> Keyboard
        {
            get
            {
                if (_keyboard is null)
                {
                    LoadKeyboard();
                }
                return _keyboard;
            }
            set
            {
                _keyboard = value;
            }
        }

        private void LoadKeyboard()
        {
            Keyboard = new Dictionary<int, Dictionary<int, char>>();

            for (int i = 0; i < 6; i++)
            {
                Keyboard[i] = new Dictionary<int, char>();
            }
            Keyboard[0].Add(0, 'A');
            Keyboard[0].Add(1, 'B');
            Keyboard[0].Add(2, 'C');
            Keyboard[0].Add(3, 'D');
            Keyboard[0].Add(4, 'E');
            Keyboard[0].Add(5, 'F');

            Keyboard[1].Add(0, 'G');
            Keyboard[1].Add(1, 'H');
            Keyboard[1].Add(2, 'I');
            Keyboard[1].Add(3, 'J');
            Keyboard[1].Add(4, 'K');
            Keyboard[1].Add(5, 'L');

            Keyboard[2].Add(0, 'M');
            Keyboard[2].Add(1, 'N');
            Keyboard[2].Add(2, 'O');
            Keyboard[2].Add(3, 'P');
            Keyboard[2].Add(4, 'Q');
            Keyboard[2].Add(5, 'R');

            Keyboard[3].Add(0, 'S');
            Keyboard[3].Add(1, 'T');
            Keyboard[3].Add(2, 'U');
            Keyboard[3].Add(3, 'V');
            Keyboard[3].Add(4, 'W');
            Keyboard[3].Add(5, 'X');

            Keyboard[4].Add(0, 'Y');
            Keyboard[4].Add(1, 'Z');
            Keyboard[4].Add(2, '1');
            Keyboard[4].Add(3, '2');
            Keyboard[4].Add(4, '3');
            Keyboard[4].Add(5, '4');

            Keyboard[5].Add(0, '5');
            Keyboard[5].Add(1, '6');
            Keyboard[5].Add(2, '7');
            Keyboard[5].Add(3, '8');
            Keyboard[5].Add(4, '9');
            Keyboard[5].Add(5, '0');
        }
    }
}

