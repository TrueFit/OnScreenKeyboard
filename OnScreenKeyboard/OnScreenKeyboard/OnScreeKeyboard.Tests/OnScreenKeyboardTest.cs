using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OnScreenKeyboard.OnScreeKeyboard.Tests
{
    [TestFixture]
    public class OnScreenKeyboardTest
    {
        private DefaultKeyboard keyboard = new DefaultKeyboard();
        private KeyboardProcessor keyBoardProcessor;

        [SetUp]
        public void SetUp()
        {
            keyBoardProcessor = new KeyboardProcessor(keyboard);
        }

        [Test]
        public async Task ThrowsErrorWithInvalidChar()
        {
            //arrage
            var testInput = "This is a test!";

            //act
            var output = keyBoardProcessor.ProcessInput(testInput);

            //assert
            Assert.AreEqual(output, "There was an error processing the input");

        }

        [Test]
        public async Task ReturnsExampleOutput()
        {
            //arrage
            var testInput = "IT CROWD";

            //act
            var output = keyBoardProcessor.ProcessInput(testInput);

            //assert
            Assert.AreEqual(output, "D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#");

        }
    }
}
