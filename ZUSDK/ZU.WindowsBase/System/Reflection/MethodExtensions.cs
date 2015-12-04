using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace System.Reflection
{
	public static class MethodExtensions
	{
		public static void Measure(String message, Action method)
		{
			Int64 mem = GC.GetTotalMemory(true);
			Int64 gcCount = GC.CollectionCount(0);

			Stopwatch stopwatch = Stopwatch.StartNew();
			method();
			TimeSpan elapsed = stopwatch.Elapsed;

			// DONTTOUCH:
			//  It is intended to call GetTotalMemory with forceFullCollection: false
			//  before CollectionCount call.
			Double tempMemDelta = Math.Max(GC.GetTotalMemory(false) - mem, 0);
			Int64 gcCountDelta = GC.CollectionCount(0) - gcCount;

			String elapsedString = elapsed.TotalSeconds.ToString("00.0000", CultureInfo.InvariantCulture);
			String memDeltaInKb = (tempMemDelta / 1024).ToString("0.00 KB", CultureInfo.InvariantCulture);

			//Log.Info(@"{0}: Elapsed: {1} s,  MemDelta: {2,10},  GC count: {3}", message, elapsedString, memDeltaInKb, gcCountDelta);
			//Process.GetCurrentProcess().Kill();
		}

		public static void Measure(String message, Func<byte[], bool, BitmapSource> method, byte[] array, bool val1, out BitmapSource img)
		{
			Int64 mem = GC.GetTotalMemory(true);
			Int64 gcCount = GC.CollectionCount(0);

			Stopwatch stopwatch = Stopwatch.StartNew();
			img = method(array, val1);
			TimeSpan elapsed = stopwatch.Elapsed;

			// DONTTOUCH:
			//  It is intended to call GetTotalMemory with forceFullCollection: false
			//  before CollectionCount call.
			Double tempMemDelta = Math.Max(GC.GetTotalMemory(false) - mem, 0);
			Int64 gcCountDelta = GC.CollectionCount(0) - gcCount;

			String elapsedString = elapsed.TotalSeconds.ToString("00.0000", CultureInfo.InvariantCulture);
			String memDeltaInKb = (tempMemDelta / 1024).ToString("0.00 KB", CultureInfo.InvariantCulture);

			//Log.Info(@"{0}: Elapsed: {1} s,  MemDelta: {2,10},  GC count: {3}", message, elapsedString, memDeltaInKb, gcCountDelta);
		}
	} // class
} // namespace
