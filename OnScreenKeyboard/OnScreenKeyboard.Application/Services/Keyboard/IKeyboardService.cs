using System;
namespace OnScreenKeyboard.Application.Services.Keyboard
{
    public interface IKeyboard
    {
        public Dictionary<int, Dictionary<int, char>> Keyboard { get; set; }
    }
}

