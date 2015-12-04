using System;
using System.Reflection;

namespace SAL.Flatbed
{
	/// <summary>Базовый интерфейс плагина, которы предоставляет расширеную информациюо плагине</summary>
	public interface IPluginBase
	{
		#region Properties
		/// <summary>Уникальный идентификатор плагина.</summary>
		/// <remarks>Пока Name является уникальным идентификатором плагина</remarks>
		String ID { get; }
		/// <summary>Источник получения плагина</summary>
		String Source { get; }
		/// <summary>Экземпляр загруженного плагина</summary>
		IPlugin Instance { get; }
		/// <summary>Наименование планина</summary>
		String Name { get; }
		/// <summary>Версия плагина</summary>
		Version Version { get; }
		/// <summary>Описание сборки</summary>
		String Description { get; }
		/// <summary>Создатель сборки</summary>
		String Company { get; }
		/// <summary>Копирайт плагина</summary>
		String Copyright { get; }
		#endregion Properties
		#region Methods
		/// <summary>Привязать событие к публичному событию плагина</summary>
		/// <param name="eventName">Наименование события</param>
		/// <param name="handler">Обработчик события в коде вызывающего плагина</param>
		void AddEventHandler(String eventName, EventHandler<DataEventArgs> handler);
		/// <summary>Удалить обработчик события от публичного события плагина</summary>
		/// <param name="eventName">Наименование события</param>
		/// <param name="handler">Обработчик события который необходимо удалить</param>
		void RemoveEventHandler(String eventName, EventHandler<DataEventArgs> handler);
		#endregion Methods
	}
}