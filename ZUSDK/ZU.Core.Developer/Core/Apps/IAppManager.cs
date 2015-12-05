using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZU.Configuration.Settings;
using ZU.Plugins;
using ZU.Semantic;

namespace ZU.Core.Apps
{
	public interface IAppManager
	{
		bool Subscribe(OnEntityOpen onEntityOpenDelegate, AppEntityKindDefinition appKind);
	
		IAppAccount CreateAppAccount(
			string accountId, 
			string accountTitle,
			IZetApp app,
			string email,
			string secret,
			string token,
			string userId,
			string userTitle,
			EntityRef uidOwner,
			EntityRef uidChanger,
			DateTime TLBirth,
			DateTime TLDeath);

		IAppSettings GetAppSettings(IZetApp app);

		IAppButtonInfo CreateAppButtonInfo(string IconGeometryPath, string Tooltip);
	} // interface
} // namespace
