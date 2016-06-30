using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnScreenKeyboard.Bootstrapping;

namespace OnScreenKeyboard
{
    public interface IAmTheConfiguration : ISingleton
    {
        int CharactersPerLine { get; set; }
    }

    public class Configuration : IAmTheConfiguration
    {
        public int CharactersPerLine { get; set; }
    }
}
