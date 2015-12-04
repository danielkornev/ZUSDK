using System;
using System.IO;
using System.Collections.Generic;

namespace SAL.Flatbed
{
	/// <summary>Интерфейс провайдера сохранения данных в жранилище</summary>
	/// <remarks>Параметры могут лежать как в XML файле, так и в хранилище БД</remarks>
	public interface ISettingsProvider : IPlugin
	{
		/// <summary>Родительский провайдер настроек</summary>
		/// <remarks>При установке провайдера, если установленный до этого провайдер присутствует, то он занимает место текущего</remarks>
		ISettingsProvider ParentProvider { get; set; }
		/// <summary>Получить значение параметра из настроек</summary>
		/// <param name="plugin">Интерфейс плагина, значение параметра которого необходимо получить</param>
		/// <param name="key">Ключ, по которому получить значение</param>
		/// <returns>Значение сохранённое в хранилище или null</returns>
		Object LoadAssemblyParameter(IPlugin plugin, String key);
		/// <summary>Получить заначение большого объекта из настроек</summary>
		/// <param name="plugin">Интерфейс плагина, значение параметра которого необходимо получить</param>
		/// <param name="key">Ключ, по которому получить значение большого объекта</param>
		/// <returns>Данные большого объекта</returns>
		Stream LoadAssemblyBlob(IPlugin plugin, String key);
		/// <summary>Загрузка параметров сборки из хранилища</summary>
		/// <param name="plugin">Плагин, для которого загрузить настройки</param>
		/// <param name="settings">Объект настроек плагина</param>
		void LoadAssemblyParameters<T>(IPlugin plugin, T settings) where T : class;
		/// <summary>Получить массив параметров из хранилища параметров</summary>
		/// <param name="plugin">Интерфейс плагина, ключи и значения параметров которого необходимо получить</param>
		/// <returns>Массив значений параметров</returns>
		IEnumerable<KeyValuePair<String,Object>> LoadAssemblyParameters(IPlugin plugin);
		/// <summary>Сохранение параметров плагина в хранилище</summary>
		/// <param name="plugin">Плагин, настройки которого сохраняются</param>
		void SaveAssemblyParameters(IPlugin plugin);
		/// <summary>Сохранить параметр в хранилище по интерфейсу плагина</summary>
		/// <remarks>При сохранении <see cref="T:Enum"/>'ов они преобразуются в соответствующий простой тип</remarks>
		/// <param name="plugin">Интерфейс плагина, параметр по которому сохранить</param>
		/// <param name="key">Ключ значения</param>
		/// <param name="value">Значение</param>
		void SaveAssemblyParameter(IPlugin plugin, String key, Object value);
		/// <summary>Сохранить большой объект в хранилище по интерфейсу плагина</summary>
		/// <param name="plugin">Интерфейс плагина, параметр по которому сохранить</param>
		/// <param name="key">Ключ значения</param>
		/// <param name="value">Значение большого объекта</param>
		void SaveAssemblyBlob(IPlugin plugin, String key, Stream value);
		/// <summary>Удалить параметр из настроек плагина</summary>
		/// <param name="plugin">Плагин, у которого необходимо удалить значение</param>
		/// <param name="key">Ключ значения для удаления</param>
		/// <returns>Удаление параметра произошло успешно</returns>
		Boolean RemoveAssemblyParameter(IPlugin plugin, String key);
		/// <summary>Удалить большой объект из хранилища по интерфейсу плагина</summary>
		/// <param name="plugin">Интерфейс плагина, параметр по которому удалить</param>
		/// <param name="key">Ключ большого объекта для удаления</param>
		/// <returns>Удаление большого объекта прошло успешно</returns>
		Boolean RemoveAssemblyBlob(IPlugin plugin, String key);
		/// <summary>Удаление всех параметров для сборки</summary>
		/// <param name="plugin">Интерфейс плагина, параметры по которому необходимо удалить</param>
		void RemoveAssemblyParameter(IPlugin plugin);
	}
}