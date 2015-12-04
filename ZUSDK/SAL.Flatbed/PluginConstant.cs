using System;

namespace SAL.Flatbed
{
	/// <summary>Константы относящиеся к ядру плагина</summary>
	public struct PluginConstant
	{
		/// <summary>Путь к базовому интерфейсу плагина</summary>
		public static String PluginInterface = typeof(IPlugin).FullName;
		/// <summary>Версия интерфейса, которую необходимо использовать в атрибуте <see cref="T:PluginVersionAttribute"/></summary>
		public const Int32 InterfaceVersion = 0;
	}
}