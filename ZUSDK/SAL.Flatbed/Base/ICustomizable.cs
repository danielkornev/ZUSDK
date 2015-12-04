using System;

namespace SAL.Flatbed
{
	/// <summary>Интерфей настройки плагина. Плагин настраиваемого типа</summary>
	public interface ICustomizable
	{
		/// <summary>Объект настроек</summary>
		/// <remarks>Пропертя захардкодена в констанатах, т.к. доступ идёт через рефлексию к генерику</remarks>
		Object Settings { get; set; }
	}
}