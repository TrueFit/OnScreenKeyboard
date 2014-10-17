namespace OnScreenKeyboard
{
    public class Key
    {
        public Key(byte row,string character,byte value)
        {
            Row = row;
            Character = character;
            Value = value;
        }

        public byte Row { get; private set; }
        public string Character { get; private set; }
        public byte Value { get; private set; }
    }
}
