using System;
using System.Threading;
using Options;

namespace choose
{
	class Program
	{
		static void Main(string[] args)
		{
			const int pageMax = 26;
			const String welcome = "\n\n\n\n\n\n\n\n\n\n\n" +
							 "				 _       __           __   __   ______                    \n" +
							 "				| |     / /  ___     / /  / /  / ____/  __  __   ____ ___ \n" +
							 "				| | /| / /  / _ \\   / /  / /  / /      / / / /  / __ `__ \\\n" +
							 "				| |/ |/ /  /  __/  / /  / /  / /___   / /_/ /  / / / / / /\n" +
							 "				|__/|__/   \\___/  /_/  /_/   \\____/   \\__,_/  /_/ /_/ /_/ \n";

			Console.WriteLine(welcome);
			Thread.Sleep(2500);

			String currentPath = @"C:\";

			int currentElem = 0,
				currentPage = 0,
				pth = 0;
			Option currentDirectory = new Option();
			currentDirectory.Set(currentPath);

			bool esc = false;

			currentDirectory.Show(currentElem, currentPage, pageMax);

			do
			{
				ConsoleKeyInfo key = Console.ReadKey();

				if (key.Key == ConsoleKey.Escape)
					esc = true;

				if (key.Key == ConsoleKey.DownArrow)
				{
					if (currentElem + 1 < currentDirectory.GetSize() && currentElem + 1 < (currentPage + 1) * pageMax)
						currentElem++;
				}
				else
					if (key.Key == ConsoleKey.UpArrow)
				{
					if (currentElem - 1 > -1 && currentElem - 1 > currentPage * pageMax)
						currentElem--;
				}
				else
					if (key.Key == ConsoleKey.RightArrow)
				{
					currentPage = currentDirectory.GetNextPage(currentPage, pageMax);
					currentElem = currentPage * pageMax;
				}
				else
					if (key.Key == ConsoleKey.LeftArrow)
				{
					currentPage = currentDirectory.GetPrevPage(currentPage, pageMax);
					currentElem = currentPage * pageMax;
				}
				else
					if (key.Key == ConsoleKey.Enter)
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
						{
							esc = true;
							continue;
						}

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
				}

				currentDirectory.Show(currentElem, currentPage, pageMax);
			} while (!esc);
		}
	}
}
