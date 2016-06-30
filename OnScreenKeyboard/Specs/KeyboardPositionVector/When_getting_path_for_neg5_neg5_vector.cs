using System.Collections.Generic;
using Machine.Specifications;
using OnScreenKeyboard;
using OnScreenKeyboard.Enums;

namespace Specs.KeyboardPositionVector
{
    [Subject(typeof(OnScreenKeyboard.Enums.KeyboardPositionVector))]
    public class When_getting_path_for_neg5_neg5_vector
    {
        Because of = () => _result = OnScreenKeyboard.Enums.KeyboardPositionVector.From(-5, -5).GetPath();

        It should_get_correct_path = () => _result.ShouldEqual(new List<Navigation> { Navigation.Left, Navigation.Left, Navigation.Left, Navigation.Left, Navigation.Left, Navigation.Up, Navigation.Up, Navigation.Up, Navigation.Up, Navigation.Up, Navigation.Select });

        static List<Navigation> _result;
    }
}