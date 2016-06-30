using Machine.Specifications;
using OnScreenKeyboard;

namespace Specs.Extensions
{
    [Subject(typeof(OnScreenKeyboard.Extensions))]
    public class When_getting_position_of_0
    {
        Because of = () => _result = '0'.GetKeyboardIndex();

        It should_return_correct_position = () => _result.ShouldEqual(35);

        static int _result;
    }
}
