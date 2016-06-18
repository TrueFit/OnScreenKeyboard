using System;
using System.Collections.Generic;
using System.Linq;

namespace Truefit.OnScreenKeyboard.Controls
{
    public interface IRemoteCommand
    {
        void Execute(object info);
    }
}
