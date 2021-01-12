using System;
using System.IO;

namespace Cnsl
{
	partial class Interpreter
	{
		public static String GetCommandsDir()
		{
			String[] Dir = Directory.GetCurrentDirectory().Split('\\');
			String Path = "";
			for (int i = 0; Dir[i] != "bin"; i++)
				Path += Dir[i] + '\\';
			Path += "\\Src\\Interpreter\\Commands";

			return Path;
		}
	}
}
