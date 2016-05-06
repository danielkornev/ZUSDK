using System;
namespace ZU.Configuration.Settings
{
	public interface IConfigurationSettings
	{
		AppMode AppMode { get; set; }
		bool AvoidMemoryCleansing { get; set; }
		DateTime DateChanged { get; set; }
		DateTime DateCreated { get; set; }
		bool EnableEmptyProjectsCreation { get; set; }
		bool IsRegistered { get; }
		long LargeFileSizeThreshold { get; set; }
		DateTime LastTimeUpdateChecked { get; set; }
		string LastUpdateCheckErrorMessage { get; set; }
		ZU.Core.Update.UpdateCheckStates LastUpdateCheckResult { get; set; }
		bool LoadTerminal { get; set; }
		string LoginProvider { get; set; }
		double MainWindowHeight { get; set; }
		double MainWindowLeft { get; set; }
		System.Windows.WindowState MainWindowState { get; set; }
		double MainWindowTop { get; set; }
		double MainWindowWidth { get; set; }
		int MaximumDownloadTimeInterval { get; set; }
		string MetaspaceFolder { get; set; }
		bool NotificationAppRunsInBackgroundShown { get; set; }
		bool OutputRedisLogToTerminal { get; set; }
		bool PopupDontShutdownAppShown { get; set; }
		bool PopupTrackYourProjectFilesShown { get; set; }
		bool PopupTrackYourProjectsShown { get; set; }
		bool PopupUseFiltersShown { get; set; }
		string ReadabilityParserToken { get; set; }
		bool SaveChangesHistory { get; set; }
		string SettingsFilePath { get; set; }
		string SetupUrl { get; set; }
		bool ShowCurrentVisualClusterNameInBreadcrumbBar { get; set; }
		bool ShowImageTitles { get; set; }
		bool ShowMinimap { get; set; }
		bool ShowPDFThumbnails { get; set; }
		bool ShowPeople { get; set; }
		bool ShowQueryPane { get; set; }
		bool ShowRulesAlerts { get; set; }
		bool ShowViewsPane { get; set; }
		bool SkipDiskThumbnailCache { get; set; }
		bool SkipLargeFilesForIndexing { get; set; }
		double SpiralVisualizationWidth { get; set; }
		string StorageProvider { get; set; }
		bool SupportSavingFiles { get; set; }
		ZU.Semantic.EntityRef UID { get; set; }
		string UserId { get; set; }
		string UserTitle { get; set; }
		bool UseSemanticProcessing { get; set; }
		string ZetInstallRoot { get; set; }
	} // class
} // namespace
