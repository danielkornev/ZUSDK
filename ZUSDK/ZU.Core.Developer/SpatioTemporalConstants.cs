//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Media.Media3D;

//namespace ZU
//{
//	public class SpatioTemporalConstants
//	{
//		public const double DefaultWidth = 100000; // 100'000
//		public const double DefaultHeight = 56250; // 100K * 9/16 (for 16/9 aspect ratio)
//		/// <summary>
//		/// 100 years
//		/// </summary>
//		public const long DefaultTimeLength = 946708128000000000; // ticks for 3000 years
//		/// <summary>
//		/// 5 years
//		/// </summary>
//		public const long ShortTimeLength = 1577664000000000; // ticks for 5 years

//		private static Bounds defaultArea3D;

//		public static int MinX = (int)-DefaultWidth / 2;
//		public static int MaxX = (int)DefaultWidth / 2;

//		public static int MinY = (int)-DefaultHeight / 2;
//		public static int MaxY = (int)DefaultHeight / 2;

//		// should be zero
//		public static long MinZ = 0;
//		public static long MaxZ = 3155378975999999999;

//		public const double DefaultEntityWidth = 100;
//		public const double DefaultEntityHeight = 100;

//		public static Bounds DefaultArea3D
//		{
//			get
//			{
//				var center = new Vector3D(0, 0, 0); // (0,0,0) - center of the area

//				var maxTime = //(double)SpatioTemporalConstants.DefaultTimeLength;
//				(double)DateTime.MaxValue.Ticks;

//				var size = new Vector3D(DefaultWidth, DefaultHeight, maxTime); //(16*b, 9*b, (ticks for 3000 years))

//				defaultArea3D = new Bounds(center, size);

//				return defaultArea3D;
//			}
//		}
//	} // class
//} // namespace
