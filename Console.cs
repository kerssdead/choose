using System;
using System.Collections.Generic;
using System.Text;

namespace Cnsl
{
	class Interpreter
	{
		String Command;

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
					Return[0] = SplitCommand[1] + ":\\";
					Return[1] = "1";
					break;
				default:
					break;
			}

			return Return;
		}
	}
}
