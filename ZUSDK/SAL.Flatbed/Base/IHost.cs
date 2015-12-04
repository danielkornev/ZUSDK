using System;
using System.Collections;

namespace SAL.Flatbed
{
	/// <summary>Интерфейс сохранения, загрузки настроек плагина</summary>
	public interface IHost : IHostItem
	{
		#region Properties
		/// <summary>Глобальное кеширование данных для всех пользователей</summary>
		IDictionary Cache { get; set; }
		/// <summary>Массив загруженных плагинов</summary>
		IPluginStorage Plugins { get; }
		/// <summary>Трассировщик</summary>
		ITraceProvider Trace { get; }
		#endregion Properties
		#region Methods
		/// <summary>Закрытие всех загруженных плагинов, путём вызова метода <see cref="T:OnDisconnection"/></summary>
		/// <param name="reason">Причина выгрузки всех плагинов</param>
		void UnloadPlugin(DisconnectMode reason);
		/// <summary>Выгрузить плагин из списка плагинов</summary>
		/// <param name="plugin">Интерфейс плагина, который надо выгрузить</param>
		/// <returns>Плагин выгрузился успешно или плагин не найден или плагин не может быть отключен в данный момент</returns>
		Boolean UnloadPlugin(IPluginBase plugin);
		/// <summary>Добавить объект в сессию</summary>
		/// <param name="name">Ключ добавляемого объекта в сессию</param>
		/// <param name="value">Значение добавляемого объекта в сессию</param>
		void SetSessionValue(String name, Object value);
		/// <summary>Получить объект из сессии</summary>
		/// <param name="name">Ключ объекта в сессии</param>
		/// <returns>Значение объекта в сессии</returns>
		Object GetSessionValue(String name);
		#endregion Methods
	}
}