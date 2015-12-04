using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace System.Windows
{
	public static class FrameworkElementExtensions
	{
		public static bool ToPngImageOfActualSize(
			this FrameworkElement surface, double scale, int quality, out byte[] results)
		{
			bool result = false;
			results = new byte[0];

			// Save current canvas transform
			Transform transform = surface.LayoutTransform;
			// reset current transform (in case it is scaled or rotated)
			surface.LayoutTransform = null;

			// Get the size of canvas
			Size size = new Size(surface.ActualWidth, surface.ActualHeight);
			// Measure and arrange the surface
			// VERY IMPORTANT
			surface.Measure(size);
			surface.Arrange(new Rect(size));

			if (size.Height > 0 && size.Width > 0)
			{
				try
				{
					// Create a render bitmap and push the surface to it
					RenderTargetBitmap renderBitmap =
					  new RenderTargetBitmap(
						(int)size.Width,
						(int)size.Height,
						96d,
						96d,
						PixelFormats.Pbgra32);
					renderBitmap.Render(surface);

					// Use png encoder for our data
					PngBitmapEncoder encoder = new PngBitmapEncoder();
					// push the rendered bitmap to it
					encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

					Byte[] _imageArray = new byte[0];

					using (MemoryStream outputStream = new MemoryStream())
					{
						encoder.Save(outputStream);
						_imageArray = outputStream.ToArray();
					}

					// Restore previously saved layout
					surface.LayoutTransform = transform;

					results = _imageArray;

					result = true;
				}
				catch
				{
					results = new byte[0];
					result = false;
				}
			}


			return result;
		}

		public static bool ToPngImageOfActualSize(
			this FrameworkElement surface, double scale, int quality, string fileName)
		{
			bool result = false;

			// Save current canvas transform
			Transform transform = surface.LayoutTransform;
			// reset current transform (in case it is scaled or rotated)
			surface.LayoutTransform = null;

			// Get the size of canvas
			Size size = new Size(surface.ActualWidth, surface.ActualHeight);
			// Measure and arrange the surface
			// VERY IMPORTANT
			surface.Measure(size);
			surface.Arrange(new Rect(size));

			if (size.Height > 0 && size.Width > 0)
			{
				try
				{
					// Create a render bitmap and push the surface to it
					RenderTargetBitmap renderBitmap =
					  new RenderTargetBitmap(
						(int)size.Width,
						(int)size.Height,
						96d,
						96d,
						PixelFormats.Pbgra32);
					renderBitmap.Render(surface);

					// Use png encoder for our data
					PngBitmapEncoder encoder = new PngBitmapEncoder();
					// push the rendered bitmap to it
					encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

					if (System.IO.File.Exists(fileName))
						return false;

					using (var stream = File.Create(fileName))
					{
						encoder.Save(stream);
					}

					// Restore previously saved layout
					surface.LayoutTransform = transform;

					result = true;
				}
				catch
				{
					result = false;
				}
			}

			return result;
		}

	
		/// <summary>
		/// Tells if the given position is within this FrameworkElement or not
		/// </summary>
		/// <param name="w"></param>
		/// <param name="position"></param>
		/// <returns></returns>
		public static bool IsPositionWithin(this FrameworkElement w, Point position)
		{
			var bounds = w.GetBoundaries();

			// one pixel to right and down
			var position2 = new Point(position.X + 1, position.Y + 1);

			// creating rect
			var posRect = new Rect(position, position2);

			if (bounds.Contains(posRect))
				return true;
			else if (bounds.IntersectsWith(posRect))
				return true;

			return false;
		}

		public static bool IsPositionWithin(this FrameworkElement w, Point position, bool useActualBoundaries)
		{
			var bounds = new Rect();

			if (useActualBoundaries)
				bounds = w.GetActualBoundaries();
			else
				bounds = w.GetBoundaries();

			// one pixel to right and down
			var position2 = new Point(position.X + 1, position.Y + 1);

			// creating rect
			var posRect = new Rect(position, position2);

			if (bounds.Contains(posRect))
				return true;
			else if (bounds.IntersectsWith(posRect))
				return true;

			return false;
		}

		public static Rect GetBoundaries(this FrameworkElement w)
		{
			Rect rect = new Rect(0, 0, w.Width, w.Height);

			return rect;
		}

		public static Rect GetActualBoundaries(this FrameworkElement w)
		{
			Rect rect = new Rect(0, 0, w.ActualWidth, w.ActualHeight);

			return rect;
		}
	}
}
