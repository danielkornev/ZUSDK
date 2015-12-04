using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;

namespace System.Windows.Input
{
	/// <summary>
	/// See http://tech.pro/tutorial/893/wpf-snippet-reliably-getting-the-mouse-position
	/// This allows us to obtain correct Mouse Position after drag-n-drop
	/// </summary>
	public static class MouseExtensions 
	{
		public static Point GetCorrectPosition(Visual relativeTo)
		{
			Win32Point w32Mouse = new Win32Point();
			GetCursorPos(ref w32Mouse);
			return relativeTo.PointFromScreen(new Point(w32Mouse.X, w32Mouse.Y));
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct Win32Point
		{
			public Int32 X;
			public Int32 Y;
		};

		internal static bool GetCursorPosition(out Win32Point point)
		{
			Win32Point w32Mouse = new Win32Point();
			point = new Win32Point();

			var result = GetCursorPos(ref w32Mouse);

			if (result)
			{
				point = w32Mouse;
				return true;
			}

			return false;
		}

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetCursorPos(ref Win32Point pt);
	}
}