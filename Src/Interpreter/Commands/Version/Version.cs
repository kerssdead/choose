using System;
using System.IO;

namespace Interpreter
{ 
	partial class InterpreterInput
	{
		public static void Version()
		{
			String Path = GetCommandsDir() + "\\Version\\Version.txt";
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
	
		public static void Update()
		{
			String Path = GetCommandsDir() + "\\Version\\Version.txt";
			int Ver;

			using (StreamReader sr = new StreamReader(Path))
			{
				Ver = Convert.ToInt32(sr.ReadToEnd());
			}

			Ver++;

			using (StreamWriter sw = new StreamWriter(Path))
			{
				sw.Write(Ver);
			}
		}
	}
}
