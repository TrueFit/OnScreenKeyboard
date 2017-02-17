using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keys.Core
{
	/// <summary>
	/// A keyboard using the QWERTY order, though still with fixed row lengths. We could do variable row length here too if necessary.
	/// </summary>
	/// <remarks>
	/// Default layout:
	///		123456
	///		7890QW
	///		ERTYUI
	///		OPASDF
	///		GHJKLZ
	///		XCVBNM
	///</remarks>
	public class QwertyKeyboard : Keyboard
	{
		protected const string QwertyKeys = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
		public QwertyKeyboard() : base(keys: QwertyKeys) { }
	}
}
