using System;
using Machine.Specifications;

namespace Specs.InputFileContentVerifier
{
    [Subject(typeof(OnScreenKeyboard.Utils.InputFileContentVerifier))]
    public class When_file_is_invalid : With<OnScreenKeyboard.Utils.InputFileContentVerifier>
    {
        Because of = () => _exception  = Catch.Exception(() => Subject.Verify("ab++d"));

        It should_not_throw_an_exception = () => _exception.ShouldNotBeNull();
        static Exception _exception;
    }
}