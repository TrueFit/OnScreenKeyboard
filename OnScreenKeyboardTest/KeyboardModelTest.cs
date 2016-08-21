using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnScreenKeyboard;

namespace KeyboardModelTest
{
    [TestClass]
    public class KeyboardModelTest
    {
        private IKeyboardModel _keyboard = new KeyboardModel();

        [TestMethod]
        public void KeyboardFindTest()
        {
            Point pt = new Point();
            Assert.IsTrue(_keyboard.TryFind('A', out pt));
            Assert.IsTrue(pt.X == 0 && pt.Y == 0);
            Assert.IsTrue(_keyboard.TryFind('4', out pt));
            Assert.IsTrue(pt.X == 5 && pt.Y == 4);
        }

        [TestMethod]
        public void KeyboardNotFoundTest()
        {
            Point pt = new Point();
            Assert.IsFalse(_keyboard.TryFind('*', out pt));
        }
    }
}
