using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Simple.Library.Misc;

namespace SimpleWeb.Models
{
    // Models returned by MeController actions.
    public class KeyboardLayoutModel
    {
        public string Information { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfColumns { get; set; }
        public List<SymbolRepresentation> KeyboardCharacters { get; set; }
    }
}