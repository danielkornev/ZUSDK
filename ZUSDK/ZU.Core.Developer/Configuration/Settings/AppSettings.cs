using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZU.Semantic;

namespace ZU.Configuration.Settings
{
	public partial class AppSettings : ZU.Configuration.Settings.IAppSettings
	{
		public string AppId { get; set; }
		public Dictionary<string, object> Settings { get; set; }
		public List<AppAccount> Accounts { get; set; }

		public string AppUserId { get; set; }
		public string CloudAppUserRecordId { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public bool AppUserIdSavedToCloud { get; set; }

		public DateTime TLChange { get; set; }
		public DateTime TLBirth { get; set; }
		public EntityRef UIDOwner { get; set; }
		public EntityRef UIDChanger { get; set; }
		public Version Version { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public string SettingsFilePath
		{ get; set; }

		public AppSettings(string appId, string appUserId)
		{
			// TODO: Complete member initialization
			this.AppId = appId;
			this.AppUserId = appUserId;
			this.Settings = new Dictionary<string, object>();
		}
	} // class
} // namespace
