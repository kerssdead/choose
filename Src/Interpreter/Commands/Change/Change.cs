using System;

namespace Cnsl
{
	partial class Interpreter
	{
		public static String[] Change(String Disk)
		{
			String[] Return = new string[5];

			Return[1] = Disk + ":\\";
			Return[0] = "1";

			return Return;
		}
	}
}
