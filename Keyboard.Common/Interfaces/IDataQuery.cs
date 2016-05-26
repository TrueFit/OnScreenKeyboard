namespace Keyboard.Common.Interfaces
{
    public interface IDataQuery<out TReturn>
    {
        TReturn Execute();
    }
}
