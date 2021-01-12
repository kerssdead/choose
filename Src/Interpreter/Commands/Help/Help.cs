using System;
using System.IO;

namespace Cnsl
{
	partial class Interpreter
	{
		public static void Help()
		{
			String Path = GetCommandsDir() + "\\Help\\Help.txt";
			ConsoleKeyInfo Key;

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
