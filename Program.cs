using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace choose
{
	class Option
	{
		private List<String> _list;
		private List<String> _ext;

		public Option()
		{
			_list = new List<String>();
			_ext = new List<String>();
		}
		public void Set(string path)
		{
			int x = 0;

			_list.AddRange(Directory.GetFiles(path));

			for (int i = 0; i < _list.Count; i++)
			{
				String[] splittedName = _list[i].Split('.');
				_list[i] = "";
				for (int j = 0; j < splittedName.Length - 1; j++)
				{
					if (j + 1 < splittedName.Length - 1)
						_list[i] += splittedName[j] + '.';
					else
						_list[i] += splittedName[j];
				}

				_ext.Add(splittedName[splittedName.Length - 1]);
			}

			x = _list.Count;

			_list.AddRange(Directory.GetDirectories(path));
			for (int i = x; i < _list.Count; i++)
				_ext.Add("dir");

			for (int i = 0; i < _list.Count; i++)
				_list[i] = GetLocalName(_list[i]);

			_list.Add("..");
			_ext.Add("back");
		}
		public void Show(int current, int page, int pageMax)
		{
			Console.Clear();
			for (int i = page * pageMax; i < (page + 1) * pageMax && i < _list.Count; i++)
			{
				if (i == current)
				{
					Console.BackgroundColor = ConsoleColor.Blue;
					Console.Write(_ext[i] + "\t");
					Console.WriteLine(_list[i]);
					Console.BackgroundColor = ConsoleColor.Black;
				}
				else
				{
					Console.Write(_ext[i] + "\t");
					Console.WriteLine(_list[i]);
				}
			}

			Console.WriteLine("\nControl [Up, Down, Left, Right], Quit [Esc], Open File/Directory [Enter]");
		}
		public void OpenTxt(string file, string extension)
		{
			using (StreamReader sr = new StreamReader(file + '.' + extension))
			{
				Console.Clear();
				Console.WriteLine(sr.ReadToEnd());
				Console.WriteLine("\nQuit - [Esc]");

				ConsoleKeyInfo key;

				do
				{
					key = Console.ReadKey();
				} while (key.Key != ConsoleKey.Escape);

				return;
			}
		}
		public void OpenExe(string file, string extension)
		{
			//using (Process newProcess = new Process())
			//{
			//	newProcess.StartInfo.UseShellExecute = false;
			//	newProcess.StartInfo.FileName = file + '.' + extension;
			//	newProcess.StartInfo.CreateNoWindow = true;
			//	newProcess.Start();
			//}

			Process.Start(file + '.' + extension);
		}
		public Option OpenDirectory(string dir)
		{
			Option NewFolder = new Option();
			NewFolder.Set(dir);

			return NewFolder;
		}
		public int GetNextPage(int currentPage, int pageMax)
		{
			if ((currentPage + 1) * pageMax <= _list.Count)
				return currentPage + 1;
			else
				return currentPage;
		}
		public int GetPrevPage(int currentPage, int pageMax)
		{
			if ((currentPage - 1) * pageMax >= 0)
				return currentPage - 1;
			else
				return currentPage;
		}
		public String GetLocalName(String path)
		{
			String[] newPath = path.Split('\\');

			return newPath[newPath.Length - 1];
		}
		public int GetSize()
		{
			return _list.Count;
		}
		public string GetCurrentElement(int index)
		{
			return _list[index];
		}
		public string GetCurrentExt(int index)
		{
			return _ext[index];
		}
	}

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

			String currentPath = @"C:\Users\Sergey\source\repos\choose";
			
			int currentElem = 0,
				currentPage = 0;
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
					if (currentElem + 1 < currentDirectory.GetSize())
						currentElem++;
				}
				else
					if (key.Key == ConsoleKey.UpArrow)
				{
					if (currentElem - 1 > -1)
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
						currentPath = currentPath + '\\' + currentDirectory.GetCurrentElement(currentElem);
						currentDirectory = currentDirectory.OpenDirectory(currentPath);
						currentElem = 0;
						currentPage = 0;
					}
					else
						if (currentDirectory.GetCurrentExt(currentElem) == "back")
					{
						String[] newPath = currentPath.Split('\\');
						currentPath = "";
						for (int i = 1; i < newPath.Length - 1; i++)
							currentPath += '\\' + newPath[i];

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
