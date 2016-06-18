using System;
using System.Collections.Generic;
using System.Linq;
using Truefit.OnScreenKeyboard.Models;

namespace Truefit.OnScreenKeyboard.Controls
{    
    /// <summary>
     /// A remote control with OK, Up, Down, Left, Right buttons
     /// and Voice to Text
     /// </summary>
    public class ArrowPadRemoteControlWithVTT : ArrowPadRemoteControl
    {
        private readonly VoiceToTextCommand _voiceToTextCommand;

        public ArrowPadRemoteControlWithVTT(OverlayKeyboard kb)
            : base(kb)
        {
            _voiceToTextCommand = new VoiceToTextCommand(kb);
        }

        public void DoVoiceToText(string voice)
        {
            _voiceToTextCommand.Execute(voice);
        }
    }
}
