using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Media;

namespace System.Windows
{
	public static class WindowExtensions
	{
		#region Constants

		const UInt32 SWP_NOSIZE = 0x0001;
		const UInt32 SWP_NOMOVE = 0x0002;
		const UInt32 SWP_SHOWWINDOW = 0x0040;

		#endregion

		#region Imports

		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

		[DllImport("user32.dll")]
		private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

		[DllImport("user32.dll")]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);



		#endregion

		/// <summary>
		/// Activate a window from anywhere by attaching to the foreground window
		/// </summary>
		public static void GlobalActivate(this Window w)
		{
			//Get the process ID for this window's thread
			var interopHelper = new WindowInteropHelper(w);
			var thisWindowThreadId = GetWindowThreadProcessId(interopHelper.Handle, IntPtr.Zero);

			//Get the process ID for the foreground window's thread
			var currentForegroundWindow = GetForegroundWindow();
			var currentForegroundWindowThreadId = GetWindowThreadProcessId(currentForegroundWindow, IntPtr.Zero);

			//Attach this window's thread to the current window's thread
			AttachThreadInput(currentForegroundWindowThreadId, thisWindowThreadId, true);

			//Set the window position
			SetWindowPos(interopHelper.Handle, new IntPtr(0), 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);

			//Detach this window's thread from the current window's thread
			AttachThreadInput(currentForegroundWindowThreadId, thisWindowThreadId, false);

			//Show and activate the window
			if (w.WindowState == WindowState.Minimized) w.WindowState = WindowState.Normal;
			w.Show();
			w.Activate();
		}

		public static Rect GetCurrentMonitorWorkingAreaSize(this Window w)
		{
			Rect result = new Rect();

			// 1. helper
			var helper = new WindowInteropHelper(w); //this being the wpf form 
			var currentScreen = Screen.FromHandle(helper.Handle);

			// 2. DPI factor
			PresentationSource MainWindowPresentationSource = PresentationSource.FromVisual(w);
			Matrix m = MainWindowPresentationSource.CompositionTarget.TransformToDevice;
			var DpiWidthFactor = m.M11;
			var DpiHeightFactor = m.M22;

			// 3. Working area
			var workingArea = currentScreen.WorkingArea;

			// 4. Applying scaling 
			var newHeight = workingArea.Height * DpiHeightFactor;
			var newWidth = workingArea.Width * DpiWidthFactor;

			// 5. Forming a scaled working area
			result.Width = newWidth;
			result.Height = newHeight;

			// and we are done.
			return result;
		}

		public static void ActivateCurrentWindow(this Window w)
		{
			//Get the process ID for this window's thread
			var interopHelper = new WindowInteropHelper(w);
			
			//Change behavior by settings the wFlags params. See http://msdn.microsoft.com/en-us/library/ms633545(VS.85).aspx
			SetWindowPos(interopHelper.Handle, new IntPtr(0), 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);
		}

		
	} // class

} // namespace