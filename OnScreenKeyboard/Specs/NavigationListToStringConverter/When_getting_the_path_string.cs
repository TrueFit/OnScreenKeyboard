using System.Collections.Generic;
using Machine.Specifications;
using OnScreenKeyboard;
using OnScreenKeyboard.Enums;

namespace Specs.NavigationListToStringConverter
{
    [Subject(typeof(OnScreenKeyboard.Utils.NavigationListToStringConverter))]
    public class When_getting_the_path_string : With<OnScreenKeyboard.Utils.NavigationListToStringConverter>
    {
        Because of = () => _result = Subject.Convert(new List<Navigation> {Navigation.Right, Navigation.Left, Navigation.Up, Navigation.Down, Navigation.Select, Navigation.Space});

        It should_return_correct_position = () => _result.ShouldEqual("R,L,U,D,#,S");
        static string _result;
    }
}