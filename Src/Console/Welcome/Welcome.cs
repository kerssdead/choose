using System;
using System.Threading;

namespace Cnsl
{
	partial class Consol
	{
		static public void Welcome()
		{
			const String welcome = "\n\n\n\n\n\n\n\n\n\n\n" +
						 "				 _       __           __   __   ______                    \n" +
						 "				| |     / /  ___     / /  / /  / ____/  __  __   ____ ___ \n" +
						 "				| | /| / /  / _ \\   / /  / /  / /      / / / /  / __ `__ \\\n" +
						 "				| |/ |/ /  /  __/  / /  / /  / /___   / /_/ /  / / / / / /\n" +
						 "				|__/|__/   \\___/  /_/  /_/   \\____/   \\__,_/  /_/ /_/ /_/ \n";

			Console.WriteLine(welcome);
			Thread.Sleep(1000);
			Console.Clear();
		}
	}
}
