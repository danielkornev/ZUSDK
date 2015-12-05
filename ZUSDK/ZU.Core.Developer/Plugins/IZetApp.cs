using SAL.Flatbed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZU.Configuration.Settings;
using ZU.Core;
using ZU.Core.Apps;

namespace ZU.Plugins
{
	public interface IZetApp : IPlugin
	{
		#region Properties
		Guid Id { get; }
		string Title { get; }
		/// <summary>
		/// Accounts
		/// </summary>
		List<IAppAccount> Accounts { get; set; }
		/// <summary>
		/// App module publisher
		/// </summary>
		string Publisher { get; }
		string Status { get; }
		bool IsSignedIn { get; }
		bool TryRemove();
		bool IsAdded { get; }
		bool IsDataImportSupported { get; }
		bool AreMultipleAccountsAllowed { get; }
		Version Version { get; }
		#endregion

		#region
		//void Initialize(IAppSettings settings, ISystemInformationModel SIM, IAppManager appManager);

		/// <summary>
		/// Keeping this for now.
		/// </summary>
		/// <returns></returns>
		bool ShowAddDialog();
		/// <summary>
		/// Flyout is a control that is shown upon click on the app's icon in the Window Bar Left.
		/// </summary>
		/// <returns></returns>
		AppPaneBase GetAppPane(IAppAccount account);
		/// <summary>
		/// Provides information about this application's button, including its name, tooltip, and icon.
		/// </summary>
		IAppButtonInfo AppButtonInfo { get; }

		/// <summary>
		/// Keeping this for now
		/// </summary>
		void ShowSettingsDialog();
		
		//void SaveSettings();


		/// <summary>
		/// Unloads this plugin
		/// </summary>
		void Shutdown();
		#endregion

		#region Events
		event AppAccountAddedEventHandler Added;
		#endregion

		/// <summary>
		/// Generic notification means different thing for different apps; usually 
		/// </summary>
		void ProcessGenericNotification();
	} // interface
} // namespace
