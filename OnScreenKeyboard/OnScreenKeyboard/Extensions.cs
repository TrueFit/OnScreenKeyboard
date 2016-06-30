using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap.Configuration.DSL;

namespace OnScreenKeyboard
{
    public static class Extensions
    {
        private static readonly Dictionary<char, int> _lookup = new Dictionary<char, int>();


        public static int GetKeyboardIndex(this char character)
        {
            if(_lookup.ContainsKey(character))   // no milliseconds can be spared, so we better memoize 
                return _lookup[character];

            // i want to use ascii characters for directional computation
            // rather than store a list. because the keyboard has the digits after
            // the alphas, we have to move them to the end

            var asciiValue = (int)character;
            if(asciiValue < 49)
                asciiValue += 52;
            if(asciiValue < 65)
                asciiValue += 42;

            // we want a to be the 0th position, so offset it
            asciiValue -= 'A';
            
            _lookup[character] = asciiValue;

            return asciiValue;
        }

        public static void Forward(this Registry registry, Type to, Type from)
        {
            var method = typeof(Registry).GetMethods()
                .Where(m => m.Name == "Forward")
                .First()
                .MakeGenericMethod(to, from);

            method.Invoke(registry, null);
        }
    }
}