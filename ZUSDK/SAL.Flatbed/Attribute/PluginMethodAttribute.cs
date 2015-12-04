using System;
using System.Runtime.InteropServices;

namespace SAL.Flatbed
{
	/// <summary>Атрибут определяющий публичный метод плагина</summary>
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class PluginMethodAttribute : Attribute
	{
		/// <summary>Наименование публичного метода, если оно отличается от наименования метода по умолчанию</summary>
		public String Name { get; set; }
		/// <summary>Идентификатор плагина, для которого доступен вызов этого метода</summary>
		public String PluginId { get; set; }
	}
}