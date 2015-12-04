using System;

namespace SAL.Flatbed
{
	/// <summary>Провайдер событий трасировки</summary>
	public interface ITraceProvider
	{
		/// <summary>Отобразить исключение в основной форме приложения</summary>
		/// <param name="source">Объект в котором произошло исключение</param>
		/// <param name="exc">Исключние для отображения</param>
		void Add(Object source, Exception exc);
		/// <summary>Добавить сообщение для логирования</summary>
		/// <param name="type">Тип добавляемого сообщения</param>
		/// <param name="source">Источник сообщения</param>
		/// <param name="message">Сообщение</param>
		void Add(EventType type, Object source, String message);
	}
}