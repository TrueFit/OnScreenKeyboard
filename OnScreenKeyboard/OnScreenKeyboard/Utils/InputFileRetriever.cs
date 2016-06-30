using System;
using SimulatableApi.StreamStore;

namespace OnScreenKeyboard.Utils
{
    public interface IRetrieveInputFiles
    {
        string Get(string path);
    }

    public class InputFileRetriever : IRetrieveInputFiles
    {
        readonly FileSystem _fs;

        public InputFileRetriever(FileSystem fs)
        {
            _fs = fs;
        }

        public string Get(string path)
        {
            if(!_fs.File(path).Exists)
                throw new Exception("You gotta provide a legit file uri, bro.");

            return _fs.File(path).ReadAllText();
        }
    }
}