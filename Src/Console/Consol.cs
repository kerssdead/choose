using Options;
using System;

namespace Cnsl
{
	partial class Consol
	{
		static public void Run()
		{
			Interpreter.InterpreterInput.Update();
			Consol.Welcome();

			const int pageMax = 26;
			String currentPath = @"C:\";
			int currentElem = 0,
				currentPage = 0,
				pth = 0;
			Commander currentDirectory = new Commander();
			currentDirectory.Set(currentPath);

			bool esc = false;

			currentDirectory.Show(currentElem, currentPage, pageMax);

			do
			{
				esc = Consol.KeyHandler(Console.ReadKey(), ref currentElem, ref currentDirectory, ref currentPage, pageMax, ref currentPath, ref pth);
			} while (!esc);
		}
	}
}
