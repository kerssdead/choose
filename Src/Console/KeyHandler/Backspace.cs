using Options;
using System;

namespace Cnsl
{
	partial class Consol
	{
		public static void Backspace(ref Commander currentDirectory, ref int currentElem, ref String currentPath, ref int pth, ref int currentPage)
		{
			if (currentPath != @"C:\")
				Enter(ref currentDirectory, ref currentElem, ref currentPath, ref pth, ref currentPage, true);
		}
	}
}
