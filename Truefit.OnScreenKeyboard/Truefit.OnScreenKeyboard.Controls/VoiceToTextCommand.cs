using System;
using System.Collections.Generic;
using System.Linq;
using Truefit.OnScreenKeyboard.Models;

namespace Truefit.OnScreenKeyboard.Controls
{
    public class VoiceToTextCommand : IRemoteCommand
    {
        private OverlayKeyboard _kb;
        private Dictionary<String, GridPosition> _cachedVKeyGridPositions = new Dictionary<string, GridPosition>();

        public VoiceToTextCommand(OverlayKeyboard kb)
        {
            _kb = kb;
        }

        private GridPosition? GetVKeyGridPosition(string vkeyToFind)
        {
            if(_cachedVKeyGridPositions.ContainsKey(vkeyToFind)) return _cachedVKeyGridPositions[vkeyToFind];

            for(int currentRow = 0; currentRow < _kb.PGrid.Rows; currentRow++)
            {
                for(int currentCol = 0; currentCol < _kb.PGrid.Columns; currentCol++)
                {
                    if(String.Equals(vkeyToFind, _kb.VGrid.VKeys[currentRow, currentCol], StringComparison.InvariantCultureIgnoreCase))
                    {
                        GridPosition found = new GridPosition(currentRow, currentCol);
                        _cachedVKeyGridPositions.Add(vkeyToFind, found);
                        return found;
                    }
                }
            }

            return null;
        }

        public void Execute(object info)
        {
            string voiceCommand = (info as string);
            if(String.IsNullOrWhiteSpace(voiceCommand)) throw new ArgumentException("VoiceCommand must receive a non-null, non-whitespace, non-empty String", "info");

            voiceCommand = voiceCommand.Trim(); // no leading or trailing whitespace

            // TODO: scanning string for >1 character phrases, like "Backspace", "Shift", etc, could be done here
            // TODO: ideally, there would be virtual key codes instead of strings/characters

            // process as a group of single character strings
            for(int i = 0; i < voiceCommand.Length; i++)
            {
                string current = voiceCommand.Substring(i, 1);

                GridPosition? newPosition = GetVKeyGridPosition(current);
                if(!newPosition.HasValue && current[0] == ' ') // not found, but the character is a space
                {
                    _kb.DoSpace(); // designed for space support in commands where keyboard does not have space char

                }
                else if(!newPosition.HasValue) // not found and not a space
                {
                    throw new KeyNotFoundException(string.Format("The VKey '{0}' was not found in the keyboard", current));
                }
                else // found, possibly a space (if keyboards have it explictly mapped), otherwise just regular input
                {
                    _kb.CurrentGridPosition = newPosition.Value;
                    _kb.DoSelect();
                }
            }
        }
    }
}
