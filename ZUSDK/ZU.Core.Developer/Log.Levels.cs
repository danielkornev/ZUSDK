namespace ZU
{
	partial class Log
	{
		public static string StrHeader =
			//"#\\!nCaps.Log";
			"---[LOG]---";
		public static string[] StrLevel = new string[]
			{"FATAL","ERROR","WARNING","INFO","DEBUG","#####","?????"};
			//{"@@@@f","@@@@e","@@@@w","!!!!i","----d","****v"};
		/// <summary>
		/// Enumeration of levels that can be logged to
		/// </summary>
		public enum Level
		{
			/// <summary>
			/// The Fatal Level
			/// </summary>
			/// <remarks>
			/// This level should be used when an unexpected error occurs that cannot be recovered from, requires immediate human intervention, and the application will most likely shutdown.
			/// </remarks>
			Fatal = 0,
			/// <summary>
			/// The Error Level 
			/// </summary>
			/// <remarks>
			/// This level should be used when an unexpected error occurs that cannot be recovered from and likely requires human intervention.
			/// But the application may not shutdown if the error is transitive (i.e. database down).
			/// </remarks>
			Error = 1,
			/// <summary>
			/// The Warning Level 
			/// </summary>
			/// <remarks>
			/// This level should be used when an unexpected error occurs that can be recovered from and a human should look at the issue at their earliest possible convenience.
			/// </remarks>
			Warn = 2,
			/// <summary>
			/// The Inform Level 
			/// </summary>
			/// <remarks>
			/// This level should be used for reporting normal running information to the end user.
			/// This can include order ids being processed, RSS urls being parsed, etc.
			/// </remarks>
			Info = 3,
			/// <summary>
			/// The Debug Level 
			/// </summary>
			/// <remarks>
			/// This level should be used for basic, high level debugging statements.
			/// Such as, entering and leaving a method, direction taking in conditional statements, events sent or received, and important objects.
			/// </remarks>
			Debug = 4,
			/// <summary>
			/// The Verbose Level
			/// </summary>
			/// <remarks>
			/// This level should be used for debugging that generates large amounts of output.
			/// Such as, input events, state of a loop (i.e. current index), method parameters, etc.
			/// </remarks>
			Verbose = 5,
			/// <summary>
			/// The Special Level
			/// </summary>
			Special = 6,
			/// <summary>
			/// User Notification Level
			/// </summary>
			Notify = 7
		} 
	} // enum
} // namespace
