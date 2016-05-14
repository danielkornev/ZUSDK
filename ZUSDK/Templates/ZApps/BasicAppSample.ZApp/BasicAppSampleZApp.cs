using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAL.Flatbed;
using ZU.Configuration.Settings;
using ZU.Core.Apps;
using System.Windows;

namespace ZU.Samples.Apps.BasicAppSample
{
	public partial class BasicAppSampleZApp : ZU.Plugins.IZetApp
	{
		#region Properties
		/// <summary>
		/// TO DO: Decide if your plugin requires signing into the public/private cloud service(s) or not
		/// </summary>
		public bool IsSignedIn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// TO DO: Decide if your plugin should report a custom status (e.g., Extracting data, or Importing Data, or Idle, etc.), or not.
		/// </summary>
		public string Status
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// TO DO: If your ZApp allows more than one account (e.g., email), return true here. Otherwise, return false.
		/// </summary>
		public bool AreMultipleAccountsAllowed
		{
			get
			{
				// TO DO: Decide if you want to support multiple accounts (e.g., plugin for multiple e-mails accounts) or one is enough (e.g., personal Facebook profile)
				return false;
			}
		}
		#endregion

		#region Init
		/// <summary>
		/// TO DO: Complete your plugin's initialization logic here, including account management, sign in, etc.
		/// </summary>
		private void Initialize()
		{
			this._appButtonInfo = _appManager.CreateAppButtonInfo("TO DO: Add XAML Path Geometry (hint: you can use SyncFusion Icon Metro Studio for that", "TO DO: Add tooltip, e.g., Show/Hide Your ZApp's pane");


			throw new NotImplementedException();
		}
		#endregion

		#region Add Account(s)
		/// <summary>
		/// TO DO: Add Dialog is used to configure account(s) for the ZApp. 
		/// 
		/// If your plugin needs special account configuration (e.g., it's an email), and there is no default account, you should implement this dialog. otherwise, simply return false
		/// </summary>
		/// <returns></returns>
		public bool ShowAddDialog()
		{
			// 
			MessageBox.Show("Not Implemented Yet", this.Title + ": Add New Account");
			return false;
		}
		#endregion

		#region Configure Settings
		/// <summary>
		/// TO DO: Add your own Dialog for custom settings if needed.
		/// </summary>
		public void ShowSettingsDialog()
		{
			// TO DO: 
			MessageBox.Show("Not Yet Implemented", this.Title);
		}
		#endregion
	} // class
} // namespace
