using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Interop;

namespace System.Windows
{
	public static class SystemParametersHelper
	{
		/// <summary>
		/// Returns 0 in case Window is null or system failed to obtain current screen
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="window"></param>
		/// <returns></returns>
		public static double GetCurrentScreenHeight(Window window)
		{
			if (window != null)
			{
				var currentScreen = WpfScreen.GetScreenFrom(window);
				if (currentScreen != null)
					return currentScreen.DeviceBounds.Height;
			}

			return 0;
		}

		public static WpfScreen GetScreen(this Window window)
		{
			if (window != null)
			{
				var currentScreen = WpfScreen.GetScreenFrom(window);
				if (currentScreen != null)
					return currentScreen;
			}

			return null;
		}

	}

	public class WpfScreen
	{
		public static IEnumerable<WpfScreen> AllScreens()
		{
			foreach (Screen screen in System.Windows.Forms.Screen.AllScreens)
			{
				yield return new WpfScreen(screen);
			}
		}

		public static WpfScreen GetScreenFrom(Window window)
		{
			WindowInteropHelper windowInteropHelper = new WindowInteropHelper(window);
			Screen screen = System.Windows.Forms.Screen.FromHandle(windowInteropHelper.Handle);
			WpfScreen wpfScreen = new WpfScreen(screen);
			return wpfScreen;
		}

		public static WpfScreen GetScreenFrom(Point point)
		{
			int x = (int)Math.Round(point.X);
			int y = (int)Math.Round(point.Y);

			// are x,y device-independent-pixels ??
			System.Drawing.Point drawingPoint = new System.Drawing.Point(x, y);
			Screen screen = System.Windows.Forms.Screen.FromPoint(drawingPoint);
			WpfScreen wpfScreen = new WpfScreen(screen);

			return wpfScreen;
		}

		public static WpfScreen Primary
		{
			get { return new WpfScreen(System.Windows.Forms.Screen.PrimaryScreen); }
		}

		private readonly Screen screen;

		internal WpfScreen(System.Windows.Forms.Screen screen)
		{
			this.screen = screen;
		}

		public Rect DeviceBounds
		{
			get { return this.GetRect(this.screen.Bounds); }
		}

		public Rect WorkingArea
		{
			get { return this.GetRect(this.screen.WorkingArea); }
		}

		private Rect GetRect(Rectangle value)
		{
			// should x, y, width, height be device-independent-pixels ??
			return new Rect
			{
				X = value.X,
				Y = value.Y,
				Width = value.Width,
				Height = value.Height
			};
		}

		public bool IsPrimary
		{
			get { return this.screen.Primary; }
		}

		public string DeviceName
		{
			get { return this.screen.DeviceName; }
		}
	} // class
} // namespace
