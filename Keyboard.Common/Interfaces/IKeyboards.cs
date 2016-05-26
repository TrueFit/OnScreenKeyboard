using Keyboard.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyboard.Common.Interfaces
{
    public interface IKeyboards<T>
    {
        T GetKeyboard(KeyboardType type);
    }
}
