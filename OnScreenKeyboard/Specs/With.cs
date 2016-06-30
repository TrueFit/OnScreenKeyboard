using Machine.Specifications;
using StructureMap.AutoMocking;
using StructureMap.AutoMocking.NSubstitute;

namespace Specs
{
    public abstract class With<T>
        where T : class
    {
        Establish context = () =>
        {
            Mocks = (AutoMocker<T>)NSubstituteAutoMockerBuilder.Build<T>();
        };


        protected static TDependency For<TDependency>()
            where TDependency : class
        {
            return Mocks.Get<TDependency>();
        }

        protected static AutoMocker<T> Mocks { get; private set; }
        protected static T Subject { get { return Mocks.ClassUnderTest; } }
    }
}