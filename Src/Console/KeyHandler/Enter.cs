using Options;
using System;

namespace Cnsl
{
	partial class Consol
	{
		public static bool Enter(ref Commander currentDirectory, ref int currentElem, ref String currentPath, ref int pth, ref int currentPage, bool bck)
		{
			if (currentDirectory.GetCurrentExt(currentElem) == "exe")
				currentDirectory.OpenExe(currentPath + '\\' + currentDirectory.GetCurrentElement(currentElem), currentDirectory.GetCurrentExt(currentElem));
			else
					if (currentDirectory.GetCurrentExt(currentElem) == "dir")
			{
				if (pth == 0)
					currentPath = currentPath + currentDirectory.GetCurrentElement(currentElem);
				else
					currentPath = currentPath + '\\' + currentDirectory.GetCurrentElement(currentElem);

				currentDirectory = currentDirectory.OpenDirectory(currentPath);
				currentElem = 0;
				currentPage = 0;
				pth++;
			}
			else
					if (currentDirectory.GetCurrentExt(currentElem) == "back")
			{
				if (currentPath == @"C:\")
					return true;

				String[] newPath = currentPath.Split('\\');
				currentPath = "";
				for (int i = 0; i < newPath.Length - 1; i++)
				{
					if (newPath.Length == 2)
					{
						currentPath = @"C:\";
						break;
					}
					else
					{
						if (i == 0)
						{
							currentPath += @"C:";
							continue;
						}
					}
					currentPath += '\\' + newPath[i];
				}
				currentDirectory = currentDirectory.OpenDirectory(currentPath);
				currentElem = 0;
				currentPage = 0;
			}
			else
				currentDirectory.OpenTxt(currentPath + '\\' + currentDirectory.GetCurrentElement(currentElem), currentDirectory.GetCurrentExt(currentElem));

			return false;
		}
	}
}
