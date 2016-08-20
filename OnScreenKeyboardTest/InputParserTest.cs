using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnScreenKeyboard;

namespace OnScreenKeyboardTest
{
    [TestClass]
    public class InputParserTest
    {
        private IInputParser _parser = new InputParser();

        [TestMethod]
        public void BaseTest()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestData\input.txt");
            var rc = _parser.Parse(path);
            Assert.IsNotNull(rc);
            Assert.AreEqual(3, rc.Count);
            Assert.AreEqual(0, rc.FindAll(x => string.IsNullOrEmpty(x)).Count);
        }

        [TestMethod]
        public void BlankLinesTest()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestData\inputBlankLines.txt");
            var rc = _parser.Parse(path);
            Assert.IsNotNull(rc);
            Assert.AreEqual(3, rc.Count);
            Assert.AreEqual(0, rc.FindAll(x => string.IsNullOrEmpty(x)).Count);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void InvalidFileTest()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestData\nosuchfile.txt");
            var rc = _parser.Parse(path);
        }
    }
}
