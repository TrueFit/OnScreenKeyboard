using System;
using System.Collections.Generic;
using System.Text;
using Simple.Library.Misc;
using System.Text.RegularExpressions;

namespace Simple.Library.Keyboard
{
    /// <summary>
    /// Truefit Keyboard representatiobn
    /// </summary>
    public class TrueFitKeyboard :  IKeyboard
    {
        private Dictionary<char, SymbolLocation> KeyboardRepresentation;
        //Alternate representation to be used for external applications requestion keyboard layout
        private List<SymbolRepresentation> AlternateKeyboardRepresentation;
        private const int KeyboardColumnCount = 6;
        private int KeyboardRowCount;
        private char[] alpha;
        /// <summary>
        /// Keyboard constructor to initialize virtual keyboard in memory. 
        /// Characters are stored in a dictionary as the key and their corresponding
        /// coordinates are the value
        /// </summary>
        public TrueFitKeyboard()
        {
            alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            KeyboardRepresentation = new Dictionary<char, SymbolLocation>();
            AlternateKeyboardRepresentation = new List<SymbolRepresentation>();
            KeyboardRowCount = alpha.Length / KeyboardColumnCount;
            for (int x = 0; x < alpha.Length; x++)
            {
                SymbolLocation location = new SymbolLocation { XCoord = x % KeyboardColumnCount, YCoord = (x == 0 ? 0 : Convert.ToInt32(Math.Floor(Convert.ToDecimal(x) / KeyboardColumnCount))) };
                KeyboardRepresentation.Add(alpha[x], location);
                AlternateKeyboardRepresentation.Add(new SymbolRepresentation { Character = alpha[x].ToString(), Location = location });
            }
        }
        /// <summary>
        /// Return location object of symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public SymbolLocation ReturnSymbolLocation(char symbol)
        {
            return KeyboardRepresentation.ContainsKey(symbol) ? KeyboardRepresentation[symbol] : new SymbolLocation { XCoord = -1, YCoord = -1 };
        }
        /// <summary>
        /// Print keyboard display in console
        /// </summary>
        public string ReturnKeyboardDisplay()
        {
            StringBuilder rep = new StringBuilder();
            rep.Append("\tTrueFit Keyboard\n");
            rep.Append("\t----------------\n");
            for(int x = 0; x < alpha.Length; x++)
            {
                if((x + 1) % KeyboardColumnCount == 0)
                {
                    rep.Append(alpha[x] + "\n");
                }
                else
                {
                    rep.Append(alpha[x]);
                }
            }
           rep.Append("\t----------------\n");
            return rep.ToString();
        }
        /// <summary>
        /// Return keyboard type for diagnostic and logging purposes
        /// </summary>
        /// <returns></returns>
        public string ReturnKeyboardType()
        {
            return "TrueFit Keyboard";
        }
        /// <summary>
        /// Remove unavailable characters from search term and return
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public string FormatSearchTerm(string term)
        {
            Regex rgx = new Regex("[^A-Z0-9 -]");
            return rgx.Replace(term.ToUpper(), "");
        }
        /// <summary>
        /// Returns alternated representation to be used for display in external applications. Primarily for web/mobile.
        /// </summary>
        /// <returns></returns>
        public List<SymbolRepresentation> ReturnWebKeyboard()
        {
            return AlternateKeyboardRepresentation;
        }
        /// <summary>
        /// Returns # of columns
        /// </summary>
        /// <returns></returns>
        public int ReturnKeyboardColumnCount()
        {
            return KeyboardColumnCount;
        }
        /// <summary>
        /// Returns # of rows
        /// </summary>
        /// <returns></returns>
        public int ReturnKeyboardRowCount()
        {
            return KeyboardRowCount;
        }
    }
}
