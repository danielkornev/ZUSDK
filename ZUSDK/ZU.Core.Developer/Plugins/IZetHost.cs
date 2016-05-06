using SAL.Flatbed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZU.Configuration.Settings;
using ZU.Core;
using ZU.Core.Apps;
using ZU.WPF;

namespace ZU.Plugins
{
	public interface IZetHost : IHost
	{
		ISystemInformationModel SIM { get; }

		IFrame Frame { get; }

		IAppManager AppManager { get; }
	} // interface
} // namespace
