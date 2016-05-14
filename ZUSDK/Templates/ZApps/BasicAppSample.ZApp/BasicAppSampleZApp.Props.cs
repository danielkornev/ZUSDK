// Includes properties that describe this plugin, like Title, Publisher, Version, etc.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZU.Configuration.Settings;
using ZU.Core.Apps;


namespace ZU.Samples.Apps.BasicAppSample
{
	public partial class BasicAppSampleZApp
	{
		/// <summary>
		/// TO DO: Generate your own unique Plugin Id
		/// </summary>
		public Guid Id
		{
			get
			{
				// TO DO: Enter your ZApp's plugin Unique Id
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// TO DO: Enter your personal or company's name here
		/// </summary>
		public string Publisher
		{
			get
			{
				return "TO DO: Enter your personal or company's name here";
			}
		}

		/// <summary>
		/// TO DO: Enter your ZApp's plugin name here
		/// </summary>
		public string Title
		{
			get
			{
				return "TO DO: Enter your ZApp's name here";
			}
		}

		/// <summary>
		/// TO DO: Enter your ZApp's plugin version here
		/// </summary>
		public Version Version
		{
			get
			{
				// TO DO: Enter your plugin's version here
				return new Version(1, 0, 0, 0);
			}
		}




		public bool IsDataImportSupported
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		




	} // class
} // namespace
