using Simple.Library.Misc;
using System.Collections.Generic;

namespace Simple.Library.Keyboard
{
    /// <summary>
    /// Interface for Voice to Text keyboard implementations
    /// </summary>
    public interface IKeyboard
    {
        /// <summary>
        /// Based on a given character, this function should return the X,Y coordinates of it's position within keyboard. If character is not found, function should return -1, -1 for coordinates
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        SymbolLocation ReturnSymbolLocation(char symbol);
        /// <summary>
        /// For console representations of keyboard. 
        /// </summary>
        string ReturnKeyboardDisplay();
        /// <summary>
        /// Used for diagnostic and logging purposes
        /// </summary>
        /// <returns></returns>
        string ReturnKeyboardType();
        /// <summary>
        /// Function should return formmated term based on availabled characters found in provided term. Characters that are not found in keyboard, or cannot be translated (i.e. lower case to upper case) must 
        /// by stripped from term string.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string FormatSearchTerm(string term);
        /// <summary>
        /// Return web version of keyboard
        /// </summary>
        /// <returns></returns>
        List<SymbolRepresentation> ReturnWebKeyboard();
        /// <summary>
        /// Should return number of keyboard columns
        /// </summary>
        /// <returns></returns>
        int ReturnKeyboardColumnCount();
        /// <summary>
        /// Should return number of keyboard rows
        /// </summary>
        /// <returns></returns>
        int ReturnKeyboardRowCount();
    }
}
