using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZU.Configuration.Settings;
using ZU.Plugins;

namespace ZU.Core.Apps
{
	public partial class AppPaneBase : ContentControl, IDisposable
	{
		IZetApp app;

		public AppPaneBase(IAppAccount appAccount)
		{
			var parentApp = appAccount.App;
			this.Account = appAccount;
			this.app = parentApp;

			InitializeContent();
		}

		public IZetApp App
		{
			get
			{
				return app;
			}
		}

		private void InitializeContent()
		{
			// Width is 299 pixels
			this.Width = 299;
			// Auto Height
			this.Height = Double.NaN;
			this.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
			this.VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch;

			this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

			// by default, our pane should be hidden
			
		}

		public virtual void ShowPane()
		{

		}

		public virtual void HidePane()
		{

		}

		public virtual bool IsPaneVisible
		{
			get;
			internal set;
		}

		public virtual void Initialize()
		{
		}

		public IAppAccount Account { get; set; }

		public virtual void Dispose()
		{
			
		}
	} // class
} // namespace
