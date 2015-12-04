using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Media.Media3D
{
	/// <summary>
	/// Provides extension methods for rects.
	/// </summary>
	public static class Rect3DExtensions
	{
		public static Bounds ToBounds(this Rect3D rect3D)
		{
			var center = (Vector3D)rect3D.GetCenter();
			var size = rect3D.GetSize();

			Bounds bounds = new Bounds(center, size, true);

			return bounds;
		}

		public static Point3D GetCenter(this Rect3D rect3D)
		{
			return new Point3D(rect3D.X + rect3D.SizeX / 2, rect3D.Y + rect3D.SizeY / 2, rect3D.Z + rect3D.SizeZ / 2);
		}

		public static Vector3D GetSize(this Rect3D rect3D)
		{
			return new Vector3D(rect3D.SizeX, rect3D.SizeY, rect3D.SizeZ);
		}


	} // class
} // namespace
