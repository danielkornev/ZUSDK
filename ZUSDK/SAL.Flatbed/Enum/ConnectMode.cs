using System;

namespace SAL.Flatbed
{
	/// <summary>Informs the add-in about how it was loaded</summary>
	public enum ConnectMode
	{
		/// <summary>The add-in was loaded after Flatbed was loaded</summary>
		AfterStartup=0,
		/// <summary>The add-in was loaded when Flatbed was started</summary>
		Startup=1,
	}
}