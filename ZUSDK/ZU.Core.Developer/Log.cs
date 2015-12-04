//		PropertyChanged(this, new PropertyChangedEventArgs(info));
using System;
using SYSD = System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using SAL.Flatbed;//--.Specialized;

namespace ZU
{
	public partial class Log : ITraceProvider, IDisposable
	{
		#region Static
		public static int IndentLevel = 0;
		public static bool UseLock = true;
		public static bool UseTrace = true;
		public static object Locker = 123;

		public static long NumErrors = 0;
		public static long NumWarnings = 0;

		public static Collection<Entry> Items = null; // new SafeCollection<Entry>();
		public static Action<Entry> OnMessage = null;
		#endregion

		#region Static methods for special purposes

		public static T Result<T>(T value, string prefix)
		{
			Log.Verbose("{1}: Result=[{0}]", value, prefix);
			return value;
		}
		public static T Result<T>(T value)
		{
			SYSD.StackFrame sf = new SYSD.StackFrame(1,true);
			string prefix = sf.GetMethod().DeclaringType.ToString() + " " + sf.GetMethod().Name+"()";
			return Result(value, prefix);
		}
		#endregion
		#region Static methods for all message levels
		public static void Message(Level level, string message, params object[] objs)
		{
			try
			{
				Entry entry = new Entry(IndentLevel, DateTime.Now, level, message, objs);
				var onm = OnMessage;
				if (onm!=null)
				{
					onm(entry);
				}
				//
				if (UseTrace)
				{
					string s = string.Format
						(
							"{0} {1,"+entry.IndentLevel+"}{2} {3}",
							StrLevel[(int)(entry.Level)],
							"",
							entry.Time.ToString("u"),
							entry.Message
						);
					SYSD.Trace.WriteLine(s);
				}
			}
			catch
			{
				// Yes, ignoring exceptions is lame. But sometimes...
			}
		}
		public static void Fatal(string message, params object[] objs)
		{
			NumErrors++;
			Message(Level.Fatal, message, objs);
		}
		public static void Error(string message, params object[] objs)
		{
			NumErrors++;
			Message(Level.Error, message, objs);
		}
		public static void Warn(string message, params object[] objs)
		{
			NumWarnings++;
			Message(Level.Warn, message, objs);
		}
		public static void Warning(string message, params object[] objs)
		{
			NumWarnings++;
			Message(Level.Warn, message, objs);
		}
		public static void Notify(string message, params object[] objs)
		{
			Message(Level.Notify, message, objs);
		}
		public static void Info(string message, params object[] objs)
		{
			Message(Level.Info, message, objs);
		}
		public static void Debug(string message, params object[] objs)
		{
			Message(Level.Debug, message, objs);
		}
		public static void Verbose(string message, params object[] objs)
		{
			Message(Level.Verbose, message, objs);
		}
		public static void Exception(string message, Exception ex, params object[] objs)
		{
			Error(message, objs);
			Debug("EXCEPTION: {0}", ex);
		}
		public static void Exception(Exception ex)
		{
			Error("EXCEPTION: {0}", ex.Message);
			Debug("EXCEPTION: {0}", ex);
		}
		#endregion
		#region Spec
		public SYSD.Stopwatch StartTimer;
		public long StartMemory;
		#endregion

		#region Constructor
		private void LogStart(string message, params object[] objs)
		{
			StartMemory = GC.GetTotalMemory(false); // Environment.WorkingSet;
			StartTimer = new SYSD.Stopwatch();
			Message(Level.Debug, message, objs);
			IndentLevel++;
			StartTimer.Start();
		}
		/// <summary>
		/// Constructor
		/// </summary>
		public Log()
		{
			SYSD.StackFrame sf = new SYSD.StackFrame(1,true);
			string message = sf.GetMethod().DeclaringType.ToString() + " " + sf.GetMethod().Name+"()"; //ToString();
			if (!string.IsNullOrEmpty(sf.GetFileName()))
				message = message + " [" + sf.GetFileName() + ":"+sf.GetFileLineNumber().ToString() + "]";

			// we save reference to our singleton for poors
			Log.Instance = this;

			//
			LogStart(message);
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">ProcessStart message</param>
		/// <param name="objs">Objects array for string.Format()</param>
		public Log(string message, params object[] objs)
		{
			LogStart(message, objs);
		}
		#endregion
		#region IDisposable
		///<summary>
		/// Finalizer, IDisposable support
		///</summary>
		~Log()
		{
			Dispose(false);
		}
		///<summary>
		/// IDisposable support
		///</summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		///<summary>
		/// IDisposable support
		///</summary>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				StartTimer.Stop();
				IndentLevel--;
				double elapsed = (double)StartTimer.Elapsed.TotalSeconds;
				double memDelta = GC.GetTotalMemory(false) /*Environment.WorkingSet*/ - StartMemory;
				Message(Level.Debug, "{0:0.0000} / {1:0,000}", elapsed, memDelta);
			}
		}
		#endregion
	} // class
} // namespace
