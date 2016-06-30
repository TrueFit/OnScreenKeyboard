using Machine.Specifications;

namespace Specs.InputFileContentVerifier
{
    [Subject(typeof(OnScreenKeyboard.Utils.InputFileContentVerifier))]
    public class When_file_is_valid : With<OnScreenKeyboard.Utils.InputFileContentVerifier>
    {
        Because of = () => Subject.Verify("abcDEFksjdjk jkasjdkljsadklj");

        It should_not_throw_an_exception = () => true.ShouldBeTrue();

    }
}
