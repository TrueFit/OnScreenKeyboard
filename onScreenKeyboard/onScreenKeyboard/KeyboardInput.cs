using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onScreenKeyboard
{
    class KeyboardInput
    {
        public static KeyboardInput UP = new KeyboardInput("UP", keyboard => keyboard.moveCursor(new Position(0, 1)));
        public static KeyboardInput DOWN = new KeyboardInput("DOWN", keyboard => keyboard.moveCursor(new Position(0, -1)));
        public static KeyboardInput LEFT = new KeyboardInput("LEFT", keyboard => keyboard.moveCursor(new Position(-1, 0)));
        public static KeyboardInput RIGHT = new KeyboardInput("RIGHT", keyboard => keyboard.moveCursor(new Position(1, 0)));
        public static KeyboardInput SPACE = new KeyboardInput("SPACE", keyboard => keyboard.addSpace());
        public static KeyboardInput SELECT = new KeyboardInput("SELECT", keyboard => keyboard.addKeyAtCursor());


        private string name;
        private Action<Keyboard> keyboardAction;

        public KeyboardInput(string name, Action<Keyboard> keyboardAction)
            {
                this.name = name;
            this.keyboardAction = keyboardAction;

            }

        public void Trigger(Keyboard keyboard)
        {
            keyboardAction(keyboard);
        }
    }
}
