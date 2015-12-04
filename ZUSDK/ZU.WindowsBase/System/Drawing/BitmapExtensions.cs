using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Drawing
{
	public static class BitmapExtensions
	{
		public static string ToFile(this System.Drawing.Bitmap bitmap)
		{
			string path = System.IO.Path.GetTempFileName();

			path = path + ".png";

			// saving
			bitmap.Save(path);

			// cause we are done, we must destroy our Bitmap
			bitmap.Dispose();
			// nulling it
			bitmap = null;

			return path;
		}
	} // class
} // namespace
