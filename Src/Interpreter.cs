using System;

namespace Interpreter
{
	partial class InterpreterInput
	{
		public static String[] Input()
		{
			Console.Clear();
			Console.Write(">");
			Console.SetCursorPosition(1, 0);
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
				case "version":
					Version();
					break;
				default:
					break;
			}

			return Return;
		}
	}
}
