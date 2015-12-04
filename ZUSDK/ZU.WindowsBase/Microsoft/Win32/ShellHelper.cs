using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Win32
{
	public static class ShellHelper
	{
		// 
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public class ShellExecuteInfo
		{
			public int Size;
			public uint Mask;
			public IntPtr hwnd;
			public string Verb;
			public string File;
			public string Parameters;
			public string Directory;
			public int Show;
			public IntPtr InstApp;
			public IntPtr IDList;
			public string Class;
			public IntPtr hkeyClass;
			public uint HotKey;
			public IntPtr IconOrMonitor;
			public IntPtr Process;

			public ShellExecuteInfo()
			{
				Size = Marshal.SizeOf(this);
			}
		}

		// Code For OpenWithDialog Box
		[DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		extern public static bool ShellExecuteEx(ShellExecuteInfo lpExecInfo);

		public const int SW_NORMAL = 1;

		public static void OpenAs(string file)
		{
			Open(file, "openas");
		}

		/// <summary>
		/// "open" - Opens a file or a application
		/// "openas" - Opens dialog when no program is associated to the extension
		/// "opennew" - see MSDN 
		/// "runas" - In Windows 7 and Vista, opens the UAC dialog and in others, open the Run as... Dialog
		/// "null" - Specifies that the operation is the default for the selected file type.
		/// "edit" - Opens the default text editor for the file.    
		/// "explore" - Opens the Windows Explorer in the folder specified in lpDirectory.
		/// "properties" - Opens the properties window of the file.
		/// "copy" - see MSDN
		/// "cut" - see MSDN
		/// "paste" - see MSDN
		/// "pastelink" - pastes a shortcut
		/// "delete" - see MSDN
		/// "print" - Start printing the file with the default application.
		/// "printto" - see MSDN
		/// "find" - Start a search
		/// </summary>
		/// <param name="file"></param>
		/// <param name="verb"></param>
		public static void Open(string file, string verb)
		{

			try
			{
				ShellExecuteInfo sei = new ShellExecuteInfo();
				sei.Size = Marshal.SizeOf(sei);
				sei.Verb = verb;
				sei.File = file;
				sei.Show = SW_NORMAL;
				if (!ShellExecuteEx(sei))
					throw new System.ComponentModel.Win32Exception();
			}
			catch (Exception ex)
			{
				Trace.WriteLine("EXCEPTION:" + ex.Message); 
			}
		}

	} // class
} // namespace
