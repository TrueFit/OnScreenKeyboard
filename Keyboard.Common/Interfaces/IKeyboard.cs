using System.Collections.Generic;

namespace Keyboard.Common.Interfaces
{
    public interface IKeyboard<T>
    {
        string Type { get; set; }

        ICollection<T> Keys { get; }
    }
}
