using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest1
{
	class Program
	{
		static void Main(string[] args)
		{
			var allKinds = ZU.Constants.Kinds.BuiltInKindsDictionary;

			Console.WriteLine("# of Built-In Kinds: " + allKinds.Count);

			Console.ReadLine();
		}
	} // class
} // namespace
