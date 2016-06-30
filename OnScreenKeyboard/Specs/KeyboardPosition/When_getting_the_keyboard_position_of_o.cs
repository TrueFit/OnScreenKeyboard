using Machine.Specifications;
using OnScreenKeyboard;

namespace Specs.KeyboardPosition
{
    [Subject(typeof(OnScreenKeyboard.Enums.KeyboardPosition))]
    public class When_getting_the_keyboard_position_of_o : With<OnScreenKeyboard.Enums.KeyboardPosition>
    {
        Because of = () => _result = OnScreenKeyboard.Enums.KeyboardPosition.FromIndex('O'.GetKeyboardIndex(), 6);

        It should_give_correct_position = () =>
        {
            _result.X.ShouldEqual(2);
            _result.Y.ShouldEqual(2);
        };


        static OnScreenKeyboard.Enums.KeyboardPosition _result;
    }
}
