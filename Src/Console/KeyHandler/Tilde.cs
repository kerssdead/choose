using Options;
using System;

namespace Cnsl
{
	partial class Consol
	{
		public static void Tilde(ref String currentPath, ref Commander currentDirectory, ref int currentPage, ref int currentElem)
		{
			String[] Return = Interpreter.InterpreterInput.Input();

			switch (Convert.ToInt32(Return[0]))
			{
				case 1:
					currentPath = Return[1];
					currentDirectory = currentDirectory.OpenDirectory(currentPath);
					currentPage = 0;
					currentElem = 0;
					Console.Clear();
					break;
			}
		}
	}
}
