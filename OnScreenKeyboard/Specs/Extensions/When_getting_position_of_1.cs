using Machine.Specifications;
using OnScreenKeyboard;

namespace Specs.Extensions
{
    [Subject(typeof(OnScreenKeyboard.Extensions))]
    public class When_getting_position_of_1
    {
        Because of = () => _result = '1'.GetKeyboardIndex();

        It should_return_correct_position = () => _result.ShouldEqual(26);

        static int _result;
    }
}