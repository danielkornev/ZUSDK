using System;

namespace SAL.Flatbed
{
	/// <summary>Аргументы передаваемые через событие</summary>
	public abstract class DataEventArgs : EventArgs
	{
		/// <summary>Пустые аргументы</summary>
		public static new readonly DataEventArgs Empty = new DataEmptyEventArgs();
		/// <summary>Версия</summary>
		public abstract Int32 Version { get; }

		/// <summary>Получение данных с сервера</summary>
		/// <typeparam name="T">Тип ожидаемых данных</typeparam>
		/// <param name="key">Ключ данных</param>
		/// <returns>Значение</returns>
		public abstract T GetData<T>(String key);
	}
}