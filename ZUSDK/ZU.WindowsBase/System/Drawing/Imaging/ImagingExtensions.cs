using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace System.Drawing.Imaging
{
	public static class ImagingExtensions
	{
		public static Bitmap ResizeImage(Bitmap imgToResize, Size size)
		{
			try
			{
				Bitmap b = new Bitmap(size.Width, size.Height);
				using (Graphics g = Graphics.FromImage((Image)b))
				{
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;

					g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
				}

				return b;
			}
			catch
			{
				throw;
			}
		}
	}
}
