using System;
using System.Collections.Generic;
using System.Linq;
using Truefit.OnScreenKeyboard.Models;

namespace Truefit.OnScreenKeyboard.Controls
{
    /// <summary>
    /// A remote control with OK, Up, Down, Left, Right buttons
    /// </summary>
    public class ArrowPadRemoteControl : DvrRemoteControl
    {
        private readonly ArrowPadCommand _arrowPadCommand;

        public ArrowPadRemoteControl(OverlayKeyboard kb)
            : base(kb)
        {
            _arrowPadCommand = new ArrowPadCommand(kb);
        }

        public void DoUpArrow()
        {
            _arrowPadCommand.Execute(ArrowPadInput.Up);
        }

        public void DoDownArrow()
        {
            _arrowPadCommand.Execute(ArrowPadInput.Down);
        }

        public void DoRightArrow()
        {
            _arrowPadCommand.Execute(ArrowPadInput.Right);
        }

        public void DoLeftArrow()
        {
            _arrowPadCommand.Execute(ArrowPadInput.Left);
        }
    }
}
