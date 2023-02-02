using System;
namespace OnScreenKeyboard.Application.Services.Keyboard
{
    //The git repo mentioned exandability for keyboards that are not just alpha numberic.
    //I created this interface that would simply populate a dictionary differently with no hard limit checks except for starting at [0][0]
    //You could fill the standard keyboard from a different source, or create new implementations for the keyboard ->
    //Whichever fits the use case better
    public interface IKeyboard
    {
        public Dictionary<int, Dictionary<int, char>> Keyboard { get; set; }
    }
}

