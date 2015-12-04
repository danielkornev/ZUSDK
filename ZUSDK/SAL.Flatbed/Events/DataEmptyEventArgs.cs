using System;

namespace SAL.Flatbed
{
	/// <summary>Класс пустых аргументов передаваемых клиенту</summary>
	public class DataEmptyEventArgs : DataEventArgs
	{
		/// <summary>Нулевая версия аргументов</summary>
		public override Int32 Version { get { return 0; } }
		/// <summary>Получение данных через аргументы</summary>
		/// <typeparam name="T">Тип требуемых данных</typeparam>
		/// <param name="key">Ключ по которому получить данные</param>
		/// <returns>Данные по аргументу.</returns>
		public override T GetData<T>(String key)
		{
			return default(T);
		}
		/// <summary>Конструктор пустых агрументов доступен только <see cref="DataEventArgs"/></summary>
		internal DataEmptyEventArgs() { }
	}
}