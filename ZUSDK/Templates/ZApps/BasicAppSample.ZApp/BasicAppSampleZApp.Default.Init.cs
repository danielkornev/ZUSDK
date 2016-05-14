using SAL.Flatbed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZU.Configuration.Settings;
using ZU.Plugins;
using ZU.Core;
using ZU.Core.Apps;

namespace ZU.Samples.Apps.BasicAppSample
{
	public partial class BasicAppSampleZApp
	{
		#region Members
		private IAppSettings _settings;
		private ISystemInformationModel _SIM;
		private IAppManager _appManager;
		private IAppButtonInfo _appButtonInfo;

		#endregion

		#region Properties
		public IHost Host
		{
			get; set;
		}
		public IZetHost ZetHost { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public IAppButtonInfo AppButtonInfo
		{
			get
			{
				return this._appButtonInfo;
			}
		}


		#endregion

		#region Methods
		public object Invoke(string message, params object[] args)
		{
			return null;
		}

		/// <summary>
		/// Called by Plugin Platform on startup. This is where main references are obtained. 
		/// </summary>
		/// <param name="mode"></param>
		/// <returns></returns>
		public bool OnConnection(ConnectMode mode)
		{
			if (this.Host == null) throw new NotImplementedException("Host is not available");

			this.ZetHost = this.Host as IZetHost;

			if (this.ZetHost == null) throw new PlatformNotSupportedException("This build of Zet Universe platform is not supported. Host doesn't implement ZU.Plugins.IZetHost interface");

			// saving reference to the current app's settings
			this._settings = this.ZetHost.AppManager.GetAppSettings(this).Result;
			// saving reference to the SIM (System Information Model)
			this._SIM = this.ZetHost.SIM;
			// saving reference to the IAppManager
			this._appManager = this.ZetHost.AppManager;

			// time to call your own Initialize method to complete plugin initialization
			this.Initialize();

			return true;
		}

		/// <summary>
		/// Not required for now. Reserved for future use.
		/// </summary>
		/// <param name="mode"></param>
		/// <returns></returns>
		public bool OnDisconnection(DisconnectMode mode)
		{
			return true;
		}

		/// <summary>
		/// Returns current plugin's IAppSettings (plugin controls its settings)
		/// </summary>
		/// <returns></returns>
		public IAppSettings GetCurrentAppSettings()
		{
			return this._settings;
		}

		#endregion


		#region Custom Implementation
		
		#endregion


		







		


		public event AppAccountAddedEventHandler Added;

		public AppPaneBase GetAppPane(IAppAccount account)
		{
			throw new NotImplementedException();
		}




		public void ProcessGenericNotification()
		{
			throw new NotImplementedException();
		}





		public void Shutdown()
		{
			throw new NotImplementedException();
		}

	} // class
} // namespace
