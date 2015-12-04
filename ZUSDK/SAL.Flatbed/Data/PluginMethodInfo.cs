using System;
using System.Reflection;
using System.Collections.Generic;

namespace SAL.Flatbed
{
	/// <summary>Информация по публичному методу плагина</summary>
	public class PluginMethodInfo
	{
		private readonly IPluginBase _plugin;
		private readonly MemberInfo _member;

		/// <summary>Информация по плагину</summary>
		protected IPluginBase Plugin { get { return this._plugin; } }
		/// <summary>Информация по методу через рефлексию</summary>
		protected MemberInfo Member { get { return this._member; } }

		/// <summary>Конструктор класса публичного метода плпгина</summary>
		/// <param name="plugin">Информация по плагину</param>
		/// <param name="member">Рефлексия метода</param>
		public PluginMethodInfo(IPluginBase plugin, MemberInfo member)
		{
			if(plugin == null)
				throw new ArgumentNullException("plugin");
			if(member == null)
				throw new ArgumentNullException("member");

			this._plugin = plugin;
			this._member = member;
		}
		/// <summary>Получить все атрибуты метода</summary>
		/// <typeparam name="A">Тип атрибута, который надо получить</typeparam>
		/// <returns>Массив атрибутов применённых к методу</returns>
		public IEnumerable<A> GetMemberAttributes<A>() where A : Attribute
		{
			foreach(Object attribute in this.Member.GetCustomAttributes(typeof(A), false))
				yield return (A)attribute;
		}
	}
}