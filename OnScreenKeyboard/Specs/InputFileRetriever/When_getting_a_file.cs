using Machine.Specifications;
using SimulatableApi.StreamStore;

namespace Specs.InputFileRetriever
{
    [Subject(typeof(OnScreenKeyboard.Utils.InputFileRetriever))]
    public class When_getting_a_file : With<OnScreenKeyboard.Utils.InputFileRetriever>
    {
        Establish context = () =>
        {
            var fs = FileSystem.Simulated();
            fs.File(@"c:\some.file").Overwrite("some contents");
            Mocks.Inject<FileSystem>(fs);
        };

        Because of = () => _result =  Subject.Get(@"c:\some.file");

        It should_return_contents = () => _result.ShouldEqual("some contents");
        static string _result;
    }
    
}