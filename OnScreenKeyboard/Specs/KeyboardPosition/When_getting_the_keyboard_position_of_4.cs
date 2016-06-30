using Machine.Specifications;
using OnScreenKeyboard;

namespace Specs.KeyboardPosition
{
    [Subject(typeof(OnScreenKeyboard.Enums.KeyboardPosition))]
    public class When_getting_the_keyboard_position_of_4 : With<OnScreenKeyboard.Enums.KeyboardPosition>
    {
        Because of = () => _result = OnScreenKeyboard.Enums.KeyboardPosition.FromIndex('4'.GetKeyboardIndex(), 6);

        It should_give_correct_position = () =>
        {
            _result.X.ShouldEqual(5);
            _result.Y.ShouldEqual(4);
        };


        static OnScreenKeyboard.Enums.KeyboardPosition _result;
    }
}