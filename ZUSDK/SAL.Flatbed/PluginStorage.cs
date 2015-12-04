using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;

namespace SAL.Flatbed
{
	/// <summary>Базовый класс хранилища плагинов</summary>
	public class PluginStorage : IPluginStorage, IEnumerable<IPluginBase>
	{
		#region Fields
		private readonly IHost _host;
		private readonly Dictionary<String, IPluginBase> _plugins;
		private IPluginBase _pluginProvider;
		private IPluginBase _settingsProvider;
		private Impersonate _impersonate;
		#endregion Fields

		/// <summary>Олицетворённый пользователь хоста</summary>
		public IImpersonate User { get { return this._impersonate; } }
		
		/// <summary>Массив загруженных плагинов</summary>
		private IDictionary<String, IPluginBase> Plugins { get { return this._plugins; } }
		/// <summary>Получить плагин по идентификатору плагина</summary>
		/// <param name="pluginId">Идентификатор плагина</param>
		/// <returns>Интерфейс плагина по идентификатору плагина или null, если pluginId null или пустой</returns>
		public IPluginBase this[String pluginId]
		{
			get
			{
				if(String.IsNullOrEmpty(pluginId))
					return null;

				IPluginBase result;
				return this.Plugins.TryGetValue(pluginId, out result) ? result : null;
			}
		}
		/// <summary>Получить кол-во плагинов</summary>
		public Int64 Count { get { return this.Plugins.Count; } }
		/// <summary>Интерфейс загрузчика плагинов</summary>
		public IPluginProvider PluginProvider { get { return this._pluginProvider == null ? null : (IPluginProvider)this._pluginProvider.Instance; } }
		/// <summary>Интерфейс загрузчика параметров</summary>
		public ISettingsProvider SettingsProvider
		{
			get { return this._settingsProvider == null ? null : (ISettingsProvider)this._settingsProvider.Instance; }
			/*private set
			{
				if(value == null) throw new ArgumentNullException("SettingsProvider");
				else
				{//Установка родительского загрузчика
					value.Instance.ParentProvider = this._settingsProvider.Instance;
					this._settingsProvider = value;
				}
			}*/
		}
		private EventHandler PluginsLoadedDelegate;
		/// <summary>Событие загрузки всех плагинов хостом</summary>
		public event EventHandler PluginsLoaded
		{
			add { this.PluginsLoadedDelegate += value; }
			remove { this.PluginsLoadedDelegate -= value; }
		}
		/// <summary>Создание базового класса хранилища плагинов</summary>
		/// <param name="host">Хост текущего приложения</param>
		/// <exception cref="ArgumentNullException">host is null</exception>
		public PluginStorage(IHost host)
		{
			if(host == null)
				throw new ArgumentNullException("Host is null");

			this._host = host;
			this._plugins = new Dictionary<String, IPluginBase>();
		}

		/// <summary>Найти все плагины определённого типа</summary>
		/// <typeparam name="T">Тип плагина для поиска</typeparam>
		/// <returns>Массив найдненных плагинов</returns>
		public IEnumerable<IPluginBase> FindPluginType<T>() where T : IPlugin
		{
			Trace.TraceInformation("Searching for pluginType {0}...", typeof(T));

			foreach(IPluginBase plugin in this.Plugins.Values)
				if(plugin.Instance is T)
					yield return plugin;
		}
		/// <summary>Передать сообщение плагину</summary>
		/// <param name="pluginId">Наименование плагина</param>
		/// <param name="msg">Сообщение для передачи плагину</param>
		/// <param name="args">Аргументы передаваемые плагину</param>
		/// <returns>Результат выполнения операции</returns>
		public Object SendMessage(String pluginId, String msg, params Object[] args)
		{
			Trace.TraceInformation("Sending message {0} to plugin {1}...", msg, pluginId);

			IPluginBase plugin = this[pluginId];
			return plugin == null
				? null
				: plugin.Instance.InvokeMessage(msg, args);
		}
		/// <summary>Вызов события при загрузке всех плагинов</summary>
		protected virtual void OnPluginsLoaded()
		{
			try
			{
				if(this.PluginsLoadedDelegate != null)
					this.PluginsLoadedDelegate(this, EventArgs.Empty);
			} catch(Exception exc)
			{
				this._host.Trace.Add(this, exc);
			}
		}
		/// <summary>Олицетворение текущего пользователя системы</summary>
		/// <param name="login">Логин пользователя</param>
		/// <param name="password">Пароль пользователя</param>
		/// <param name="tag">Дополнительная информация</param>
		/// <exception cref="ApplicationException">User already impersonated</exception>
		public void ImpersonateUser(String login, String password, Object tag)
		{
			if(this._impersonate == null)
				this._impersonate = new Impersonate(login, password, tag);
			else
				throw new ApplicationException("User already impersonated");
		}
		/// <summary>Поиск в сборке плагинов и создание экземпляров плагина</summary>
		/// <param name="assembly">Сбока в которой производить поиск</param>
		/// <returns>Интерфейсы найденных плагинов</returns>
		protected static IEnumerable<IPlugin> LoadAssemblyPlugins(Assembly assembly)
		{
			Trace.TraceInformation("Loading plugins from assembly {0}...", assembly);

			foreach(Type pluginType in assembly.GetTypes())
			{
				IPlugin result = PluginStorage.LoadAssemblyType(assembly, pluginType);
				if(result != null)
					yield return result;
			}
		}
		/// <summary>Загрузка сборки и получение указателя на интерфейс плагина</summary>
		/// <param name="assembly">Сборка для загрузки</param>
		/// <param name="pluginType">Тип плагина</param>
		/// <returns>Интерфейс найденного плагина или null</returns>
		protected static IPlugin LoadAssemblyType(Assembly assembly, Type pluginType)
		{
			if(pluginType.IsPublic && pluginType.IsClass)//!pluginType.IsAbstract
			{
				Trace.TraceInformation("Try to load type {0} from assembly {1}...", pluginType, assembly);

				Type typeInterface = pluginType.GetInterface(PluginConstant.PluginInterface, false);

				if(typeInterface != null)
				//if(pluginType.IsAssignableFrom(typeof(IPlugin)) && pluginType.IsClass)//Чтобы исключить все наследования интерфейсов
					return (IPlugin)Activator.CreateInstance(assembly.GetType(pluginType.ToString()));
			}
			return null;
		}
		/// <summary>Загрузить плагины из сборки</summary>
		/// <param name="assembly">Сборка в которой производится поиск на все экземпляры плагина</param>
		/// <param name="source">Источник загрузки плагина</param>
		/// <param name="mode">Режим загрузки плагина. (В рантайме/С запуском приложения)</param>
		public void LoadPlugin(Assembly assembly, String source, ConnectMode mode)
		{
			if(assembly == null)
				throw new ArgumentNullException("assembly");
			if(String.IsNullOrEmpty(source))
				throw new ArgumentNullException("source");

			Trace.TraceInformation("Loading assembly {0} from {1} with mode {2}", assembly, source, mode);

			//Byte[] raw = File.ReadAllBytes(filePath);
			//Assembly pluginAssembly = Assembly.Load(raw);

			foreach(IPlugin plugin in PluginStorage.LoadAssemblyPlugins(assembly))
			{
				plugin.Host = this._host;
				PluginBase pluginBase = new PluginBase(plugin, source);
				if(this[pluginBase.ID] == null)
				{//Добавление только уникальных плагинов
					this.Plugins.Add(pluginBase.ID, pluginBase);

					if(mode != ConnectMode.Startup)
						plugin.OnConnection(mode);//Инициализация плагина после загрузки интерфейса

					if(pluginBase.Instance is IPluginProvider)
					{//Найден провайдер плагинов. Необходимо загрузить все найденные им плагины
						this.SetPluginProvider(pluginBase);
						pluginBase.Instance.OnConnection(ConnectMode.Startup);
						((IPluginProvider)pluginBase.Instance).LoadPlugins();
					}
				}
			}
		}
		/// <summary>Инициализация всех плагинов. Происходит после указания папки с настройками сборки</summary>
		public void InitializePlugins()
		{
			//Инициализация Kernel плагинов
			foreach(IPluginBase plugin in this.FindPluginType<IPluginKernel>())
				try
				{
					plugin.Instance.OnConnection(ConnectMode.Startup);
				} catch(Exception exc)
				{
					this._host.Trace.Add(plugin, exc);
				}
			//TODO: Если необходимо передать инициализацию плагинов ядру, необходимо расширить (добавить IPluginKernelEx) интерфейс плагина IPluginKernel, в котором будут инициализироваться все плагины
			// if(this.Kernel is IPluginKernelEx) if(!((IPluginKernelEx)this.Kernel).InitializeEx()) Application.Exit(); else foreach(var plugin in this.Plugins)...

			//Инициализация всех плагинов за исключением Kernel плагинов
			foreach(IPluginBase plugin in this.Plugins.Values)
				if(!(plugin.Instance is IPluginKernel))
					try
					{
						plugin.Instance.OnConnection(ConnectMode.Startup);
					} catch(Exception exc)
					{
						this._host.Trace.Add(plugin, exc);
					}

			this.OnPluginsLoaded();
		}
		/// <summary>Удалить плагин из массива плагинов</summary>
		/// <param name="plugin">Интерфейс базового плагина для удаления</param>
		public void RemovePlugin(IPluginBase plugin)
		{
			if(plugin == null)
				throw new ArgumentNullException("plugin");

			Trace.TraceInformation("Removing plugin {0}...", plugin);

			if(!this.Plugins.Remove(plugin.ID))
				throw new ArgumentException(String.Format("Plugin {0} not found in collection", plugin.ID));
		}
		/// <summary>Удалить все плагины из коллекции</summary>
		public void RemovePlugins()
		{
			Trace.TraceInformation("Removinf all plugins...");

			this.Plugins.Clear();
		}
		/// <summary>Установить провайдер настроек</summary>
		/// <param name="plugin">Плагин который устанавливается в качестве провайдера настроек</param>
		public void SetSetingsProvider(IPluginBase plugin)
		{
			if(plugin == null)
				throw new ArgumentNullException("SettingsProvider");
			else
			{//Установка родительского загрузчика
				Trace.TraceInformation("Set Settings Provider {0}", plugin);

				if(this._settingsProvider != null)
					((ISettingsProvider)plugin.Instance).ParentProvider = (ISettingsProvider)this._settingsProvider.Instance;
				this._settingsProvider = plugin;
			}
		}
		/// <summary>Установить провайдер плагинов</summary>
		/// <param name="plugin">Плагин который устанавливается в качестве провайдера плагинов</param>
		public void SetPluginProvider(IPluginBase plugin)
		{
			if(plugin == null)
				throw new ArgumentNullException("PluginProvider");
			else
			{//Установка родительского загрузчика
				Trace.TraceInformation("Set Plugin Provider {0}", plugin);

				if(this._pluginProvider != null)
					((IPluginProvider)plugin.Instance).ParentProvider = (IPluginProvider)this._pluginProvider.Instance;
				this._pluginProvider = plugin;
			}
		}
		/// <summary>Получить енумератор по плагинам</summary>
		/// <returns>Интерфейс перечислимого типа</returns>
		public IEnumerator<IPluginBase> GetEnumerator()
		{
			foreach(IPluginBase plugin in this.Plugins.Values)
				yield return plugin;
		}
		/// <summary>Получить енумератор по плагинам</summary>
		/// <returns>Интерфейс перечислимого типа</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}