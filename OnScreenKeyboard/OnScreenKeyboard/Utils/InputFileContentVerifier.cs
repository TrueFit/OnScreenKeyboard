using System;
using System.Text.RegularExpressions;

namespace OnScreenKeyboard.Utils
{
    public interface IVerifyInputFileContent
    {
        void Verify(string contents);
    }

    public class InputFileContentVerifier : IVerifyInputFileContent
    {
        public void Verify(string contents)
        {
            if (!(new Regex("^[a-zA-Z0-9 ]*$")).IsMatch(contents)) throw new Exception("The input file can only contain alphanumerics and spaces.");
        }
    }
}
