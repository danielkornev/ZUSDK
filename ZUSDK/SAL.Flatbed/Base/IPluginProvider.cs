using System;
using System.Reflection;

namespace SAL.Flatbed
{
	/// <summary>Интерфейс провайдера загрузчика плагинов</summary>
	public interface IPluginProvider : IPlugin
	{
		/// <summary>Родительский провайдер плагинов</summary>
		/// <remarks>При установке провайдера, если установленный до этого провайдер присутствует, то он занимает место текущего</remarks>
		IPluginProvider ParentProvider { get; set; }
		/// <summary>Загрузить плагины из хранилища</summary>
		void LoadPlugins();
		/// <summary>Запрос хоста на поиск сборки в провайдере плагинов</summary>
		/// <param name="assemblyName">Наименование сборки</param>
		/// <returns>Найденная сборка провайдером или null</returns>
		Assembly ResolveAssembly(String assemblyName);
	}
}