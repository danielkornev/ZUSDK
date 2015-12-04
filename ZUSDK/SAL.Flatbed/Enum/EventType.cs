using System;

namespace SAL.Flatbed
{
	/// <summary>Тип сообщения</summary>
	public enum EventType
	{
		/// <summary>Логирование</summary>
		Log,
		/// <summary>Статус сообщение</summary>
		Status,
		/// <summary>Нотификация польователя</summary>
		Notify,
	}
}