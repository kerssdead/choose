using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Options
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
			Console.SetCursorPosition(0, 0);
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

			Console.WriteLine("\nCurrent page: " + (page + 1));
			Console.WriteLine("Control [Up, Down, Left, Right], Quit [Esc], Open File/Directory [Enter], Console [~]");
		}
		public void OpenTxt(string file, string extension)
		{
			Console.Clear();

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
			Process.Start(file + '.' + extension);
		}
		public Option OpenDirectory(string dir)
		{
			Console.Clear();

			Option NewFolder = new Option();
			NewFolder.Set(dir);

			return NewFolder;
		}
		public int GetNextPage(int currentPage, int pageMax)
		{
			if ((currentPage + 1) * pageMax <= _list.Count)
			{
				Console.Clear();
				return currentPage + 1;
			}
			else
				return currentPage;
		}
		public int GetPrevPage(int currentPage, int pageMax)
		{
			if ((currentPage - 1) * pageMax >= 0)
			{
				Console.Clear();
				return currentPage - 1;
			}
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
}