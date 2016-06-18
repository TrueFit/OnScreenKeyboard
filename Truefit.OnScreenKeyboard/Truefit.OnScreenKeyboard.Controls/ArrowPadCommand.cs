using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Truefit.OnScreenKeyboard.Models;

namespace Truefit.OnScreenKeyboard.Controls
{
    public class ArrowPadCommand : IRemoteCommand
    {
        private OverlayKeyboard _kb;

        public ArrowPadCommand(OverlayKeyboard kb)
        {
            _kb = kb;
        }

        public void Execute(object info)
        {
            if(!(info is ArrowPadInput)) throw new ArgumentException("DirectionCommand must receive Direction", "info");
            ArrowPadInput input = (ArrowPadInput)info;

            switch(input)
            {
                case ArrowPadInput.Up:
                    _kb.CurrentGridPosition = _kb.GetNextGridPositionUp(_kb.CurrentGridPosition);
                    break;
                case ArrowPadInput.Down:
                    _kb.CurrentGridPosition = _kb.GetNextGridPositionDown(_kb.CurrentGridPosition);
                    break;
                case ArrowPadInput.Left:
                    _kb.CurrentGridPosition = _kb.GetNextGridPositionLeft(_kb.CurrentGridPosition);
                    break;
                case ArrowPadInput.Right:
                    _kb.CurrentGridPosition = _kb.GetNextGridPositionRight(_kb.CurrentGridPosition);
                    break;
            }
        }
    }

}
