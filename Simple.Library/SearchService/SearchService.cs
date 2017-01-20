using System;
using System.Collections.Generic;
using System.Text;
using Simple.Library.Keyboard;
using Simple.Library.Misc;
using Simple.Library.Logging;

namespace Simple.Library.SearchService
{
    /// <summary>
    /// Search Service is a component of the Voice to Text search algorithm that maps a search term to it's given path based on keyboard implementation
    /// </summary>
    public class SearchService
    {
        private IKeyboard KeyboardImplementation { get; set; }
        private ILogger Logger { get; set; }
        private bool SupressLog { get; set; }
        private Dictionary<string, string> PreviousSearches = new Dictionary<string, string>();

        public SearchService(IKeyboard custom, bool _suppressLog)
        {
            KeyboardImplementation = custom;
            SupressLog = _suppressLog;
        }
        /// <summary>
        /// Search Service constructor. Default implementation of keyboard is TrueFitKeyboard;
        /// </summary>
        /// <param name="_logger"></param>
        public SearchService(ILogger _logger)
        {
            IKeyboard truefit = new TrueFitKeyboard();
            KeyboardImplementation = truefit;
            Logger = _logger;
            Logger.Log("Search Service Constructor", "Search Service initialized with TrueFitKeyboard");
            SupressLog = false;
        }
        /// <summary>
        /// Search Service constructor
        /// </summary>
        /// <param name="custom"></param>
        /// <param name="_logger"></param>
        public SearchService(IKeyboard custom, ILogger _logger)
        {
            KeyboardImplementation = custom;
            Logger = _logger;
            Logger.Log("Search service Constructor", "Search service initialized wtih " + custom.ReturnKeyboardType());
            SupressLog = false;
        }
        /// <summary>
        /// Returns the search path for a a given term based on keyboard implementation.
        /// Empty path is returned for errors.
        /// </summary>
        /// <param name="term"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public string ReturnSearchPath(string term, string delimeter)
        {
            try
            {
                if(!SupressLog) Logger.Log("Return Search Path", "Search path request term: " + term);

                //Format string based on keyboard implementation. Keyboards may differ in size and # of distinct characters, thus stripping all characters not found on keyboard
                //implementation is needed.
                term = KeyboardImplementation.FormatSearchTerm(term);

                //If term is empty, return empty path.
                if (string.IsNullOrEmpty(term) || term.Length < 1)
                {
                    return string.Empty;
                }
                
                //Cache previous searches. If it exists, return cached path.                 
                if (PreviousSearches.ContainsKey(term))
                {
                    return PreviousSearches[term];
                }
                else
                {
                    StringBuilder path = new StringBuilder();
                    //Start in top right corner of keyboard
                    SymbolLocation current = new SymbolLocation { XCoord = 0, YCoord = 0 };
                    for (int x = 0; x < term.Length; x++)
                    {
                        if (char.IsWhiteSpace(term[x]))
                        {
                            path.Append("S" + (x != term.Length - 1 ? "," : ""));
                            continue;
                        }
                        //Return current character location.
                        SymbolLocation destination = KeyboardImplementation.ReturnSymbolLocation(term[x]);

                        //While vertical position is not equal to destination position, add delta to current coordinates to move closer. 
                        //Repeat for horizontal position until final position is met.
                        while (current.YCoord != destination.YCoord)
                        {
                            SearchMove next = MoveCursor(current.YCoord, destination.YCoord, false);
                            current.YCoord += next.Delta;
                            path.Append(next.Direction + delimeter);
                        }
                        while (current.XCoord != destination.XCoord)
                        {
                            SearchMove next = MoveCursor(current.XCoord, destination.XCoord, true);
                            current.XCoord += next.Delta;
                            path.Append(next.Direction + delimeter);
                        }

                        path.Append("#" + (x != term.Length - 1 ? "," : ""));
                    }
                    //Cache calculated path
                    PreviousSearches[term] = path.ToString();
                    return path.ToString();
                }
            }
            catch (Exception ex)
            {
                //Log exception and return empty final path
                if (!SupressLog) Logger.LogError("Return Search Path", "Error generating search path", ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// Move Cursor function return direction and delta change of start and end points. 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="horizontalCoordinates"></param>
        /// <returns></returns>
        private SearchMove MoveCursor(int start, int end, bool horizontalCoordinates)
        {
            if(horizontalCoordinates)
            {
                if (start < end)
                {
                    return new SearchMove { Direction = "R", Delta = 1 };
                }
                else
                {
                    return new SearchMove { Direction = "L", Delta = -1 };
                }
            }
            else
            {
                if (start < end)
                {
                    return new SearchMove { Direction = "D", Delta = 1 };
                }
                else
                {
                    return new SearchMove { Direction = "U", Delta = -1 };
                }
            }
        }
        
        public string ReturnKeyboardInformation()
        {
            return KeyboardImplementation.ReturnKeyboardType();
        } 

        public string ReturnKeyboardRepresentation()
        {
            return KeyboardImplementation.ReturnKeyboardDisplay();
        }
    }
}
