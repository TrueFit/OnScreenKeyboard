using System;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace OnScreenKeyboard.Test
{
    [TestFixture]
    public class CommandParserTests
    {
        [TestCase("IT Crowd", "D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#")]
        [TestCase("Kill Bill", "D,R,R,R,R,#,L,L,#,R,R,R,#,#,S,U,L,L,L,L,#,D,R,#,R,R,R,#,#")]
        public void CommandParserTests_ShouldReturn_ExpectedResult(string input,string expectedOutput)
        {
            // Arrange
            var keyboard = new OnScreenKeyboard.CommandParser();
            var reader = new StringLiteralReader(input);

            // Act
            var result = keyboard.ReadInput(reader);

            // Assert
            Assert.AreEqual(result, expectedOutput);
        }

        [ExpectedException(typeof(InvalidCharacterException))]
        [TestCase("A movie name &$ For you")]
        [TestCase("Porky's")]
        public void CommandParserTests_ShouldReturn_ExpectedEception(string input)
        {
            // Arrange
            var keyboard = new OnScreenKeyboard.CommandParser();
            var reader = new StringLiteralReader(input);

            // Act
            var result = keyboard.ReadInput(reader);
        }
    }
}
