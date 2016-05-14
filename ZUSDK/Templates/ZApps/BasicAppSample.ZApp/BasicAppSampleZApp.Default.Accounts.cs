using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZU.Configuration.Settings;

namespace ZU.Samples.Apps.BasicAppSample
{
	public partial class BasicAppSampleZApp
	{
		#region Default Implementation
		/// <summary>
		/// Uses Accounts property of the IAppSettings as the value here.
		/// </summary>
		public List<IAppAccount> Accounts
		{
			get
			{
				return this._settings.Accounts;
			}
			set
			{
				this._settings.Accounts = value;
			}
		}
		

		/// <summary>
		/// Uses Accounts property to identify if this ZApp has been properly congifured and added to the list of configured ZApps for the given user.
		/// </summary>
		public bool IsAdded
		{
			get
			{
				#region It's considered as added if there's at least one account configured
				if (this.Accounts == null)
					return false;

				return
					this.Accounts.Any();
				#endregion
			}
		}

		/// <summary>
		/// Not Required for now.
		/// </summary>
		/// <returns></returns>
		public bool TryRemove()
		{
			return false;
		}

		#endregion
	} // class
} // namespace
