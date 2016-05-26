namespace Keyboard.Common.Interfaces
{
    public interface IDataExecutor
    {
        TReturn ExecuteQuery<TReturn>(IDataQuery<TReturn> query);
    }
}
