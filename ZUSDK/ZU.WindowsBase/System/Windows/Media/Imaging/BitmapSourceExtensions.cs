using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Windows.Media.Imaging
{
	public static class BitmapSourceExtensions
	{
		public static string ToFile(this BitmapSource bitmap)
		{
			string path = System.IO.Path.GetTempFileName();
			path = path + ".png";

			PngBitmapEncoder encoder = new PngBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(bitmap));

			using (var filestream = new FileStream(path, FileMode.Create))
				encoder.Save(filestream);

			return path;
		}

		public static string ToBmpFile(this BitmapSource bitmap, string path)
		{
			BmpBitmapEncoder encoder = new BmpBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(bitmap));

			using (var filestream = new FileStream(path, FileMode.Create))
				encoder.Save(filestream);

			return path;
		}

		public static byte[] ToArray(this BitmapSource bitmap)
		{
			MemoryStream stream = new MemoryStream();
			PngBitmapEncoder encoder = new PngBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(bitmap));
			encoder.Save(stream);
			byte[] bytestream = stream.GetBuffer();
			stream.Close();
			return bytestream;
		}

		public static BitmapSource FromArray(byte[] array, bool useWrapper = false)
		{
			BitmapImage bmp = new BitmapImage();

			if (!useWrapper)
			{
				using (MemoryStream stream = new MemoryStream(array))
				{
					bmp.BeginInit();
					bmp.CacheOption = BitmapCacheOption.OnLoad;
					bmp.StreamSource = stream;
					bmp.EndInit();
					bmp.Freeze();
				}

				return bmp;
			}
			else
			{
				using (MemoryStream stream = new MemoryStream(array))
				{
					using (WrappingStream wrapper = new WrappingStream(stream))
					{
						bmp.BeginInit();
						bmp.CacheOption = BitmapCacheOption.OnLoad;
						bmp.StreamSource = wrapper;
						bmp.EndInit();
						bmp.Freeze();
					}
				}

				return bmp;
			}
		}

		public static BitmapSource FromFile(string filePath, int decodedWidth = 0, bool useWrapper = false)
		{
			BitmapImage bmp = new BitmapImage();

			if (!useWrapper)
			{
				bmp.BeginInit();
				bmp.CacheOption = BitmapCacheOption.OnLoad;
				if (decodedWidth != 0)
					bmp.DecodePixelWidth = decodedWidth;
				bmp.UriSource = new Uri(filePath);
				bmp.EndInit();
				bmp.Freeze();

				return bmp;
			}
			else
			{
				using (FileStream stream = new FileStream(filePath, FileMode.Open))
				{
					using (WrappingStream wrapper = new WrappingStream(stream))
					{
						bmp.BeginInit();
						bmp.CacheOption = BitmapCacheOption.OnLoad;
						if (decodedWidth != 0)
							bmp.DecodePixelWidth = decodedWidth;
						bmp.StreamSource = wrapper;
						bmp.EndInit();
						bmp.Freeze();
					}
				}

				return bmp;
			}
		}

		public static BitmapSource FromArray2(byte[] array, bool useWrapper = false)
		{
			BitmapSource bmp;

			using (MemoryStream stream = new MemoryStream(array))
			{
				bmp = CreateImageSource(stream);
			}

			return bmp;
		}

		public static BitmapSource CreateImageSource(Stream stream)
		{
			BitmapImage bi = new BitmapImage();
			bi.BeginInit();
			bi.CacheOption = BitmapCacheOption.OnLoad;
			bi.StreamSource = stream;
			bi.EndInit();
			bi.Freeze();

			BitmapSource prgbaSource = new FormatConvertedBitmap(bi, PixelFormats.Pbgra32, null, 0);
			WriteableBitmap bmp = new WriteableBitmap(prgbaSource);
			int w = bmp.PixelWidth;
			int h = bmp.PixelHeight;
			int[] pixelData = new int[w * h];
			//int widthInBytes = 4 * w;
			int widthInBytes = bmp.PixelWidth * (bmp.Format.BitsPerPixel / 8); //equals 4*w
			bmp.CopyPixels(pixelData, widthInBytes, 0);

			bmp.WritePixels(new Int32Rect(0, 0, w, h), pixelData, widthInBytes, 0);
			bi = null;

			return bmp;
		}

		
		public static BitmapSource FromStream(MemoryStream stream)
		{
			BitmapImage bmp = new BitmapImage();

			using (stream)
			{
				bmp.BeginInit();
				bmp.CacheOption = BitmapCacheOption.OnLoad;
				bmp.StreamSource = stream;
				bmp.EndInit();
				return bmp;
			}
		}
	} // class
} // namespace
