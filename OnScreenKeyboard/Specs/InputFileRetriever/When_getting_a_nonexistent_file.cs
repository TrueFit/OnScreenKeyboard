using System;
using Machine.Specifications;
using SimulatableApi.StreamStore;

namespace Specs.InputFileRetriever
{
    [Subject(typeof(OnScreenKeyboard.Utils.InputFileRetriever))]
    public class When_getting_a_nonexistent_file : With<OnScreenKeyboard.Utils.InputFileRetriever>
    {
        Establish context = () => Mocks.Inject<FileSystem>(FileSystem.Simulated());

        Because of = () => _exception = Catch.Exception(() => Subject.Get(@"c:\nonexistant.file"));

        It should_throw_exception = () => _exception.ShouldNotBeNull();

        static Exception _exception; 
    }
}