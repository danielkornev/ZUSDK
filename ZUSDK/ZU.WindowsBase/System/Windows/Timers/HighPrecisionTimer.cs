using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FILETIME = Microsoft.Win32.FILETIME;

namespace Microsoft.Win32
{
	/// <summary>
	/// From http://pinvoke.net/default.aspx/Structures/FileTime.html
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct FILETIME
	{
		private long timestamp;
		public DateTime Local
		{
			get { return DateTime.FromFileTime(this.timestamp); }
			set { this.timestamp = value.ToFileTime(); }
		}
		public DateTime Utc
		{
			get { return DateTime.FromFileTimeUtc(this.timestamp); }
			set { this.timestamp = value.ToFileTimeUtc(); }
		}
	}
}

namespace System.Windows
{
	public static class HighPrecisionTimer
	{
		#region Imports
		[DllImport("ntdll.dll", SetLastError = true)]
		static extern uint NtQuerySystemTime(out long SystemTime);

		[DllImport("kernel32.dll")]
		static extern void GetSystemTimeAsFileTime(out FILETIME
		   lpSystemTimeAsFileTime);

		[DllImport("Kernel32.dll", CallingConvention = CallingConvention.Winapi)]
		private static extern void GetSystemTimePreciseAsFileTime(out long filetime);

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceFrequency(out long lpFrequency);
		#endregion

		#region Using NtQuerySystemTime or GetSystemTimeAsFileTime APIs
		/// <summary>
		/// In Windows Vista, Windows 7 returns DateTime obtained using Windows NT API NtQuerySystemTime
		/// In Windows 8 and later returns DateTime obtained using Win32 API GetSystemTimeAsFileTime
		/// Otherwise returns 
		/// </summary>
		/// <param name="useUtc"></param>
		/// <returns></returns>
		public static DateTime GetHighPrecisionDateTime(bool useUtc, out DateTimePrecisionVersion precisionVersion)
		{
			long timestamp;

			return GetHighPrecisionTimestamp(useUtc, out precisionVersion, out timestamp);
		}

		public static DateTime UtcNow
		{
			get
			{
				DateTimePrecisionVersion ver;

				return GetHighPrecisionDateTime(true, out ver);
			}
		}

		public static long GetHighPrecisionTimestamp(bool useUtc, out DateTimePrecisionVersion precisionVersion)
		{
			long timestamp;
			GetHighPrecisionTimestamp(useUtc, out precisionVersion, out timestamp);

			return timestamp;
		}


		/// <summary>
		/// In Windows Vista, Windows 7 returns DateTime obtained using Windows NT API NtQuerySystemTime
		/// In Windows 8 and later returns DateTime obtained using Win32 API GetSystemTimeAsFileTime
		/// Otherwise returns 
		/// </summary>
		/// <param name="useUtc"></param>
		/// <returns></returns>
		private static DateTime GetHighPrecisionTimestamp(bool useUtc, out DateTimePrecisionVersion precisionVersion, out long timestamp)
		{
			if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor <= 1) // Windows Vista (6.0), Windows 7 (6.1)
			{
				// we use NtQuerySystemTime
				long t;
				NtQuerySystemTime(out t);

				var dateTime = DateTime.FromFileTime(t);

				if (useUtc)
				{
					dateTime = dateTime.ToUniversalTime();
				}

				precisionVersion = DateTimePrecisionVersion.WindowsVistaAndWindows7;
				timestamp = t;

				return dateTime;
			}
			else if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 2
				||
				Environment.OSVersion.Version.Major == 10
				) // Windows 8 (6.2), Windows 8.1 (6.3), Windows 10 (6.4 or 10.0)
			{
				// we use GetSystemTimePreciseAsFileTime
				long filetime;
				GetSystemTimePreciseAsFileTime(out filetime);


				//FILETIME myTime = new FILETIME();
				//GetSystemTimeAsFileTime(out myTime);

				precisionVersion = DateTimePrecisionVersion.Windows8AndLater;
				timestamp = filetime;

				if (useUtc)
				{
					return DateTime.FromFileTimeUtc(filetime);
				}

				else
				{
					return DateTime.FromFileTimeUtc(filetime);
				}
			}

			precisionVersion = DateTimePrecisionVersion.Legacy;

			if (useUtc)
			{
				var utcNow = DateTime.UtcNow;
				timestamp = utcNow.ToFileTime();

				return utcNow;
			}
			else
			{
				var now = DateTime.Now;
				timestamp = now.ToFileTime();

				return now;
			}
		}
		#endregion

		public enum DateTimePrecisionVersion
		{
			Legacy,
			WindowsVistaAndWindows7,
			Windows8AndLater
		}
	} // class
} // namespace
