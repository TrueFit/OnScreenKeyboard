using System;
using OnScreenKeyboard.Utils;

namespace OnScreenKeyboard
{
    public class Orchestrator
    {
        readonly IRetrieveInputFiles _inputFileRetriever;
        readonly IDetermineKeyboardNavigationPaths _pathNavigationDeterminer;
        readonly IVerifyInputFileContent _inputFileVerifier;
        readonly IConvertNavigationListsToStrings _navListToStringConverter;

        public Orchestrator(IAmTheConfiguration config, 
            IRetrieveInputFiles inputFileRetriever, 
            IDetermineKeyboardNavigationPaths pathNavigationDeterminer, 
            IVerifyInputFileContent inputFileVerifier, 
            IConvertNavigationListsToStrings navListToStringConverter)
        {
            _inputFileRetriever = inputFileRetriever;
            _pathNavigationDeterminer = pathNavigationDeterminer;
            _inputFileVerifier = inputFileVerifier;
            _navListToStringConverter = navListToStringConverter;
            config.CharactersPerLine = 6; //let's just make this the default for now
        }

        public void Do(string path)
        {
            var contents = _inputFileRetriever.Get(path);
            _inputFileVerifier.Verify(contents);
            var navigation = _pathNavigationDeterminer.Do(contents);
            var output = _navListToStringConverter.Convert(navigation);

            Console.WriteLine(output);
        }
    }
}
