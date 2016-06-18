using System;
using System.Collections.Generic;
using System.Linq;
using Truefit.OnScreenKeyboard.Models;

namespace Truefit.OnScreenKeyboard.Controls
{
    public class OKCommand : IRemoteCommand
    {
        private OverlayKeyboard _kb;

        public OKCommand(OverlayKeyboard kb)
        {
            _kb = kb;
        }

        public void Execute(object info)
        {
            _kb.DoSelect();
        }
    }
}
