using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using NSubstitute;
using OnScreenKeyboard;
using OnScreenKeyboard.Enums;

namespace Specs.PathDeterminer
{
    [Subject(typeof(OnScreenKeyboard.Utils.PathDeterminer))]
    public class When_getting_it_crowd : With<OnScreenKeyboard.Utils.PathDeterminer>
    {
        Establish context = () => For<IAmTheConfiguration>().CharactersPerLine.Returns(6);

        Because of = () => _result = Subject.Do("IT Crowd").ToList();

        It should_give_correct_directions = () => _result.ShouldEqual(new List<Navigation>
                                                                      {
                                                                          Navigation.Down, Navigation.Right, Navigation.Right, Navigation.Select,
                                                                          Navigation.Down, Navigation.Down, Navigation.Left, Navigation.Select,
                                                                          Navigation.Space,
                                                                          Navigation.Up, Navigation.Up, Navigation.Up, Navigation.Right, Navigation.Select,
                                                                          Navigation.Down, Navigation.Down, Navigation.Right, Navigation.Right, Navigation.Right, Navigation.Select,
                                                                          Navigation.Left, Navigation.Left, Navigation.Left, Navigation.Select,
                                                                          Navigation.Down, Navigation.Right, Navigation.Right, Navigation.Select,
                                                                          Navigation.Up, Navigation.Up, Navigation.Up, Navigation.Left, Navigation.Select
                                                                      });

        static List<Navigation> _result;
    }
}
