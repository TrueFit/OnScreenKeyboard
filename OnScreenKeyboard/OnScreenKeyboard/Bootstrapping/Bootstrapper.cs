using OnScreenKeyboard.Utils;
using StructureMap;

namespace OnScreenKeyboard.Bootstrapping
{
    public class Bootstrapper
    {
        public static Container BuildContainer()
        {
            return new Container(x => x.Scan(y =>
            {
                y.AssemblyContainingType<PathDeterminer>();
                y.LookForRegistries();
                y.Convention<RegisterTheOnlyConcrete>();
            }));

        }
    }
}