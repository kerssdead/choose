using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Options
{
	class Commander
	{
		private List<String> _list;
		private List<String> _ext;
		private List<String> _size;

		public Commander()
		{
			_list = new List<String>();
			_ext = new List<String>();
			_size = new List<String>();
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

			for (int i = 0; i < _list.Count; i++)
			{
				if (_ext[i] == "txt" || _ext[i] == "cs" || _ext[i] == "exe" || _ext[i] == "bin"
					|| _ext[i] == "m4a" || _ext[i] == "mp3")
					_size.Add(BytesToString(new FileInfo(path + '\\' + _list[i] + '.' + _ext[i]).Length));
				else
					_size.Add("-1"); 
			}

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
					Console.Write(_list[i]);
					for (int t = 0; t < (8 - _list[i].Length / 8); t++)
						Console.Write('\t');

					if (_ext[i] != "back" && _size[i] != "-1")
						Console.WriteLine(_size[i]);
					else
						Console.WriteLine();
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

				Console.Clear();

				return;
			}
		}
		public void OpenExe(string file, string extension)
		{
			Process.Start(file + '.' + extension);
		}
		public Commander OpenDirectory(string dir)
		{
			Console.Clear();

			Commander NewFolder = new Commander();
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

		public static String BytesToString(long byteCount)
		{
			string[] suf = { "Byte", "KB", "MB", "GB", "TB", "PB", "EB" };
			if (byteCount == 0)
				return "0" + suf[0];
			long bytes = Math.Abs(byteCount);
			int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
			double num = Math.Round(bytes / Math.Pow(1024, place), 1);
			return (Math.Sign(byteCount) * num).ToString() + suf[place];
		}
	}
}