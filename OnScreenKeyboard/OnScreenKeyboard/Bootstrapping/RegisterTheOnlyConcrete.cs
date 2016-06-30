using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace OnScreenKeyboard.Bootstrapping
{
    public class RegisterTheOnlyConcrete : IRegistrationConvention
    {
        
        private HashSet<Type> _registeredTypes = new HashSet<Type>();
        static readonly Dictionary<Type, List<Type>> InterfacesMappedToConcretes = new Dictionary<Type, List<Type>>();
        public void Process(Type type, Registry registry)
        {
            if (!type.IsInterface)
                return;

            if (_registeredTypes.Contains(type))
                throw new Exception(string.Format("The type {0} has already been processed. More than likely, you are scanning the {1} assembly multiple times.", type, type.Assembly));

            _registeredTypes.Add(type);

            var allTypesThatImplementInterface = Assembly.GetAssembly(type).GetTypes().Where(inner => type.IsAssignableFrom(inner) && inner != type).ToList();

            if (ThereAreSeveralImplmentationsOfInterface(allTypesThatImplementInterface))
                return;

            var allInterfacesThatImplementInterface = allTypesThatImplementInterface.Where(x => x.IsInterface).ToList();

            if (allInterfacesThatImplementInterface.Count == 1)
            {
                var interfaceToForwardTo = allInterfacesThatImplementInterface.First();
                registry.Forward(interfaceToForwardTo, type);
                return;
            }

            var concreteToRegister = allTypesThatImplementInterface.First();

            if (!InterfacesMappedToConcretes.ContainsKey(concreteToRegister))
                InterfacesMappedToConcretes[concreteToRegister] = new List<Type>();

            InterfacesMappedToConcretes[concreteToRegister].Add(type);

            var lifeCycle = InstanceScope.Transient;

            var shouldThisBeASingleton = typeof(ISingleton).IsAssignableFrom(concreteToRegister);
            var shouldBeAlwaysUnique = typeof(IAlwaysUnique).IsAssignableFrom(concreteToRegister);

            if (shouldThisBeASingleton)
            {
                var theFirstInterfaceThatTheClassImplements = InterfacesMappedToConcretes[concreteToRegister].First();

                if (theFirstInterfaceThatTheClassImplements != type)
                {
                    registry.Forward(theFirstInterfaceThatTheClassImplements, type);
                    return;
                }
            }

            lifeCycle = (shouldThisBeASingleton) ? InstanceScope.Singleton : lifeCycle;
            lifeCycle = (shouldBeAlwaysUnique) ? InstanceScope.Unique : lifeCycle;

            registry.For(type).LifecycleIs(lifeCycle).Use(concreteToRegister);
        }

        private static bool ThereAreSeveralImplmentationsOfInterface(IEnumerable<Type> types)
        {
            return types.Where(x => !x.IsInterface).ToList().Count != 1;
        }
    }

    public interface ISingleton { }
    public interface IAlwaysUnique { }
}
