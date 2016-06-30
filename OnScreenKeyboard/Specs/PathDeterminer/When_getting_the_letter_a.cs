using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using NSubstitute;
using OnScreenKeyboard;
using OnScreenKeyboard.Enums;

namespace Specs.PathDeterminer
{
    [Subject(typeof(OnScreenKeyboard.Utils.PathDeterminer))]
    public class When_getting_the_letter_a : With<OnScreenKeyboard.Utils.PathDeterminer>
    {
        Establish context = () => For<IAmTheConfiguration>().CharactersPerLine.Returns(6);

        Because of = () => _result = Subject.Do("a").ToList();

        It should_give_correct_directions = () => _result.ShouldEqual(new List<Navigation> { Navigation.Select });

        static List<Navigation> _result;
    }
}