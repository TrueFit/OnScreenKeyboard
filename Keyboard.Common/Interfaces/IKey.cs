namespace Keyboard.Common.Interfaces
{
    public interface IKey
    {
        int X { get; set; }

        int Y { get; set; }

        char Value { get; set; }
    }
}
