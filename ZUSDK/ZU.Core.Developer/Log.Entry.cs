using System;
using System.ComponentModel;
using System.Diagnostics;

namespace ZU
{
	partial class Log
	{
		public struct Entry
		{
			#region Properties
			public int IndentLevel {get; set;}
			public DateTime Time {get; set;}
			public Level Level {get; set;}
			public string Message {get; set;}
			#endregion
			#region .ctor
			public Entry(int indentLevel, DateTime time, Level level, string message, params object[] objs):this()
			{
				IndentLevel = indentLevel;
				Time = time;
				Level = level;
				if ( (objs!=null) && (objs.Length>0) )
					Message = string.Format(message, objs);
				else
					Message = message;
			}
			#endregion
			//
			public override string ToString()
			{
				return string.Format("{0} {1} {2}", Time.ToString("u"), Level, Message);
			}
		}
	} // class
} // namespace
