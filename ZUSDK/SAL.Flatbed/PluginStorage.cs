using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;

namespace SAL.Flatbed
{
	/// <summary>������� ����� ��������� ��������</summary>
	public class PluginStorage : IPluginStorage, IEnumerable<IPluginBase>
	{
		#region Fields
		private readonly IHost _host;
		private readonly Dictionary<String, IPluginBase> _plugins;
		private IPluginBase _pluginProvider;
		private IPluginBase _settingsProvider;
		private Impersonate _impersonate;
		#endregion Fields

		/// <summary>������������� ������������ �����</summary>
		public IImpersonate User { get { return this._impersonate; } }
		
		/// <summary>������ ����������� ��������</summary>
		private IDictionary<String, IPluginBase> Plugins { get { return this._plugins; } }
		/// <summary>�������� ������ �� �������������� �������</summary>
		/// <param name="pluginId">������������� �������</param>
		/// <returns>��������� ������� �� �������������� ������� ��� null, ���� pluginId null ��� ������</returns>
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
		/// <summary>�������� ���-�� ��������</summary>
		public Int64 Count { get { return this.Plugins.Count; } }
		/// <summary>��������� ���������� ��������</summary>
		public IPluginProvider PluginProvider { get { return this._pluginProvider == null ? null : (IPluginProvider)this._pluginProvider.Instance; } }
		/// <summary>��������� ���������� ����������</summary>
		public ISettingsProvider SettingsProvider
		{
			get { return this._settingsProvider == null ? null : (ISettingsProvider)this._settingsProvider.Instance; }
			/*private set
			{
				if(value == null) throw new ArgumentNullException("SettingsProvider");
				else
				{//��������� ������������� ����������
					value.Instance.ParentProvider = this._settingsProvider.Instance;
					this._settingsProvider = value;
				}
			}*/
		}
		private EventHandler PluginsLoadedDelegate;
		/// <summary>������� �������� ���� �������� ������</summary>
		public event EventHandler PluginsLoaded
		{
			add { this.PluginsLoadedDelegate += value; }
			remove { this.PluginsLoadedDelegate -= value; }
		}
		/// <summary>�������� �������� ������ ��������� ��������</summary>
		/// <param name="host">���� �������� ����������</param>
		/// <exception cref="ArgumentNullException">host is null</exception>
		public PluginStorage(IHost host)
		{
			if(host == null)
				throw new ArgumentNullException("Host is null");

			this._host = host;
			this._plugins = new Dictionary<String, IPluginBase>();
		}

		/// <summary>����� ��� ������� ������������ ����</summary>
		/// <typeparam name="T">��� ������� ��� ������</typeparam>
		/// <returns>������ ���������� ��������</returns>
		public IEnumerable<IPluginBase> FindPluginType<T>() where T : IPlugin
		{
			Trace.TraceInformation("Searching for pluginType {0}...", typeof(T));

			foreach(IPluginBase plugin in this.Plugins.Values)
				if(plugin.Instance is T)
					yield return plugin;
		}
		/// <summary>�������� ��������� �������</summary>
		/// <param name="pluginId">������������ �������</param>
		/// <param name="msg">��������� ��� �������� �������</param>
		/// <param name="args">��������� ������������ �������</param>
		/// <returns>��������� ���������� ��������</returns>
		public Object SendMessage(String pluginId, String msg, params Object[] args)
		{
			Trace.TraceInformation("Sending message {0} to plugin {1}...", msg, pluginId);

			IPluginBase plugin = this[pluginId];
			return plugin == null
				? null
				: plugin.Instance.InvokeMessage(msg, args);
		}
		/// <summary>����� ������� ��� �������� ���� ��������</summary>
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
		/// <summary>������������� �������� ������������ �������</summary>
		/// <param name="login">����� ������������</param>
		/// <param name="password">������ ������������</param>
		/// <param name="tag">�������������� ����������</param>
		/// <exception cref="ApplicationException">User already impersonated</exception>
		public void ImpersonateUser(String login, String password, Object tag)
		{
			if(this._impersonate == null)
				this._impersonate = new Impersonate(login, password, tag);
			else
				throw new ApplicationException("User already impersonated");
		}
		/// <summary>����� � ������ �������� � �������� ����������� �������</summary>
		/// <param name="assembly">����� � ������� ����������� �����</param>
		/// <returns>���������� ��������� ��������</returns>
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
		/// <summary>�������� ������ � ��������� ��������� �� ��������� �������</summary>
		/// <param name="assembly">������ ��� ��������</param>
		/// <param name="pluginType">��� �������</param>
		/// <returns>��������� ���������� ������� ��� null</returns>
		protected static IPlugin LoadAssemblyType(Assembly assembly, Type pluginType)
		{
			if(pluginType.IsPublic && pluginType.IsClass)//!pluginType.IsAbstract
			{
				Trace.TraceInformation("Try to load type {0} from assembly {1}...", pluginType, assembly);

				Type typeInterface = pluginType.GetInterface(PluginConstant.PluginInterface, false);

				if(typeInterface != null)
				//if(pluginType.IsAssignableFrom(typeof(IPlugin)) && pluginType.IsClass)//����� ��������� ��� ������������ �����������
					return (IPlugin)Activator.CreateInstance(assembly.GetType(pluginType.ToString()));
			}
			return null;
		}
		/// <summary>��������� ������� �� ������</summary>
		/// <param name="assembly">������ � ������� ������������ ����� �� ��� ���������� �������</param>
		/// <param name="source">�������� �������� �������</param>
		/// <param name="mode">����� �������� �������. (� ��������/� �������� ����������)</param>
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
				{//���������� ������ ���������� ��������
					this.Plugins.Add(pluginBase.ID, pluginBase);

					if(mode != ConnectMode.Startup)
						plugin.OnConnection(mode);//������������� ������� ����� �������� ����������

					if(pluginBase.Instance is IPluginProvider)
					{//������ ��������� ��������. ���������� ��������� ��� ��������� �� �������
						this.SetPluginProvider(pluginBase);
						pluginBase.Instance.OnConnection(ConnectMode.Startup);
						((IPluginProvider)pluginBase.Instance).LoadPlugins();
					}
				}
			}
		}
		/// <summary>������������� ���� ��������. ���������� ����� �������� ����� � ����������� ������</summary>
		public void InitializePlugins()
		{
			//������������� Kernel ��������
			foreach(IPluginBase plugin in this.FindPluginType<IPluginKernel>())
				try
				{
					plugin.Instance.OnConnection(ConnectMode.Startup);
				} catch(Exception exc)
				{
					this._host.Trace.Add(plugin, exc);
				}
			//TODO: ���� ���������� �������� ������������� �������� ����, ���������� ��������� (�������� IPluginKernelEx) ��������� ������� IPluginKernel, � ������� ����� ������������������ ��� �������
			// if(this.Kernel is IPluginKernelEx) if(!((IPluginKernelEx)this.Kernel).InitializeEx()) Application.Exit(); else foreach(var plugin in this.Plugins)...

			//������������� ���� �������� �� ����������� Kernel ��������
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
		/// <summary>������� ������ �� ������� ��������</summary>
		/// <param name="plugin">��������� �������� ������� ��� ��������</param>
		public void RemovePlugin(IPluginBase plugin)
		{
			if(plugin == null)
				throw new ArgumentNullException("plugin");

			Trace.TraceInformation("Removing plugin {0}...", plugin);

			if(!this.Plugins.Remove(plugin.ID))
				throw new ArgumentException(String.Format("Plugin {0} not found in collection", plugin.ID));
		}
		/// <summary>������� ��� ������� �� ���������</summary>
		public void RemovePlugins()
		{
			Trace.TraceInformation("Removinf all plugins...");

			this.Plugins.Clear();
		}
		/// <summary>���������� ��������� ��������</summary>
		/// <param name="plugin">������ ������� ��������������� � �������� ���������� ��������</param>
		public void SetSetingsProvider(IPluginBase plugin)
		{
			if(plugin == null)
				throw new ArgumentNullException("SettingsProvider");
			else
			{//��������� ������������� ����������
				Trace.TraceInformation("Set Settings Provider {0}", plugin);

				if(this._settingsProvider != null)
					((ISettingsProvider)plugin.Instance).ParentProvider = (ISettingsProvider)this._settingsProvider.Instance;
				this._settingsProvider = plugin;
			}
		}
		/// <summary>���������� ��������� ��������</summary>
		/// <param name="plugin">������ ������� ��������������� � �������� ���������� ��������</param>
		public void SetPluginProvider(IPluginBase plugin)
		{
			if(plugin == null)
				throw new ArgumentNullException("PluginProvider");
			else
			{//��������� ������������� ����������
				Trace.TraceInformation("Set Plugin Provider {0}", plugin);

				if(this._pluginProvider != null)
					((IPluginProvider)plugin.Instance).ParentProvider = (IPluginProvider)this._pluginProvider.Instance;
				this._pluginProvider = plugin;
			}
		}
		/// <summary>�������� ���������� �� ��������</summary>
		/// <returns>��������� ������������� ����</returns>
		public IEnumerator<IPluginBase> GetEnumerator()
		{
			foreach(IPluginBase plugin in this.Plugins.Values)
				yield return plugin;
		}
		/// <summary>�������� ���������� �� ��������</summary>
		/// <returns>��������� ������������� ����</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}