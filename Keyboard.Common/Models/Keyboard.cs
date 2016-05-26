using Keyboard.Common.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;

namespace Keyboard.Common.Models
{
    [DebuggerDisplay("Type: {Type} Keys: {Keys.Count}")]
    public class Keyboard : IKeyboard<Key>
    {
        public ICollection<Key> Keys { get; set; }

        public string Type { get; set; }
    }
}
