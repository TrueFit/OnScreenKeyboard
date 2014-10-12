using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Keys.Core;
using NUnit.Framework;

namespace Keys.Tests
{
	[TestFixture]
	public class Tests
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void TestBasicInput()
		{
			var input = "IT Crowd";
			var output		= "D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#";

			var kb = new Keyboard();
			var result = kb.GenerateCommands(input);
			Assert.AreEqual(output, result);

			input = "Cranberry Beret";
			output = "R,R,#,D,D,R,R,R,#,U,U,L,L,L,L,L,#,D,D,R,#,U,U,#,R,R,R,#,D,D,R,#,#,D,D,L,L,L,L,L,#,S,U,U,U,U,R,#,R,R,R,#,D,D,R,#,U,U,L,#,D,D,D,L,L,L,#";
			kb = new Keyboard();
			result = kb.GenerateCommands(input);
			Assert.AreEqual(output, result);

			input = "Tru3 F1t";
			output = "D,D,D,R,#,U,R,R,R,R,#,D,L,L,L,#,D,R,R,#,S,U,U,U,U,R,#,D,D,D,D,L,L,L,#,U,L,#";
			kb = new Keyboard();
			result = kb.GenerateCommands(input);
			Assert.AreEqual(output, result);
		}

		[Test]
		public void TestCasing()
		{
			var input = "IT Crowd";
			var output = "D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#";

			var kb = new Keyboard();
			var result = kb.GenerateCommands(input);
			Assert.AreEqual(output, result);

			kb = new Keyboard();
			var result2 = kb.GenerateCommands(input.ToLower());
			Assert.AreEqual(result, result2);

			kb = new Keyboard();
			result2 = kb.GenerateCommands(input.ToUpper());
			Assert.AreEqual(result, result2);

		}

		[Test]
		public void TestFileInput()
		{
			var kb = new Keyboard();
			var output = "D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#";
			var cmds = kb.ProcessFile(@"App_Data\ITCrowd.txt");
			foreach (var cmd in cmds)
				Assert.AreEqual(output, cmd);
		}

		[Test]
		public void TestCursorReset()
		{
			var input = "IT Crowd";
			var output = "D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#";

			var kb = new Keyboard();
			var result1 = kb.GenerateCommands(input);
			Assert.AreEqual(output, result1);

			kb.ResetCursor();
			var result2 = kb.GenerateCommands(input);
			Assert.AreEqual(result1, result2);

			var result3 = kb.GenerateCommands(input);
			Assert.AreNotEqual(result2, result3);
		}

		[Test]
		public void TestEdges()
		{
			var kb = new Keyboard();
			var input = "AF05";
			var output = "#,R,R,R,R,R,#,D,D,D,D,D,#,L,L,L,L,L,#";
			var result = kb.GenerateCommands(input);
			Assert.AreEqual(output, result);

		}

		[Test]
		public void TestWrappingKeyboard()
		{
			var input = "IT Crowd";
			var output = "D,R,R,#,D,D,L,#,S,U,U,U,R,#,D,D,R,R,R,#,L,L,L,#,D,R,R,#,U,U,U,L,#";
			var kb = new WrappingKeyboard();
			var result = kb.GenerateCommands(input);
			Assert.AreEqual(output, result);

			input = "AF05";
			output = "#,L,#,U,#,R,#";
			kb.ResetCursor();
			result = kb.GenerateCommands(input);
			Assert.AreEqual(output, result);

			input = "BF9A";
			output = "R,#,L,L,#,U,L,#,D,R,R,#";
			kb.ResetCursor();
			result = kb.GenerateCommands(input);
			Assert.AreEqual(output, result);
		}

		[Test]
		public void TestQwertyKeyboard()
		{
			var input = "IT Crowd";
			var output = "D,D,R,R,R,R,R,#,L,L,L,#,S,D,D,D,L,#,U,U,U,#,D,L,#,U,U,R,R,R,R,R,#,D,D,L,#";
			var kb = new QwertyKeyboard();
			var result = kb.GenerateCommands(input);
			Assert.AreEqual(output, result);
		}

		[Test]
		public void TestLayouts()
		{
			var input = "IT Crowd";
			var output = "D,D,#,D,D,R,R,R,#,S,U,U,U,U,L,#,D,D,D,D,L,#,U,R,#,D,D,#,U,U,U,U,U,R,#";
			var kb = new Keyboard(width: 4);
			var result1 = kb.GenerateCommands(input);
			Assert.AreEqual(output, result1);

			var kb2 = new Keyboard();
			var result2 = kb2.GenerateCommands(input);
			Assert.AreNotEqual(result1, result2);
		}

		[Test]
		public void TestConsoleApp()
		{
			var files = new List<string> {@"App_Data\ITCrowd.txt"};
			Console.Program.Main(files.ToArray());

			files.Add(@"App_Data\SomeOtherFile.txt");
			Console.Program.Main(files.ToArray());

			files.Add(@"App_Data\SomeBadFile.txt");
			Console.Program.Main(files.ToArray());
		}
	}
}
