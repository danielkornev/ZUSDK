using System;
using System.Collections;
using System.Reflection;

namespace SAL.Flatbed
{
	/// <summary>Базовый абстрактный класс для хоста</summary>
	public abstract class HostBase : IHost, IDisposable
	{
		private PluginStorage _storage;
		#region IHost Members
		/// <summary>Объект, который инкапсулируется интерфейсом</summary>
		public abstract Object Object { get; }
		/// <summary>Глобальное кеширование данных для всех пользователей экземпляром хоста</summary>
		public abstract IDictionary Cache { get; set; }
		/// <summary>Провайдер трассировщика хоста</summary>
		public abstract ITraceProvider Trace { get; }
		/// <summary>Массив загруженных плагинов</summary>
		public virtual IPluginStorage Plugins
		{
			get
			{
				if(this._storage == null)
				{
					this._storage = new PluginStorage(this);
					AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
				}
				return this._storage;
			}
		}
		/// <summary>Выгрузить все плагины</summary>
		/// <param name="reason">Причина выгрузки всех плагинов</param>
		public abstract void UnloadPlugin(DisconnectMode reason);
		/// <summary>Выгрузить специфичный плагин</summary>
		/// <param name="plugin">Интерфейс описателя плагина</param>
		/// <returns>Результат выгрузки плагина</returns>
		public abstract Boolean UnloadPlugin(IPluginBase plugin);
		/// <summary>Добавить объект в сессию</summary>
		/// <param name="name">Ключ добавляемого объекта в сессию</param>
		/// <param name="value">Значение добавляемого объекта в сессию</param>
		public abstract void SetSessionValue(String name, Object value);
		/// <summary>Получить объект из сессии</summary>
		/// <param name="name">Ключ объекта в сессии</param>
		/// <returns>Значение объекта в сессии</returns>
		public abstract Object GetSessionValue(String name);

		/// <summary>Resolve assembly from loaded plugins</summary>
		/// <param name="sender">Sender assembly</param>
		/// <param name="args">Arguments with assembly identity data</param>
		/// <returns>Resolved assembly from loaded plugins</returns>
		public virtual Assembly CurrentDomain_AssemblyResolve(Object sender, ResolveEventArgs args)
		{
			foreach(IPluginBase plugin in this.Plugins)
			{
				Assembly asm = plugin.Instance.GetType().Assembly;
				if(asm.FullName == args.Name)
					return asm;
			}

			if(this.Plugins.PluginProvider != null)
				return this.Plugins.PluginProvider.ResolveAssembly(args.Name);
			else
				return null;
		}
		/// <summary>Выгрузить все плагины</summary>
		public void Dispose()
		{
			this.UnloadPlugin(DisconnectMode.HostShutdown);
			ISettingsProvider settings = this.Plugins.SettingsProvider;
			while(settings != null)
			{
				settings.OnDisconnection(DisconnectMode.FlatbedClosed);
				settings = settings.ParentProvider;
			}

			IPluginProvider plugins = this.Plugins.PluginProvider;
			while(plugins != null)
			{
				plugins.OnDisconnection(DisconnectMode.FlatbedClosed);
				plugins = plugins.ParentProvider;
			}

			AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(CurrentDomain_AssemblyResolve);

			GC.SuppressFinalize(this);
		}
		
		#endregion
	}
}