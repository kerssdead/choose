using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cnsl
{
	class Interpreter
	{
		public static String[] Input()
		{
			Console.Clear();
			Console.Write(">");
			String Command = Console.ReadLine();

			String[] SplitCommand = Command.Split(' ');
			String[] Return = new string[5];
			switch (SplitCommand[0])
			{
				case "change":
					Return = Change(Return[0]);
					break;
				case "help":
					Help();
					break;
				default:
					break;
			}

			return Return;
		}
		public static String[] Change(String Disk)
		{
			String[] Return = new string[5];

			Return[0] = Disk + ":\\";
			Return[1] = "1";

			return Return;
		}
		public static void Help()
		{
			String[] Dir = Directory.GetCurrentDirectory().Split('\\');
			String Path = "";
			ConsoleKeyInfo Key;
			for (int i = 0; Dir[i] != "bin"; i++)
				Path += Dir[i] + '\\';
			Path += "\\textFiles\\help.txt";

			using (StreamReader sr = new StreamReader(Path))
			{
				Console.Clear();
				Console.WriteLine(sr.ReadToEnd());
				Console.WriteLine("\nQuit - [Esc]");
			}
			do
			{
				Key = Console.ReadKey();
			} while (Key.Key != ConsoleKey.Escape);
		}
	}
}
