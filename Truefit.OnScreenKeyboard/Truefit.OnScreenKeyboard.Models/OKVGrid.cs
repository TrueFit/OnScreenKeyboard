using System;
using System.Collections.Generic;
using System.Linq;

namespace Truefit.OnScreenKeyboard.Models
{
    /// <summary>
    /// The overlay keyboard virtual grid allows for defining the virtual keys that exist
    /// on top of a set of physical keys
    /// </summary>
    public struct OKVGrid
    {
        public string[,] VKeys;

        public OKVGrid(string[,] vkeys)
        {
            this.VKeys = vkeys;
        }
    }
}
