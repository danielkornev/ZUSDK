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
	public interface IZetHost : IHost
	{
		ISystemInformationModel Sim { get; }

		IAppManager AppManager { get; }

		void AddAppAccount(IAppAccount account, Boolean isAdded);
	} // interface
} // namespace
