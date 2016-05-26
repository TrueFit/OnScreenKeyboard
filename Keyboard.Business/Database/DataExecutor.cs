using Keyboard.Common.Interfaces;

namespace Keyboard.Business.Database
{

    public class DataExecutor : IDataExecutor
    {
        public TReturn ExecuteQuery<TReturn>(IDataQuery<TReturn> query)
        {
            return query.Execute();
        }
        
    }
}
