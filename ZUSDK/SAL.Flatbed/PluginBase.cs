using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SAL.Flatbed
{
	/// <summary>
	/// ������� ����� ��� �������� ���������� �������.
	/// �������� ��������� ������������ �������, ���-�� ��� ���� � ������ �������
	/// </summary>
	public class PluginBase : IPluginBase
	{
		#region Fields
		private readonly String _source;
		private readonly IPlugin _instance;
		private static readonly Type PluginMethodAttributeType = typeof(PluginMethodAttribute);
		#endregion Fields
		#region Properties
		/// <summary>������������� �������</summary>
		/// <exception cref="ArgumentNullException">GuidAttribute �� ������ �� ������ ������</exception>
		public String ID
		{
			get
			{
				GuidAttribute guid = this.GetAssemblyAttribute<GuidAttribute>();
				if(guid == null)
					throw new ArgumentNullException(String.Format("GuidAttribute not specified in assembly {0}", this.Instance.GetType().Assembly.FullName));
				else
					return guid.Value;
			}
		}
		/// <summary>���� � ������ �������</summary>
		public String Source { get { return this._source; } }
		/// <summary>��������� ������������ �������</summary>
		public IPlugin Instance { get { return this._instance; } }
		/// <summary>������ ���������� ������������ �������</summary>
		private Assembly Assembly { get { return this.Instance.GetType().Assembly; } }
		/// <summary>������������ �������</summary>
		public String Name
		{
			get
			{
				PluginEntryPointAttribute pluginAttribute = this.GetPluginAttribute<PluginEntryPointAttribute>();
				if(pluginAttribute != null)
					return pluginAttribute.Name;
				else
					return this.Assembly.GetName().Name;
			}
		}
		/// <summary>������ �������</summary>
		public Version Version
		{
			get
			{
				PluginEntryPointAttribute pluginAttribute = this.GetPluginAttribute<PluginEntryPointAttribute>();
				if(pluginAttribute != null)
					return pluginAttribute.Version;
				else
					return this.Assembly.GetName().Version;
			}
		}
		/// <summary>�������� ������</summary>
		public String Description
		{
			get
			{
				PluginEntryPointAttribute pluginAttribute = this.GetPluginAttribute<PluginEntryPointAttribute>();
				if(pluginAttribute != null)
					return pluginAttribute.Description;
				else
				{
					AssemblyDescriptionAttribute attribute = this.GetAssemblyAttribute<AssemblyDescriptionAttribute>();
					return attribute == null ? String.Empty : attribute.Description;
				}
			}
		}
		/// <summary>��������� ������</summary>
		public String Company
		{
			get
			{
				AssemblyCompanyAttribute attibute = this.GetAssemblyAttribute<AssemblyCompanyAttribute>();
				return attibute == null ? String.Empty : attibute.Company;
			}
		}
		/// <summary>�������� ������</summary>
		public String Copyright
		{
			get
			{
				AssemblyCopyrightAttribute attribute = this.GetAssemblyAttribute<AssemblyCopyrightAttribute>();
				return attribute == null ? String.Empty : attribute.Copyright;
			}
		}
		/// <summary>������ ���������� �����, ��� ������� ���������� ������</summary>
		public Int32 InterfaceVersion
		{
			get
			{
				PluginVersionAttribute attribute = this.GetAssemblyAttribute<PluginVersionAttribute>();
				if(attribute == null)
					return PluginConstant.InterfaceVersion;
				else
					return attribute.Version;
			}
		}
		#endregion Properties
		#region Constructors
		/// <summary>�������� ���������� �������� ������ �������</summary>
		/// <param name="instance">��������� ������� � ������� �������</param>
		/// <param name="source">���� � �������</param>
		public PluginBase(IPlugin instance, String source)
		{
			this._instance = instance;
			this._source = source;
		}
		#endregion Constructors
		#region Methods
		/// <summary>�������� ��������� ������ �������</summary>
		/// <returns>���������� �� ��������� ������� �������</returns>
		public IEnumerable<PluginMethodInfo> GetPublicMembers()
		{
			foreach(MemberInfo member in this.Instance.GetType().GetMembers())
				if(member.IsDefined(PluginBase.PluginMethodAttributeType, false))
					yield return new PluginMethodInfo(this, member);
		}
		/// <summary>��������� ������� � ���������� ������� �������</summary>
		/// <param name="eventName">������������ �������</param>
		/// <param name="handler">���������� ������� � ���� ����������� �������</param>
		/// <exception cref="ArgumentNullException">Event name is null</exception>
		/// <exception cref="ArgumentNullException">Handler is null</exception>
		public void AddEventHandler(String eventName, EventHandler<DataEventArgs> handler)
		{
			if(String.IsNullOrEmpty(eventName))
				throw new ArgumentNullException("eventName");
			if(handler == null)
				throw new ArgumentNullException("handler");

			EventInfo evtInfo = this.Instance.GetType().GetEvent(eventName);
			if(evtInfo == null)
				throw new MissingMethodException(String.Format("Event {0} not found", eventName));

			evtInfo.AddEventHandler(this.Instance, handler);
		}
		/// <summary>������� ���������� ������� �� ���������� ������� �������</summary>
		/// <param name="eventName">������������ �������</param>
		/// <param name="handler">���������� ������� ������� ���������� �������</param>
		/// <exception cref="ArgumentNullException">eventName is null</exception>
		/// <exception cref="ArgumentNullException">handler is null</exception>
		public void RemoveEventHandler(String eventName, EventHandler<DataEventArgs> handler)
		{
			if(String.IsNullOrEmpty(eventName))
				throw new ArgumentNullException("eventName");
			if(handler == null)
				throw new ArgumentNullException("handler");

			EventInfo evtInfo = this.Instance.GetType().GetEvent(eventName);
			if(evtInfo == null)
				throw new MissingMethodException(String.Format("Event {0} not found", eventName));

			evtInfo.RemoveEventHandler(this.Instance, handler);
		}
		/// <summary>�������� ������� ������ �������</summary>
		/// <typeparam name="A">��� �������� ������� ���������� ��������</typeparam>
		/// <returns>������ ��������� �������. ���� �� �������, �� null</returns>
		protected A GetPluginAttribute<A>() where A : Attribute
		{
			Object[] attributes = this.Instance.GetType().GetCustomAttributes(typeof(A), false);
			return attributes.Length == 0 ? null : (A)attributes[0];
		}
		/// <summary>�������� ����������� ������� �� ������</summary>
		/// <typeparam name="A">��� �������� ������� ���������� ��������</typeparam>
		/// <returns>������ ��������� �������. ���� �� �������, �� null</returns>
		protected A GetAssemblyAttribute<A>() where A : Attribute
		{
			Object[] attributes = this.Assembly.GetCustomAttributes(typeof(A), false);
			return attributes.Length == 0 ? null : (A)attributes[0];
		}
		#endregion Methods
	}
}