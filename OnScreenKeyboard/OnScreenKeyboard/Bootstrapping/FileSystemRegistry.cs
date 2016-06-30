using SimulatableApi.StreamStore;
using StructureMap.Configuration.DSL;

namespace OnScreenKeyboard.Bootstrapping
{
    public class FileSystemRegistry : Registry
    {
        public FileSystemRegistry()
        {
            For<FileSystem>().Singleton().Use(FileSystem.Real);
        }
    }
}
