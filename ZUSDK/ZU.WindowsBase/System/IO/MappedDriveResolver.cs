﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>
	/// Source: http://stackoverflow.com/questions/17472168/check-if-directory-exists-on-network-drive
	/// </summary>
	public static class MappedDriveResolver
	{
		[DllImport("mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int WNetGetConnection([MarshalAs(UnmanagedType.LPTStr)] string localName, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder remoteName, ref int length);
		public static string GetUNCPath(string originalPath)
		{
			StringBuilder sb = new StringBuilder(512);
			int size = sb.Capacity;

			// look for the {LETTER}: combination ...
			if (originalPath.Length > 2 && originalPath[1] == ':')
			{
				// don't use char.IsLetter here - as that can be misleading
				// the only valid drive letters are a-z && A-Z.
				char c = originalPath[0];
				if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
				{
					int error = WNetGetConnection(originalPath.Substring(0, 2), sb, ref size);
					if (error == 0)
					{
						DirectoryInfo dir = new DirectoryInfo(originalPath);
						string path = Path.GetFullPath(originalPath).Substring(Path.GetPathRoot(originalPath).Length);
						return Path.Combine(sb.ToString().TrimEnd(), path);
					}
				}
			}
			return originalPath;
		}
	} // class
} // namespace
