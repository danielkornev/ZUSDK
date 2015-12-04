using System;
using System.Runtime.InteropServices;

namespace SAL.Flatbed
{
	/// <summary>Описание точки входа для класса плагина, если в плагине есть несколько классов реализации плагина</summary>
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class PluginEntryPointAttribute : Attribute
	{
		private readonly Version _version;
		/// <summary>Версия класса плагина</summary>
		public Version Version { get { return this._version; } }
		private readonly String _name;
		/// <summary>Наименование класса плагина</summary>
		public String Name { get { return this._name; } }
		private readonly String _description;
		/// <summary>Описание класса плагина</summary>
		public String Description { get { return this._description; } }
		/// <summary>Создание экземпляра класса описателя класса плагина</summary>
		/// <param name="version">Версия класса лагина</param>
		/// <param name="name">Наименование класса плагина</param>
		/// <param name="description">Описание класса плагина</param>
		public PluginEntryPointAttribute(String version, String name, String description)
		{
			this._version = new Version(version);
			this._name = name;
			this._description = description;
		}
	}
}