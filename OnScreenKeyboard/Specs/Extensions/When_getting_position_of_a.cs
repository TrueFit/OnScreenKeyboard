using Machine.Specifications;
using OnScreenKeyboard;

namespace Specs.Extensions
{
    [Subject(typeof(OnScreenKeyboard.Extensions))]
    public class When_getting_position_of_a
    {
        Because of = () => _result = 'A'.GetKeyboardIndex();

        It should_return_correct_position = () => _result.ShouldEqual(0);

        static int _result;
    }
}