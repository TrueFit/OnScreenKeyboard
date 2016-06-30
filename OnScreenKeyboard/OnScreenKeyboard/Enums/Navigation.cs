namespace OnScreenKeyboard.Enums
{
    public class Navigation
    {
        readonly string _value;

        private Navigation(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }

        public static Navigation Up = new Navigation("U");
        public static Navigation Down = new Navigation("D");
        public static Navigation Left = new Navigation("L");
        public static Navigation Right = new Navigation("R");
        public static Navigation Space = new Navigation("S");
        public static Navigation Select = new Navigation("#");

    }
}