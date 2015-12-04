using System;
namespace ZU.Configuration.Settings
{
	public interface IAppSettings
	{
		System.Collections.Generic.List<IAppAccount> Accounts { get; set; }
		string AppId { get; set; }
		string AppUserId { get; set; }
		bool AppUserIdSavedToCloud { get; set; }
		string CloudAppUserRecordId { get; set; }
		System.Collections.Generic.Dictionary<string, object> Settings { get; set; }
		string SettingsFilePath { get; set; }
		DateTime TLBirth { get; set; }
		DateTime TLChange { get; set; }
		ZU.Semantic.EntityRef UIDChanger { get; set; }
		ZU.Semantic.EntityRef UIDOwner { get; set; }
		Version Version { get; set; }
	} // interface
} // namespace
