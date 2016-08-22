using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnScreenKeyboard;

namespace OnScreenKeyboardTest
{
    [TestClass]
    public class KeyboardNavigatorTest
    {
        [TestMethod]
        public void NavigatePathTo()
        {
            IKeyboardModel keyboard = new KeyboardModel();
            IKeyboardNavigator keyboardNavigator = new KeyboardNavigator(keyboard);

            var path = keyboardNavigator.PathTo("IT Crowd");
            Assert.AreEqual("D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#", path);
        }

        [TestMethod]
        public void NavigatePathToEmptyInput()
        {
            IKeyboardModel keyboard = new KeyboardModel();
            IKeyboardNavigator keyboardNavigator = new KeyboardNavigator(keyboard);

            var path = keyboardNavigator.PathTo(string.Empty);
            Assert.AreEqual(string.Empty, path);
        }

        [TestMethod]
        public void NavigatePathToMixedCase()
        {
            IKeyboardModel keyboard = new KeyboardModel();
            IKeyboardNavigator keyboardNavigator = new KeyboardNavigator(keyboard);

            var path = keyboardNavigator.PathTo("It cROwD");
            Assert.AreEqual("D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#", path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NavigatePathToInvalidCharacters()
        {
            IKeyboardModel keyboard = new KeyboardModel();
            IKeyboardNavigator keyboardNavigator = new KeyboardNavigator(keyboard);

            var path = keyboardNavigator.PathTo("!@$");
        }
    }
}
