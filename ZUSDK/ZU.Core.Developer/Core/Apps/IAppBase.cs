using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ZU.Configuration.Settings;

namespace ZU.Core.Apps
{
	public delegate void AppAccountAddedEventHandler (IAppAccount account, AccountAddedEventArgs e);

	public class AccountAddedEventArgs : EventArgs
	{
		public bool IsAdded {get; set;}
		public IAppAccount Account { get; set; }
	}

	public interface IApp : IDisposable, INotifyPropertyChanged
	{
		#region Properties
		Guid Id { get; }
		string Title { get; }
		/// <summary>
		/// Accounts
		/// </summary>
		List <IAppAccount> Accounts { get; set; }
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

		#region Methods
		void Initialize(IAppSettings settings, ISystemInformationModel SIM, IAppManager appManager);
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
		void ShowSettingsDialog();
		void SaveSettings();
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
