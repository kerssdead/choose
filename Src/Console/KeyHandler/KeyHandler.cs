using Options;
using System;

namespace Cnsl
{
	partial class Consol
	{
		static bool KeyHandler(ConsoleKeyInfo key, ref int currentElem, ref Commander currentDirectory, ref int currentPage, int pageMax, ref String currentPath, ref int pth)
		{
			switch (key.Key)
			{
				case ConsoleKey.Escape:
					return true;
				case ConsoleKey.DownArrow:
					Consol.DownArrow(ref currentElem, currentDirectory, currentPage, pageMax);
					break;
				case ConsoleKey.UpArrow:
					Consol.UpArrow(ref currentElem, currentDirectory, currentPage, pageMax);
					break;
				case ConsoleKey.RightArrow:
					Consol.RightArrow(ref currentPage, ref currentElem, currentDirectory, pageMax);
					break;
				case ConsoleKey.LeftArrow:
					Consol.LeftArrow(ref currentPage, ref currentElem, currentDirectory, pageMax);
					break;
				case ConsoleKey.Oem3:
					Consol.Tilde(ref currentPath, ref currentDirectory, ref currentPage, ref currentElem);
					break;
				case ConsoleKey.Enter:
					if (Consol.Enter(ref currentDirectory, ref currentElem, ref currentPath, ref pth, ref currentPage, false))
						return true;
					break;
				//case ConsoleKey.Backspace:
					//Consol.Backspace(ref currentDirectory, ref currentElem, ref currentPath, ref pth, ref currentPage);
					//break;
			}

			currentDirectory.Show(currentElem, currentPage, pageMax);
			return false;
		}
	}
}
