//#define disablesorter 

using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace System.Windows.Shell
{
	public class ShellItemSorter : IComparer<ShellObject>, System.Collections.IComparer
	{
		public int Compare(ShellObject x, ShellObject y)
		{
			bool isXFolder = false;

#if disablesorter
			if (System.IO.File.Exists(x.ParsingName))
			{

				try
				{
#endif
			string parsingName = x.ParsingName;
			if (!System.IO.File.Exists(parsingName))
				isXFolder = true;
			else
			{
				// we need two things
				// get the file attributes for file or directory
				FileAttributes attr = File.GetAttributes(x.ParsingName);

				//detect whether its a directory or file
				if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
					isXFolder = true;
				else
					isXFolder = false;
			}

			bool isYFolder = false;

			string parsingName2 = y.ParsingName;
			if (!System.IO.File.Exists(parsingName2))
				isYFolder = true;
			else
			{
				// we need two things
				// get the file attributes for file or directory
				FileAttributes attr2 = File.GetAttributes(y.ParsingName);

				//detect whether its a directory or file
				if ((attr2 & FileAttributes.Directory) == FileAttributes.Directory)
					isYFolder = true;
				else
					isYFolder = false;
			}

			// now: 0 = equal, 1: x is great; -1: y is greater

			if (isXFolder == isYFolder)
			{
				// we need to compare names
				return string.Compare(x.Name, y.Name);
			}
			else if (isXFolder && !isYFolder)
				return -1;
			else if (!isXFolder && isYFolder)
				return 1;
#if disablesorter
				}
				catch (Exception ex)
				{
					Log.Exception(ex);
				}

			}
#endif

			return 0;
		}

		public int Compare(object x, object y)
		{
			if (x is ShellObject && y is ShellObject)
				return Compare(x as ShellObject, y as ShellObject);

			return 0;
		}
	} // class
} // namespace
