using System;

namespace SAL.Flatbed
{
	/// <summary>Отключение плагина от хоста</summary>
	public enum DisconnectMode
	{
		/// <summary>The add-in was unloaded by unknown reason</summary>
		/// <remarks>If this event recieved it must be closed without any answers</remarks>
		Unknown = 0,
		/// <summary>The add-in was unloaded with OS</summary>
		HostShutdown = 1,
		/// <summary>The add-in was unloaded when Flatbed is closed by user</summary>
		FlatbedClosed = 2,
		/// <summary>The add-in was unloaded by a user</summary>
		UserClosed = 3,
	}
}