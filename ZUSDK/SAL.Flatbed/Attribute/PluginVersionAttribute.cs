using System;
using System.Runtime.InteropServices;

namespace SAL.Flatbed
{
	/// <summary>Атрибут версионности плагина по отношению к версии</summary>
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class PluginVersionAttribute : Attribute
	{
		private readonly Int32 _version;
		/// <summary>Версия интерфейса, под которую собирался плагин</summary>
		public Int32 Version { get { return this._version; } }
		private readonly Boolean _showErrorIfDifferent;
		/// <summary>Отобразить ошибку и не загружать плагин, если версия отличается от версии плагина</summary>
		public Boolean ShowErrorIfDifferent { get { return this._showErrorIfDifferent; } }
		/// <summary>Создание атрибута версии</summary>
		/// <param name="version">Версия плагина, который собирается под версию базового интерфейса</param>
		/// <param name="showErrorIfDifferent">Отобразить ошибку и не загружать плагин, если версия отличается от интерфейса</param>
		public PluginVersionAttribute(Int32 version, Boolean showErrorIfDifferent)
		{
			this._version = version;
			this._showErrorIfDifferent = showErrorIfDifferent;
		}
	}
}