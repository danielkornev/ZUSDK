using System;

namespace SAL.Flatbed
{
	/// <summary>Базовый интерфейс для всех плагинов</summary>
	public interface IPlugin
	{
		#region Properties
		/// <summary>Основной модуль управления приложением</summary>
		IHost Host {get;set;}
		#endregion Properties
		#region Methods
		/// <summary>Получить сообщение от основной формы или от другого плагина</summary>
		/// <param name="message">Сообщение</param>
		/// <param name="args">Аргументы передаваемые плагину с сообщением</param>
		/// <returns>Результат выполнения операции</returns>
		Object InvokeMessage(String message, params Object[] args);
		/// <summary>Подключение модуля</summary>
		/// <param name="mode">Тип подключения модуля к хосту</param>
		/// <returns>Успех подключения модуля к системе. Иначе, сразу будт вызван процесс отключения модуля от системы</returns>
		Boolean OnConnection(ConnectMode mode);
		/// <summary>Отключение модуля</summary>
		/// <param name="mode">Тип отключения модуля</param>
		/// <returns>Успех отключения модуля</returns>
		Boolean OnDisconnection(DisconnectMode mode);
		#endregion Methods
	}
}