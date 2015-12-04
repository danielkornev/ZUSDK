using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Diagnostics
{
	public static class ProcessThreadExtensions
	{
		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, uint dwThreadId);


		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		static extern bool TerminateThread(IntPtr hThread, uint dwExitCode);

		public static bool Terminate(this ProcessThread thread)
		{
			IntPtr ptrThread = OpenThread(1, false, (uint)thread.Id);
			if (!thread.IsCurrent())
			{
				try
				{
					TerminateThread(ptrThread, 1);
					return true;
				}
				catch (Exception e)
				{
					//Log.Info("Failed to terminate thread: " + e.Message);
					return false;
				}
			}
			else
			{
				return false;
			}

		}

		public static bool IsCurrent(this ProcessThread thread)
		{
			IntPtr ptrThread = OpenThread(1, false, (uint)thread.Id);
			if (AppDomain.GetCurrentThreadId() != thread.Id)
				return false;
			else
				return true;
		}
	} // class
} // namespace
