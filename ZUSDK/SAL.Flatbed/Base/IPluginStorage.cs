using System;
using System.Collections.Generic;
using System.Reflection;

namespace SAL.Flatbed
{
	/// <summary>Интерфейс хранилища плагинов</summary>
	public interface IPluginStorage : IEnumerable<IPluginBase>
	{
		#region Properties
		/// <summary>Олицетворённый пользователь хоста</summary>
		IImpersonate User { get; }
		/// <summary>Получить плагин по идентификатору плагина</summary>
		/// <param name="pluginId">Идентификатор плагина</param>
		/// <returns>Интерфейс плагина по идентификатору плагина</returns>
		IPluginBase this[String pluginId] { get; }
		/// <summary>Получить кол-во всех плагинов</summary>
		Int64 Count{get;}
		/// <summary>Интерфейс загрузчика плагинов</summary>
		IPluginProvider PluginProvider { get; }
		/// <summary>Интерфейс загрузчика параметров</summary>
		ISettingsProvider SettingsProvider { get; }
		#endregion Properties
		#region Events
		/// <summary>Событие загрузки всех плагинов хостом</summary>
		event EventHandler PluginsLoaded;
		#endregion Events
		#region Methods
		/// <summary>Удалить плагин из массива плагинов</summary>
		/// <param name="plugin">Интерфейс базового плагина для удаления</param>
		void RemovePlugin(IPluginBase plugin);
		/// <summary>Удалить все плагины из коллекции</summary>
		void RemovePlugins();
		/// <summary>Добавить плагин в коллекцию</summary>
		/// <param name="assembly">Загружаемая сборка</param>
		/// <param name="source">Источник получения плагина</param>
		/// <param name="mode">Режим подключения плагина</param>
		void LoadPlugin(Assembly assembly, String source, ConnectMode mode);
		/// <summary>Инициализация всех плагинов после загрузки</summary>
		void InitializePlugins();
		/// <summary>Найти все плагины определённого типа</summary>
		/// <typeparam name="T">Тип плагина для поиска</typeparam>
		/// <returns>Массив найдненных плагинов</returns>
		IEnumerable<IPluginBase> FindPluginType<T>() where T : IPlugin;
		/// <summary>Передать сообщение плагину</summary>
		/// <param name="pluginId">Наименование плагина</param>
		/// <param name="msg">Сообщение для передачи плагину</param>
		/// <param name="args">Аргументы передаваемые плагину</param>
		/// <returns>Результат выполнения операции</returns>
		Object SendMessage(String pluginId, String msg, params Object[] args);
		/// <summary>Олицетворить текущего пользователя ядром</summary>
		/// <param name="userName">Логин пользователя</param>
		/// <param name="password">Пароль пользователя</param>
		/// <param name="tag">Дополнительная информация о пользователе и о выбранных настройках связанных с безопасностью</param>
		/// <exception cref="T:ApplicationException">Если пользователь уже авторизован, а модуль попытается авторизовать его ещё раз</exception>
		void ImpersonateUser(String userName, String password, Object tag);
		/// <summary>Установить провайдер настроек</summary>
		/// <param name="plugin">Плагин который устанавливается в качестве провайдера настроек</param>
		void SetSetingsProvider(IPluginBase plugin);
		/// <summary>Установить провайдер плагинов</summary>
		/// <param name="plugin">Плагин который устанавливается в качестве провайдера плагинов</param>
		void SetPluginProvider(IPluginBase plugin);
		#endregion Methods
	}
}