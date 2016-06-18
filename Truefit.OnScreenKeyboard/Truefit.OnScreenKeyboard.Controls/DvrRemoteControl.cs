using System;
using System.Collections.Generic;
using System.Linq;

using Truefit.OnScreenKeyboard.Models;

namespace Truefit.OnScreenKeyboard.Controls
{
    /// <summary>
    /// The base remote control capable of just an OK
    /// </summary>
    public abstract class DvrRemoteControl
    {
        private readonly OKCommand _okCommand;

        public DvrRemoteControl(OverlayKeyboard kb)
        {
            _okCommand = new OKCommand(kb);
        }

        public void DoOK()
        {
            _okCommand.Execute(null);
        }
    }
}
