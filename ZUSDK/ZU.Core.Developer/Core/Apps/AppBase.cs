using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Core.Apps
{
	public delegate void OnInformationStructureChangedEventHandler(object sender, EventArgs e);

	#region SignIn-Related Delegates
	public delegate void SignInStatusChangedEventHandler(object sender, SignInStatusChangedEventArgs args);
	#endregion

	//public abstract class AppBase : BasePropertyChanged
	//{
		


	//} // class

	public class SignInStatusChangedEventArgs : EventArgs
	{
		public bool IsSignedIn { get; set; }
	}
} // namespace
