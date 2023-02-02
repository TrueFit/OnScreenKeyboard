using System;
namespace OnScreenKeyboard.Application.Services.Keyboard
{
    public interface IKeyboardService
    {
        public Dictionary<int, Dictionary<int, char>> Keyboard { get; set; }
    }
}

