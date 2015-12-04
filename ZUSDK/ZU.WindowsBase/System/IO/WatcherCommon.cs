using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System.IO
{
	////////////////////////////////////////////////////////////////////////////////////// 
	////////////////////////////////////////////////////////////////////////////////////// 
	/// <summary>
	/// This enum is used to indicate which argument type is valid in the WatcherEventArgs 
	/// object.
	/// </summary>
	public enum ArgumentType { FileSystem, Renamed, Error, StandardEvent, PathAvailability };
}
