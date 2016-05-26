using Keyboard.Common.Interfaces;
using System.Diagnostics;

namespace Keyboard.Common.Models
{
    [DebuggerDisplay("{Value} - {X},{Y}")]
    public class Key : IKey
    {
        public char Value { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
